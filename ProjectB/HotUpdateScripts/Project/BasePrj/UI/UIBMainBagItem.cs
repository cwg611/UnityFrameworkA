using HotUpdateScripts.Project.Common;
using JEngine.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBMainBagItem : MonoBehaviour
    {
        private Button btn_GoodsIcon;
        public int goodsId;
        public string goodsDesc;
        public Text txt_GoodsName;
        public Text txt_GoodsNum;
        public Image img_GoodsIcon;
        public GameObject obj_GoodsDetial;//详情
        UIBMainBagItemDetial detial;

        void Awake()
        {
            btn_GoodsIcon = transform.GetComponent<Button>();
            img_GoodsIcon = transform.Find("img_GoodsIcon").GetComponent<Image>();
            txt_GoodsName = transform.Find("txt_GoodsName").GetComponent<Text>();
            txt_GoodsNum = transform.Find("txt_GoodsNum").GetComponent<Text>();

            btn_GoodsIcon.onClick.AddListener(OnBtnGoodsIconClick);
        }


        void OnBtnGoodsIconClick()
        {
            //debug.Log(goodsId+"  "+ goodsDesc);

            if (!obj_GoodsDetial.activeSelf)
            {
                obj_GoodsDetial.SetActive(true);
            
                DOTweenMgr.Instance.OpenWindowScale(obj_GoodsDetial.transform.GetChild(0).gameObject, .3f);

            }
            if (!obj_GoodsDetial.GetComponent<UIBMainBagItemDetial>())
                obj_GoodsDetial.AddComponent<UIBMainBagItemDetial>();

            detial = obj_GoodsDetial.GetComponent<UIBMainBagItemDetial>();
            detial.txt_GoodsName.text = txt_GoodsName.text;
            detial.txt_DescA.text = goodsDesc.Split('^')[0];
            detial.txt_DescB.text = goodsDesc.Split('^')[1];
            detial.txt_DescC.text = goodsDesc.Split('^')[2];

            string djSpName = "";
            if (goodsId == 101)
            {
                djSpName = "nengliang";
            }
            else if (goodsId == 102)
            {
                djSpName = "jiasu";
            }
            else if (goodsId == 103)
            {
                djSpName = "jiaoyou";
            }
            else if (goodsId == 104)
            {
                djSpName = "aixin";
            }
            JResource.LoadResAsync<Sprite>("Common/DaoJu/" + djSpName + ".png",
                (sp) =>
                {
                    detial.img_GoodsIcon.sprite = sp; //赋值道具icon
                    detial.img_GoodsIcon.SetNativeSize();
                },
                JResource.MatchMode.UI);

           
            //刷新自动排版
            GameTools.Instance.UIRebuildLayoutImdSig(detial.txt_DescA.rectTransform);
            GameTools.Instance.UIRebuildLayoutImdSig(detial.txt_DescB.rectTransform);
            GameTools.Instance.UIRebuildLayoutImdSig(detial.txt_DescC.rectTransform);

            GameTools.Instance.UIRebuildLayoutImdSig(detial.txt_DescA.transform.parent.GetComponent<RectTransform>());
            GameTools.Instance.UIRebuildLayoutImdSig(detial.txt_DescB.transform.parent.GetComponent<RectTransform>());
            GameTools.Instance.UIRebuildLayoutImdSig(detial.txt_DescC.transform.parent.GetComponent<RectTransform>());
        }


        void OnDestory()
        {
            btn_GoodsIcon.onClick.RemoveListener(OnBtnGoodsIconClick);
        }

    }
}