using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBShopItem : MonoBehaviour
    {
        private Image img_GoodIcon;
        private Text txt_Title, txt_Price, txt_ActivityPrice, txt_ProductNum, txt_ExchangeNum;
        private Button btn_Exchange;
        private bool isFirst = true;
        private ConversionShop m_Data;

        private Transform ActivityBg;

        void setObj()
        {
            img_GoodIcon = GameTools.GetByName(transform, "GoodIcon").GetComponent<Image>();
            txt_Title = GameTools.GetByName(transform, "TitleText").GetComponent<Text>();
            txt_Price = GameTools.GetByName(transform, "PriceText").GetComponent<Text>();
            txt_ActivityPrice = GameTools.GetByName(transform, "ActivityPriceText").GetComponent<Text>();
            txt_ProductNum = transform.Find("Content/ProductNum").GetComponent<Text>();
            txt_ExchangeNum = transform.Find("Content/ExchangeNum").GetComponent<Text>();
            ActivityBg = transform.Find("Content/ActivityBg");
            btn_Exchange = GameTools.GetByName(transform, "ExchangeBtn").GetComponent<Button>();
            btn_Exchange.onClick.AddListener(() =>
            {
                GameData.curShop = m_Data;
                UIBShopPanel.Act_OpenWindow();

            });
            img_GoodIcon.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameData.curShopData = m_Data;
                if (m_Data.isVirtual == 0)
                {
                    UIMgr.Instance.Open(UIPath.UIBShopDescPanel);
                }
                else
                {
                    //实物详情
                    UIMgr.Instance.Open(UIPath.UIBShopReallyDescPanel);
                }
            });
        }

        public void InitItem(ConversionShop data)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            m_Data = data;

            //0：虚拟商品  1：实物商品
            if (m_Data.isVirtual == 0)
            {
                UIBShopPanel.imgQiehuan.SetImg(data.productType - 1, img_GoodIcon.gameObject);
            }
            else
            {
                NetMgr.Instance.DownLoadImg(r =>
                {
                    if (img_GoodIcon == null) return;
                    img_GoodIcon.sprite = r;
                    //GameTools.Instance.MatchImgBySprite(img_GoodIcon, Match_Img.Height);
                }, data.productPicture);

            }
            ActivityBg.localScale = data.isActivityProduct == 1 ? Vector3.one : Vector3.zero;
            txt_Title.text = data.productName;
            txt_Price.text = data.needLoveMoney.ToString();
            txt_ActivityPrice.text = data.activityNeedLoveMoney.ToString();
            txt_ProductNum.text = data.productNum.ToString();
            txt_ExchangeNum.text = data.exchangedNum.ToString();
        }

        void OnDestroy()
        {

        }


    }
}
