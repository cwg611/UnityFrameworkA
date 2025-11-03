using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBShopReallyDescPanel : BasePanel
    {
        private Button btn_Close, Btn_Submit;
        private Text title, desc, desc2, Title;
        private RectTransform BgContent;
        private Image Icon;

        public override void InitPanel(object o)
        {
            BgContent = GameTools.GetByName(transform, "BgContent").GetComponent<RectTransform>();
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            Btn_Submit = GameTools.GetByName(transform, "Btn_Submit").GetComponent<Button>();
            title = GameTools.GetByName(transform, "title").GetComponent<Text>();
            desc = GameTools.GetByName(transform, "desc").GetComponent<Text>();
            desc2 = GameTools.GetByName(transform, "desc2").GetComponent<Text>();
            Title = GameTools.GetByName(transform, "Title").GetComponent<Text>();
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();

            btn_Close.onClick.AddListener(OnBtnCloseClick);
            Btn_Submit.onClick.AddListener(OnBtnCloseClick);
            DOTweenMgr.Instance.OpenWindowScale(BgContent.gameObject, .3f);

            NetMgr.Instance.DownLoadImg(r =>
            {
                if (Icon == null) return;
                Icon.sprite = r;
                GameTools.Instance.MatchImgBySprite(Icon, Match_Img.Height);
            }, GameData.curShopData.productPicture);

            Title.text = GameData.curShopData.productName;
            title.text = '"' + GameData.curShopData.productName + '"' + "详情说明";

            string descStr = getProductDesc(GameData.curShopData.productDesc, 1);
            string descStr2 = getProductDesc(GameData.curShopData.productDesc, 2);
            if (string.IsNullOrEmpty(descStr))
            {
                desc.gameObject.SetActive(false);
            }
            else
            {
                desc.text = descStr;
            }
            if (string.IsNullOrEmpty(descStr2))
            {
                desc2.gameObject.SetActive(false);
            }
            else
            {
                desc2.text = descStr2;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(BgContent);
        }

        //商品详情说明只要后面两段,并且加个换行符
        string getProductDesc(string s, int index)
        {
            string desc = "";
            string[] result = s.Split('^');
            if (result.Length > 1)
            {
                desc += result[index];
            }
            else
            {
                desc = s;
            }
            return desc;
        }

        void OnBtnCloseClick()
        {
            DOTweenMgr.Instance.CloseWindowScale(BgContent.gameObject, 0.2f, () =>
            {
                GameData.curShopData = null;
                UIMgr.Instance.Close(UIPath.UIBShopReallyDescPanel);
            });
        }
    }
}
