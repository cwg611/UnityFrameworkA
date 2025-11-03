using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//主模块 主页面
namespace My.UI.Panel
{
    public class UIBLoveHomePanel : BasePanel
    {
        //View
        private Button btn_CloseA, btn_Task, btn_Recharge;
        private Text txt_Time;
        private Image LoveValue;
        private RectTransform Ani_Mask, Mask_QiPao;
        private GameObject Ani_Love, Ani_Point, Btn_Point, Qipao_Love;
        private Transform wheelBig, wheelSmall;
        private Text LoveNum, EneryNum;
        DataBChargeTime dataBChargeTime;

        private bool isIdle = true; //判断当前倒计时是否为空闲状态
        private bool isOpen = false; //判断是否已经拿到服务器数据
        private int currentServerTime;
        private int targetTime; //倒计时结束时间
        private Coroutine coroutine_refreshPage;//延迟刷新界面
        private Button btn_Bg;
        private bool isAlreadyGet = false;
        private Coroutine coroutine_Love;//产出爱心
        private float Speed_bigWheel = 80;
        private float Speed_smallWheel = 160;
        private GameObject Btn_AddEnergy, Btn_AddLove, Btn_AddTime, ImgUnCollect;
        private Text unLoveNum;

        private GameObject ExplainView;

        public override void InitPanel(object o)
        {
            IsHomePanel = true;
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            }
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"),
                new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f, TweenCallBack);

            btn_CloseA = GameTools.GetByName(transform, "btn_CloseA").GetComponent<Button>();
            btn_Task = GameTools.GetByName(transform, "btn_Task").GetComponent<Button>();
            btn_Recharge = GameTools.GetByName(transform, "btn_Recharge").GetComponent<Button>();
            txt_Time = GameTools.GetByName(transform, "txt_Time").GetComponent<Text>();
            LoveValue = GameTools.GetByName(transform, "LoveValue").GetComponent<Image>();
            LoveNum = GameTools.GetByName(transform, "LoveNum").GetComponent<Text>();
            EneryNum = GameTools.GetByName(transform, "EneryNum").GetComponent<Text>();

            Ani_Mask = GameTools.GetByName(transform, "Ani_Mask").GetComponent<RectTransform>();
            Mask_QiPao = GameTools.GetByName(transform, "Mask_QiPao").GetComponent<RectTransform>();

            Qipao_Love = GameTools.GetByName(transform, "Qipao_Love");

            Ani_Love = GameTools.GetByName(transform, "Ani_Love");
            Ani_Love.SetActive(false);
            Ani_Point = GameTools.GetByName(transform, "Ani_Point");
            Ani_Point.SetActive(false);
            Btn_Point = GameTools.GetByName(transform, "Btn_Point");

            ImgUnCollect = GameTools.GetByName(transform, "ImgUnCollect");
            unLoveNum = GameTools.GetByName(transform, "unLoveNum").GetComponent<Text>();
            ImgUnCollect.SetActive(false);

            ExplainView = transform.Find("ExplainPop").gameObject;
            GameTools.Instance.AddClickEvent(transform.Find("Content_bg/Bg/ExplainBtn"), () => { ExplainView.SetActive(true); });
            GameTools.Instance.AddClickEvent(transform.Find("ExplainPop/Bg/CloseBtn"), () => { ExplainView.SetActive(false); });
            //点击手指采集爱心
            GameTools.Instance.AddClickEvent(Btn_Point.gameObject, () =>
            {
                if (!Ani_Point.activeSelf) return;
                PlayerPrefs.SetInt(GameData.isCollected, -1);
                PlayerPrefs.SetInt(GameData.unCollectLoveNum, 0);
                //清空记录的时间
                PlayerPrefs.SetInt(GameData.quitTime, 0);

                Ani_Love.SetActive(true);
                Ani_Point.SetActive(false);
                if (coroutine_Love != null)
                {
                    StopCoroutine(coroutine_Love);
                }
                coroutine_Love = StartCoroutine(GeneralLove());
            });

            Ani_Mask.sizeDelta = new Vector2(152, 15);
            Mask_QiPao.sizeDelta = new Vector2(134, 0);

            wheelBig = GameTools.GetByName(transform, "wheelBig").transform;
            wheelSmall = GameTools.GetByName(transform, "wheelSmall").transform;

            btn_Bg = GameTools.GetByName(transform, "btn_Bg").GetComponent<Button>();

            btn_CloseA.onClick.AddListener(OnBtnCloseClick);
            btn_Task.onClick.AddListener(OnBtn_TaskClick);
            //点击充能
            GameTools.Instance.AddClickEvent(btn_Recharge.gameObject, Onbtn_RechargeClick);
            btn_Bg.onClick.AddListener(OnBtnCloseClick);

            //使用 能量卡
            Btn_AddEnergy = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_AddEnergy"), () =>
            {
                GameData.cardType = CardType.EnergyCard;
                if (GameData.GetBagCardDataByCardType(GameData.cardType) != null &&
                GameData.GetBagCardDataByCardType(GameData.cardType).productNum > 0)
                {
                    OpenLoveWindowPanel();
                }
                else
                {
                    GameTools.SetTip("能量卡不足");
                }
            });

            //使用 爱心卡
            Btn_AddLove = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_AddLove"), () =>
            {
                GameData.cardType = CardType.LoveCard;
                if (GameData.GetBagCardDataByCardType(GameData.cardType) != null && GameData.GetBagCardDataByCardType(GameData.cardType).productNum > 0)
                {
                    OpenLoveWindowPanel();
                }
                else
                {
                    GameTools.SetTip("爱心卡不足");
                }
            });

            //使用 加速卡
            Btn_AddTime = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_AddTime"), () =>
            {
                //if (isOpen && GameData.UserData.isHatching == 1) //是否在孵化中  1：正在孵化 0:没有孵化
                if (isOpen && !isIdle) //是否在孵化中  1：正在孵化 0:没有孵化
                {
                    GameData.cardType = CardType.AcculateCard;
                    if (GameData.GetBagCardDataByCardType(GameData.cardType) != null && GameData.GetBagCardDataByCardType(GameData.cardType).productNum > 0)
                    {
                        OpenLoveWindowPanel();
                    }
                    else
                    {
                        GameTools.SetTip("加速卡不足");
                    }
                }
                else
                {
                    GameTools.SetTip("不在孵化中，无法使用加速卡");
                }
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_GetChargeTime, S2C_Love_GetChargeTimeCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_Recharge, S2C_Love_RechargeCallBack);

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);

            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }

        void TweenCallBack()
        {
            NetMgr.Instance.C2S_Love_GetChargeTime(GameData.userId.ToString());
            LoveNum.text = GameData.GetCurLoveNum().ToString();
            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_LOVE];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="o"></param>
        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{
        //    GameData.taskInfo = null;
        //}

        void OpenLoveWindowPanel() { UIMgr.Instance.Open(UIPath.UIBLoveWindowPanel); }

        IEnumerator GeneralLove()
        {
            yield return new WaitForSeconds(0.6f);
            Ani_Love.SetActive(false);
            isAlreadyGet = false;
            ImgUnCollect.SetActive(false);
            DOTweenMgr.Instance.MovePos(Qipao_Love, new Vector3(269, 181.34f, 0), new Vector3(269, 285, 0), .5f, () =>
            {
                Qipao_Love.transform.localPosition = new Vector3(269, 181.34f, 0);
            }
        );
            DOTweenMgr.Instance.DoScale(Qipao_Love, 0.1f, .5f);
            LoveNum.text = GameData.GetCurLoveNum().ToString();
        }

        //卡片数量为0时隐藏按钮
        //正在孵化隐藏能量卡、未孵化隐藏加速卡
        private void RefreshCardButtons()
        {
            debug.Log_yellow("RefreshCardButtons");
            debug.Log_yellow("空闲=="+ isIdle);
            //未孵化且数量大于0显示
            Btn_AddEnergy.SetActive((GameData.GetBagCardDataByCardType(CardType.EnergyCard) != null
                && GameData.GetBagCardDataByCardType(CardType.EnergyCard).productNum > 0)
                && isIdle);
            Btn_AddLove.SetActive(GameData.GetBagCardDataByCardType(CardType.LoveCard) != null
                && GameData.GetBagCardDataByCardType(CardType.LoveCard).productNum > 0);
            //孵化且数量大于0显示
            Btn_AddTime.SetActive(!isIdle && (GameData.GetBagCardDataByCardType(CardType.AcculateCard) != null
                && GameData.GetBagCardDataByCardType(CardType.AcculateCard).productNum > 0));
        }

        void Update()
        {
            if (isOpen && !isIdle)
            {
                currentServerTime = TimeMgr.Instance.serverTime;
                if (targetTime - currentServerTime > 0)
                {
                    txt_Time.text = GameTools.TimeStampToTime((int)(targetTime - currentServerTime));
                    GameData.CurTimeDistance = targetTime - currentServerTime;
                    Ani_Mask.sizeDelta = new Vector2(152, (15 + 125 * (1 - ((float)(targetTime - currentServerTime) / (4 * 60 * 60)))));
                    Mask_QiPao.sizeDelta = new Vector2(134, 125 * (1 - ((float)(targetTime - currentServerTime) / (4 * 60 * 60))));
                    wheelBig.Rotate(0, 0, -Speed_bigWheel * Time.deltaTime);
                    wheelSmall.Rotate(0, 0, Speed_smallWheel * Time.deltaTime);
                }
                else
                {
                    GameData.CurTimeDistance = 0;
                    isIdle = true;
                    Debug.Log("-->> 倒计时结束");
                    isAlreadyGet = true;
                    //显示手指
                    Ani_Point.SetActive(true);
                    //待收集爱心数 +1
                    if (!GameData.isUseCard)
                    {
                        PlayerPrefs.SetInt(GameData.unCollectLoveNum, PlayerPrefs.GetInt(GameData.unCollectLoveNum) + 1);
                        PlayerPrefs.SetInt(GameData.isCollected, 1);
                        PlayerPrefs.SetInt(GameData.isAlreadyAdd, -1);
                    }
                    //ToDo: 延迟3秒钟后请求服务器数据
                    txt_Time.text = GameTools.TimeStampToTime(0);
                    Ani_Mask.sizeDelta = new Vector2(152, 15);
                    Mask_QiPao.sizeDelta = new Vector2(117, 0);
                    if (coroutine_refreshPage != null)
                    {
                        StopCoroutine(coroutine_refreshPage);
                    }
                    coroutine_refreshPage = StartCoroutine(refreshPage());

                    wheelBig.Rotate(0, 0, 0 * Time.deltaTime);
                    wheelSmall.Rotate(0, 0, 0 * Time.deltaTime);
                }
            }
        }

        IEnumerator refreshPage()
        {
            yield return new WaitForSeconds(0.5f);
            NetMgr.Instance.C2S_Love_GetChargeTime(GameData.userId.ToString());
        }

        //获取充能时间返回
        void S2C_Love_GetChargeTimeCallBack(object o)
        {
            dataBChargeTime = DataMgr.Instance.dataBChargeTime;
            GameData.curEnergy = dataBChargeTime.curEnergy;

            //默认第一次   服务器不返回 该字段的处理
            if (dataBChargeTime.lastChargePastTime == null)
            {
                dataBChargeTime.lastChargePastTime = "2020-06-03 10:18:35";
            }

            targetTime = GameTools.TmGetTimeStamp(DateTime.Parse(dataBChargeTime.lastChargePastTime));
            isOpen = true;
            //通过充电结束时间与当前服务器时间对比，判断当前倒计时的状态
            if (GameTools.TmGetTimeStamp(DateTime.Parse(dataBChargeTime.lastChargePastTime)) > TimeMgr.Instance.getServerTime())
            {
                isIdle = false;
                Debug.Log("当前为奔跑状态");
            }
            else
            {
                isIdle = true;
                Debug.Log("当前为空闲状态");
                Ani_Mask.sizeDelta = new Vector2(152, 15);
                Mask_QiPao.sizeDelta = new Vector2(117, 0);
                txt_Time.text = GameTools.TimeStampToTime(0);
            }

            EneryNum.text = GameData.curEnergy.ToString();

            //进来后，如果是空闲状态，判断上次倒计时结束时间与记录的时间进行匹对，如果结束时间大于记录时间，则待收集爱心+1
            debug.Log_Blue("记录的状态： " + PlayerPrefs.GetInt(GameData.isAlreadyAdd));
            //if (GameData.UserData.isHatching == 0)
            if (isIdle)
            {
                if (!GameData.isUseCard && PlayerPrefs.GetInt(GameData.quitTime) != 0 && GameTools.TmGetTimeStamp(DateTime.Parse(dataBChargeTime.lastChargePastTime)) > PlayerPrefs.GetInt(GameData.quitTime))
                {
                    Ani_Point.SetActive(true);
                    //待收集爱心数 +1
                    if (PlayerPrefs.GetInt(GameData.isAlreadyAdd) == 0 || PlayerPrefs.GetInt(GameData.isAlreadyAdd) == 1)
                    {
                        PlayerPrefs.SetInt(GameData.unCollectLoveNum, PlayerPrefs.GetInt(GameData.unCollectLoveNum) + 1);
                        PlayerPrefs.SetInt(GameData.isAlreadyAdd, -1);
                    }
                    PlayerPrefs.SetInt(GameData.isCollected, 1);
                    debug.Log_Red("---->> +1");
                }

                //判断使用加速道具卡后是否增加了爱心数
                if (GameData.isUseCard)
                {
                    if (targetTime - TimeMgr.Instance.serverTime <= 0)
                    {
                        debug.Log_Red("使用道具卡后，增加了爱心数量  +1");
                        PlayerPrefs.SetInt(GameData.unCollectLoveNum, PlayerPrefs.GetInt(GameData.unCollectLoveNum) + 1);
                        PlayerPrefs.SetInt(GameData.isAlreadyAdd, -1);
                        PlayerPrefs.SetInt(GameData.isCollected, 1);
                        GameData.isUseCard = false;
                    }
                }
            }

            //判断是否有未采集的爱心
            if (PlayerPrefs.GetInt(GameData.isCollected) == 1 && PlayerPrefs.GetInt(GameData.unCollectLoveNum) > 0)
            {
                Ani_Point.SetActive(true);
                ImgUnCollect.SetActive(true);
                unLoveNum.text = "×" + GameData.GetCurUnCollectNum().ToString();
            }
            else
            {
                Ani_Point.SetActive(false);
                ImgUnCollect.SetActive(false);
            }

            RefreshCardButtons();
        }

        void S2C_Love_RechargeCallBack(object o)
        {
            NetMgr.Instance.C2S_Love_GetChargeTime(GameData.userId.ToString());
        }

        void S2C_Update_UpdateGameDataCallBack(object o)
        {
            RefreshCardButtons();
            LoveNum.text = GameData.GetCurLoveNum().ToString();
            //能量值
            EneryNum.text = GameData.curEnergy.ToString();
        }

        void OnBtn_TaskClick()
        {
            UIMgr.Instance.Open(UIPath.UIBLoveTaskPanel, null, UILayer.Layer3);
        }

        void Onbtn_RechargeClick()
        {
            if (!isIdle)
            {
                GameTools.SetTip("正在孵化中，等会再来吧");
                return;
            }

            if (GameData.curEnergy >= GameData.consumeOil)
            {
                NetMgr.Instance.C2S_Love_Recharge(GameData.userId.ToString());
                PlayerPrefs.SetInt(GameData.isAlreadyAdd, 1);

                //if (GameData.taskInfo != null && GameData.taskInfo.taskType == 311)
                //{
                //    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                //    DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
                //    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
                //    NetMgr.Instance.C2S_Love_TaskUpdateItem();
                //}
                //----------------设置任务完成------------
                var task = DataMgr.Instance.GetTaskItemByType(311);
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
                GameTools.SetTip("能量不足（每次充能需要10能量）");
            }
        }

        void OnBtnCloseClick()
        {
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
            }
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, 0, 0), new Vector3(0, -Screen.height - 800, 0), .3f, () =>
                {
                    //退出界面时如果还在孵化状态，记录退出时的服务器时间
                    if (!isIdle)
                    {
                        PlayerPrefs.SetInt(GameData.quitTime, currentServerTime);
                    }
                    UIMgr.Instance.Close(UIPath.UIBLoveHomePanel);
                });
        }

        public override void ReleasePanel()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_GetChargeTime, S2C_Love_GetChargeTimeCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_Recharge, S2C_Love_RechargeCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);
            coroutine_refreshPage = null;
            //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }
    }
}