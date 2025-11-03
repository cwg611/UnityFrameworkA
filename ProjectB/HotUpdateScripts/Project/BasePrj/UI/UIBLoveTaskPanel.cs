using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using My.Msg;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBLoveTaskPanel : BasePanel
    {
        private Button btn_CloseA, btn_Bg;
        private Transform sv_TaskContent;
        public static ImgQiehuan QieHuan, taskQiehuan, RewardQiehuan;  //按钮切换  任务类型图标切换
        private Dictionary<int, UIBLoveTaskItem> list_UIBLoveTaskItem; //存储生成的任务列表预制体
        private bool isFirstOpen = true; //是否第一次打开界面
        DataBLoveTask task;
        private List<DataBMainTaskItem> DataBMainTaskList = new List<DataBMainTaskItem>();

        public override void InitPanel(object o)
        {
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f);
            btn_CloseA = GameTools.GetByName(transform, "btn_CloseA").GetComponent<Button>();
            sv_TaskContent = GameTools.GetByName(transform, "sv_TaskContent").transform;
            QieHuan = GameTools.GetByName(transform, "QieHuan").GetComponent<ImgQiehuan>();
            taskQiehuan = GameTools.GetByName(transform, "taskQiehuan").GetComponent<ImgQiehuan>();
            RewardQiehuan = GameTools.GetByName(transform, "RewardQiehuan").GetComponent<ImgQiehuan>();
            btn_Bg = GameTools.GetByName(transform, "btn_Bg").GetComponent<Button>();

            btn_CloseA.onClick.AddListener(OnBtn_CloseClick);
            btn_Bg.onClick.AddListener(OnBtn_CloseClick);

            list_UIBLoveTaskItem = new Dictionary<int, UIBLoveTaskItem>();

            //请求数据 任务列表
            NetMgr.Instance.C2S_Love_TaskGetList(GameData.userId.ToString());
            //监听消息刷新显示
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskGetList, S2C_Main_TaskGetListCallBack);
        }

        void S2C_Main_TaskGetListCallBack(object o)
        {
            task = DataMgr.Instance.dataBLoveTask;
            if (task == null) return;
            if (!DataMgr.Instance.dataBMainHome.AllowMakingFriends)
            {
                for (int i = 0; i < task.taskList.Count; i++)
                {
                    if (task.taskList[i].taskType == 341)//寻找一位有缘人
                    {
                        task.taskList.Remove(task.taskList[i]);
                        break;
                    }
                }
            }
            //设置签到显示
            debug.Log_Blue("task.taskList.Count---"+task.taskList.Count);
            if (isFirstOpen)
            {
                for (int i = 0; i < task.taskList.Count; i++)
                {
                    //签到任务
                    if (task.taskList[i].taskType == 101)
                    {
                        GameObject g = GameTools.GetByName(transform, "pan_Task_Sign");
                        g.AddComponent<UIBLoveTaskSignIn>();
                        UIBLoveTaskSignIn sc = g.GetComponent<UIBLoveTaskSignIn>();
                        sc.taskStatus = task.taskList[i].taskStatus;
                        sc.signInDayNum = task.signInDayNum;
                        sc.rewardList = task.signInRewardList;
                        sc.taskId = task.taskList[i].id;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < task.taskList.Count; i++)
                {
                    //签到任务
                    if (task.taskList[i].taskType == 101)
                    {
                        UIBLoveTaskSignIn sc = GameTools.GetByName(transform, "pan_Task_Sign").GetComponent<UIBLoveTaskSignIn>();

                        sc.taskStatus = task.taskList[i].taskStatus;
                        sc.signInDayNum = task.signInDayNum;
                        sc.rewardList = task.signInRewardList;
                        sc.taskId = task.taskList[i].id;
                        sc.InitSignView();
                        break;
                    }
                }
            }
            //筛选数据
            DataBMainTaskList.Clear();
            DataBMainTaskList.AddRange(task.taskList.Where(t => t.taskType != 101).ToList());
            //排序
            SortByStatusAndTaskId(DataBMainTaskList);
            debug.Log_Blue("DataBMainTaskList--"+ DataBMainTaskList.Count);
            //第一次实例化界面
            if (isFirstOpen)
            {
                isFirstOpen = false;
                new JPrefab("UI/UIBLoveTaskItem", (result, prefab) =>
                {
                    //其他任务
                    for (int i = 0; i < DataBMainTaskList.Count; i++)
                    {
                        GameObject g = Instantiate(prefab.Instance, sv_TaskContent);
                        g.AddComponent<UIBLoveTaskItem>();

                        UIBLoveTaskItem sc = g.GetComponent<UIBLoveTaskItem>();
                        list_UIBLoveTaskItem.Add(i, sc);
                        sc.curItem = DataBMainTaskList[i];
                    }
                });
            }
            //刷新界面
            else
            {
                for (int i = 0; i < DataBMainTaskList.Count; i++)
                {
                    list_UIBLoveTaskItem[i].curItem = DataBMainTaskList[i];
                    list_UIBLoveTaskItem[i].InitItem();
                }
            }
        }

        /// <summary>
        /// 根据任务完成状态及Id排序
        /// </summary>
        /// <param name="TaskInfoList"></param>
        /// <returns></returns>
        private List<DataBMainTaskItem> SortByStatusAndTaskId(List<DataBMainTaskItem> TaskInfoList)
        {
            List<DataBMainTaskItem> _TaskInfoList = TaskInfoList;
            _TaskInfoList.Sort((a, b) =>
            {
                int result = 0;
                int _statusOne = a.taskStatus;
                int _statusTwo = b.taskStatus;

                if (_statusOne == 1 && _statusTwo != 1)
                {
                    result = -1;
                }
                else if (_statusOne != 1 && _statusTwo == 1)
                {
                    result = 1;
                }
                else if (_statusOne != 1 && _statusTwo != 1)
                {
                    if (_statusOne.CompareTo(_statusTwo) != 0)
                    {
                        if (_statusOne - _statusTwo < 0)
                            result = -1;
                        else
                            result = 1;
                    }
                    else
                    {
                        _sortById(a, b);
                    }
                }
                else
                {
                    _sortById(a, b);
                }
                return result;
            });
            return _TaskInfoList;
        }
        private int _sortById(DataBMainTaskItem a, DataBMainTaskItem b)
        {
            int result = 0;
            if (a.taskId.CompareTo(b.taskId) != 0)
            {
                if (a.taskId - b.taskId < 0)
                    result = -1;
                else
                    result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        //关闭按钮
        void OnBtn_CloseClick()
        {
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                task = null;
                UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
            });
        }

        public override void ReleasePanel()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskGetList, S2C_Main_TaskGetListCallBack);
            task = null;
        }
    }
}
