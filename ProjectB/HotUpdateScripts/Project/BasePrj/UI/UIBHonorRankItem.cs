using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBHonorRankItem : MonoBehaviour
    {
        private bool isFirst = true;

        private Image rankIcon; //奖章
        private Text rankNum;  //排名
        private Image Icon; //头像
        private Text Name; //昵称

        private GameObject titleBg;

        private Image madelIcon; //勋章Icon
        private Text madelName; //勋章名称
        private Text donateNum; //捐献数量

        void setObj()
        {
            rankIcon = GameTools.GetByName(transform, "rankIcon").GetComponent<Image>();
            rankNum = GameTools.GetByName(transform, "rankNum").GetComponent<Text>();
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();

            Name = GameTools.GetByName(transform, "Name").GetComponent<Text>();
            madelIcon = GameTools.GetByName(transform, "madelIcon").GetComponent<Image>();

            madelName = GameTools.GetByName(transform, "madelName").GetComponent<Text>();
            donateNum = GameTools.GetByName(transform, "donateNum").GetComponent<Text>();

            titleBg = GameTools.GetByName(transform, "titleBg");
        }
        public void setRankItem(DonateLoveRankListItem data, List<HonorMedal> honorMedalList, List<UserMedalRecord> userMedalRecord)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            setRankIcon(data.rowNum);
            rankNum.text = data.rowNum.ToString();
            if (!string.IsNullOrEmpty(data.headImgUrl) && data.headImgUrl != "null")  //数据中有出现过头像地址 "null" 的情况
            {
                NetMgr.Instance.DownLoadHeadImg(r =>
                {
                    if (Icon == null) return;
                    Icon.sprite = r;
                    //GameTools.Instance.MatchImgBySprite(Icon);
                }, data.headImgUrl);
            }
            if (data.userId == GameData.userId)
            {
                Name.text = data.nickName + "(我自己)";
            }
            else
            {
                Name.text = data.nickName;
            }

            donateNum.text = "已捐赠  " + data.donateLoveNum.ToString();
            
            //设置勋章
            UIBHonorHomePanel.ModelQieHuan.SetImg(getMedalId(data.userId, userMedalRecord) - 1, madelIcon.gameObject);

            string name = getMedalName(honorMedalList, getMedalId(data.userId, userMedalRecord));
            if (string.IsNullOrEmpty(name))
            {
                titleBg.SetActive(false);
            }
            else
            {
                madelName.text = getMedalName(honorMedalList, getMedalId(data.userId, userMedalRecord));
            }
        }

        //通过用户ID，获得当前最高勋章的ID
        int getMedalId(long userId, List<UserMedalRecord> userMedalRecord)
        {
            for (int i = 0; i < userMedalRecord.Count; i++)
            {
                if (userMedalRecord[i].userId == userId)
                {
                    return userMedalRecord[i].medalId;
                }
            }
            return 1;
        }

        string getMedalName(List<HonorMedal> data, int _id)
        {
            string result = "";
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].medalId == _id)
                {
                    result = data[i].medalTitle;
                }
            }
            return result;
        }

        void setRankIcon(int rankNum)
        {
            if (rankNum > 3) rankIcon.gameObject.SetActive(false);
            else
            {
                rankIcon.gameObject.SetActive(true);
                UIBHonorHomePanel.RankQieHuan.SetImg((rankNum - 1), rankIcon.gameObject, true);
            }
        }
    }
}
