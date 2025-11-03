using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdateScripts.Project.GameB.UI
{
    public class RankingItem : MonoBehaviour
    {
        private Image img_HeadImg;

        private Text txt_NickName;

        private Text tet_Score, txt_Num;

        private Transform RankingFirst;
        private Transform RankingSecond;
        private Transform RankingThird;
        bool haveInit = false;
        void InitView()
        {
            img_HeadImg = transform.Find("HeadImage").GetComponent<Image>();
            txt_NickName = transform.Find("NickName").GetComponent<Text>();
            tet_Score = transform.Find("ScoreText").GetComponent<Text>();
            txt_Num = transform.Find("Num").GetComponent<Text>();

            RankingFirst = transform.Find("RankingImg1");
            RankingSecond = transform.Find("RankingImg2");
            RankingThird = transform.Find("RankingImg3");

            RankingFirst.localScale = Vector3.zero;
            RankingSecond.localScale = Vector3.zero;
            RankingThird.localScale = Vector3.zero;
        }

        public void SetView(DataScoreRankItem data)
        {
            if (!haveInit)
            {
                InitView();
                haveInit = true;
            }
            debug.Log_yellow("data.rowNum--" + data.rowNum);
            RankingFirst.localScale = data.rowNum == 1 ? Vector3.one : Vector3.zero;
            RankingSecond.localScale = data.rowNum == 2 ? Vector3.one : Vector3.zero;
            RankingThird.localScale = data.rowNum == 3 ? Vector3.one : Vector3.zero;

            txt_NickName.text = SetName(data.nickName);
            txt_Num.text = data.rowNum.ToString();

            tet_Score.text = data.topScore.ToString();

            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (img_HeadImg == null) return;
                img_HeadImg.sprite = r;
            }, data.headImgUrl);
        }


        private string SetName(string str)
        {
            return str.Length <= 1 ? str : str.Replace(str.Substring(1, str.Length - 1), "**");
        }
    }
}
