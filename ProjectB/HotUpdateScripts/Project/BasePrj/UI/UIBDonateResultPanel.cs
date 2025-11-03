using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBDonateResultPanel : BasePanel
    {
        private Button btn_Close;
        private Image Img_Content;
        private Image Icon;
        private GameObject TxtNum;
        private GameObject panContent;
        private Text[] TxtNums;
        private Text Txt_title, Txt_PersonNum, Txt_helpPerson, Txt_desc;
        private Button Btn_submit;
        private Button BtnClose;
        private Coroutine coroutine_SetImg;//设置封面图片

        public override void InitPanel(object o)
        {
            panContent = GameTools.GetByName(transform, "panContent");
            DOTweenMgr.Instance.OpenWindowScale(panContent, .3f);
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            Img_Content = GameTools.GetByName(transform, "Img_Content").GetComponent<Image>();
            //Img_Content.gameObject.SetActive(false);
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            Txt_title = GameTools.GetByName(transform, "Txt_title").GetComponent<Text>();
            TxtNum = GameTools.GetByName(transform, "TxtNum");
            TxtNums = TxtNum.GetComponentsInChildren<Text>();

            Txt_PersonNum = GameTools.GetByName(transform, "Txt_PersonNum").GetComponent<Text>();
            Txt_helpPerson = GameTools.GetByName(transform, "Txt_helpPerson").GetComponent<Text>();
            Txt_desc = GameTools.GetByName(transform, "Txt_desc").GetComponent<Text>();

            Btn_submit = GameTools.GetByName(transform, "Btn_submit").GetComponent<Button>();
            BtnClose = GameTools.GetByName(transform, "BtnClose").GetComponent<Button>();
            Btn_submit.onClick.AddListener(OnBtnCloseClick);
            BtnClose.onClick.AddListener(OnBtnCloseClick);
            btn_Close.onClick.AddListener(OnBtnCloseClick);

            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (Icon == null) return;
                Icon.sprite = r;
                //GameTools.Instance.MatchImgBySprite(Icon);
            }, GameData.userHeadImg);

            if (GameData.newProjectDonateInfo == null) return;
            NewProjectDonateInfo data = GameData.newProjectDonateInfo;

            //项目图片
            //string[] s = data.projectDonateFinishPicture.Split(new char[] { '^' }, System.StringSplitOptions.RemoveEmptyEntries);
            //string url = s[UnityEngine.Random.Range(0, s.Length)];
            //NetMgr.Instance.DownLoadImg(r =>
            //{
            //    if (Img_Content == null) return;
            //    Img_Content.sprite = r;
            //}, url);

            if (coroutine_SetImg != null) StopCoroutine(coroutine_SetImg);
            coroutine_SetImg = StartCoroutine(SetImg());

            Txt_title.text = "感谢您的第" + data.userDonateSumForProject + "次捐助";

            //助力人次
            Txt_PersonNum.text = data.donateNum + "次";

            //帮助对象
            Txt_helpPerson.text = data.projectHelpObject;

            Txt_desc.text = data.projectTitle;

            int donateNum = data.projectTotal;
            TxtNums[0].text = getLastStr((donateNum / 1000000).ToString());
            TxtNums[1].text = getLastStr((donateNum / 100000).ToString());
            TxtNums[2].text = getLastStr((donateNum / 10000).ToString());
            TxtNums[3].text = getLastStr((donateNum / 1000).ToString());
            TxtNums[4].text = getLastStr((donateNum / 100).ToString());
            TxtNums[5].text = getLastStr((donateNum / 10).ToString());
            TxtNums[6].text = getLastStr((donateNum).ToString());
        }

        IEnumerator SetImg()
        {
            yield return new WaitUntil(() => GameData.curDonateResultImg != null);
            Img_Content.sprite = GameData.curDonateResultImg;
        }

        void OnBtnCloseClick()
        {
            DOTweenMgr.Instance.CloseWindowScale(panContent, 0.2f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBDonateResultPanel);
            });
        }


        string getLastStr(string str)
        {
            return str.Substring(str.Length - 1, 1);
        }

        void OnDestroy()
        {
            coroutine_SetImg = null;
        }
    }
}
