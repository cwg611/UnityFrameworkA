using System;
using System.Collections.Generic;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class ShowPop : MonoBehaviour
    {
        private Text txt_Title, txt_Message;
        Button btn_OK, btn_Cancel;
        Text txt_Ensure, txt_Cancel;
        Action okAction;
        Action cancelAction;
        private GameObject IconBg;
        private GameObject icon;
        private ImgQiehuan imgQiehuan;


        private bool open;

        public void InitView()
        {
            txt_Title = GameTools.GetByName(transform, "TitleText").GetComponent<Text>();
            txt_Ensure = GameTools.GetByName(transform, "EnsureText").GetComponent<Text>();
            txt_Cancel = GameTools.GetByName(transform, "CancelText").GetComponent<Text>();
            txt_Message = GameTools.GetByName(transform, "MessageText").GetComponent<Text>();
            btn_OK = GameTools.GetByName(transform, "OKBtn").GetComponent<Button>();
            btn_Cancel = GameTools.GetByName(transform, "CancelBtn").GetComponent<Button>();
            IconBg = GameTools.GetByName(transform, "IconBg");
            icon = GameTools.GetByName(transform, "Icon");
            btn_OK.onClick.AddListener(OnOkClick);
            btn_Cancel.onClick.AddListener(OnCancleClick);
            gameObject.SetActive(false);
            imgQiehuan = GetComponent<ImgQiehuan>();
        }

        public void ShowMessage(string message,
          string title = "",
          string ok = "确认",
          string cancle = "取消",
          Action okAction = null,
          Action cancleAction = null,
          bool open = false,
          string messageColor = "", PopWinType popWinType = PopWinType.WithoutIcon)
        {
            this.open = open;
            txt_Message.text = message;
            this.okAction = okAction;
            this.cancelAction = cancleAction;
            txt_Ensure.text = ok;
            txt_Cancel.text = cancle;

            IconBg.SetActive(popWinType != PopWinType.WithoutIcon);
            if (imgQiehuan != null)
            {
                imgQiehuan.SetImg((int)popWinType, icon, true);
            }
            gameObject.SetActive(true);

            GameData.isOpenTipPanel = true;
        }


        private void OnOkClick()
        {
            gameObject.SetActive(open);
            this.okAction?.Invoke();
            GameData.isOpenTipPanel = open;
        }

        private void OnCancleClick()
        {
            gameObject.SetActive(false);
            this.cancelAction?.Invoke();
            GameData.isOpenTipPanel = false;
        }
    }
}
