using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using My.Msg;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UILeaderBorderPanel : BasePanel
    {
        private TableView tableView;
        //private GameObject ItemPrefab;
        private UILeaderBorderPanelItem MyItem;
        public static ImgQiehuan Qiehuan;

        private Button btn_Day, btn_Week, btn_All;
        private void Awake()
        {

        }


        private void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_GetGameDXGRankList, S2C_Game_GetGameDXGRankListCallBack);
        }

        public override void InitPanel(object o)
        {
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Scroll View"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f);
            //ItemPrefab = GameTools.GetByName(transform, "Item");
            tableView = transform.Find("Scroll View").gameObject.AddComponent<TableView>();
            MyItem = GameTools.GetByName(transform, "MyItem").AddComponent<UILeaderBorderPanelItem>();
            btn_Day = transform.Find("Bg/DayButton").GetComponent<Button>();
            btn_Week = transform.Find("Bg/WeekButton").GetComponent<Button>();
            btn_All = transform.Find("Bg/AllButton").GetComponent<Button>();
            Qiehuan = GameTools.GetByName(transform, "Qiehuan").GetComponent<ImgQiehuan>();

            //ItemPrefab.SetActive(false);

            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Back"), () =>
            {
                closeUIPanel();
            });

            btn_Day.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.one;
                btn_Week.transform.GetChild(0).localScale = Vector3.zero;
                btn_All.transform.GetChild(0).localScale = Vector3.zero;
                NetMgr.Instance.C2S_Game_GetGameDXGRankList(RankingType.Day);
            });
            btn_Week.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.zero;
                btn_Week.transform.GetChild(0).localScale = Vector3.one;
                btn_All.transform.GetChild(0).localScale = Vector3.zero;
                NetMgr.Instance.C2S_Game_GetGameDXGRankList(RankingType.Week);
            });
            btn_All.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.zero;
                btn_Week.transform.GetChild(0).localScale = Vector3.zero;
                btn_All.transform.GetChild(0).localScale = Vector3.one;
                NetMgr.Instance.C2S_Game_GetGameDXGRankList(RankingType.All);
            });
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_GetGameDXGRankList, S2C_Game_GetGameDXGRankListCallBack);


            InitPage();
        }

        private void closeUIPanel()
        {
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Scroll View"), new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                UIMgr.Instance.Close(UIPath.UILeaderBorderPanel);
            });
            UIMgr.Instance.Open(UIPath.UIGameAStartPanel);
        }

        private void InitPage()
        {
            DataMgr.Instance.dataBGetGameDXGScoreReq.userId = GameData.userId;
            DataMgr.Instance.dataBGetGameDXGScoreReq.gameName = GameData.GameAName;
            NetMgr.Instance.C2S_Game_GetGameDXGRankList(RankingType.Day);

        }

        void S2C_Game_GetGameDXGRankListCallBack(object o)
        {
            debug.Log_yellow("获取排行榜成功");
            if (DataMgr.Instance.dataBScoreRankRes!=null&& DataMgr.Instance.dataBScoreRankRes.scoreRankList != null)
            {
                var dataList = DataMgr.Instance.dataBScoreRankRes.scoreRankList;
                Action<int, GameObject> onItemRender = (i, go) =>
                {
                    go.GetComponent<UILeaderBorderPanelItem>().SetLeaderBorderItem(dataList[i], 2);
                };
                if (tableView != null)
                {
                    if (!tableView.Init)
                    {
                        tableView.onItemCreated = (go) => { go.AddComponent<UILeaderBorderPanelItem>(); };

                        tableView.onItemRender = onItemRender;
                        tableView.InitTable(dataList.Count);
                    }
                    else
                    {
                        tableView.onItemRender = onItemRender;
                        tableView.ReLoadTable(dataList.Count);
                    }
                }
                DataGameA myData = DataMgr.Instance.dataBScoreRankRes.userGameInfo;
                if (myData != null)
                {
                    MyItem.gameObject.SetActive(true);
                    MyItem.SetLeaderBorderItem(myData, 1);

                }
                else
                {
                    MyItem.gameObject.SetActive(false);
                }
            }
            else
            {
                if (tableView!=null&& tableView.Init)
                {
                    tableView.ReLoadTable(0);
                }
                MyItem.gameObject.SetActive(false);
            }

           
            //for (int i = 0; i < DataMgr.Instance.dataBScoreRankRes.scoreRankList.Count; i++)
            //{
            //    UILeaderBorderPanelItem Item = GameObject.Instantiate(ItemPrefab, ItemPrefab.transform.parent).AddComponent<UILeaderBorderPanelItem>();
            //    Item.gameObject.SetActive(true);
            //    Item.SetLeaderBorderItem(DataMgr.Instance.dataBScoreRankRes.scoreRankList[i], 2);
            //}
           

        }
    }


}
