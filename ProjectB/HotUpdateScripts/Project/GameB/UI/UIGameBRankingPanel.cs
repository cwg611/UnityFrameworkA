using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using HotUpdateScripts.Project.GameB.UI;
using My.Msg;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    /// <summary>
    /// 跳跳乐排行榜
    /// </summary>
    public class UIGameBRankingPanel : BasePanel
    {
        //private ItemList rankingList;
        private TableView tableView;

        private GameObject myItem;

        private Image img_HeadImg;

        private Text txt_NickName;

        private Text txt_Score;

        private Text txt_Num;

        private Button btn_Day, btn_Week, btn_All;
        public int sign = 0;
        public override void InitPanel(object o)
        {
            base.InitPanel(o);

            DOTweenMgr.Instance.DoScale(gameObject, Vector3.zero, Vector3.one, .3f, DoTweenCallBack);

            //rankingList = transform.Find("ScrollView/Viewport/ItemList").gameObject.AddComponent<ItemList>();
            tableView = transform.Find("ScrollView").gameObject.AddComponent<TableView>();
            myItem = transform.Find("ScrollView/MyRecord").gameObject;
            img_HeadImg = transform.Find("ScrollView/MyRecord/HeadImage").GetComponent<Image>();
            txt_NickName = transform.Find("ScrollView/MyRecord/NickName").GetComponent<Text>();
            txt_Score = transform.Find("ScrollView/MyRecord/ScoreText").GetComponent<Text>();
            txt_Num = transform.Find("ScrollView/MyRecord/Num").GetComponent<Text>();
            btn_Day = transform.Find("Bg/DayButton").GetComponent<Button>();
            btn_Week = transform.Find("Bg/WeekButton").GetComponent<Button>();
            btn_All = transform.Find("Bg/AllButton").GetComponent<Button>();

            transform.Find("BackButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                DOTweenMgr.Instance.DoScale(gameObject, Vector3.one, Vector3.zero, .15f, () =>
                {
                    MsgCenter.Call(null, MsgCode.GameB_SwitchCharacter, null);
                    UIMgr.Instance.Close(UIPath.UIGameBRankingPanel);
                    UIMgr.Instance.Open(UIPath.UIGameBStartPanel);
                });
            });

            btn_Day.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.one;
                btn_Week.transform.GetChild(0).localScale = Vector3.zero;
                btn_All.transform.GetChild(0).localScale = Vector3.zero;
                sign = 1;
                NetMgr.Instance.C2S_Game_Jump_Get_Score_Rank(RankingType.Day);
            });
            btn_Week.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.zero;
                btn_Week.transform.GetChild(0).localScale = Vector3.one;
                btn_All.transform.GetChild(0).localScale = Vector3.zero;
                sign = 2;
                NetMgr.Instance.C2S_Game_Jump_Get_Score_Rank(RankingType.Week);
            });
            btn_All.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.zero;
                btn_Week.transform.GetChild(0).localScale = Vector3.zero;
                btn_All.transform.GetChild(0).localScale = Vector3.one;
                sign = 3;
                NetMgr.Instance.C2S_Game_Jump_Get_Score_Rank(RankingType.All);
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_Jump_Get_Score_Rank, GetScoreRankCallBack);


        }

        public override void ReleasePanel()
        {
            base.ReleasePanel();
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_Jump_Get_Score_Rank, GetScoreRankCallBack);

        }

        private void DoTweenCallBack()
        {
            NetMgr.Instance.C2S_Game_Jump_Get_Score_Rank(RankingType.Day);
        }


        private void GetScoreRankCallBack(object o)
        {
            DataScoreRank data = DataMgr.Instance.dataJumpScoreRank;
            if (data != null)
            {
                var dataList = data.scoreRank;

                Action<int, GameObject> onItemRender = (i, go) =>
                {
                    go.GetComponent<RankingItem>().SetView(dataList[i]);
                };//用Action代替委托需要tableView重新赋值onItemRender
                if (tableView != null)
                {
                    if (!tableView.Init)
                    {
                        tableView.onItemCreated = (go) => { go.AddComponent<RankingItem>(); };

                        tableView.onItemRender = onItemRender;
                        tableView.InitTable(dataList.Count);
                    }
                    else
                    {
                        tableView.onItemRender = onItemRender;
                        tableView.ReLoadTable(dataList.Count);
                    }
                }

                var myData = data.rankByUserId;
                if (myData != null)
                {
                    myItem.SetActive(true);
                    txt_NickName.text = myData.nickName;

                    txt_Score.text = myData.topScore.ToString();

                    txt_Num.text = myData.rowNum.ToString();

                    NetMgr.Instance.DownLoadHeadImg(r =>
                    {
                        if (img_HeadImg == null) return;
                        img_HeadImg.sprite = r;
                    }, myData.headImgUrl);
                }
                else
                {
                    myItem.SetActive(false);
                }
            }
            else
            {
                if (tableView != null && tableView.Init)
                {
                    tableView.ReLoadTable(0);
                }
                myItem.SetActive(false);
            }
            //if (rankingList != null)
            //{
            //    rankingList.Reset();

            //    for (int i = 0; i < dataList.Count; i++)
            //    {
            //        rankingList.Refresh(i, dataList[i], (data, go) =>
            //        {
            //            RankingItem item = go.AddComponent<RankingItem>();
            //            item.SetView(data);
            //        });
            //    }
            //}


        }


    }
}

