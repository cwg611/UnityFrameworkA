using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using My.Msg;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace My.UI.Panel
{
    /// <summary>
    /// 活动
    /// </summary>
    public class UIBActivityPanel : BasePanel
    {
        private Button btn_Close;
        private RectTransform activityRect;
        private RectTransform noticeBoard;
        private GameObject RedPackage;
        private GameObject ConversionGo;
        private GameObject LeaderBoardsGo;

        private BulletScreenComponent redPackageBulletScreen, bulletScreenComponent;
        private CarouselComponent carouselComponent;

        List<List<ActivityReward>> activityRewardList;
        //Dictionary<string, List<ActivityReward>> activityRewardMap;
        //List<MDataRank> mDataRankList;
        private List<RectTransform> rankListItems = new List<RectTransform>();//排行榜
        private List<Text> rankListTexts = new List<Text>();

        private const string RankConstStr = "“{0}”获得了“{1}”奖品";

        public override void InitPanel(object o)
        {
            IsHomePanel = true;
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            }
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f, tweenerCallBack);
            btn_Close = transform.Find("ScrollView/CloseBtn").GetComponent<Button>();
            activityRect = transform.Find("ScrollView/Viewport/Content/ActivityImage1").rectTransform();
            noticeBoard = GameTools.GetByName(activityRect.transform, "NoticeBoard").transform.rectTransform();
            RedPackage = GameTools.GetByName(noticeBoard.transform, "RedPackage");
            ConversionGo = GameTools.GetByName(noticeBoard.transform, "Conversion");
            LeaderBoardsGo = GameTools.GetByName(noticeBoard.transform, "LeaderBoards");
            redPackageBulletScreen = RedPackage.transform.Find("BulletScreen").gameObject.AddComponent<BulletScreenComponent>();
            bulletScreenComponent = ConversionGo.transform.Find("BulletScreen").gameObject.AddComponent<BulletScreenComponent>();
            carouselComponent = transform.Find("ScrollView/Viewport/Content/ActivityImage2/Carousel").gameObject.AddComponent<CarouselComponent>();

            var imageArray = LeaderBoardsGo.transform.Find("RankList").GetComponentsInChildren<Image>();
            for (int i = 0; i < imageArray.Length; i++)
            {
                rankListItems.Add(imageArray[i].rectTransform());
                rankListTexts.Add(imageArray[i].GetComponentInChildren<Text>());
            }

            btn_Close.onClick.AddListener(OnClose);

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Get_Newest_Red_List, S2C_Get_Newest_Red_List);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Shop_GetShopRecordList, S2C_Shop_GetShopRecordListCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Shop_GetShopInfoList, S2C_Shop_GetShopInfoListCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Activity_PrizeList, S2C_Activity_PrizeList);
        }

        private void tweenerCallBack()
        {
            NetMgr.Instance.C2S_Get_Newest_Red_List();
            NetMgr.Instance.C2S_Shop_GetShopRecordList();
            NetMgr.Instance.C2S_Shop_GetShopInfoList();
            NetMgr.Instance.C2S_Activity_GetRecard();
        }

        //排行榜
        void S2C_Activity_PrizeList(object o)
        {
            DataBActivity dataBActivity = DataMgr.Instance.DataBActivity;
            if (dataBActivity == null || dataBActivity.friendList == null || dataBActivity.friendList.Count == 0)
            {
                SetActivityView(false);
            }
            else
            {
                activityRewardList = new List<List<ActivityReward>>();

                if (dataBActivity.friendList != null && dataBActivity.friendList.Count > 0)
                {
                    activityRewardList.Add(dataBActivity.friendList);
                }
                if (dataBActivity.honorList != null && dataBActivity.honorList.Count > 0)
                {
                    activityRewardList.Add(dataBActivity.honorList);
                }
                if (dataBActivity.composeGameList != null && dataBActivity.composeGameList.Count > 0)
                {
                    activityRewardList.Add(dataBActivity.composeGameList);
                }
                if (dataBActivity.jumpGameList != null && dataBActivity.jumpGameList.Count > 0)
                {
                    activityRewardList.Add(dataBActivity.jumpGameList);
                }
                SetActivityView(true);

            }
        }

        //红包记录
        void S2C_Get_Newest_Red_List(object o)
        {
            List<DataRedPackageRecord> recordList = DataMgr.Instance.DataUserRedRecordList.userRedList;

            List<string> bullets = new List<string>();
            for (int i = 0; i < recordList.Count; i++)
            {
                bullets.Add(string.Format("“{0}”获得了{1}元余额", recordList[i].nickName, recordList[i].redEnvelopesQuota)); //"“李雪峰”兑换了“猪肉礼盒”奖品"
            }
            if (bullets.Count < 5)
            {
                bullets.Add("“Juicy”获得了0.1元余额");
                bullets.Add("“阿宁”获得了0.1元余额");
                bullets.Add("“中国梦”获得了0.2元余额");
                bullets.Add("“一切随心”获得了0.3元余额");
                bullets.Add("“谢婉莹”获得了0.5元余额");
                bullets.Add("“养猪大佬”获得了0.8元余额");
                bullets.Add("“Az”获得了2元余额");
                bullets.Add("“牧原家人”获得了8元余额");
            }
            redPackageBulletScreen.InitData(bullets, 10, 3.2f);
        }

        //兑换记录回调
        void S2C_Shop_GetShopRecordListCallBack(object o)
        {
            List<UserConversionRecord> recordList = DataMgr.Instance.dataBShopRecordListRes.conversionRecordList;
            List<UserConversionRecord> PhysicalRecordList = new List<UserConversionRecord>();
            if (recordList != null)
            {
                PhysicalRecordList.AddRange(recordList.Where(t => t.isVirtual == 1).ToList());
            }
            List<string> bullets = new List<string>();
            for (int i = 0; i < PhysicalRecordList.Count; i++)
            {
                bullets.Add(string.Format("“{0}”兑换了“{1}”奖品", PhysicalRecordList[i].nickName, PhysicalRecordList[i].productName)); //"“李雪峰”兑换了“猪肉礼盒”奖品"
            }
            if (bullets.Count < 5)
            {
                bullets.Add("“叫我祎祎”兑换了“聚小哼聚爱定制卷纸”奖品");
                bullets.Add("“jayx_cx”兑换了“小猪哼皮克挂件”奖品");
                bullets.Add("“my002714”兑换了“定制创意抱枕”奖品");
                bullets.Add("“打工人打工魂”兑换了“盲盒”奖品");
                bullets.Add("“平安是福”兑换了“聚小哼聚爱定制卷纸”奖品");
            }
            bulletScreenComponent.InitData(bullets, 10);
        }

        //奖品
        void S2C_Shop_GetShopInfoListCallBack(object o)
        {
            List<ConversionShop> PhysicalShopData = new List<ConversionShop>();
            var dataList = DataMgr.Instance.dataBShopListRes.conversionShopList;
            if (dataList == null) return;
            PhysicalShopData.AddRange(dataList.Where(t => t.isVirtual == 1).ToList());

            List<CarouselComponent.DataCarousel> dataCarousels = new List<CarouselComponent.DataCarousel>();
            for (int i = 0; i < PhysicalShopData.Count; i++)
            {
                var item = PhysicalShopData[i];
                dataCarousels.Add(new CarouselComponent.DataCarousel() { Name = item.productName, Picture = item.productPicture });
            }
            carouselComponent.InitData(dataCarousels);
        }

        int circleIndex = 0;
        float deadTime = 0;//停滞时间
        float roteteTime = 0.6f;//旋转时间
        private void SetActivityView(bool isActivity)
        {
            LeaderBoardsGo.SetActive(isActivity);
            //noticeBoard.sizeDelta = isActivity ? new Vector2(982, 1340) : new Vector2(1080, 1020);

            noticeBoard.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 755, isActivity ? 1660 : 1340);

            activityRect.sizeDelta = isActivity ? new Vector2(1080, 3100) : new Vector2(1080, 2760);
            LayoutRebuilder.ForceRebuildLayoutImmediate(activityRect);

            if (isActivity)
            {
                SetRankListContent(0);//0
                if (activityRewardList.Count > 1)//Play
                {
                    Tween t = DOTween.To(() => deadTime, x => deadTime = x, 5, 5).OnStepComplete(() =>
                    {
                        rankListItems[0].DORotateQuaternion(Quaternion.Euler(90, 0, 0), roteteTime).OnStepComplete(() =>
                        {
                            circleIndex = (circleIndex + 1) % activityRewardList.Count;
                            rankListItems[0].localRotation = Quaternion.Euler(-90, 0, 0);
                            SetRankListContent(circleIndex);
                            rankListItems[0].DORotateQuaternion(Quaternion.Euler(0, 0, 0), roteteTime);
                        });
                        SetItemTween(1);
                        SetItemTween(2);
                        SetItemTween(3);
                    }).SetLoops(-1);
                }

            }
        }

        private void SetItemTween(int index)
        {
            rankListItems[index].DORotateQuaternion(Quaternion.Euler(90, 0, 0), roteteTime).OnStepComplete(() =>
            {
                rankListItems[index].localRotation = Quaternion.Euler(-90, 0, 0);
                rankListItems[index].DORotateQuaternion(Quaternion.Euler(0, 0, 0), roteteTime);
            });
        }

        private void SetRankListContent(int index)
        {
            var dataList = activityRewardList[index];
            rankListTexts[0].text = dataList.Count > 0 ? dataList[0].groupName : "";

            rankListTexts[1].text = dataList.Count > 0 ? string.Format(RankConstStr, dataList[0].nickName, dataList[0].productName) : "";
            rankListTexts[2].text = dataList.Count > 1 ? string.Format(RankConstStr, dataList[1].nickName, dataList[1].productName) : "";
            rankListTexts[3].text = dataList.Count > 2 ? string.Format(RankConstStr, dataList[2].nickName, dataList[2].productName) : "";
        }

        void OnClose()
        {
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
            }
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBActivityPanel);
            });
        }

        void OnDestroy()
        {
            rankListItems = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Get_Newest_Red_List, S2C_Get_Newest_Red_List);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Shop_GetShopRecordList, S2C_Shop_GetShopRecordListCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Shop_GetShopInfoList, S2C_Shop_GetShopInfoListCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Activity_PrizeList, S2C_Activity_PrizeList);
        }

    }
}
