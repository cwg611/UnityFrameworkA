using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;
using JEngine.Core;

namespace My.UI.Panel
{
    public class UIBLuckyRotatePanel : BasePanel
    {
        private GameObject Btn_Start, Btn_Close, btn_Bg, Btn_instruct;
        private Transform LightGroup;
        private Animator[] dengs;
        private Sprite deng_Sp;
        private Image[] dengImgs;

        private Text Txt_Num;
        // 转盘
        private Transform ZhuanPanImg;

        //小猪
        private RectTransform Image_Pig;

        // 旋转动画时间      
        private float RotateTime = 3f;
        private float rotateTiming = 0f;

        /// <summary>
        /// 需要转动到的目标位置
        /// </summary>
        private Quaternion targetAngels = Quaternion.identity;

        // 转动的速度 
        private float rotateSpeed = 20;

        /// <summary>
        /// 是否开始抽奖转动
        /// </summary>
        private bool isStartRotate = false;
        // 本次中奖ID
        private int rewardIndex = 0;

        private Coroutine coroutine_PlayDengAni;
        private Coroutine coroutine_OpenPage;//产出爱心

        int resultId;

        private bool IsUnmarried => DataMgr.Instance.dataBMainHome.AllowMakingFriends;//用户是否未婚员工
        private GameObject ExplainView;


        void Awake()
        {
            IsHomePanel = true;
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            }
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f);

            Txt_Num = GameTools.GetByName(transform, "Txt_Num").GetComponent<Text>();
            ZhuanPanImg = GameTools.GetByName(transform, "ZhuanPanImg").transform;
            LightGroup = GameTools.GetByName(transform, "LightGroup").transform;
            Btn_Start = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_Start"), OnClickDrawFun);
            Image_Pig = GameTools.GetByName(transform, "Image_Pig").GetComponent<RectTransform>();

            ExplainView = transform.Find("ExplainPop").gameObject;
            GameTools.Instance.AddClickEvent(transform.Find("Content_bg/Bg/ExplainBtn"), () => { ExplainView.SetActive(true); });
            GameTools.Instance.AddClickEvent(transform.Find("ExplainPop/Bg/CloseBtn"), () => { ExplainView.SetActive(false); });
            Btn_Close = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_Close"), OnBtnCloseClick);
            btn_Bg = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Bg"), OnBtnCloseClick);

            Btn_instruct = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_instruct"), () =>
            {
                GameTools.SetTip("上午（0-12）、下午（12-18）、晚上（18-24）,每个时间段各有一次抽奖机会，记得准时回来噢！");
            });

            dengs = LightGroup.GetComponentsInChildren<Animator>();
            dengImgs = LightGroup.GetComponentsInChildren<Image>();
            deng_Sp = dengImgs[0].sprite;

            string imgPath = string.Format("BaseProject/Lucky/{0}" + ".png", IsUnmarried ? "xuanzhuan" : "xuanzhuan2");
            ZhuanPanImg.GetComponent<Image>().sprite =
                JResource.LoadRes<Sprite>(imgPath, JResource.MatchMode.UI);

            PigPingPong(Image_Pig, Image_Pig.localPosition.y, Image_Pig.localPosition.y + 70f);

            if (coroutine_PlayDengAni != null) StopCoroutine(coroutine_PlayDengAni);
            coroutine_PlayDengAni = StartCoroutine(PlayDengAni());

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Lucky_LuckyHouseDraw, S2C_Lucky_LuckyHouseDrawCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);

            //1可以 0不可
            if (GameData.canDraw == 0)
            {
                debug.Log_Blue("不能抽奖");
                Txt_Num.text = "0";
            }
            else
            {
                debug.Log_Blue("可以抽奖");
                Txt_Num.text = GameData.canDraw.ToString();
            }

            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_LUCKY];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();

            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }

        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{
        //    GameData.taskInfo = null;
        //}

        void OnBtnCloseClick()
        {
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
            }
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBLuckyRotatePanel);
            });
        }

        IEnumerator PlayDengAni()
        {
            for (int i = 0; i < dengs.Length; i++)
            {
                dengs[i].enabled = true;
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void PigPingPong(RectTransform trans, float from, float to)
        {
            trans.DOLocalMoveY(to, 2f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    PigPingPong(trans, to, from);
                }
                );
        }

        //减速
        //float endSpeed = 2f;
        void Update()
        {
            if (!isStartRotate) return;

            rotateTiming += Time.deltaTime;
            // 过了动画时间
            if (rotateTiming >= RotateTime)
            {
                //endSpeed -= Time.deltaTime;
                //if (endSpeed < 0.3f) endSpeed = 0.3f;
                //ZhuanPanImg.Rotate(Vector3.back * endSpeed);
                ZhuanPanImg.Rotate(Vector3.back * 1.5f);
                // 计算当前正在旋转的角度和目标角度的夹角
                if (Quaternion.Angle(ZhuanPanImg.rotation, targetAngels) <= 1f)
                {
                    // 设置目标夹角
                    ZhuanPanImg.rotation = targetAngels;
                    // 转动停止
                    isStartRotate = false;
                    // todo... 奖品展示，数据维护
                    //Debug.Log("***** 恭喜或的奖品，奖品索引：" + rewardIndex + " ，转盘角度是：" + ZhuanPanImg.localEulerAngles.z + "*****");

                    if (DataMgr.Instance.dataBLuck.prizeInfo.prizeDesc == null)
                    {
                        debug.Log_Blue("抽奖返回数据为空");
                        return;
                    }
                    debug.Log_yellow("恭喜获得奖励： " + DataMgr.Instance.dataBLuck.prizeInfo.prizeDesc);

                    if (coroutine_OpenPage != null) StopCoroutine(coroutine_OpenPage);
                    coroutine_OpenPage = StartCoroutine(OpenRewardPage());

                    /*
                    for (int i = 0; i < dengs.Length; i++)
                    {
                        dengs[i].enabled = false;
                    }
                    for (int i = 0; i < dengImgs.Length; i++)
                    {
                        dengImgs[i].sprite = deng_Sp;
                    }
                    */
                }
            }
            else
            {
                // 转动转盘(back为顺时针, forward为逆时针)
                if (rotateTiming >= 1.5f)
                {
                    rotateSpeed -= Time.deltaTime * 10;
                    //rotateSpeed -= Time.deltaTime * 5;
                }
                ZhuanPanImg.Rotate(Vector3.back * rotateSpeed);
            }
        }

        IEnumerator OpenRewardPage()
        {
            yield return new WaitForSeconds(0.5f);
            UIMgr.Instance.Open(UIPath.UIBGetRewardPanel);

        }

        // 点击抽奖按钮
        void OnClickDrawFun()
        {
            if (!isStartRotate)
            {
                if (GameData.canDraw != 0)
                {
                    //开始旋转
                    isStartRotate = true;
                    rotateTiming = 0;
                    rotateSpeed = 20;
                    NetMgr.Instance.C2S_Lucky_LuckyHouseDraw(GameData.userId.ToString());
                    //if (GameData.taskInfo != null && GameData.taskInfo.taskType == 371)
                    //{
                    //    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                    //    DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
                    //    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
                    //    NetMgr.Instance.C2S_Love_TaskUpdateItem();
                    //}
                    //----------------设置任务完成------------
                    var task = DataMgr.Instance.GetTaskItemByType(371);
                    if (task != null && task.taskStatus == 0)
                    {
                        DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                        DataMgr.Instance.dataBTaskInfoReq.taskId = task.id;
                        DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = task.taskCurProgress + 1;
                        NetMgr.Instance.C2S_Love_TaskUpdateItem();
                    }
                    //---------------------------------------
                }
                else
                {
                    GameTools.SetTip("当前时间段已抽过奖啦");
                }
            }
        }

        void S2C_Lucky_LuckyHouseDrawCallBack(object o)
        {
            if (DataMgr.Instance.dataBLuck.prizeInfo == null)
            {
                return;
            }

            GameData.curPrizeData = DataMgr.Instance.dataBLuck.prizeInfo;
            // 随机到转盘第几块区域  0开头
            resultId = DataMgr.Instance.dataBLuck.prizeInfo.id;
            //内部员工展示
            //1： 爱心*1  2：爱心*2  3：加速卡*1  4：加速卡*2  5：能量卡*3  6：交友卡*1  7:交友卡*3
            //图片对应： 0：交友卡*3  1：加速卡*1  2：爱心卡*2  3：能量卡*3  4：交友卡*1   5：爱心卡*1  6:加速卡*2
            //非内部员工展示
            //8：能量卡*3  9：能量卡*5  10：加速卡*1  11：加速卡*2  12：爱心卡*1  13：爱心卡*2
            //图片对应： 0：能量卡*3  1：加速卡*1  2：爱心卡*2  3：能量卡*5  4：加速卡*2   5：爱心卡*1

            float targetRot;
            if (resultId == 1) rewardIndex = 5;
            else if (resultId == 2) rewardIndex = 2;
            else if (resultId == 3) rewardIndex = 1;
            else if (resultId == 4) rewardIndex = 6;
            else if (resultId == 5) rewardIndex = 3;
            else if (resultId == 6) rewardIndex = 4;
            else if (resultId == 7) rewardIndex = 0;

            else if (resultId == 8) rewardIndex = 0;
            else if (resultId == 9) rewardIndex = 3;
            else if (resultId == 10) rewardIndex = 1;
            else if (resultId == 11) rewardIndex = 4;
            else if (resultId == 12) rewardIndex = 5;
            else if (resultId == 13) rewardIndex = 2;

            debug.Log_yellow("+++>> " + rewardIndex);

            if (IsUnmarried)
            {
                targetRot = Random.Range(rewardIndex * (360 / 7f) - (360 / 7f / 2) + 5, rewardIndex * (360 / 7f) + (360 / 7f / 2) - 5);
            }
            else
            {
                targetRot = Random.Range(rewardIndex * (360 / 6f) - (360 / 6f / 2) + 5, rewardIndex * (360 / 6f) + (360 / 6f / 2) - 5);
            }

            //设置目标位置
            targetAngels = Quaternion.Euler(0, 0, targetRot);

            Debug.Log("----- 开始抽奖 随机到的区域，索引是:" + rewardIndex + "，角度是：" + targetRot + "-----");
        }

        void OnDestroy()
        {
            coroutine_OpenPage = null;
            coroutine_PlayDengAni = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Lucky_LuckyHouseDraw, S2C_Lucky_LuckyHouseDrawCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);
            //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }

        //抽奖成功更新数据
        void S2C_Update_UpdateGameDataCallBack(object o)
        {
            if (Txt_Num == null)
            {
                debug.Log_Red("Txt_Num Is Null");
                return;
            }
            //1可以 0不可
            if (GameData.canDraw == 0)
            {
                debug.Log_Blue("不能抽奖");
                Txt_Num.text = "0";
            }
            else
            {
                debug.Log_Blue("可以抽奖");
                Txt_Num.text = GameData.canDraw.ToString();
            }
        }
    }
}
