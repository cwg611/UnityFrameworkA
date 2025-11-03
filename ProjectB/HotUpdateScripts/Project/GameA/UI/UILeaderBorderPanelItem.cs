using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UILeaderBorderPanelItem : MonoBehaviour
    {
        Image icon;
        Image rankBg;
        Text textId;

        bool haveInit = false;
        private void InitView()
        {
            icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            rankBg = GameTools.GetByName(transform, "rankBg").GetComponent<Image>();
            haveInit = true;
        }
        public void SetLeaderBorderItem(DataGameA data, int Type)
        {
            if (!haveInit) InitView();

            if (Type == 1)
            {
                //GameTools.GetByName(transform, "txt_Id").GetComponent<Text>().text = data.nickName + "(我自己)";
                GameTools.GetByName(transform, "txt_Id").GetComponent<Text>().text = data.nickName + "(我自己)";
                GameTools.GetByName(transform, "rankBg").SetActive(false);
            }
            else
            {
                if (data.userId == GameData.userId)
                {
                    GameTools.GetByName(transform, "txt_Id").GetComponent<Text>().text = SetName(data.nickName) + "(我自己)";
                }
                else
                {
                    GameTools.GetByName(transform, "txt_Id").GetComponent<Text>().text = SetName(data.nickName);
                }
                if (data.rowNum > 3)
                {
                    rankBg.gameObject.SetActive(false);
                }
                else
                {
                    rankBg.gameObject.SetActive(true);
                    if (data.rowNum == 1)
                    {
                        UILeaderBorderPanel.Qiehuan.SetImg(0, rankBg.gameObject);
                    }
                    else if (data.rowNum == 2)
                    {
                        UILeaderBorderPanel.Qiehuan.SetImg(1, rankBg.gameObject);
                    }
                    else if (data.rowNum == 3)
                    {
                        UILeaderBorderPanel.Qiehuan.SetImg(2, rankBg.gameObject);
                    }
                }

                //if (data.userId == GameData.userId)
                //{
                //    UILeaderBorderPanel.Qiehuan.SetImg(4, transform.gameObject);
                //}
                //else
                //{
                //    UILeaderBorderPanel.Qiehuan.SetImg(3, transform.gameObject);
                //}
            }

            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (icon == null) return;
                icon.sprite = r;
                //GameTools.Instance.MatchImgBySprite(icon);
            }, data.headImgUrl);



            GameTools.GetByName(transform, "txt_Score").GetComponent<Text>().text = data.gameScore.ToString();
            GameTools.GetByName(transform, "txt_RowNum").GetComponent<Text>().text = data.rowNum.ToString();
        }

        private string SetName(string str)
        {
            return str.Length <= 1 ? str : str.Replace(str.Substring(1, str.Length - 1), "**");
        }
    }
}
