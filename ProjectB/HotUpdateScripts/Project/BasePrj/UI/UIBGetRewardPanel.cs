using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBGetRewardPanel : BasePanel
    {
        private Button btn_Close, Btn_Submit;
        private RectTransform BgContent;
        private Image Icon;
        private Text txt_Name, txt_Num;

        public override void InitPanel(object o)
        {
            BgContent = GameTools.GetByName(transform, "BgContent").GetComponent<RectTransform>();
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            Btn_Submit = GameTools.GetByName(transform, "Btn_Submit").GetComponent<Button>();
            btn_Close.onClick.AddListener(OnBtnCloseClick);
            Btn_Submit.onClick.AddListener(OnBtnCloseClick);
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            txt_Name = GameTools.GetByName(transform, "name").GetComponent<Text>();
            txt_Num = GameTools.GetByName(transform, "num").GetComponent<Text>();
            DOTweenMgr.Instance.OpenWindowScale(BgContent.gameObject, .3f);
            setView();
        }

        void setView()
        {
            //设置图标
            if (GameData.curPrizeData != null)
            {
                GameTools.SetDaoJuUIIcon(GameData.curPrizeData.prizeId, Icon);
            }
            //设置名字及数量
            txt_Name.text = GameData.curPrizeData.prizeDesc.Split('*')[0];
            txt_Num.text = "×" + GameData.curPrizeData.prizeDesc.Split('*')[1];
        }

        void OnBtnCloseClick()
        {
            DOTweenMgr.Instance.CloseWindowScale(BgContent.gameObject, 0.2f, () =>
            {
                GameData.curPrizeData = null;
                UIMgr.Instance.Close(UIPath.UIBGetRewardPanel);
            });
        }
    }
}
