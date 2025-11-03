using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using HotUpdateScripts.Project.BasePrj.Data;
using My.Msg;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using JEngine.Core;
using System.Threading;

namespace My.UI.Panel
{
    /// <summary>
    /// 交友大厅
    /// </summary>
    public class UIBFriendHomePanel : BasePanel
    {
        private FriendPageBase[] friendPages;
        private Button[] bottomBtnArray;
        private Text[] btnTextArray;
        private GameObject[] btnBgGbs;
        private Button btn_Close;

        public static ImgQiehuan IconQieHuan;//sp7 sp8 男性 女性

        public static Action SwitchFriendPageAction;

        private GameObject ExplainView;

        public override void InitPanel(object o)
        {
            IsHomePanel = true;
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            }
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .3f, TweenCallBack);
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            btn_Close.onClick.AddListener(OnBtnCloseClick);
            Transform btnGroup = GameTools.GetByName(transform, "BtnGroup").transform;
            bottomBtnArray = new Button[3] { GameTools.GetByName(btnGroup, "btn_one").GetComponent<Button>(),
                GameTools.GetByName(btnGroup, "btn_two").GetComponent<Button>(),
                GameTools.GetByName(btnGroup, "btn_three").GetComponent<Button>() };
            btnTextArray = new Text[3] { GameTools.GetByName(btnGroup, "btnTxtOne").GetComponent<Text>(),
                GameTools.GetByName(btnGroup, "btnTxtTwo").GetComponent<Text>(),
                GameTools.GetByName(btnGroup, "btnTxtThree").GetComponent<Text>() };
            btnBgGbs = new GameObject[3] { GameTools.GetByName(btnGroup, "one"),
                GameTools.GetByName(btnGroup, "two"),
                GameTools.GetByName(btnGroup, "three") };

            friendPages = new FriendPageBase[3];
            friendPages[0] = GameTools.GetByName(transform, "ExplorePage").AddComponent<UIBFriendExplorePage>();
            friendPages[1] = GameTools.GetByName(transform, "ChatListPage").AddComponent<UIBFriendListPage>();
            friendPages[2] = GameTools.GetByName(transform, "MyPage").AddComponent<UIBFriendHomeMyPage>();

            ExplainView = transform.Find("ExplainPop").gameObject;
            GameTools.Instance.AddClickEvent(transform.Find("ExplorePage/ExplainBtn"), () => { ExplainView.SetActive(true); });
            GameTools.Instance.AddClickEvent(transform.Find("ExplainPop/Bg/CloseBtn"), () => { ExplainView.SetActive(false); });

            IconQieHuan = GameTools.GetByName(transform, "IconQieHuan").GetComponent<ImgQiehuan>();

            for (int i = 0; i < bottomBtnArray.Length; i++)
            {
                int index = i;
                bottomBtnArray[index].onClick.AddListener(() =>
                {
                    for (int k = 0; k < bottomBtnArray.Length; k++)
                    {
                        if (k == index)
                        {
                            btnTextArray[k].color = Color.white;
                            friendPages[k].InitPage();
                            friendPages[k].OpenPage();
                        }
                        else
                        {
                            btnTextArray[k].color = new Color32(83, 95, 105, 255);
                            friendPages[k].ClosePage();
                        }
                    }
                    if (index == 0)
                    {
                        IconQieHuan.SetImg(0, btnBgGbs[0].gameObject);
                        IconQieHuan.SetImg(3, btnBgGbs[1].gameObject);
                        IconQieHuan.SetImg(5, btnBgGbs[2].gameObject);
                    }
                    else if (index == 1)
                    {
                        IconQieHuan.SetImg(1, btnBgGbs[0].gameObject);
                        IconQieHuan.SetImg(2, btnBgGbs[1].gameObject);
                        IconQieHuan.SetImg(5, btnBgGbs[2].gameObject);
                    }
                    else
                    {
                        IconQieHuan.SetImg(1, btnBgGbs[0].gameObject);
                        IconQieHuan.SetImg(3, btnBgGbs[1].gameObject);
                        IconQieHuan.SetImg(4, btnBgGbs[2].gameObject);
                    }
                    UIMgr.Instance.Close(UIPath.UIBFriendFocusListPanel);
                    GameData.isOpenFocusListPanel = false;
                });
            }

            if (GameData.isOpenFriend)
            {
                bottomBtnArray[2].onClick.Invoke();
            }
            else if (GameData.isOpenFriendList)
            {
                bottomBtnArray[1].onClick.Invoke();
            }
            else
            {
                bottomBtnArray[0].onClick.Invoke();
            }
            SwitchFriendPageAction = () => { bottomBtnArray[1].onClick.Invoke(); };

            //加载大表情包信息
            if (GameData.BigEmojiDic == null)
            {
                GameData.BigEmojiDic = new Dictionary<string, EmojiInfo>();

                JResource.LoadResAsync<TextAsset>("emoji2.txt", (s) =>
                {
                    TextAsset emojiContentTwo = s;
                    string[] linesTwo = emojiContentTwo.text.Split('\n');
                    for (int i = 1; i < linesTwo.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(linesTwo[i]))
                        {
                            string[] strs = linesTwo[i].Split('\t');
                            EmojiInfo info;
                            info.name = strs[0];
                            info.x = float.Parse(strs[3]);
                            info.y = float.Parse(strs[4]);
                            info.size = float.Parse(strs[5]);
                            GameData.BigEmojiDic.Add(strs[1], info);
                        }
                    }
                });
            }
            if (GameData.SensitiveWordFilter == null)
            {
                string[] Words;
                TextAsset textAsset = JResource.LoadRes<TextAsset>("sensitiveword.txt", JResource.MatchMode.Other);
                Words = textAsset.text.Split(',');
                ThreadStart childRef = new ThreadStart(() =>
                {
                    GameData.SensitiveWordFilter = new SensitiveWordFilter();
                    GameData.SensitiveWordFilter.Init(Words);
                    debug.Log_Blue("敏感词库初始完成");
                });
                Thread childThread = new Thread(childRef);
                childThread.Start();
            }

            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_FRIEND];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
        }

        private void TweenCallBack()
        {
            NetMgr.Instance.C2S_Friend_MatchUserPool();
        }

        void OnBtnCloseClick()
        {
            //判断是否打开了 关注列表界面
            if (UIMgr.Instance.GetPanelIsExit(UIPath.UIBFriendFocusListPanel))
            {
                UIMgr.Instance.Close(UIPath.UIBFriendFocusListPanel);
            }
            else
            {
                if (IsHomePanel)
                {
                    HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
                }
                DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
                DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
                {
                    UIMgr.Instance.Close(UIPath.UIBFriendHomePanel);
                });
            }
        }

        void OnDestroy()
        {
            GameData.isOpenFriend = false;
            GameData.isOpenFriendList = false;
        }
    }
}