using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBDonateHomeItem : MonoBehaviour
    {
        //View
        private Text m_curNum, m_targetNum, m_Title;  //今日已捐助,今日目标
        private Image SliderValue, Item;
        private Sprite sp;
        PublicBenefitProject projectData;

        void Awake()
        {
            m_Title = GameTools.GetByName(transform, "m_Title").GetComponent<Text>();
            SliderValue = GameTools.GetByName(transform, "SliderValue").GetComponent<Image>();
            m_curNum = GameTools.GetByName(transform, "m_curNum").GetComponent<Text>();
            m_targetNum = GameTools.GetByName(transform, "m_targetNum").GetComponent<Text>();
            transform.GetComponent<Button>().onClick.AddListener(OnBtn_DonateClick);
            Item = transform.GetComponent<Image>();
            gameObject.SetActive(false);
        }

        public void setUIView(PublicBenefitProject data)
        {
            projectData = data;
            m_Title.text = projectData.projectTitle.ToString();
            SliderValue.fillAmount = (float)projectData.todayDonateNum / projectData.todayDonateTarget; 
            m_curNum.text = projectData.todayDonateNum + "    颗";
            m_targetNum.text = projectData.todayDonateTarget + "    颗";
            NetMgr.Instance.DownLoadImg(r =>
            {
                if (Item == null) return;
                Item.sprite = r;
                sp = r;
                //GameTools.Instance.MatchImgBySprite(Item);
                gameObject.SetActive(true);
            }, data.projectCover);
        }

        public void OnBtn_DonateClick()
        {
            debug.Log_Blue("去 捐献详情页");
            GameData.cur_ProjectId = projectData.projectId;
            GameData.curDonateProjectImg = sp;
            UIMgr.Instance.Open(UIPath.UIBDonateDetailPanel);
        }
    }
}
