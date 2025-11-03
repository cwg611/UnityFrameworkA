using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using HotUpdateScripts.Project.BasePrj.Data;
using My.Msg;


namespace My.UI.Panel
{
    public class UIBFriendEditorNotePanel : BasePanel
    {
        private GameObject MyPage;
        private Button btn_Bg;
        private Button Btn_Cancel;
        private Button Btn_Submit;
        private InputField InputName;

        private string str;

        void Awake()
        {
            MyPage = GameTools.GetByName(transform, "MyPage");
            DOTweenMgr.Instance.OpenWindowScale(MyPage, .3f);
            btn_Bg = GameTools.GetByName(transform, "btn_Bg").GetComponent<Button>();
            btn_Bg.onClick.AddListener(() =>
            {
                ClosePage();
            });

            Btn_Cancel = GameTools.GetByName(transform, "Btn_Cancel").GetComponent<Button>();
            Btn_Cancel.onClick.AddListener(() =>
            {
                ClosePage();
            });
            Btn_Submit = GameTools.GetByName(transform, "Btn_Submit").GetComponent<Button>();
            Btn_Submit.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(str)) str = "";
                //if (!string.IsNullOrEmpty(str))
                //{
                DataMgr.Instance.updateRemarkReq.userId = GameData.userId;
                DataMgr.Instance.updateRemarkReq.friendId = GameData.curFriendUserId;
                DataMgr.Instance.updateRemarkReq.remark = str;
                debug.Log_Blue("传的备注： " + str);
                NetMgr.Instance.C2S_Friend_UpdateRemark();
                //}
            });

            InputName = GameTools.GetByName(transform, "InputName").GetComponent<InputField>();
            InputName.onEndEdit.AddListener(s =>
            {
                if (!string.IsNullOrEmpty(s))
                {
                    str = s;
                }
                else
                {
                    str = "";
                }
            });

            if (!string.IsNullOrEmpty(GameData.curFriendRemark))
            {
                InputName.text = GameData.curFriendRemark;
            }

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_UpdateRemark, S2C_Friend_UpdateRemarkCallBack);
        }

        void ClosePage()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_UpdateRemark, S2C_Friend_UpdateRemarkCallBack);
            DOTweenMgr.Instance.CloseWindowScale(MyPage.gameObject, 0.2f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBFriendEditorNotePanel);
            });
        }

        void S2C_Friend_UpdateRemarkCallBack(object o)
        {
            if (DataMgr.Instance.updateRemarkRes.msg)
            {
                UIBFriendChatPanel.UpdateRemark.Invoke(str);
                ClosePage();
            }
        }
    }
}
