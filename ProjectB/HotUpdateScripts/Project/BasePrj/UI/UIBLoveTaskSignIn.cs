using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBLoveTaskSignIn : MonoBehaviour
    {
        private Transform pan_Task_Sign_Left, pan_Task_Sign_Right;
        private Button btn_Task_Sign_Receive;
        private Text txt_Task_Sign_Num;
        private GameObject pan_UnShowRec, pan_ShowRec;
        //数据
        public int taskStatus, signInDayNum;//签到状态 0 or 2 , 连签天数
        public List<DataBMainTaskSignInRewardItem> rewardList;//奖励列表
        public int taskId;

        public void Awake()
        {
            pan_Task_Sign_Left = transform.Find("pan_Task_Sign_Left").transform;
            pan_Task_Sign_Right = transform.Find("pan_Task_Sign_Right").transform;
            btn_Task_Sign_Receive = transform.Find("pan_Task_Sign_Right/pan_ShowRec/btn_Task_Sign_Receive").GetComponent<Button>();
            txt_Task_Sign_Num = transform.Find("pan_Task_Sign_Right/pan_ShowRec/pan_Task_Sign_Receive/txt_Task_Sign_Num").GetComponent<Text>();
            pan_UnShowRec = GameTools.GetByName(transform, "pan_UnShowRec");
            pan_ShowRec = GameTools.GetByName(transform, "pan_ShowRec");
            GameTools.Instance.AddClickEvent(btn_Task_Sign_Receive.gameObject, OnBtn_Task_Sign_ReceiveClick);
        }

        void Start()
        {
            InitSignView();
            //监听消息刷新显示
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateSignIn, S2C_Love_TaskUpdateSignInCallBack);
        }

        public void InitSignView()
        {
            //签到按钮
            if (taskStatus == 0)
            {
                pan_UnShowRec.SetActive(false);
                pan_ShowRec.SetActive(true);
            }
            else
            {
                pan_UnShowRec.SetActive(true);
                pan_ShowRec.SetActive(false);
            }
            int dayNum = signInDayNum;
            //签到列表
            for (int i = 0; i < pan_Task_Sign_Left.childCount; i++)
            {
                if (i > 0)
                {
                    Text txt_Num = pan_Task_Sign_Left.GetChild(i).GetChild(2).GetComponent<Text>();
                    Text txt_Day = pan_Task_Sign_Left.GetChild(i).GetChild(3).GetComponent<Text>();

                    if (i == 1)
                    {
                        txt_Day.text = "<Color=#FE817A>今 天</Color>";
                        if (taskStatus == 0)
                        {
                            txt_Num.text = getRewardNum(signInDayNum + 1, rewardList);
                            txt_Task_Sign_Num.text = getRewardNum(signInDayNum + 1, rewardList);
                        }
                        else
                        {
                            txt_Num.text = getRewardNum(signInDayNum, rewardList);
                        }
                    }
                    else
                    {
                        if (taskStatus == 0)
                        {
                            txt_Day.text = "第 " + (dayNum + (i - 1) + 1).ToString() + " 天";
                            txt_Num.text = getRewardNum(dayNum + (i - 1) + 1, rewardList);
                        }
                        else
                        {
                            txt_Day.text = "第 " + (dayNum + (i - 1)).ToString() + " 天";
                            txt_Num.text = getRewardNum(dayNum + (i - 1), rewardList);
                        }
                    }
                }
            }
        }

        //获取对应天数的奖励数量
        private string getRewardNum(int dayNum, List<DataBMainTaskSignInRewardItem> continuousSignInRewardList)
        {
            // 通过每天第几天进行取值，如果找不到，则按List中的最大天数的值
            continuousSignInRewardList.Sort((a, b) =>
            {
                return a.signInDayNum.CompareTo(b.signInDayNum);
            });
            int rewardNum = continuousSignInRewardList[continuousSignInRewardList.Count - 1].signInReward;

            for (int i = 0; i < continuousSignInRewardList.Count; i++)
            {
                if (dayNum == continuousSignInRewardList[i].signInDayNum)
                {
                    rewardNum = continuousSignInRewardList[i].signInReward;
                }
            }
            return /*"×"*/"X" + rewardNum;
        }

        public void OnBtn_Task_Sign_ReceiveClick()
        {
            pan_UnShowRec.SetActive(true);
            pan_ShowRec.SetActive(false);

            DataMgr.Instance.dataBSignInItemReq.userId = GameData.userId;
            DataMgr.Instance.dataBSignInItemReq.taskId = taskId;
            DataMgr.Instance.dataBSignInItemReq.continuousSignInDayNum = signInDayNum + 1;
            NetMgr.Instance.C2S_Love_TaskUpdateSignIn();
        }

        public void S2C_Love_TaskUpdateSignInCallBack(object o)
        {
            //NetMgr.Instance.C2S_Love_TaskGetList(GameData.userId.ToString());
        }

        private void OnDestroy()
        {
            btn_Task_Sign_Receive.onClick.RemoveListener(OnBtn_Task_Sign_ReceiveClick);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateSignIn, S2C_Love_TaskUpdateSignInCallBack);
        }
    }
}