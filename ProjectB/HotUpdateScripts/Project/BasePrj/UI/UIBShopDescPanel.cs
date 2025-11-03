using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBShopDescPanel : BasePanel
    {
        private Button btn_Close, Btn_Submit;
        private Text title, desc, desc2, Title;
        private RectTransform BgContent;
        private GameObject Icon;

        public override void InitPanel(object o)
        {
            BgContent = GameTools.GetByName(transform, "BgContent").GetComponent<RectTransform>();
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            Btn_Submit = GameTools.GetByName(transform, "Btn_Submit").GetComponent<Button>();
            title = GameTools.GetByName(transform, "title").GetComponent<Text>();
            desc = GameTools.GetByName(transform, "desc").GetComponent<Text>();
            desc2 = GameTools.GetByName(transform, "desc2").GetComponent<Text>();
            Title = GameTools.GetByName(transform, "Title").GetComponent<Text>();
            Icon = GameTools.GetByName(transform, "Icon");
            btn_Close.onClick.AddListener(OnBtnCloseClick);
            Btn_Submit.onClick.AddListener(OnBtnCloseClick);
            DOTweenMgr.Instance.OpenWindowScale(BgContent.gameObject, .3f);
            GameTools.SetDaoJuUIIcon(GameData.curShopData.productId, Icon.GetComponent<Image>());
            Title.text = GameData.curShopData.productName;
            title.text = '"' + GameData.curShopData.productName + '"' + "详情说明";
            desc.text = getProductDesc(GameData.curShopData.productDesc, 1);
            desc2.text = getProductDesc(GameData.curShopData.productDesc, 2);
            LayoutRebuilder.ForceRebuildLayoutImmediate(BgContent);
        }

        //商品详情说明只要后面两段,并且加个换行符
        string getProductDesc(string s, int index)
        {
            string desc = "";
            string[] result = s.Split('^');
            desc = result[index];
            return desc;
        }

        void OnBtnCloseClick()
        {
            DOTweenMgr.Instance.CloseWindowScale(BgContent.gameObject, 0.2f, () =>
            {
                GameData.curShopData = null;
                UIMgr.Instance.Close(UIPath.UIBShopDescPanel);
            });
        }
    }
}
