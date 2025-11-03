using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{

    public class UIBDonateDetailDonatePanel : MonoBehaviour
    {
        private Text txt_Title, DonateNum, txt_Num;
        private GameObject btn_minus, btn_add, btn_donateQuick, btn_Close;
        private ImgQiehuan btn_QH;
        private int addNum = 1;
        private bool isFirst = true;

        void setObj()
        {
            txt_Title = GameTools.GetByName(transform, "txt_Title").GetComponent<Text>();
            DonateNum = GameTools.GetByName(transform, "DonateNum").GetComponent<Text>();
            txt_Num = GameTools.GetByName(transform, "txt_Num").GetComponent<Text>();
            btn_QH = GameTools.GetByName(transform, "btn_QH").GetComponent<ImgQiehuan>();

            btn_minus = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_minus"), () =>
            {
                if (addNum <= 1) return;
                addNum--;
                setNum();
            });
            btn_add = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_add"), () =>
            {
                if (addNum >= (GameData.curLoveNum / 5)) return;
                addNum++;
                setNum();
            });
            btn_donateQuick = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_donateQuick"), () =>
            {
                DataMgr.Instance.dataBDonateReq.userId = GameData.userId;
                DataMgr.Instance.dataBDonateReq.projectId = GameData.cur_ProjectId;
                DataMgr.Instance.dataBDonateReq.donateNum = addNum * 5;
                NetMgr.Instance.C2S_Love_Donate();
                UIBDonateDetailPanel.act_RefreshDonateNum(addNum * 5);
                //if (GameData.taskInfo != null && GameData.taskInfo.taskType == 321)
                //{
                //    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                //    DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
                //    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
                //    NetMgr.Instance.C2S_Love_TaskUpdateItem();
                //}
                //----------------设置任务完成------------
                var task = DataMgr.Instance.GetTaskItemByType(321);
                if (task != null && task.taskStatus == 0)
                {
                    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                    DataMgr.Instance.dataBTaskInfoReq.taskId = task.id;
                    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = task.taskCurProgress + 1;
                    NetMgr.Instance.C2S_Love_TaskUpdateItem();
                }
                //---------------------------------------

                DOTweenMgr.Instance.MovePos(UIBDonateDetailPanel.pan_Donate, new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .1f, () =>
                {
                    UIBDonateDetailPanel.pan_Donate.SetActive(false);
                    GameData.taskInfo = null;
                    //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
                });

            });
            btn_Close = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Close"), () =>
            {
                //Todo:
                addNum = 1;
                transform.gameObject.SetActive(false);
            });
            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }

        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{
        //    GameData.taskInfo = null;
        //}

        void setNum()
        {
            DonateNum.text = addNum * 5 + "颗";
            if (addNum <= 1)
            {
                btn_QH.SetImg(3, btn_minus);
            }
            else
            {
                btn_QH.SetImg(2, btn_minus);
            }
            if (addNum >= (GameData.curLoveNum / 5))
            {
                btn_QH.SetImg(1, btn_add);
            }
            else
            {
                btn_QH.SetImg(0, btn_add);
            }
        }

        public void InitData(string projectTitle)
        {
            if (isFirst)
            {
                isFirst = false;
                setObj();
            }
            addNum = 1;
            debug.Log_yellow("当前共有 " + GameData.curLoveNum);
            txt_Title.text = projectTitle;
            txt_Num.text = "最少捐赠5颗，你可以捐赠" + (GameData.curLoveNum / 5) * 5 + "<size=38>颗</size>";
            DonateNum.text = addNum * 5 + "颗";
            btn_QH.SetImg(3, btn_minus);
            if (GameData.curLoveNum / 5 < 2)
            {
                btn_QH.SetImg(1, btn_add);
            }
            else
            {
                btn_QH.SetImg(0, btn_add);
            }
        }
    }
}
