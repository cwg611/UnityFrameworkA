using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBMainBagItemDetial : MonoBehaviour
    {
        private Button btn_Know;
        public Text txt_GoodsName, txt_DescA, txt_DescB, txt_DescC;
        public Image img_GoodsIcon;
        public RectTransform pan_rectTrans;

        void Awake()
        {
            pan_rectTrans = transform.Find("pan_Kuang/pan_Item").GetComponent<RectTransform>();
            btn_Know = transform.Find("pan_Kuang/btn_Know").GetComponent<Button>();
            txt_GoodsName = transform.Find("pan_Kuang/pan_Title/txt_GoodsName").GetComponent<Text>();
            img_GoodsIcon = transform.Find("pan_Kuang/pan_Title/img_GoodsIcon").GetComponent<Image>();
            txt_DescA = transform.Find("pan_Kuang/pan_Item/ItemA/txt_DescA").GetComponent<Text>();
            txt_DescB = transform.Find("pan_Kuang/pan_Item/ItemB/txt_DescB").GetComponent<Text>();
            txt_DescC = transform.Find("pan_Kuang/pan_Item/ItemC/txt_DescC").GetComponent<Text>();
            btn_Know.onClick.AddListener(OnBtnKnowClick);
        }

        void OnBtnKnowClick()
        {
            DOTweenMgr.Instance.CloseWindowScale(this.gameObject, 0.2f, () =>
            {
                this.gameObject.SetActive(false);
                this.gameObject.transform.localScale = new Vector3(1, 1, 1);
            });
        }
    }
}