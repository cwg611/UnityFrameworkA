using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using HotUpdateScripts.Project.BasePrj.Data;
using My.Msg;
using System.Collections.Generic;
using System.Linq;
using System;

namespace My.UI.Panel
{
    /// <summary>
    /// 兑换商城
    /// </summary>
    public class UIBShopPanel : BasePanel
    {
        //View
        private Button btn_Close;

        public static ImgQiehuan imgQiehuan;

        private Button[] btns;

        private GameObject ScrollViewOne;
        private GameObject ScrollViewTwo;
        //预制体父类
        private Transform ContentOne;
        private Transform ContentTwo;

        //预制体
        private GameObject ItemOne; //实物商品
        private GameObject ItemTwo; //虚拟商品

        private List<GameObject> ItemOneList = new List<GameObject>();//实物商品集合
        private List<GameObject> ItemTwoList = new List<GameObject>();//虚拟商品集合

        private Button btn_Bg;

        private Text CurLoveMoneyNum;
        private Button btn_DhRecord;

        //二次确认窗口
        private GameObject pan;
        private GameObject pan_Content;
        private GameObject btn_minus;
        private GameObject btn_add;
        private Material m_GrayMat;

        private GameObject pan_Icon;

        private Text buyNum;
        private Text consumeNum;

        private Button Btn_DuiHuan;

        private Button btn_PanClose;

        public static Action Act_OpenWindow;

        private int BuyNum;

        List<ConversionShop> dataList;

        List<ConversionShop> virtualShopData = new List<ConversionShop>();
        List<ConversionShop> PhysicalShopData = new List<ConversionShop>();

        private GameObject obj_reallyIcon;
        private Image pan_IconReally;

        private GameObject ExplainView;
        void Awake()
        {
            IsHomePanel = true;
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            }
            GameData.isClickVirtual = false;

            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f, tweenerCallBack);
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            btn_Bg = GameTools.GetByName(transform, "btn_Bg").GetComponent<Button>();
            imgQiehuan = GameTools.GetByName(transform, "QieHuan").GetComponent<ImgQiehuan>();

            btns = new Button[2] { GameTools.GetByName(transform, "btn_one").GetComponent<Button>(), GameTools.GetByName(transform, "btn_two").GetComponent<Button>() };
            ContentOne = GameTools.GetByName(transform, "ContentOne").transform;
            ContentTwo = GameTools.GetByName(transform, "ContentTwo").transform;
            ScrollViewOne = GameTools.GetByName(transform, "ScrollViewOne");
            ScrollViewTwo = GameTools.GetByName(transform, "ScrollViewTwo");

            ItemOne = GameTools.GetByName(transform, "ItemOne");
            ItemTwo = GameTools.GetByName(transform, "ItemTwo");
            ItemOne.SetActive(false);
            ItemTwo.SetActive(false);

            btn_Close.onClick.AddListener(OnBtnClockA);
            btn_Bg.onClick.AddListener(OnBtnClockA);

            CurLoveMoneyNum = GameTools.GetByName(transform, "CurLoveMoneyNum").GetComponent<Text>();
            btn_DhRecord = GameTools.GetByName(transform, "btn_DhRecord").GetComponent<Button>();

            //----------二次确认窗--------
            BuyNum = 1;
            pan = GameTools.GetByName(transform, "pan");
            pan_Content = GameTools.GetByName(transform, "pan_Content");

            obj_reallyIcon = GameTools.GetByName(transform, "obj_reallyIcon");
            pan_IconReally = GameTools.GetByName(transform, "pan_IconReally").GetComponent<Image>();

            pan.SetActive(false);
            btn_minus = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_minus"), () =>
             {
                 if (BuyNum > 1) BuyNum--;
                 setWondowUIVIew();
             });

            btn_add = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_add"), () =>
            {
                BuyNum++;
                setWondowUIVIew();
            });
            ExplainView = transform.Find("ExplainPop").gameObject;
            GameTools.Instance.AddClickEvent(transform.Find("Content_bg/Bg/ExplainBtn"), () => { ExplainView.SetActive(true); });
            GameTools.Instance.AddClickEvent(transform.Find("ExplainPop/Bg/CloseBtn"), () => { ExplainView.SetActive(false); });

            Btn_DuiHuan = GameTools.GetByName(transform, "Btn_DuiHuan").GetComponent<Button>();
            Btn_DuiHuan.onClick.AddListener(() =>
            {
                debug.Log_Blue("兑换");
                if (float.Parse(GameData.curLoveMoney) >= (GameData.curShop.isActivityProduct == 1 ? GameData.curShop.activityNeedLoveMoney :
                GameData.curShop.needLoveMoney) * BuyNum)
                {
                    DataMgr.Instance.dataBShopExchangeReq.userId = GameData.userId;
                    DataMgr.Instance.dataBShopExchangeReq.productId = GameData.curShop.productId;
                    DataMgr.Instance.dataBShopExchangeReq.conversionNum = BuyNum;
                    NetMgr.Instance.C2S_Shop_exchange();
                }
                else
                {
                    GameTools.SetTip("爱心币不足");
                }
            });

            pan_Icon = GameTools.GetByName(transform, "pan_Icon");

            m_GrayMat = btn_minus.GetComponent<Image>().material; //灰色材质

            buyNum = GameTools.GetByName(transform, "buyNum").GetComponent<Text>();
            consumeNum = GameTools.GetByName(transform, "consumeNum").GetComponent<Text>();

            Act_OpenWindow = () =>
            {
                BuyNum = 1;
                pan.SetActive(true);
                DOTweenMgr.Instance.OpenWindowScale(pan_Content.gameObject, .3f);
                setWondowUIVIew();
            };

            btn_PanClose = GameTools.GetByName(transform, "btn_PanClose").GetComponent<Button>();
            btn_PanClose.onClick.AddListener(() =>
            {
                ClosePanWindow();
            });
            //-------------------------
            for (int i = 0; i < btns.Length; i++)
            {
                int index = i;
                btns[index].onClick.AddListener(() =>
                {
                    for (int k = 0; k < btns.Length; k++)
                    {
                        if (k == index) imgQiehuan.SetImg(4, btns[k].gameObject);
                        else imgQiehuan.SetImg(5, btns[k].gameObject);
                    }

                    if (index == 0)
                    {
                        debug.Log_Blue("第一个");
                        ScrollViewOne.gameObject.SetActive(true);
                        ScrollViewTwo.gameObject.SetActive(false);
                        GameData.isClickVirtual = false;
                    }
                    else if (index == 1)
                    {
                        debug.Log_Blue("第二个");
                        ScrollViewOne.gameObject.SetActive(false);
                        ScrollViewTwo.gameObject.SetActive(true);
                        GameData.isClickVirtual = true;
                    }
                });
            }

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Shop_GetShopInfoList, S2C_Shop_GetShopInfoListCallBack);

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Shop_exchange, S2C_Shop_exchangeCallBack);

            btn_DhRecord.onClick.AddListener(() =>
            {
                debug.Log_Blue("点击兑换记录");
                UIMgr.Instance.Open(UIPath.UIBShopRecordPanel);
            });

            CurLoveMoneyNum.text = float.Parse(GameData.curLoveMoney).ToString("F2");
            debug.Log_Red("当前爱心币： " + GameData.curLoveMoney);
            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_SHOP];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();

            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);

            //if (GameData.taskInfo != null && GameData.taskInfo.taskType == 361)
            //{
            //    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
            //    DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
            //    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
            //    NetMgr.Instance.C2S_Love_TaskUpdateItem();
            //}
            //----------------设置任务完成------------
            var task = DataMgr.Instance.GetTaskItemByType(361);
            if (task != null && task.taskStatus == 0)
            {
                DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                DataMgr.Instance.dataBTaskInfoReq.taskId = task.id;
                DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = task.taskCurProgress + 1;
                NetMgr.Instance.C2S_Love_TaskUpdateItem();
            }
            //---------------------------------------
        }

        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{

        //}

        //设置兑换弹窗UI
        void setWondowUIVIew()
        {
            //图标
            //0：虚拟商品  1：实物商品
            if (GameData.curShop.isVirtual == 0)
            {
                pan_Icon.SetActive(true);
                obj_reallyIcon.SetActive(false);
                GameTools.SetDaoJuUIIcon(GameData.curShop.productId, pan_Icon.GetComponent<Image>());
            }
            else
            {
                pan_Icon.SetActive(false);
                obj_reallyIcon.SetActive(true);
                NetMgr.Instance.DownLoadImg(r =>
                {
                    if (pan_IconReally == null) return;
                    pan_IconReally.sprite = r;
                    GameTools.Instance.MatchImgBySprite(pan_IconReally, Match_Img.Height);
                }, GameData.curShop.productPicture);
            }
            buyNum.text = BuyNum.ToString();
            consumeNum.text = (BuyNum * (GameData.curShop.isActivityProduct == 1 ?
                GameData.curShop.activityNeedLoveMoney : GameData.curShop.needLoveMoney)).ToString();
            //consumeNum.text = (BuyNum * GameData.curShop.needLoveMoney).ToString();
            if (BuyNum <= 1)
            {
                btn_minus.GetComponent<Image>().material = m_GrayMat;
            }
            else
            {
                btn_minus.GetComponent<Image>().material = null;
            }
        }

        //兑换成功回调
        void S2C_Shop_exchangeCallBack(object o)
        {
            if (o != null)
            {
                var message = o.ToString();
                if (message.Equals("用户兑换商品成功"))//成功
                {
                    if (GameData.curShop.isVirtual == 0)//虚拟
                    {
                        GameTools.SetTip("恭喜你已兑换成功\n(请前往首页背包查看)");
                    }
                    else
                    {
                        GameTools.SetTip("恭喜你已兑换成功\n(请前往APP-'我的'-'优惠券'查看)");
                    }
                }
                else
                {
                    GameTools.SetTip(message);//失败
                }
            }
            else
            {
                GameTools.SetTip("商品兑换失败");
            }
            NetMgr.Instance.C2S_Shop_GetShopInfoList();
            CurLoveMoneyNum.text = float.Parse(GameData.curLoveMoney).ToString("F2");
            debug.Log_Red("当前爱心币： " + GameData.curLoveMoney);
            ClosePanWindow();
        }

        //关闭窗口
        void ClosePanWindow()
        {
            DOTweenMgr.Instance.CloseWindowScale(pan_Content.gameObject, 0.2f, () =>
            {
                pan.SetActive(false);
            });
        }

        void tweenerCallBack()
        {
            NetMgr.Instance.C2S_Shop_GetShopInfoList();

        }

        void Start()
        {

        }

        void S2C_Shop_GetShopInfoListCallBack(object o)
        {
            dataList = null;
            PhysicalShopData.Clear();
            virtualShopData.Clear();
            dataList = DataMgr.Instance.dataBShopListRes.conversionShopList;
            if (dataList == null) return;

            PhysicalShopData.AddRange(dataList.Where(t => t.isVirtual == 1).ToList());
            virtualShopData.AddRange(dataList.Where(t => t.isVirtual == 0).ToList());
            //剩余0的排在后面
            PhysicalShopData.Sort((a, b) => b.productNum == 0 && a.productNum != 0 ? -1 : 0);
            virtualShopData.Sort((a, b) => b.productNum == 0 && a.productNum != 0 ? -1 : 0);
            //PhysicalShopData.Sort((a, b) => b.productNum.CompareTo(a.productNum));
            //virtualShopData.Sort((a, b) => b.productNum.CompareTo(a.productNum));

            for (int i = 0; i < ItemOneList.Count; i++)
            {
                ItemOneList[i].SetActive(false);
            }
            for (int i = 0; i < ItemTwoList.Count; i++)
            {
                ItemTwoList[i].SetActive(false);
            }
            for (int i = 0; i < PhysicalShopData.Count; i++)
            {
                var item = GetItem(ItemOneList);
                if (!item)
                {
                    item = Instantiate(ItemOne, ContentOne);
                    ItemOneList.Add(item);
                }
                item.SetActive(true);
                item.AddComponent<UIBShopItem>().InitItem(PhysicalShopData[i]);
            }

            for (int i = 0; i < virtualShopData.Count; i++)
            {
                if (!DataMgr.Instance.dataBMainHome.AllowMakingFriends && virtualShopData[i].productType == 3)
                    continue;

                var item = GetItem(ItemTwoList);
                if (!item)
                {
                    item = Instantiate(ItemTwo, ContentTwo);
                    ItemTwoList.Add(item);
                }
                item.SetActive(true);
                item.AddComponent<UIBShopItem>().InitItem(virtualShopData[i]);

                //GameObject Item = Instantiate(ItemTwo, ContentTwo);
                //Item.SetActive(true);
                //Item.AddComponent<UIBShopItem>().InitItem(virtualShopData[i]);
                //ItemTwoList.Add(Item);
            }
        }

        private GameObject GetItem(List<GameObject> objs)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (!objs[i].activeSelf)
                {
                    return objs[i];
                }
            }
            return null;
        }

        //刷新数据
        void RefreshPage()
        {

        }

        void OnBtnClockA()
        {
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
            }
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                dataList = null;
                GameData.taskInfo = null;
                UIMgr.Instance.Close(UIPath.UIBShopPanel);
            });
        }

        void OnDestroy()
        {
            dataList = null;
            virtualShopData.Clear();
            PhysicalShopData.Clear();
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Shop_GetShopInfoList, S2C_Shop_GetShopInfoListCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Shop_exchange, S2C_Shop_exchangeCallBack);
            //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }
    }
}
