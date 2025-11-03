using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;


namespace My.UI.Panel
{
    public class UIBHonorUnLockPage : MonoBehaviour
    {
        private bool isFirst = true;
        private Image Icon;
        private Text txt_One, txt_Two, GainNum;
        private RectTransform BgContent;
        private Button BtnPanSubmit, btn_PanClose;

        void SetObj()
        {
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            txt_One = GameTools.GetByName(transform, "txt_One").GetComponent<Text>();
            txt_Two = GameTools.GetByName(transform, "txt_Two").GetComponent<Text>();
            GainNum = GameTools.GetByName(transform, "GainNum").GetComponent<Text>();
            BgContent = GainNum.transform.parent.GetComponent<RectTransform>();
            BtnPanSubmit = GameTools.GetByName(transform, "BtnPanSubmit").GetComponent<Button>();
            btn_PanClose = GameTools.GetByName(transform, "btn_PanClose").GetComponent<Button>();
            BtnPanSubmit.onClick.AddListener(() =>
            {
                ClosePage();
            });
            btn_PanClose.onClick.AddListener(() =>
            {
                ClosePage();
            });
        }

        public void SetView(HonorMedal data)
        {
            if (isFirst)
            {
                SetObj();
                isFirst = false;
            }
            //图标
            UIBHonorHomePanel.ModelQieHuan.SetImg(data.medalId - 1, Icon.gameObject);
            txt_One.text = "恭喜,获得" + data.medalTitle + "称号！";
            txt_Two.text = "在聚爱星球—希望灯塔累计捐献" + data.unlockNum + "颗爱心,可以获得" + '“' + data.medalTitle + '”' + "称号";

            var unlockData = DataMgr.Instance.dataBHonorMadelListRes.userMedalRecordListByUserId;
            for (int i = 0; i < unlockData.Count; i++)
            {
                if (unlockData[i].medalId == data.medalId)
                {
                    GainNum.text = "聚爱星球第" + unlockData[i].gainNum + "位";
                    break;
                }
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(BgContent);
        }

        void ClosePage()
        {
            DOTweenMgr.Instance.CloseWindowScale(this.transform.GetChild(1).gameObject, 0.2f, () =>
            {
                this.gameObject.SetActive(false);
            });
        }
    }
}
