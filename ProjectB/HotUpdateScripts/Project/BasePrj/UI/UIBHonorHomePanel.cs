using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;
using System;
using System.Collections;
using HotUpdateScripts.Project.ACommon;
using System.Collections.Generic;

//荣誉模块（荣誉展厅） 主页面
namespace My.UI.Panel
{
    public class UIBHonorHomePanel : BasePanel
    {
        //View
        private Button btn_Close;
        public static ImgQiehuan imgQiehuan, RankQieHuan, ModelQieHuan;
        private Button[] btns;
        private GameObject[] Contents;
        private bool isFirstOne = true, isFirstTwo = true, isFirstThree = true;
        //预制体父类
        private Transform ContentOne, ContentTwo, ContentThree;
        //预制体
        private GameObject ItemOne, ItemTwo, ItemThree, ItemSelf;
        private Coroutine coroutine_GenerateItemOne, coroutine_GenerateItemTwo, coroutine_GenerateItemThree;
        private Button btn_Bg;
        //已经解锁勋章弹窗
        private GameObject pan_Unlock, pan_lock;
        public static Action<HonorMedal> OpenUnLockWindow, OpenLockWindow;
        //
        private TableView tableView;
        private Button btn_Day, btn_Week, btn_All;

        private GameObject ExplainView;

        void Awake()
        {
            IsHomePanel = true;
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            }
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f, tweenerCallBack);
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            btn_Bg = GameTools.GetByName(transform, "btn_Bg").GetComponent<Button>();
            imgQiehuan = GameTools.GetByName(transform, "QieHuan").GetComponent<ImgQiehuan>();
            RankQieHuan = GameTools.GetByName(transform, "RankQieHuan").GetComponent<ImgQiehuan>();
            ModelQieHuan = GameTools.GetByName(transform, "ModelQieHuan").GetComponent<ImgQiehuan>();
            btns = new Button[3] { GameTools.GetByName(transform, "btn_one").GetComponent<Button>(), GameTools.GetByName(transform, "btn_two").GetComponent<Button>(), GameTools.GetByName(transform, "btn_three").GetComponent<Button>() };
            Contents = new GameObject[3] { GameTools.GetByName(transform, "ScrollView_Record"), GameTools.GetByName(transform, "ScrollView_Model"), GameTools.GetByName(transform, "ScrollView_Rank") };
            ContentOne = GameTools.GetByName(transform, "ContentOne").transform;
            ContentTwo = GameTools.GetByName(transform, "ContentTwo").transform;
            ContentThree = GameTools.GetByName(transform, "ContentThree").transform;

            ItemOne = GameTools.GetByName(transform, "ItemOne");
            ItemTwo = GameTools.GetByName(transform, "ItemTwo");
            ItemThree = GameTools.GetByName(transform, "ItemThree");
            ItemSelf = GameTools.GetByName(transform, "ItemSelf");

            btn_Day = transform.Find("Content_bg/Bg/ScrollView_Rank/Top/DayButton").GetComponent<Button>();
            btn_Week = transform.Find("Content_bg/Bg/ScrollView_Rank/Top/WeekButton").GetComponent<Button>();
            btn_All = transform.Find("Content_bg/Bg/ScrollView_Rank/Top/AllButton").GetComponent<Button>();
            tableView = transform.Find("Content_bg/Bg/ScrollView_Rank").gameObject.AddComponent<TableView>();
            btn_Day.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.one;
                btn_Week.transform.GetChild(0).localScale = Vector3.zero;
                btn_All.transform.GetChild(0).localScale = Vector3.zero;
                NetMgr.Instance.C2S_Honor_GetHonorRank(RankingType.Day);
            });
            btn_Week.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.zero;
                btn_Week.transform.GetChild(0).localScale = Vector3.one;
                btn_All.transform.GetChild(0).localScale = Vector3.zero;
                NetMgr.Instance.C2S_Honor_GetHonorRank(RankingType.Week);
            });
            btn_All.onClick.AddListener(() =>
            {
                btn_Day.transform.GetChild(0).localScale = Vector3.zero;
                btn_Week.transform.GetChild(0).localScale = Vector3.zero;
                btn_All.transform.GetChild(0).localScale = Vector3.one;
                NetMgr.Instance.C2S_Honor_GetHonorRank(RankingType.All);
            });

            ItemOne.SetActive(false);
            ItemTwo.SetActive(false);
            ItemThree.SetActive(false);
            ItemSelf.SetActive(false);
            btn_Close.onClick.AddListener(OnBtnClockA);
            btn_Bg.onClick.AddListener(OnBtnClockA);

            ExplainView = transform.Find("ExplainPop").gameObject;
            GameTools.Instance.AddClickEvent(transform.Find("Content_bg/Bg/ExplainBtn"), () => { ExplainView.SetActive(true); });
            GameTools.Instance.AddClickEvent(transform.Find("ExplainPop/Bg/CloseBtn"), () => { ExplainView.SetActive(false); });
            //点击已解锁勋章打开的页面
            pan_Unlock = GameTools.GetByName(transform, "pan_Unlock");
            pan_Unlock.SetActive(false);
            pan_Unlock.AddComponent<UIBHonorUnLockPage>();
            OpenUnLockWindow = (data) =>
            {
                pan_Unlock.SetActive(true);
                DOTweenMgr.Instance.OpenWindowScale(pan_Unlock.transform.GetChild(1).gameObject, .3f);
                pan_Unlock.GetComponent<UIBHonorUnLockPage>().SetView(data);
            };

            pan_lock = GameTools.GetByName(transform, "pan_lock");
            pan_lock.SetActive(false);
            pan_lock.AddComponent<UIBHonorLockPage>();
            OpenLockWindow = (data) =>
            {
                pan_lock.SetActive(true);
                DOTweenMgr.Instance.OpenWindowScale(pan_lock.transform.GetChild(1).gameObject, .3f);
                pan_lock.GetComponent<UIBHonorLockPage>().SetView(data);
            };

            for (int i = 0; i < btns.Length; i++)
            {
                int index = i;
                btns[index].onClick.AddListener(() =>
                {
                    for (int j = 0; j < Contents.Length; j++)
                    {
                        if (j == index) Contents[j].SetActive(true);
                        else Contents[j].SetActive(false);
                    }
                    for (int k = 0; k < btns.Length; k++)
                    {
                        if (k == index) imgQiehuan.SetImg(1, btns[k].gameObject);
                        else imgQiehuan.SetImg(0, btns[k].gameObject);
                    }

                    if (index == 0)
                    {
                        if (isFirstOne)
                        {
                            isFirstOne = false;
                            NetMgr.Instance.C2S_Honor_GetHonorRecordList(GameData.userId.ToString());
                        }
                    }
                    else if (index == 1)
                    {
                        if (isFirstTwo)
                        {
                            isFirstTwo = false;
                            NetMgr.Instance.C2S_Honor_GetHonormedalInfo(GameData.userId.ToString());
                        }
                    }
                    else
                    {
                        if (isFirstThree)
                        {
                            isFirstThree = false;
                            NetMgr.Instance.C2S_Honor_GetHonorRank(RankingType.Day);//荣誉排行
                            //----------------设置任务完成------------
                            var task = DataMgr.Instance.GetTaskItemByType(331);
                            if (task != null && task.taskStatus == 0)
                            {
                                DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                                DataMgr.Instance.dataBTaskInfoReq.taskId = task.id;
                                DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = task.taskCurProgress + 1;
                                NetMgr.Instance.C2S_Love_TaskUpdateItem();
                            }
                            //---------------------------------------
                        }

                    }
                });
            }
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Honor_GetHonorRecordList, S2C_Honor_GetHonorRecordListCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Honor_GetHonormedalInfo, S2C_Honor_GetHonormedalInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Honor_GetHonorRank, S2C_Honor_GetHonorRankCallBack);
            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_HONOR];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }

        void tweenerCallBack()
        {
            if (GameData.taskInfo != null && GameData.taskInfo.taskType == 331)
            {
                btns[2].onClick.Invoke();

                //if (GameData.taskInfo != null && GameData.taskInfo.taskType == 331)
                //{
                //    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                //    DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
                //    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
                //    NetMgr.Instance.C2S_Love_TaskUpdateItem();
                //}
            }

            else
                btns[0].onClick.Invoke();
        }

        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{
        //    GameData.taskInfo = null;
        //}

        void Start()
        {

        }

        void OnDestroy()
        {
            GameData.taskInfo = null;
            if (coroutine_GenerateItemOne != null) coroutine_GenerateItemOne = null;
            if (coroutine_GenerateItemTwo != null) coroutine_GenerateItemTwo = null;
            DataMgr.Instance.dataBHonorMadelListRes.honorMedalList = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Honor_GetHonorRecordList, S2C_Honor_GetHonorRecordListCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Honor_GetHonormedalInfo, S2C_Honor_GetHonormedalInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Honor_GetHonorRank, S2C_Honor_GetHonorRankCallBack);
            //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }

        void S2C_Honor_GetHonorRecordListCallBack(object o)
        {
            DataBDonateRecordListRes data = DataMgr.Instance.dataBDonateRecordListRes;
            //对列表按时间倒序排序
            data.userDonateRecordList.Sort((a, b) =>
            {
                return -GameTools.TmGetTimeStamp(DateTime.Parse(a.donateTime)).CompareTo(GameTools.TmGetTimeStamp(DateTime.Parse(b.donateTime)));
            });

            if (coroutine_GenerateItemOne != null)
            {
                CoroutineMgr.Instance.Coroutine_Stop(coroutine_GenerateItemOne);
            }
            coroutine_GenerateItemOne = CoroutineMgr.Instance.Coroutine_Start(GenerateItemOne(data));
        }

        IEnumerator GenerateItemOne(DataBDonateRecordListRes data)
        {
            for (int i = 0; i < data.userDonateRecordList.Count; i++)
            {
                GameObject Item = Instantiate(ItemOne, ContentOne);
                Item.SetActive(true);
                Item.AddComponent<UIBHonorRecordItem>().setItemView(data.userDonateRecordList[i]);
            }
            yield return 1;
        }

        //公益排行回调
        void S2C_Honor_GetHonorRankCallBack(object o)
        {
            //if (coroutine_GenerateItemThree != null)
            //{
            //    CoroutineMgr.Instance.Coroutine_Stop(coroutine_GenerateItemThree);
            //}
            //coroutine_GenerateItemThree = CoroutineMgr.Instance.Coroutine_Start(GenerateItemThree(DataMgr.Instance.dataBDonateRankInfoRes.donateLoveRankList, DataMgr.Instance.dataBDonateRankInfoRes.honorMedalList, DataMgr.Instance.dataBDonateRankInfoRes.userHighestMedalList));
            var dataList = DataMgr.Instance.dataBDonateRankInfoRes.donateLoveRankList;
            var honorMedalList = DataMgr.Instance.dataBDonateRankInfoRes.honorMedalList;
            var userMedalRecord = DataMgr.Instance.dataBDonateRankInfoRes.userHighestMedalList;
            if (dataList != null && dataList.Count > 0)
            {
                DonateLoveRankListItem myItem = null;
                for (int i = 0; i < dataList.Count; i++)
                {
                    if (dataList[i].userId == GameData.userId)
                    {
                        myItem = dataList[i];
                        break;
                    }
                }
                if (myItem != null)
                {
                    debug.Log_Blue("自己的排名： " + myItem.rowNum);
                    ItemSelf.SetActive(true);
                    ItemSelf.AddComponent<UIBHonorRankItem>().setRankItem(myItem, honorMedalList, userMedalRecord);
                }
                else
                {
                    ItemSelf.SetActive(false);
                }

                Action<int, GameObject> onItemRender = (i, go) =>
                {
                    go.GetComponent<UIBHonorRankItem>().setRankItem(dataList[i], honorMedalList, userMedalRecord);
                };
                if (tableView != null)
                {
                    if (!tableView.Init)
                    {
                        tableView.onItemCreated = (go) => { go.AddComponent<UIBHonorRankItem>(); };

                        tableView.onItemRender = onItemRender;
                        tableView.InitTable(dataList.Count);
                    }
                    else
                    {
                        tableView.onItemRender = onItemRender;
                        tableView.ReLoadTable(dataList.Count);
                    }
                }
            }
            else
            {
                ItemSelf.SetActive(false);
                if (tableView != null && tableView.Init)
                {
                    tableView.ReLoadTable(0);
                }
            }
        }

        IEnumerator GenerateItemThree(List<DonateLoveRankListItem> data, List<HonorMedal> honorMedalList, List<UserMedalRecord> userMedalRecord)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].userId == GameData.userId)
                {
                    debug.Log_Blue("自己的排名： " + data[i].rowNum);
                    ItemSelf.SetActive(true);
                    ItemSelf.AddComponent<UIBHonorRankItem>().setRankItem(data[i], honorMedalList, userMedalRecord);
                }
                //else
                //{
                GameObject Item = Instantiate(ItemThree, ContentThree);
                Item.SetActive(true);
                Item.AddComponent<UIBHonorRankItem>().setRankItem(data[i], honorMedalList, userMedalRecord);
                //}
            }
            yield return 1;
        }

        /// <summary>
        /// 获取荣誉勋章数据成功
        /// </summary>
        /// <param name="o"></param>
        void S2C_Honor_GetHonormedalInfoCallBack(object o)
        {
            if (coroutine_GenerateItemTwo != null) CoroutineMgr.Instance.Coroutine_Stop(coroutine_GenerateItemTwo);
            coroutine_GenerateItemTwo = CoroutineMgr.Instance.Coroutine_Start(GenerateItemTwo(DataMgr.Instance.dataBHonorMadelListRes.honorMedalList));
        }

        IEnumerator GenerateItemTwo(List<HonorMedal> data)
        {
            yield return 1;
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    GameObject Item = Instantiate(ItemTwo, ContentTwo);
                    Item.SetActive(true);
                    Item.AddComponent<UIBHonorMadelItem>().setMadelView(data[i], (i % 3 + 1), getMadelStatus(data[i].medalId));
                }
            }
        }

        //获取勋章是否获取状态
        bool getMadelStatus(int _medalId)
        {
            bool result = false;
            for (int i = 0; i < DataMgr.Instance.dataBHonorMadelListRes.userMedalRecordListByUserId.Count; i++)
            {
                if (DataMgr.Instance.dataBHonorMadelListRes.userMedalRecordListByUserId[i].medalId == _medalId)
                {
                    result = true;
                }
            }
            return result;
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
                UIMgr.Instance.Close(UIPath.UIBHonorHomePanel);
            });
        }
    }
}
