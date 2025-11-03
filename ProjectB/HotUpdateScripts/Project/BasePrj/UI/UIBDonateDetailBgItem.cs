using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBDonateDetailBgItem : MonoBehaviour
    {
        private Image txt_Icon;
        private Text txt_Show;
        private bool isFirst = true;

        void setObj()
        {
            txt_Icon = GameTools.GetByName(transform, "txt_Icon").GetComponent<Image>();
            txt_Show = GameTools.GetByName(transform, "txt_Show").GetComponent<Text>();
        }

        public void InitItem(UserDonateRecord data)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (txt_Icon == null) return;
                txt_Icon.sprite = r;
                //GameTools.Instance.MatchImgBySprite(txt_Icon);
            }, data.headImgUrl);

            txt_Show.text = "**" + data.nickName.Substring(data.nickName.Length - 1, 1) + "助力了" + data.donateNum + "颗爱心  " + data.donateTime;
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
        }
    }
}

