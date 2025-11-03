using HotUpdateScripts.Project.Common;
using My.Msg;
using My.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using JEngine.Core;
using HotUpdateScripts.Project.BasePrj.Data;
using System.Collections;
using HotUpdateScripts.Project.ACommon;

namespace HotUpdateScripts.Project.BasePrj.Ctrl
{

    public class CtrlBPlanet : MonoBehaviour
    {
        //背景
        GameObject obj_Day;
        GameObject obj_Night;

        //建筑
        Transform trans_Love;
        Transform trans_Donate;
        Transform trans_Honor;
        Transform trans_Game;
        Transform trans_Shop;
        Transform trans_Lucky;

        Transform trans_IsLand;
        //主页信息
        DataBMainHome mainHomeData;

        //场景效果
        Material mat_Zhao;
        Material mat_Yuan;
        float f_zhaoPar;//罩特效 参数
        float f_yuanPar;//罩特效 参数
        Coroutine cort_ZhaoFlashTimer;

        Transform trans_PlanetGlow;
        //Transform trans_Xingqiu1;
        SpriteRenderer spRenderer_Xingqiu2;
        Transform trans_HuoJian;
        Transform XingGroupA;
        Transform XingGroupB;
        SpriteRenderer spRenderer_Xing1;
        SpriteRenderer spRenderer_Xing2;

        public static CtrlBPlanet instance { get; private set; }
        bool haveInit = false;

        #region 生命周期
        void Awake()
        {
            if (haveInit) return;
            instance = this;
            obj_Day = GameObject.Find("Game/SceneBg/obj_Day");
            obj_Night = GameObject.Find("Game/SceneBg/obj_Night");

            trans_Love = transform.Find("aixingongfang").transform;
            trans_Donate = transform.Find("xiwangdengta").transform;
            trans_Honor = transform.Find("rongyuzhanguan").transform;
            trans_Game = transform.Find("youxidating").transform;
            //trans_Friend = transPlanet.Find("jiaoyoudating").transform;
            trans_Shop = transform.Find("duihuanshangcheng").transform;
            trans_Lucky = transform.Find("xingyunxiaowu").transform;
            trans_IsLand = GameObject.Find("Game/SceneBg/TaohuaIsLand").transform;

            mat_Zhao = JResource.LoadRes<Material>("Diqiu/Zhao/Materials/" + "006" + ".mat", JResource.MatchMode.Other);
            mat_Yuan = JResource.LoadRes<Material>("Diqiu/Zhao/Materials/" + "007" + ".mat", JResource.MatchMode.Other);

            trans_PlanetGlow = GameObject.Find("PlanetGlow").transform;
            //trans_Xingqiu1 = GameObject.Find("Xingqiu1").transform;
            spRenderer_Xingqiu2 = GameObject.Find("Xingqiu2").GetComponent<SpriteRenderer>();
            trans_HuoJian = GameObject.Find("HuoJian").transform;
            XingGroupA = GameObject.Find("XingGroupA").GetComponent<Transform>();
            XingGroupB = GameObject.Find("XingGroupB").GetComponent<Transform>();
            spRenderer_Xing1 = GameObject.Find("Xing1").GetComponent<SpriteRenderer>();
            spRenderer_Xing2 = GameObject.Find("Xing2").GetComponent<SpriteRenderer>();

            XingQiu1PingPong(trans_IsLand, trans_IsLand.localPosition.y, trans_IsLand.localPosition.y + 10f);
            XingQiu2PingPong(spRenderer_Xingqiu2, new Color(1, 1, 1, 0.6f), new Color(1, 1, 1, 1));
            HuoJianPingPong(trans_HuoJian);
            Xing1PingPong(spRenderer_Xing1, new Color(1, 1, 1, 0.45f), new Color(1, 1, 1, 1), 20);
            Xing2PingPong(spRenderer_Xing2, new Color(1, 1, 1, 0.45f), new Color(1, 1, 1, 1), 5);

            MsgCenter.RegisterMsg(null, MsgCode.OneFingerRotateTransfer, OneFingerRotateTransfer);
            MsgCenter.RegisterMsg(null, MsgCode.DayNightUpt, DayNightUptCallBack);
            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_GetServerTime, S2C_Game_GetServerTimeCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Main_HomeGetInfo, S2C_Main_HomeGetInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);
            haveInit = true;
        }


        void Start()
        {
            //需要被点击的建筑 添加脚本
            for (int i = 0; i < base.transform.childCount; i++)
            {
                string name = base.transform.GetChild(i).name;
                if (name == "bingchuan" || name == "diqiu" || name == "shitou" || name == "shu")
                    continue;

                if (!base.transform.GetChild(i).gameObject.GetComponent<BuildingOperate>())
                    base.transform.GetChild(i).gameObject.AddComponent<BuildingOperate>();
            }
            if (!trans_IsLand.gameObject.GetComponent<BuildingOperate>())
            {
                var operate = trans_IsLand.gameObject.AddComponent<BuildingOperate>();
                operate.canClick = true;
            }

            //跳转场景 重设建筑
            if (!GameData.isFirstOpenPlanet)
            {
                mainHomeData = DataMgr.Instance.dataBMainHome;
                //SetBuildings();
                SetBuildingsEvent();
            }

            //开启罩定时闪烁
            cort_ZhaoFlashTimer = CoroutineMgr.Instance.Coroutine_Start(TiShiFlashTimer());
        }

        float curScale = 0;
        float lastX = 0;
        void Update()
        {
            //星星旋转
            if (XingGroupA != null)
                XingGroupB.Rotate(Vector3.back * Time.deltaTime * 1f);
            if (XingGroupB != null)
                XingGroupB.Rotate(Vector3.back * Time.deltaTime * 1f);

            //罩子特效 参数
            if (mat_Zhao != null)
                mat_Zhao.SetFloat("_Alpha", f_zhaoPar);

            //圆片特效 参数
            if (mat_Yuan != null)
                mat_Yuan.SetColor("_TintColor", new Color(f_yuanPar, f_yuanPar, f_yuanPar, f_yuanPar));

            //光晕缩放 位置
            if (trans_PlanetGlow.localScale != transform.localScale * 125)
            {
                trans_PlanetGlow.localScale = transform.localScale * 125f;

                float z = 75 * transform.localScale.x;

                trans_PlanetGlow.localPosition = new Vector3(0, 0, z);
            }
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                if (GameTools.Instance.PanelsNumOutpace())
                {

                }
                else
                {
                    OneFingerRotateUpdate();//单指立体旋转
                    TwoFingerZoomUpdate();//双指缩放
                    TwoFingerRotateUpdate();//双指平面转
                }
                //if (Input.GetAxis("Mouse ScrollWheel") != 0)
                //{
                //    lastX = curScale;
                //    curScale += Input.GetAxis("Mouse ScrollWheel");
                //    curScale = Mathf.Clamp(curScale, 0.7f, 2);
                //    if (Mathf.Abs(lastX - curScale) > 1E-6)
                //    {
                //        transform.localScale = transform.localScale / lastX * curScale;
                //    }
                //}

                return;
            }


            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            {
                if (GameTools.Instance.PanelsNumOutpace())
                {

                }
                else
                {
                    OneFingerRotateUpdate();
                    TwoFingerZoomUpdate();
                    TwoFingerRotateUpdate();
                }
            }

            lastTouchCount = Input.touchCount;//解决2指变1指时地球旋转问题
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.OneFingerRotateTransfer, OneFingerRotateTransfer);
            MsgCenter.UnRegisterMsg(null, MsgCode.DayNightUpt, DayNightUptCallBack);

            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Main_HomeGetInfo, S2C_Main_HomeGetInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);
        }
        #endregion


        void S2C_Update_UpdateGameDataCallBack(object o)
        {
            debug.Log_yellow("--------更新星球数据--------");
            DataBUpdataData updateData = DataMgr.Instance.dataBUpdataData;
            if (updateData == null) return;
            GameData.curLoveNum = updateData.curLoveNum;
            GameData.curDonateLoveNum = updateData.donateLoveNum;
            GameData.curEnergy = updateData.curEnergy;
            GameData.accumulativeGetLoveNum = updateData.accumulativeGetLoveNum;
            //if (float.Parse(updateData.curLoveMoney) != 0)
            GameData.curLoveMoney = updateData.curLoveMoney;
            GameData.BagData = new DataBag() { prizeStoreList = updateData.prizeStoreList };

            //SetBuildings();
        }

        void S2C_Main_HomeGetInfoCallBack(object o)
        {
            mainHomeData = DataMgr.Instance.dataBMainHome;
            SetHomeData();
            SetBuildingsEvent();
        }

        private void SetHomeData()
        {
            if (GameData.isFirstOpenPlanet && mainHomeData != null)
            {
                GameData.userId = mainHomeData.userId;
                GameData.curLoveNum = mainHomeData.curLoveNum;
                GameData.curDonateLoveNum = mainHomeData.donateLoveNum;
                GameData.accumulativeGetLoveNum = mainHomeData.accumulativeGetLoveNum;
                GameData.curLoveMoney = mainHomeData.curLoveMoney;
                GameData.isFirstOpenPlanet = false;
            }
        }

        void SetBuildingsEvent()
        {
            string zhaoPreName = "Eff_Zhaozi_Use";
            if (trans_IsLand != null && mainHomeData != null)
            {
                trans_IsLand.gameObject.SetActive(mainHomeData.AllowMakingFriends);//桃花岛
            }
            if (trans_Love == null)
            {
                trans_Love = transform.Find("aixingongfang").transform;
            }
            trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
            trans_Love.GetComponent<BuildingOperate>().canClick = true;

            trans_Donate.Find(zhaoPreName).gameObject.SetActive(false);
            trans_Donate.GetComponent<BuildingOperate>().canClick = true;
            trans_Honor.Find(zhaoPreName).gameObject.SetActive(false);
            trans_Honor.GetComponent<BuildingOperate>().canClick = true;
            trans_Game.Find(zhaoPreName).gameObject.SetActive(false);
            trans_Game.GetComponent<BuildingOperate>().canClick = true;
            //trans_Friend.Find(zhaoPreName).gameObject.SetActive(false);
            //trans_Friend.GetComponent<BuildingOperate>().canClick = true;
            trans_Shop.Find(zhaoPreName).gameObject.SetActive(false);
            trans_Shop.GetComponent<BuildingOperate>().canClick = true;
            trans_Lucky.Find(zhaoPreName).gameObject.SetActive(false);
            trans_Lucky.GetComponent<BuildingOperate>().canClick = true;

            debug.Log_Blue("建筑已添加点击事件");
        }

        //设置建筑的解锁状态   首页首次拉完刷新  数据更新刷新
        void SetBuildings()
        {
            string zhaoPreName = "Eff_Zhaozi_Use";

            if (trans_IsLand != null)
            {
                trans_IsLand.gameObject.SetActive(mainHomeData.AllowMakingFriends);//桃花岛
            }
            //爱心工坊：默认解锁 累计孵化<5颗爱心
            if (GameData.accumulativeGetLoveNum <= 5 && GameData.curDonateLoveNum < 10)
            {
                trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Love.GetComponent<BuildingOperate>().canClick = true;

                debug.Log_Blue("1建筑 解锁___________________爱心工坊");

            }

            //希望灯塔：爱心工坊 累计孵化>=5颗爱心
            if (GameData.accumulativeGetLoveNum >= 5 && GameData.curDonateLoveNum < 10)
            {
                trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Love.GetComponent<BuildingOperate>().canClick = true;
                trans_Donate.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Donate.GetComponent<BuildingOperate>().canClick = true;
                debug.Log_Blue("2建筑 解锁___________________希望灯塔");
            }

            //荣誉展厅：希望灯塔 累计捐献>=10颗爱心
            if (GameData.curDonateLoveNum >= 10 && GameData.curDonateLoveNum < 15)
            {
                trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Love.GetComponent<BuildingOperate>().canClick = true;
                trans_Donate.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Donate.GetComponent<BuildingOperate>().canClick = true;
                trans_Honor.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Honor.GetComponent<BuildingOperate>().canClick = true;
                debug.Log_Blue("3建筑 解锁___________________荣誉展厅");
            }

            //游戏大厅：希望灯塔 累计捐献>=15颗爱心
            if (GameData.curDonateLoveNum >= 15 && GameData.curDonateLoveNum < 20)
            {
                trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Love.GetComponent<BuildingOperate>().canClick = true;
                trans_Donate.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Donate.GetComponent<BuildingOperate>().canClick = true;
                trans_Honor.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Honor.GetComponent<BuildingOperate>().canClick = true;
                trans_Game.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Game.GetComponent<BuildingOperate>().canClick = true;

                debug.Log_Blue("4建筑 解锁___________________游戏大厅");
            }

            /*            //交友大厅：希望灯塔 累计捐献>=20颗爱心
                        if (GameData.curDonateLoveNum >= 20 && GameData.curDonateLoveNum < 25)
                        {
                            trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
                            trans_Love.GetComponent<BuildingOperate>().canClick = true;
                            trans_Donate.Find(zhaoPreName).gameObject.SetActive(false);
                            trans_Donate.GetComponent<BuildingOperate>().canClick = true;
                            trans_Honor.Find(zhaoPreName).gameObject.SetActive(false);
                            trans_Honor.GetComponent<BuildingOperate>().canClick = true;
                            trans_Game.Find(zhaoPreName).gameObject.SetActive(false);
                            trans_Game.GetComponent<BuildingOperate>().canClick = true;
                            //trans_Friend.Find(zhaoPreName).gameObject.SetActive(false);
                            //trans_Friend.GetComponent<BuildingOperate>().canClick = true;

                            debug.Log_Blue("5建筑 解锁___________________交友大厅");
                        }*/
            //交友大厅不再设解锁条件 只对内部单身默认开发


            //兑换商城：希望灯塔 累计捐献>=20颗爱心
            if (GameData.curDonateLoveNum >= 20 && GameData.curDonateLoveNum < 25)
            {
                trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Love.GetComponent<BuildingOperate>().canClick = true;
                trans_Donate.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Donate.GetComponent<BuildingOperate>().canClick = true;
                trans_Honor.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Honor.GetComponent<BuildingOperate>().canClick = true;
                trans_Game.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Game.GetComponent<BuildingOperate>().canClick = true;
                //trans_Friend.Find(zhaoPreName).gameObject.SetActive(false);
                //trans_Friend.GetComponent<BuildingOperate>().canClick = true;
                trans_Shop.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Shop.GetComponent<BuildingOperate>().canClick = true;

                debug.Log_Blue("6建筑 解锁___________________兑换商城");
            }

            //幸运小屋：希望灯塔 累计捐献>=25颗爱心
            if (GameData.curDonateLoveNum >= 25)
            {
                debug.Log_Blue(transform);
                if (trans_Love == null)
                {
                    trans_Love = transform.Find("aixingongfang").transform;

                }
                trans_Love.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Love.GetComponent<BuildingOperate>().canClick = true;

                trans_Donate.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Donate.GetComponent<BuildingOperate>().canClick = true;
                trans_Honor.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Honor.GetComponent<BuildingOperate>().canClick = true;
                trans_Game.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Game.GetComponent<BuildingOperate>().canClick = true;
                //trans_Friend.Find(zhaoPreName).gameObject.SetActive(false);
                //trans_Friend.GetComponent<BuildingOperate>().canClick = true;
                trans_Shop.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Shop.GetComponent<BuildingOperate>().canClick = true;
                trans_Lucky.Find(zhaoPreName).gameObject.SetActive(false);
                trans_Lucky.GetComponent<BuildingOperate>().canClick = true;

                debug.Log_Blue("7建筑 解锁___________________幸运小屋");
            }
        }

        #region 场景效果

        //根据服务器时间 设置背景(白天 晚上)

        void DayNightUptCallBack(object o)
        {
            if (o.ToString() == "day")
            {
                if (obj_Day == null || obj_Night == null)
                    return;

                if (!obj_Day.activeSelf)
                    obj_Day.SetActive(true);

                if (obj_Night.activeSelf)
                    obj_Night.SetActive(false);

            }
            else if (o.ToString() == "night")
            {
                if (obj_Day == null || obj_Night == null)
                    return;

                if (obj_Day.activeSelf)
                    obj_Day.SetActive(false);

                if (!obj_Night.activeSelf)
                    obj_Night.SetActive(true);

            }
        }

        //星球1动画
        private void XingQiu1PingPong(Transform trans, float from, float to)
        {
            trans.DOLocalMoveY(to, 2f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    XingQiu1PingPong(trans, to, from);
                }
                );
        }
        //星球2动画
        private void XingQiu2PingPong(SpriteRenderer sp, Color from, Color to)
        {
            sp.DOColor(to, 3f).OnComplete(
                () =>
                {
                    XingQiu2PingPong(sp, to, from);
                }
                );
        }
        //火箭动画
        private void HuoJianPingPong(Transform trans)
        {
            Vector3[] m_Path = new Vector3[4];

            m_Path[0] = new Vector3(-210, 0, 65);

            m_Path[1] = new Vector3(135, 300, 65);

            m_Path[2] = new Vector3(-300, 300, 65);

            m_Path[3] = new Vector3(-210, 0, 65);

            float time = UnityEngine.Random.Range(30, 50);

            //new Vector3(-55, 105, 65)
            trans.DOPath(m_Path, time, PathType.Linear, PathMode.Ignore).OnComplete(() =>
            {
                HuoJianPingPong(trans);
            });

        }

        //星星动画
        private void Xing1PingPong(SpriteRenderer sp, Color from, Color to, float toScale)
        {
            sp.DOColor(to, 3f).OnComplete(
                () =>
                {
                    if (toScale == 10)
                        toScale = 20;
                    else
                        toScale = 10;

                    Xing1PingPong(sp, to, from, toScale);
                }
                );
            sp.transform.DOScale(new Vector3(toScale, toScale, toScale), 3f);
        }
        private void Xing2PingPong(SpriteRenderer sp, Color from, Color to, float toScale)
        {
            sp.DOColor(to, 2f).OnComplete(
                () =>
                {
                    if (toScale == 3)
                        toScale = 5;
                    else
                        toScale = 3;

                    Xing2PingPong(sp, to, from, toScale);
                }
                );
        }



        /*
        
        关闭
        cort_ZhaoFlashTimer = CoroutineMgr.Instance.Coroutine_Start(ZhaoFlashTimer());

        开启
        CoroutineMgr.Instance.Coroutine_Stop(cort_ZhaoFlashTimer);
        cort_ZhaoFlashTimer = null;
         */

        IEnumerator TiShiFlashTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(5);
                //Debug.Log("5s 渐变一次   ");
                TiShiFlash();
            }
        }

        void TiShiFlash()
        {

            f_zhaoPar = 0;
            Tween tweenZhao = DOTween.To(() => f_zhaoPar, x => f_zhaoPar = x, 0.2f, 1).OnComplete(() =>
            {
                //debug.Log("亮啦");
                Tween tweenZhaoBack = DOTween.To(() => f_zhaoPar, x => f_zhaoPar = x, 0f, 1);
                tweenZhaoBack.OnComplete(() =>
                {
                    //debug.Log("暗啦");
                });
            });


            f_yuanPar = 0;
            Tween tweenYuan = DOTween.To(() => f_yuanPar, x => f_yuanPar = x, 0.5f, 1).OnComplete(() =>
            {
                //debug.Log("亮啦");
                Tween tweenYuanBack = DOTween.To(() => f_yuanPar, x => f_yuanPar = x, 0f, 1);
                tweenYuanBack.OnComplete(() =>
                {
                    //debug.Log("暗啦");
                });
            });




        }
        #endregion

        #region 单指滑动立旋 
        public float rotateSpeed = 0.2f;//旋转速度   
        private float dampingSpeed;//阻尼速度
        private bool onDrag = false;//是否被拖拽   

        private float axisX = 1;//鼠标沿水平方向移动的增量   
        private float axisY = 1;//鼠标沿竖直方向移动的增量   
        private float cXY;

        /*
         * 解决 两指按下 单指抬起时 地球旋转问题
         * 思路：
         * 1.先判断 触摸指数 是否变化
         * 2.如果变化 2->1 lastTouchCount==2&&input.Touchcount==1
         * 3.
         */
        int lastTouchCount;//记录上帧 是单指还是双指

        void OnMouseDown()
        {
            if (!gameObject.activeSelf) return;
            axisX = 0f;
            axisY = 0f;
        }

        void OnMouseUp()
        {
            if (!gameObject.activeSelf) return;

            onDrag = false;
        }

        void OnMouseDrag()
        {
            if (!gameObject.activeSelf) return;

            onDrag = true;
            //获得鼠标增量 
            axisX = -Input.GetAxis("Mouse X");
            axisY = Input.GetAxis("Mouse Y");

            //计算鼠标移动的长度
            cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY);

            if (cXY == 0f)
            {
                cXY = 1f;
            }
        }

        float dampingFactor()//计算阻尼速度    
        {
            if (onDrag)
            {
                dampingSpeed = rotateSpeed;
            }
            else
            {
                if (dampingSpeed > 0)
                {
                    //通过除以鼠标移动长度实现拖拽越长速度减缓越慢
                    dampingSpeed -= rotateSpeed * 2 * Time.deltaTime / cXY;
                }
                else
                {
                    dampingSpeed = 0;
                }
            }
            return dampingSpeed;
        }

        void OneFingerRotateUpdate()
        {
            //两指限制滑动旋转
            if (Input.touchCount > 1)
            {
                return;
            }

            /*if (axisY == -axisX)//相等时会无限快速旋转
                return;   */

            if (onDrag)
            {
                if (Input.touchCount != lastTouchCount)//解决2指变1指时地球旋转问题
                    return;

                transform.Rotate(new Vector3(axisY, axisX, 0) * dampingFactor() * 5, Space.World);//加快 拖拽中旋转速度
            }
            else if (!onDrag)
            {
                if (axisY == -axisX)//相等时会无限快速旋转
                    return;

                //这个是是按照之前方向一直慢速旋转
                transform.Rotate(new Vector3(axisY, axisX, 0) * dampingFactor(), Space.World);
            }
        }

        //单指滑动传递
        void OneFingerRotateTransfer(object o)
        {
            List<object> oList = o as List<object>;

            if (oList != null)
            {
                for (int i = 0; i < oList.Count; i++)
                {
                    onDrag = (bool)oList[0];
                    axisX = (float)oList[1];
                    axisY = (float)oList[2];
                    cXY = (float)oList[3];
                }
            }

        }
        #endregion

        #region 双指扭动平旋
        private MeshRenderer mesh;//球体网格

        private float rotateCo = 1f;    //旋转系数
        Touch oldTouch3;  //上次触摸点1(手指1)
        Touch oldTouch4;  //上次触摸点2(手指2)


        private void TwoFingerRotateUpdate()
        {


            if (Input.touchCount <= 1)
            {
                return;
            }

            if (mesh == null)
            {
                mesh = transform.Find("diqiu").GetComponent<MeshRenderer>();
            }

            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            //启用双指，尚未旋转
            if (touch2.phase == TouchPhase.Began)
            {
                oldTouch3 = touch1;
                oldTouch4 = touch2;
                return;
            }
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 curVec = touch2.position - touch1.position;
                Vector2 oldVec = oldTouch4.position - oldTouch1.position;
                float angle = Vector2.Angle(oldVec, curVec);
                angle *= Mathf.Sign(Vector3.Cross(oldVec, curVec).z);

                base.transform.RotateAround(mesh.bounds.center, new Vector3(0, 0, 1), angle * rotateCo);//左右平着旋转

                oldTouch3 = touch1;
                oldTouch4 = touch2;
            }
        }



        #endregion

        #region 双指张合缩放
        public float defaultScale = 1;//默认尺寸
        private Touch oldTouch1;  //上次触摸点1(手指1)  
        private Touch oldTouch2;  //上次触摸点2(手指2)  


        //双指触控缩放
        void TwoFingerZoomUpdate()
        {
            //没有触摸 且 scale < 1 让弹回1
            if (Input.touchCount <= 0)
            {
                Vector3 loSc = transform.localScale;
                float coef = 0.01f;
                if (loSc.x < 1 && loSc.y < 1 && loSc.z < 1)
                    transform.localScale = new Vector3(loSc.x + coef, loSc.y + coef, loSc.z + coef);
                return;
            }

            if (Input.touchCount <= 1)//<=0
            {
                return;
            }

            //多点触摸, 放大缩小  
            Touch newTouch1 = Input.GetTouch(0);
            Touch newTouch2 = Input.GetTouch(1);

            //第2点刚开始接触屏幕, 只记录，不做处理  
            if (newTouch2.phase == TouchPhase.Began)
            {
                oldTouch2 = newTouch2;
                oldTouch1 = newTouch1;
                return;
            }

            //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
            float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
            float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);

            //两个距离之差，为正表示放大手势， 为负表示缩小手势  
            float offset = newDistance - oldDistance;

            //放大因子， 一个像素按 0.01倍来算(100可调整)  
            float scaleFactor = offset / 1000f;
            Vector3 localScale = transform.localScale;
            Vector3 scale = new Vector3(localScale.x + scaleFactor,
                                        localScale.y + scaleFactor,
                                        localScale.z + scaleFactor);

            if (offset < 0)
            {
                //最小缩放到 0.7 倍  
                if (scale.x > defaultScale * 0.7f && scale.y > defaultScale * 0.7f && scale.z > defaultScale * 0.7f)
                {
                    transform.localScale = scale;
                }
            }

            if (offset > 0)
            {
                //最大缩放到 2 倍  
                if (scale.x < defaultScale * 2f && scale.y < defaultScale * 2f && scale.z < defaultScale * 2f)
                {
                    transform.localScale = scale;
                }
            }


            //记住最新的触摸点，下次使用  
            oldTouch1 = newTouch1;
            oldTouch2 = newTouch2;
        }

        #endregion
    }


    /* 解决 单指滑地球和单指点建筑 操作冲突问题
    思路:
    1.触碰到建筑，先判断是单击还是滑动
    2.如果是滑动，地球跟随
     */
    /// <summary>
    /// 操作建筑
    /// </summary>
    public class BuildingOperate : MonoBehaviour
    {
        public Vector2 startPos;
        public Vector2 endPos;
        public bool isTime;//是否检测中
        public bool canClick;//是否被点打开面板

        void OnMouseDown()
        {
            if (!gameObject.activeSelf) return;

            if (GameTools.Instance.PanelsNumOutpace())
                return;

            ////传递
            axisX = 0f; oList[1] = axisX;
            axisY = 0f; oList[2] = axisY;
            ////


            startPos = Input.mousePosition;

            if (!isTime)
                Invoke("Timer", 0.1f);//初始化定时器，100毫秒后执行预定方法
            isTime = true;

            //Debug.Log("按下");
        }

        void OnMouseUp()
        {
            if (!gameObject.activeSelf) return;

            ////传递
            onDrag = false; oList[0] = onDrag;
            MsgCenter.Call("", MsgCode.OneFingerRotateTransfer, oList);
            ////


            //Debug.Log("抬起");
        }

        void OnMouseDrag()
        {
            if (!gameObject.activeSelf) return;

            endPos = Input.mousePosition;
            //Debug.Log("拖拽");


            ////传递
            onDrag = true; oList[0] = onDrag;
            //获得鼠标增量 
            axisX = -Input.GetAxis("Mouse X"); oList[1] = axisX;
            axisY = Input.GetAxis("Mouse Y"); oList[2] = axisY;
            //计算鼠标移动的长度
            cXY = Mathf.Sqrt(axisX * axisX + axisY * axisY); oList[3] = cXY;
            if (cXY == 0f)
            {
                cXY = 1f; oList[3] = cXY;
            }
            //广播 就把数值传回
            MsgCenter.Call("", MsgCode.OneFingerRotateTransfer, oList);
            ////
            ///
        }

        private void Timer()
        {
            float dis = Vector2.Distance(startPos, endPos);
            //Debug.Log(dis);

            isTime = false;

            if (dis > 10)
            {
                Debug.Log("滑动操作");
            }
            else
            {
                Debug.Log("点击操作" + transform.name);

                //未解锁
                if (!canClick)
                {
                    //switch (transform.name)
                    //{
                    //    case "aixingongfang":
                    //        break;
                    //    case "xiwangdengta":
                    //        GameTools.SetTip("<b>希望灯塔</b> 未解锁 \n 解锁条件：累计孵化≥5颗爱心");
                    //        break;
                    //    case "rongyuzhanguan":
                    //        GameTools.SetTip("<b>荣誉展馆</b> 未解锁 \n 解锁条件：累计捐献≥10颗爱心");
                    //        break;
                    //    case "youxidating":
                    //        GameTools.SetTip("<b>游戏大厅</b> 未解锁 \n 解锁条件：累计捐献≥15颗爱心");
                    //        break;
                    //    /*                        case "jiaoyoudating":
                    //                                GameTools.SetTip("<b>交友大厅</b> 未解锁 \n 解锁条件：累计捐献>=20颗爱心");
                    //                                break;*/
                    //    case "duihuanshangcheng":
                    //        GameTools.SetTip("<b>兑换商城</b> 未解锁 \n 解锁条件：累计捐献≥20颗爱心");
                    //        break;
                    //    case "xingyunxiaowu":
                    //        GameTools.SetTip("<b>幸运小屋</b> 未解锁 \n 解锁条件：累计捐献≥25颗爱心");
                    //        break;
                    //}
                }
                //解锁
                else
                {
                    switch (transform.name)
                    {
                        case "aixingongfang":
                            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBLoveHomePanel);
                            break;
                        case "xiwangdengta":
                            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBDonateHomePanel);
                            break;
                        case "rongyuzhanguan":
                            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBHonorHomePanel);
                            break;
                        case "youxidating":
                            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBGameHomePanel);
                            break;
                        case "duihuanshangcheng":
                            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBShopPanel);
                            break;
                        case "xingyunxiaowu":
                            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBLuckyRotatePanel);
                            break;
                        case "TaohuaIsLand":
                            UIMgr.Instance.OpenRedPackageActivityPanel(UIPath.UIBFriendHomePanel);
                            break;
                    }
                }


            }

        }

        //传递
        private static bool onDrag = false;//是否被拖拽   
        private static float axisX = 1;//鼠标沿水平方向移动的增量   
        private static float axisY = 1;//鼠标沿竖直方向移动的增量   
        private static float cXY;
        List<object> oList = new List<object> { onDrag, axisX, axisY, cXY };

    }


    /* 👇请勿删除👇 */

    /// <summary>
    /// 双指滑动 左右旋转
    /// </summary>
    public class TwoFingerRotate : MonoBehaviour
    {
        private MeshRenderer mesh;

        private float rotateCo = 1f;    //旋转系数
        Touch oldTouch1;  //上次触摸点1(手指1)
        Touch oldTouch2;  //上次触摸点2(手指2)

        void Start()
        {
            mesh = GameObject.Find("Sphere001").gameObject.GetComponent<MeshRenderer>();
        }

        void Update()
        {
            Rotate();
        }

        private void Rotate()
        {

            if (Input.touchCount <= 1)
            {
                return;
            }

            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            //启用双指，尚未旋转
            if (touch2.phase == TouchPhase.Began)
            {
                oldTouch1 = touch1;
                oldTouch2 = touch2;
                return;
            }
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 curVec = touch2.position - touch1.position;
                Vector2 oldVec = oldTouch2.position - oldTouch1.position;
                float angle = Vector2.Angle(oldVec, curVec);
                angle *= Mathf.Sign(Vector3.Cross(oldVec, curVec).z);

                transform.RotateAround(mesh.bounds.center, new Vector3(0, 0, 1), angle * rotateCo);//左右平着旋转

                oldTouch1 = touch1;
                oldTouch2 = touch2;
            }
        }

    }


    /// <summary>
    /// 双指张合 缩放
    /// </summary>
    public class TwoFingerZoom : MonoBehaviour
    {
        RectTransform rectTransform;

        void Start()
        {
            rectTransform = transform as RectTransform;
        }


        void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
        Zoom_PC();
#else
            Zoom();
#endif
        }

        private float currentZoomScale = 1;
        private float zoomScaleMax = 2;
        private float zoomScaleMin = 0.8f;
        private float zoomScaleCo = 0.003f;   //缩放系数
        Touch oldTouch1;  //上次触摸点1(手指1)
        Touch oldTouch2;  //上次触摸点2(手指2)
        private void Zoom()
        {
            if (Input.touchCount <= 1)
            {
                return;
            }

            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            //启用双指，尚未旋转
            if (touch2.phase == TouchPhase.Began)
            {
                oldTouch2 = touch2;
                oldTouch1 = touch1;
                return;
            }
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float oldDistance = Vector2.Distance(oldTouch2.position, oldTouch1.position);
                float curDistance = Vector2.Distance(touch2.position, touch1.position);
                currentZoomScale += (curDistance - oldDistance) * zoomScaleCo;
                currentZoomScale = Mathf.Clamp(currentZoomScale, zoomScaleMin, zoomScaleMax);
                transform.DOScale(Vector3.one * currentZoomScale, 0.3f);

                float clampX = Mathf.Clamp(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
                float clampY = Mathf.Clamp(rectTransform.anchoredPosition.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
                Vector2 endValue = new Vector2(clampX, clampY);
                rectTransform.DOAnchorPos(endValue, 0.3f);

                oldTouch1 = touch1;
                oldTouch2 = touch2;
            }
        }

        private void Zoom_PC()
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                currentZoomScale -= 0.3f;
                currentZoomScale = Mathf.Clamp(currentZoomScale, zoomScaleMin, zoomScaleMax);
                transform.DOScale(Vector3.one * currentZoomScale, 0.3f);

                float clampX = Mathf.Clamp(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
                float clampY = Mathf.Clamp(rectTransform.anchoredPosition.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
                Vector2 endValue = new Vector2(clampX, clampY);
                rectTransform.DOAnchorPos(endValue, 0.3f);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                currentZoomScale += 0.3f;
                currentZoomScale = Mathf.Clamp(currentZoomScale, zoomScaleMin, zoomScaleMax);
                transform.DOScale(Vector3.one * currentZoomScale, 0.3f);

                float clampX = Mathf.Clamp(rectTransform.anchoredPosition.x, -rectTransform.sizeDelta.x * transform.localScale.x / 2, rectTransform.sizeDelta.x * transform.localScale.x / 2);
                float clampY = Mathf.Clamp(rectTransform.anchoredPosition.y, -rectTransform.sizeDelta.y * transform.localScale.y / 2, rectTransform.sizeDelta.y * transform.localScale.y / 2);
                Vector2 endValue = new Vector2(clampX, clampY);
                rectTransform.DOAnchorPos(endValue, 0.3f);
            }

        }

        public void Reset()
        {
            currentZoomScale = 1;
            transform.DOScale(Vector3.one * currentZoomScale, 0.3f);
        }
    }


}
