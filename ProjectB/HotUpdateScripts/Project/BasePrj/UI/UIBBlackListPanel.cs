using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using HotUpdateScripts.Project.BasePrj.Data;
using My.Msg;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace My.UI.Panel
{
    /// <summary>
    /// 黑名单列表
    /// </summary>
    public class UIBBlackListPanel : BasePanel
    {
        private Button btn_Close;
        //private GameObject ItemPrefab;
        private Transform ContentOne;
        private ItemList itemList;
        private bool isFirst;//是否第一次打开页面
        private GameObject Window, btn_Submit, btn_Cancel; //取消关注弹框,继续关注按钮,取消关注
        public static Action<bool> Act_OpenWindow;
        List<UserFriend> dataList;

        public override void InitPanel(object o)
        {
            isFirst = true;
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            ContentOne = GameTools.GetByName(transform, "ContentOne").transform;
            if (itemList == null) itemList = ContentOne.gameObject.AddComponent<ItemList>();
            //ItemPrefab = GameTools.GetByName(transform, "Prefab");
            //ItemPrefab.SetActive(false);
            //二次弹窗
            Window = GameTools.GetByName(transform, "Window");
            Window.SetActive(false);
            btn_Submit = GameTools.GetByName(transform, "btn_Submit");
            btn_Cancel = GameTools.GetByName(transform, "btn_Cancel");
            GameTools.Instance.AddClickEvent(btn_Submit, () =>
            {
                OpenWindow(false);
            });
            GameTools.Instance.AddClickEvent(btn_Cancel, () =>
            {
                //取消关注
                NetMgr.Instance.C2S_Friend_UnFollowToUser();
            });
            Act_OpenWindow = OpenWindow;

            btn_Close.onClick.AddListener(() =>
            {
                UIMgr.Instance.Close(this.name);
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_BlackListUserInfoList, S2C_Friend_BlackListCallBack);
            NetMgr.Instance.C2S_GetBlackList();
        }

        private void OpenWindow(bool state)
        {
            Window.SetActive(state);
        }

        void S2C_Friend_BlackListCallBack(object o)
        {
            var list = o as DataUserBlackList;
            if (list == null) return;
            dataList = list.blackList;
            if (dataList == null) return;

            itemList.Reset();
            for (int i = 0; i < dataList.Count; i++)
            {
                itemList.Refresh(i, dataList[i], (data, go) =>
                {

                    go.AddComponent<UIBBlackListItem>().InitItem(dataList[i], true);
                });
            }
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_BlackListUserInfoList, S2C_Friend_BlackListCallBack);
        }
    }
}
