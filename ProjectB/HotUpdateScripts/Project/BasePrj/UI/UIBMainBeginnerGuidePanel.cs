using HotUpdateScripts.Project.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBMainBeginnerGuidePanel : BasePanel
    {

        private Button btnBack;

        void Awake()
        {
            btnBack = GameTools.GetByName(transform, "btn_Back").GetComponent<Button>();

            btnBack.onClick.AddListener(() =>
            {
                UIMgr.Instance.Close(UIPath.UIBMainBeginnerGuidePanel);
            });
        }

    }
}