using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBBigImgPanel : BasePanel
    {
        private RawImage BigImage;
        private Button btn_Close;

        void Awake()
        {
            BigImage = GameTools.GetByName(transform, "BigImage").GetComponent<RawImage>();
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            btn_Close.onClick.AddListener(() =>
            {
                UIMgr.Instance.Close(UIPath.UIBBigImgPanel);
            });
            BigImage.gameObject.SetActive(false);
        }

        void Start()
        {
            setImage();
        }

        public void setImage()
        {
            if (GameData.bigImg == null) return;
            BigImage.texture = GameData.bigImg;
            GameTools.Instance.MatchImgBySprite(BigImage, Match_Img.Width);
            BigImage.gameObject.SetActive(true);
        }

        void OnDestroy()
        {
            GameData.bigImg = null;
        }

    }
}


