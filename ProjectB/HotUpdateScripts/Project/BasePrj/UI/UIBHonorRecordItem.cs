using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBHonorRecordItem : MonoBehaviour
    {
        private Text time, desc, rewardNum, num_Aibi;
        private RectTransform Reward;

        private bool isFirst = true;

        void setObj()
        {
            time = GameTools.GetByName(transform, "time").GetComponent<Text>();
            Reward = GameTools.GetByName(transform, "Reward").GetComponent<RectTransform>();
            desc = GameTools.GetByName(transform, "desc").GetComponent<Text>();
            rewardNum = GameTools.GetByName(transform, "rewardNum").GetComponent<Text>();
            num_Aibi = GameTools.GetByName(transform, "num_Aibi").GetComponent<Text>();
        }

        public void setItemView(DonateRecord recordData)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            time.text = recordData.donateTime;
            desc.text = "在" + '"' + recordData.projectTitle + '"' + "捐了";
            rewardNum.text = "×" + recordData.donateNum.ToString();
            num_Aibi.text = "×" + recordData.getLoveMoneyNum.ToString();
            LayoutRebuilder.ForceRebuildLayoutImmediate(Reward);
        }
    }
}
