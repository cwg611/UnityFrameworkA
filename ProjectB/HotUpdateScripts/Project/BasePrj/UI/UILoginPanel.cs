using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UILoginPanel : BasePanel
    {
        public InputField InputField;

        public override void InitPanel(object o)
        {
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f);
            InputField = GameTools.GetByName(transform, "InputField").GetComponent<InputField>();
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Button"), OnClick_btn_login);
            InputField.onEndEdit.AddListener((string str) =>
            {
                if (str != "")
                {
                    GameData.userId = int.Parse(str);
                }
            });
        }

        private void OnClick_btn_login()
        {
            if (String.IsNullOrEmpty(InputField.text))
            {
                GameTools.SetTip("请先输入对应的用户Id");
                return;
            }
            //if (InputField.text == "10" || InputField.text == "11" || InputField.text == "12" || InputField.text == "13" || InputField.text == "14" || InputField.text == "15")
            //{
                DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .1f, () =>
                {
                    UIMgr.Instance.Close(UIPath.UILoginPanel, true);
                    UIMgr.Instance.Open(UIPath.UIBMainHomePanel, null, UILayer.Layer3);

                });
            //}
            //else
            //{
            //    CommonTipPanel.SetTip("请先输入对应的用户Id");
            //}
        }
    }
}
