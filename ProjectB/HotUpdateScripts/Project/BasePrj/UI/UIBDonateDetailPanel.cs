using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBDonateDetailPanel : BasePanel
    {
        private Image pan_Top;
        private GameObject btn_PlayVideo;
        //View
        private GameObject btn_Back, btn_Donate, btn_DonateNone, btn_ProjectBg, btn_ProjectProgress, sv_ProjectBg, sv_ProjectProgress, ItemProgress;
        private Text txt_Title, txt_AllDonate, txt_MyDonate;
        private RectTransform sv_ProjectBgContent, sv_ProjectProgressContent;
        DataBProjectInfoRes data;
        //预制体
        private Text text_Title, text_Desc;
        private GameObject img, txt_BgOne, txt_BgTwo;
        public ImgQiehuan imgQiehuan;
        private List<UserDonateRecord> recordList = new List<UserDonateRecord>();
        private GameObject[] recordObj;
        private bool isFirstOpenProgressView = true;
        public static GameObject pan_Donate;
        public static Action<int> act_RefreshDonateNum;
        private bool isFirstGetData = true;
        private Coroutine coroutine_RollMove, coroutine_setProjectIntroduceView, coroutine_setProjectProgressView;//协程展示项目介绍  协程进度介绍

        void Awake()
        {
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f);
            pan_Top = GameTools.GetByName(transform, "pan_Top").GetComponent<Image>();
            btn_PlayVideo = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_PlayVideo"), () =>
             {
                 if (data == null) return;
                 //UIMgr.Instance.Open(UIPath.CommonVideoPanel, data.projectVideo);
                 UIMgr.Instance.Open(UIPath.CommonMediaPlayer, data.projectVideo);

             });
            btn_Back = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Back"), OnBtn_BackClick);
            btn_Donate = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Donate"), OnBtn_DonateClick);
            btn_DonateNone = GameTools.GetByName(transform, "btn_DonateNone");
            btn_ProjectBg = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_ProjectBg"), OnBtn_ProjectBgClick);
            btn_ProjectProgress = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_ProjectProgress"), OnBtn_ProjectProgressClick);

            txt_Title = GameTools.GetByName(transform, "txt_Title").GetComponent<Text>();
            txt_AllDonate = GameTools.GetByName(transform, "txt_AllDonate").GetComponent<Text>();
            txt_MyDonate = GameTools.GetByName(transform, "txt_MyDonate").GetComponent<Text>();

            sv_ProjectBgContent = GameTools.GetByName(transform, "sv_ProjectBgContent").GetComponent<RectTransform>();
            sv_ProjectProgressContent = GameTools.GetByName(transform, "sv_ProjectProgressContent").GetComponent<RectTransform>();

            sv_ProjectBg = GameTools.GetByName(transform, "sv_ProjectBg");
            sv_ProjectProgress = GameTools.GetByName(transform, "sv_ProjectProgress");

            imgQiehuan = GameTools.GetByName(transform, "QieHuan").GetComponent<ImgQiehuan>();

            ItemProgress = GameTools.GetByName(transform, "ItemProgress");
            ItemProgress.SetActive(false);

            //漂浮预制体
            txt_BgOne = GameTools.GetByName(transform, "txt_BgOne");
            txt_BgTwo = GameTools.GetByName(transform, "txt_BgTwo");
            recordObj = new GameObject[2] { txt_BgOne, txt_BgTwo };

            text_Title = GameTools.GetByName(transform, "text_Title").GetComponent<Text>();
            text_Desc = GameTools.GetByName(transform, "text_Desc").GetComponent<Text>();
            img = GameTools.GetByName(transform, "img");
            text_Title.gameObject.SetActive(false);
            text_Desc.gameObject.SetActive(false);
            img.gameObject.SetActive(false);

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_DonateDetailsInfo, S2C_Love_DonateDetailsInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_Donate, S2C_Love_DonateCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Project_NewProjectDonateInfo, S2C_Project_NewProjectDonateInfoCallBack);

            txt_BgOne.AddComponent<UIBDonateDetailBgItem>();
            txt_BgTwo.AddComponent<UIBDonateDetailBgItem>();

            pan_Donate = GameTools.GetByName(transform, "pan_Donate");
            pan_Donate.AddComponent<UIBDonateDetailDonatePanel>();

            act_RefreshDonateNum = refreshDonateNum;

            if (GameData.curDonateProjectImg != null) pan_Top.sprite = GameData.curDonateProjectImg;
        }

        void Start()
        {
            DataMgr.Instance.dataBProjectInfoReq.userId = GameData.userId;
            DataMgr.Instance.dataBProjectInfoReq.projectId = GameData.cur_ProjectId;
            NetMgr.Instance.C2S_Love_DonateDetailsInfo();
            setBtnView();
        }

        void OnDestroy()
        {
            recordList.Clear();
            GameData.curDonateResultImg = null;
            GameData.curDonateProjectImg = null;
            GameData.cur_ProjectId = -1;
            GameData.newProjectDonateInfo = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_DonateDetailsInfo, S2C_Love_DonateDetailsInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_Donate, S2C_Love_DonateCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Project_NewProjectDonateInfo, S2C_Project_NewProjectDonateInfoCallBack);
            coroutine_RollMove = null;
            coroutine_setProjectIntroduceView = null;
            coroutine_setProjectProgressView = null;
        }

        void setBtnView()
        {
            if (GameData.curLoveNum < 5)
            {
                btn_Donate.SetActive(false);
                btn_DonateNone.SetActive(true);
            }
            else
            {
                btn_Donate.SetActive(true);
                btn_DonateNone.SetActive(false);
            }
        }

        //获取项目详情数据成功
        void S2C_Love_DonateDetailsInfoCallBack(object o)
        {
            data = DataMgr.Instance.dataBProjectInfoRes;
            //预加载捐献项目图片
            string[] s = data.projectDonateFinishPicture.Split(new char[] { '^' }, System.StringSplitOptions.RemoveEmptyEntries);
            string url = s[UnityEngine.Random.Range(0, s.Length)];
            NetMgr.Instance.DownLoadImg(r =>
            {
                GameData.curDonateResultImg = r;
            }, url);

            if (isFirstGetData)
            {
                isFirstGetData = false;
                txt_Title.text = data.projectTitle;
                txt_AllDonate.text = data.donateTotal.ToString();
                txt_MyDonate.text = data.userDonateSumForProject.ToString();

                if (data.projectVideo == "" || data.projectVideo == null)
                {
                    btn_PlayVideo.SetActive(false);
                }
                else
                {
                    btn_PlayVideo.SetActive(true);
                }
                for (int i = 0; i < data.lastTenDonateRecordList.Count; i++)
                {
                    recordList.Add(data.lastTenDonateRecordList[i]);
                }
                if (recordList.Count == 0) { recordObj[0].SetActive(false); recordObj[1].SetActive(false); recordObj[0].transform.parent.gameObject.SetActive(false); }
                if (recordList.Count >= 1)
                {
                    recordObj[0].GetComponent<UIBDonateDetailBgItem>().InitItem(recordList[0]);
                }
                if (recordList.Count >= 2)
                {
                    recordObj[1].GetComponent<UIBDonateDetailBgItem>().InitItem(recordList[1]);
                }
                if (coroutine_setProjectIntroduceView != null) StopCoroutine(coroutine_setProjectIntroduceView);
                coroutine_setProjectIntroduceView = StartCoroutine(cor_setProjectIntroduceView());
                InvokeRepeating("RollMove", 2f, 3.5f);
            }
            else
            {
                //刷新捐献数量
                txt_AllDonate.text = data.donateTotal.ToString();
                txt_MyDonate.text = data.userDonateSumForProject.ToString();
                setBtnView();
            }
        }

        //展示项目介绍
        IEnumerator cor_setProjectIntroduceView()
        {
            int headImgIndex = 0;
            string a = data.projectIntroduceWord;
            string[] s = a.Split(new char[] { '@' }, System.StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < s.Length; i++)
            {
                GameObject item_Title = Instantiate(text_Title.gameObject, sv_ProjectBgContent.transform);
                item_Title.GetComponent<Text>().text = getTitleByIndex(i);
                item_Title.SetActive(true);

                string[] b = s[i].Split(new char[] { '_' }, System.StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < b.Length; j++)
                {
                    GameObject item_desc = Instantiate(text_Desc.gameObject, sv_ProjectBgContent.transform);
                    item_desc.SetActive(true);
                    if (b[j].Contains("^"))
                    {
                        item_desc.GetComponent<Text>().text = b[j].Substring(0, b[j].Length - 1);
                    }
                    else
                    {
                        item_desc.GetComponent<Text>().text = b[j];
                    }
                    if (b[j].Contains("^"))
                    {
                        GameObject imgItem = Instantiate(img.gameObject, sv_ProjectBgContent.transform);
                        imgItem.gameObject.SetActive(true);
                        string imgUrl = getHeadImgUrlByIndex(headImgIndex);
                        imgItem.gameObject.AddComponent<UIBSetImg>().InitItem(imgUrl, Match_Img.Width);
                        headImgIndex++;
                    }
                }
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(sv_ProjectBgContent.GetComponent<RectTransform>());
            yield return 1;
        }


        void SetImgContent(string url, Image img)
        {
            WebRequestHelper.GetTexture(url, r =>
            {
                Sprite sp = Sprite.Create(r, new Rect(0, 0, r.width, r.height), new Vector2(0, 0));
                img.sprite = sp;
            });
        }

        string getTitleByIndex(int index)
        {
            string result = "";
            string[] title = data.projectIntroduceTitle.Split(new char[] { '@' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (index < title.Length)
            {
                result = title[index];
            }
            else
            {
                result = "";
            }
            return result;
        }

        string getHeadImgUrlByIndex(int index)
        {
            string[] s = data.projectIntroduceImg.Split(new char[] { '^' }, System.StringSplitOptions.RemoveEmptyEntries);
            return s[index];
        }

        //展示项目进度
        IEnumerator cor_setProjectProgressView()
        {
            yield return 1;
            if (data != null)
            {
                for (int i = 0; i < data.projectProgressList.Count; i++)
                {
                    GameObject Item = Instantiate(ItemProgress, sv_ProjectProgressContent);
                    Item.SetActive(true);
                    Item.AddComponent<UIBDonateDetailProgressItem>().InitItem(data.projectProgressList[i]);
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(sv_ProjectProgressContent);
                LayoutRebuilder.ForceRebuildLayoutImmediate(sv_ProjectProgressContent);
                isFirstOpenProgressView = false;
            }
        }

        void RollMove()
        {
            if (coroutine_RollMove != null)
            {
                StopCoroutine(coroutine_RollMove);
            }
            coroutine_RollMove = StartCoroutine(RollRecord());
        }
        //循环播放捐献列表
        IEnumerator RollRecord()
        {
            if (recordList.Count >= 1)
            {
                recordObj[0].GetComponent<UIBDonateDetailBgItem>().InitItem(recordList[0]);
                recordList.RemoveAt(0);
            }
            else
            {
                for (int i = 0; i < data.lastTenDonateRecordList.Count; i++)
                {
                    recordList.Add(data.lastTenDonateRecordList[i]);
                }
            }
            DOTweenMgr.Instance.MovePos(recordObj[0], new Vector3(-492, 0, 0), new Vector3(-492, 66, 0), 1f, () =>
                    {
                        recordObj[0].transform.localPosition = new Vector3(-492, -62, 0);

                    });

            if (recordList.Count == 0)
            {
                for (int i = 0; i < data.lastTenDonateRecordList.Count; i++)
                {
                    recordList.Add(data.lastTenDonateRecordList[i]);
                }
            }
            if (recordList.Count >= 1)
            {
                recordObj[1].GetComponent<UIBDonateDetailBgItem>().InitItem(recordList[0]);
                recordList.RemoveAt(0);
            }

            yield return new WaitForSeconds(0.3f);

            DOTweenMgr.Instance.MovePos(recordObj[1], new Vector3(-492, -62, 0), new Vector3(-492, 0, 0), 1f, () =>
            {
                GameObject objTempt = recordObj[0];
                recordObj[0] = recordObj[1];
                recordObj[1] = objTempt;
            });
        }

        //捐献成功回调
        void S2C_Love_DonateCallBack(object o)
        {
            //捐献成功后，需要刷新捐献数量的数据
            //TODO：
            if (GameData.cur_ProjectId > 0)
                NetMgr.Instance.C2S_Love_DonateDetailsInfo();
            //刷新项目列表
            NetMgr.Instance.C2S_Love_GetDonateList();
            GameData.isNewDonate = true;
        }

        //捐献成功后最新数据
        void S2C_Project_NewProjectDonateInfoCallBack(object o)
        {
            GameData.newProjectDonateInfo = DataMgr.Instance.newProjectDonateInfo;
            if (GameData.isNewDonate)
            {
                GameData.isNewDonate = false;
                //展示捐献反馈
                UIMgr.Instance.Open(UIPath.UIBDonateResultPanel);
            }
        }

        //Business
        public void OnBtn_BackClick()
        {
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBDonateDetailPanel);
            });
        }

        //项目介绍按钮
        public void OnBtn_ProjectBgClick()
        {
            imgQiehuan.SetImg(1, btn_ProjectBg);
            imgQiehuan.SetImg(0, btn_ProjectProgress);

            if (!sv_ProjectBg.activeSelf)
                sv_ProjectBg.SetActive(true);
            if (sv_ProjectProgress.activeSelf)
                sv_ProjectProgress.SetActive(false);
        }

        //项目进展按钮
        public void OnBtn_ProjectProgressClick()
        {
            imgQiehuan.SetImg(0, btn_ProjectBg);
            imgQiehuan.SetImg(1, btn_ProjectProgress);

            if (sv_ProjectBg.activeSelf)
                sv_ProjectBg.SetActive(false);
            if (!sv_ProjectProgress.activeSelf)
                sv_ProjectProgress.SetActive(true);

            if (isFirstOpenProgressView)
            {
                if (coroutine_setProjectProgressView != null) StopCoroutine(coroutine_setProjectProgressView);
                coroutine_setProjectProgressView = StartCoroutine(cor_setProjectProgressView());
            }
        }

        //捐献按钮
        public void OnBtn_DonateClick()
        {
            if (data == null) return;
            pan_Donate.SetActive(true);
            DOTweenMgr.Instance.MovePos(pan_Donate, new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f);
            pan_Donate.GetComponent<UIBDonateDetailDonatePanel>().InitData(data.projectTitle);
        }

        //本地更新捐献数量
        public void refreshDonateNum(int donateNum)
        {
            txt_AllDonate.text = (data.donateTotal + donateNum).ToString();
            txt_MyDonate.text = (data.userDonateSumForProject + donateNum).ToString();
        }
    }
}
