using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBShopRecordItem : MonoBehaviour
    {
        private Text time, desc, num_Aibi;
        private bool isFirst = true;

        void setObj()
        {
            time = GameTools.GetByName(transform, "time").GetComponent<Text>();
            desc = GameTools.GetByName(transform, "desc").GetComponent<Text>();
            num_Aibi = GameTools.GetByName(transform, "num_Aibi").GetComponent<Text>();
        }

        public void InitItem(UserConversionRecord data)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            time.text = data.coinTime;
            desc.text = "兑换" + '"' + data.productName + '"' + "X" + data.conversionNum + "张";
            num_Aibi.text = "X" + data.spendLoveMoney;
        }

        void OnDestroy()
        {

        }
    }
}



