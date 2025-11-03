using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public enum IsFocus
    {
        UnState, //无状态
        Focus,     //已关注
        UnFocus,   //未关注
        AllFocus,//互相关注
    }

    public class UIBFriendFocusListPanel : BasePanel
    {
        //public static ImgQiehuan imgQiehuan;
        private Button btn_Close;
        private Text txt_Title;
        private Button[] btns;
        private GameObject ItemPrefab;
        private Transform ContentOne, ContentTwo;
        private GameObject ScrollViewOne, ScrollViewTwo;
        private bool isFirst;//是否第一次打开页面
        private List<UIBFriendFocusItem> myFocusList;// = new List<UIBFriendFocusItem>();
        private List<UIBFriendFocusItem> FocusMeList;// = new List<UIBFriendFocusItem>();   
        private GameObject Window, btn_Submit, btn_Cancel; //取消关注弹框,继续关注按钮,取消关注
        public static Action<bool> Act_OpenWindow;

        public override void InitPanel(object o)
        {
            isFirst = true;
           // imgQiehuan = GameTools.GetByName(transform, "QieHuan").GetComponent<ImgQiehuan>();
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            txt_Title = GameTools.GetByName(transform, "TitleText").GetComponent<Text>();
            btns = new Button[2] { GameTools.GetByName(transform, "btn_one").GetComponent<Button>(),
                GameTools.GetByName(transform, "btn_two").GetComponent<Button>() };
            ContentOne = GameTools.GetByName(transform, "ContentOne").transform;
            ContentTwo = GameTools.GetByName(transform, "ContentTwo").transform;
            ScrollViewOne = GameTools.GetByName(transform, "ScrollViewOne");
            ScrollViewTwo = GameTools.GetByName(transform, "ScrollViewTwo");
            ItemPrefab = GameTools.GetByName(transform, "Prefab");
            ItemPrefab.SetActive(false);
            //二次弹窗
            Window = GameTools.GetByName(transform, "Window");
            Window.SetActive(false);
            btn_Submit = GameTools.GetByName(transform, "btn_Submit");
            btn_Cancel = GameTools.GetByName(transform, "btn_Cancel");
            GameTools.Instance.AddClickEvent(btn_Submit, () =>
             {
                 OpenWindow(false);
             });
            GameTools.Instance.AddClickEvent(btn_Cancel, () =>
            {
                //取消关注
                NetMgr.Instance.C2S_Friend_UnFollowToUser();
            });
            Act_OpenWindow = OpenWindow;

            for (int i = 0; i < btns.Length; i++)
            {
                int index = i;
                btns[index].onClick.AddListener(() =>
                {
                    //for (int k = 0; k < btns.Length; k++)
                    //{
                    //    if (k == index) imgQiehuan.setImg(1, btns[k].gameObject);
                    //    else imgQiehuan.setImg(0, btns[k].gameObject);
                    //}

                    ScrollViewOne.gameObject.SetActive(index == 0);
                    ScrollViewTwo.gameObject.SetActive(index == 1);
                });
            }

            btn_Close.onClick.AddListener(() =>
            {
                GameData.isOpenFocusListPanel = false;
                UIMgr.Instance.Close(UIPath.UIBFriendFocusListPanel);
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_AttentionList, S2C_Friend_AttentionListCallBack);
            NetMgr.Instance.C2S_Friend_AttentionList(GameData.userId.ToString());

            if (GameData.isMyFoucs)
            {
                btns[0].onClick.Invoke();
            }
            else
            {
                btns[1].onClick.Invoke();
            }
            txt_Title.text = GameData.isMyFoucs ? "我关注" : "关注我";
        }

        private void OpenWindow(bool state)
        {
            Window.SetActive(state);
        }

        //关注的列表
        void S2C_Friend_AttentionListCallBack(object o)
        {
            List<AttentionToMeList> dataList;
            if (GameData.isMyFoucs)
            {
                dataList = DataMgr.Instance.attentionListRes.meAttentionList;
                if (myFocusList == null)
                {
                    myFocusList = new List<UIBFriendFocusItem>();
                    if (dataList != null)
                    {
                        for (int i = 0; i < dataList.Count; i++)
                        {
                            GameObject Item = Instantiate(ItemPrefab, ContentOne);
                            Item.SetActive(true);
                            Item.AddComponent<UIBFriendFocusItem>().InitItem(dataList[i], true);
                            myFocusList.Add(Item.GetComponent<UIBFriendFocusItem>());
                        }
                    }
                }
                else
                {
                    if (dataList == null)
                    {
                        for (int i = 0; i < myFocusList.Count; i++)
                        {
                            Destroy(myFocusList[i].gameObject);
                            myFocusList.Clear();
                            return;
                        }
                    }
                    //先判断预制体数量
                    if (dataList.Count <= myFocusList.Count)
                    {
                        for (int i = 0; i < (myFocusList.Count - dataList.Count); i++)
                        {
                            Destroy(myFocusList[i].gameObject);
                            myFocusList.RemoveAt(i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < (dataList.Count - myFocusList.Count); i++)
                        {
                            GameObject Item = Instantiate(ItemPrefab, ContentOne);
                            Item.SetActive(true);
                            Item.AddComponent<UIBFriendFocusItem>();
                            myFocusList.Add(Item.GetComponent<UIBFriendFocusItem>());
                        }
                    }
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        if (myFocusList[i] != null)
                        {
                            myFocusList[i].InitItem(dataList[i], true);
                        }
                    }
                }
            }
            else
            {
                dataList = DataMgr.Instance.attentionListRes.attentionToMeList;
                if (FocusMeList == null)
                {
                    FocusMeList = new List<UIBFriendFocusItem>();
                    if (dataList != null)
                    {
                        for (int i = 0; i < dataList.Count; i++)
                        {
                            GameObject Item = Instantiate(ItemPrefab, ContentTwo);
                            Item.SetActive(true);
                            Item.AddComponent<UIBFriendFocusItem>().InitItem(dataList[i], false);
                            FocusMeList.Add(Item.GetComponent<UIBFriendFocusItem>());
                        }
                    }
                }
                else
                {
                    if (dataList == null)
                    {
                        for (int i = 0; i < FocusMeList.Count; i++)
                        {
                            Destroy(FocusMeList[i].gameObject);
                            FocusMeList.Clear();
                            return;
                        }
                    }
                    if (dataList.Count <= FocusMeList.Count)
                    {
                        for (int i = 0; i < (FocusMeList.Count - dataList.Count); i++)
                        {
                            Destroy(FocusMeList[i].gameObject);
                            FocusMeList.RemoveAt(i);
                        }
                    }
                    else
                    {
                        GameObject Item = Instantiate(ItemPrefab, ContentTwo);
                        Item.SetActive(true);
                        Item.AddComponent<UIBFriendFocusItem>();
                        FocusMeList.Add(Item.GetComponent<UIBFriendFocusItem>());
                    }
                    for (int i = 0; i < dataList.Count; i++)
                    {
                        if (FocusMeList[i] != null)
                        {
                            FocusMeList[i].InitItem(dataList[i], false);
                        }
                    }
                }
            }


            //List<AttentionToMeList> data1 = DataMgr.Instance.attentionListRes.meAttentionList;
            //List<AttentionToMeList> data2 = DataMgr.Instance.attentionListRes.attentionToMeList;

            //第一次创建，后面更新数据

            //if (isFirst)
            //{
            //    isFirst = false;
            //    if (data1 != null)
            //    {
            //        for (int i = 0; i < data1.Count; i++)
            //        {
            //            GameObject Item = Instantiate(ItemPrefab, ContentOne);
            //            Item.SetActive(true);
            //            Item.AddComponent<UIBFriendFocusItem>().InitItem(data1[i], true);
            //            myFocusList.Add(Item.GetComponent<UIBFriendFocusItem>());
            //        }
            //    }

            //    if (data2 != null)
            //    {
            //        for (int i = 0; i < data2.Count; i++)
            //        {
            //            GameObject Item = Instantiate(ItemPrefab, ContentTwo);
            //            Item.SetActive(true);
            //            Item.AddComponent<UIBFriendFocusItem>().InitItem(data2[i], false);
            //            FocusMeList.Add(Item.GetComponent<UIBFriendFocusItem>());
            //        }
            //    }
            //}
            //else
            //{
            //    //刷新数据
            //    //刷新时要判断两个List的数量，进行增加和删除
            //    if (data1 != null)
            //    {
            //        //先判断预制体数量
            //        if (data1.Count <= myFocusList.Count)
            //        {
            //            for (int i = 0; i < (myFocusList.Count - data1.Count); i++)
            //            {
            //                Destroy(myFocusList[i].gameObject);
            //                myFocusList.RemoveAt(i);
            //            }
            //        }
            //        else
            //        {
            //            for (int i = 0; i < (data1.Count - myFocusList.Count); i++)
            //            {
            //                GameObject Item = Instantiate(ItemPrefab, ContentOne);
            //                Item.SetActive(true);
            //                Item.AddComponent<UIBFriendFocusItem>();
            //                myFocusList.Add(Item.GetComponent<UIBFriendFocusItem>());
            //            }
            //        }
            //        for (int i = 0; i < data1.Count; i++)
            //        {
            //            if (myFocusList[i] != null)
            //            {
            //                myFocusList[i].InitItem(data1[i], true);
            //            }
            //        }
            //    }
            //else
            //{
            //    for (int i = 0; i < myFocusList.Count; i++)
            //    {
            //        Destroy(myFocusList[i].gameObject);
            //        myFocusList.Clear();
            //    }
            //}
            //if (data2 != null)
            //{
            //    if (data2.Count <= FocusMeList.Count)
            //    {
            //        for (int i = 0; i < (FocusMeList.Count - data2.Count); i++)
            //        {
            //            Destroy(FocusMeList[i].gameObject);
            //            FocusMeList.RemoveAt(i);
            //        }
            //    }
            //    else
            //    {
            //        GameObject Item = Instantiate(ItemPrefab, ContentTwo);
            //        Item.SetActive(true);
            //        Item.AddComponent<UIBFriendFocusItem>();
            //        FocusMeList.Add(Item.GetComponent<UIBFriendFocusItem>());
            //    }
            //    for (int i = 0; i < data2.Count; i++)
            //    {
            //        if (FocusMeList[i] != null)
            //        {
            //            FocusMeList[i].InitItem(data2[i], false);
            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < FocusMeList.Count; i++)
            //    {
            //        Destroy(FocusMeList[i].gameObject);
            //        FocusMeList.Clear();
            //    }
            //}
        }
        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_AttentionList, S2C_Friend_AttentionListCallBack);
            myFocusList?.Clear();
            FocusMeList?.Clear();
        }
    }
}



