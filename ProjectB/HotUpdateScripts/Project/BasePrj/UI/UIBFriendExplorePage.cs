using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;
using System.Collections;

namespace My.UI.Panel
{
    public class UIBFriendExplorePage : FriendPageBase
    {
        private Text num_pipei, num_cishu;
        private RectTransform Ani_xin;
        private GameObject btn_AddNum, ItemPrefab;
        private GameObject filterPopWin;
        private Button btn_EnsureBtn;
        private Slider maxSlider, minSlider;
        private Toggle allAreaToggle, zhengzhouToggle;
        private Text txt_AgeRange;
        public static List<GameObject> ItemPool = new List<GameObject>(); //对象池
        Queue<UserFriend> OriginData = new Queue<UserFriend>();
        //使用交友卡弹窗
        private GameObject pan_UseDaoJu, PanContent;
        private Text pan_MaxNum;
        private Button pan_Btn_Submit, pan_Btn_Cancel, pan_btn_Close;
        bool isGetData = false; //是否拿到数据   如果 没有拿到数据，点击匹配 不提示信息

        public static System.Action act_UserCard;

        MatchingConditionUserReq conditionUserReq;

        const string MatchingCityKey = "MatchingCity";//0不限 1同城

        bool effectPlaying = false;

        public override void InitPage()
        {
            if (HasInitPage) return;

            num_pipei = GameTools.GetByName(transform, "num_pipei").GetComponent<Text>();
            num_cishu = GameTools.GetByName(transform, "num_cishu").GetComponent<Text>();
            Ani_xin = GameTools.GetByName(transform, "Ani_xin").GetComponent<RectTransform>();
            filterPopWin = GameTools.GetByName(transform.parent, "FilterPopWin");
            filterPopWin.SetActive(false);

            minSlider = GameTools.GetByName(filterPopWin, "SliderMin").GetComponent<Slider>();
            maxSlider = GameTools.GetByName(filterPopWin, "SliderMax").GetComponent<Slider>();
            allAreaToggle = GameTools.GetByName(filterPopWin, "AllBtn").GetComponent<Toggle>();
            zhengzhouToggle = GameTools.GetByName(filterPopWin, "ZhengzhouBtn").GetComponent<Toggle>();
            minSlider.minValue = 18;
            minSlider.maxValue = 60;
            maxSlider.minValue = 18;
            maxSlider.maxValue = 60;
            btn_EnsureBtn = GameTools.GetByName(filterPopWin, "EnsureBtn").GetComponent<Button>();
            txt_AgeRange = GameTools.GetByName(filterPopWin, "AgeRange").GetComponent<Text>();



            ItemPrefab = GameTools.GetByName(transform, "ItemPrefab");
            ItemPrefab.SetActive(false);
            ItemPrefab.transform.parent.localScale = ItemPrefab.transform.parent.localScale * (Screen.height / 1920f) * 1.2f;

            pan_UseDaoJu = GameTools.GetByName(transform, "pan_UseDaoJu");
            pan_UseDaoJu.SetActive(false);
            PanContent = GameTools.GetByName(transform, "PanContent");
            pan_MaxNum = GameTools.GetByName(transform, "pan_MaxNum").GetComponent<Text>();
            pan_Btn_Submit = GameTools.GetByName(transform, "pan_Btn_Submit").GetComponent<Button>();
            pan_Btn_Cancel = GameTools.GetByName(transform, "pan_Btn_Cancel").GetComponent<Button>();
            pan_btn_Close = GameTools.GetByName(transform, "pan_btn_Close").GetComponent<Button>();

            act_UserCard = () =>
            {
                DataMgr.Instance.dataBUseBagCard.userId = GameData.userId;
                DataMgr.Instance.dataBUseBagCard.productId = (int)CardType.FriendCard;
                DataMgr.Instance.dataBUseBagCard.productNum = 1;
                NetMgr.Instance.C2S_BagCard_UserCard();
            };
            #region RegisterEvent
            minSlider.onValueChanged.AddListener((f) =>
            {
                conditionUserReq.minAge = (int)f;
                maxSlider.minValue = f;
                txt_AgeRange.text = string.Format("{0}—{1}", conditionUserReq.minAge, conditionUserReq.maxAge);
            });
            maxSlider.onValueChanged.AddListener((f) =>
            {
                conditionUserReq.maxAge = (int)f;
                txt_AgeRange.text = string.Format("{0}—{1}", conditionUserReq.minAge, conditionUserReq.maxAge);
            });
            allAreaToggle.onValueChanged.AddListener((p) =>
            {
                if (p)
                {
                    PlayerPrefs.SetInt(MatchingCityKey, 0);
                    conditionUserReq.city = "不限";
                }
                allAreaToggle.GetComponent<Image>().color = new Color32(22, 173, 205, 255);
                zhengzhouToggle.GetComponent<Image>().color = new Color32(63, 63, 63, 255);
            });
            zhengzhouToggle.onValueChanged.AddListener((p) =>
            {
                if (p)
                {
                    PlayerPrefs.SetInt(MatchingCityKey, 1);
                    conditionUserReq.city = "同城";
                }
                allAreaToggle.GetComponent<Image>().color = new Color32(63, 63, 63, 255);
                zhengzhouToggle.GetComponent<Image>().color = new Color32(22, 173, 205, 255);
            });
            btn_EnsureBtn.onClick.AddListener(() =>
            {
                DataMgr.Instance.matchingConditionUserReq = this.conditionUserReq;
                NetMgr.Instance.C2S_Friend_MatchUserPool();
                filterPopWin.SetActive(false);
            });
            //使用道具按钮
            pan_Btn_Submit.onClick.AddListener(() =>
            {
                GameData.isUserCardToMatchFriend = true;
                ClosePan();
                UIMgr.Instance.Open(UIPath.UIBFriendMatchPanel);
            });

            pan_Btn_Cancel.onClick.AddListener(() =>
            {
                ClosePan();
            });

            pan_btn_Close.onClick.AddListener(() =>
            {
                ClosePan();
            });

            btn_AddNum = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_AddNum"), () =>
            {
                debug.Log_Blue("添加次数");
                if (GameData.curFreeMatchNum > 0)
                {
                    GameTools.SetTip("今日免费次数未使用完");
                }
                else
                {
                    if (DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList.Count == 0)
                    {
                        if (isGetData)
                        {
                            GameTools.SetTip("当前可匹配数量为0");
                        }
                        return;
                    }
                    //ToDo: 去使用一张交友卡
                    debug.Log_Blue("去使用一张交友卡");
                    if (GameData.GetBagCardDataByCardType(CardType.FriendCard) != null && GameData.GetBagCardDataByCardType(CardType.FriendCard).productNum > 0)
                    {
                        OpenPan();
                    }
                    else
                    {
                        GameTools.SetTip("当前交友卡已用完");
                    }
                }
            });

            num_cishu.text = GameData.curFreeMatchNum + "/" + GameData.totalFreeMatchNum;
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Filter"), () =>
            {
                filterPopWin.SetActive(true);
                conditionUserReq = DataMgr.Instance.matchingConditionUserReq;
                conditionUserReq.userId = GameData.userId;

                txt_AgeRange.text = string.Format("{0}—{1}", conditionUserReq.minAge, conditionUserReq.maxAge);
                minSlider.value = conditionUserReq.minAge;
                maxSlider.value = conditionUserReq.maxAge;
                if (PlayerPrefs.HasKey(MatchingCityKey))
                {
                    bool isSameCity = PlayerPrefs.GetInt(MatchingCityKey) == 1;
                    conditionUserReq.city = isSameCity ? "同城" : "不限";
                    allAreaToggle.isOn = !isSameCity;
                    zhengzhouToggle.isOn = isSameCity;
                }
                else
                {
                    int value = conditionUserReq.city == "不限" ? 0 : 1;
                    PlayerPrefs.SetInt(MatchingCityKey, value);
                    allAreaToggle.isOn = value == 0;
                    zhengzhouToggle.isOn = value == 1;
                }

            });
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Match"), () =>
             {
                 if (DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList.Count == 0)
                 {
                     if (isGetData)
                     {
                         GameTools.SetTip("当前可匹配数量为0");
                     }
                     return;
                 }
                 if (GameData.curFreeMatchNum > 0)
                 {
                     UIMgr.Instance.Open(UIPath.UIBFriendMatchPanel);
                 }
                 else
                 {
                     //ToDo: 去使用一张交友卡
                     if (GameData.GetBagCardDataByCardType(CardType.FriendCard) != null && GameData.GetBagCardDataByCardType(CardType.FriendCard).productNum > 0)
                     {
                         OpenPan();
                     }
                     else
                     {
                         GameTools.SetTip("当前交友卡已用完\n请到兑换商城或幸运小屋获得");
                     }
                 }
             });

            PigPingPong(Ani_xin, Ani_xin.localPosition.y, Ani_xin.localPosition.y + 50f);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_MatchUserPool, S2C_Friend_MatchUserPoolCallBack);

            //更新匹配次数数据
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);


            #endregion


            base.InitPage();
        }

        public override void OpenPage()
        {
            base.OpenPage();
        }

        //更新数据
        void S2C_Update_UpdateGameDataCallBack(object o)
        {
            num_cishu.text = GameData.curFreeMatchNum + "/" + GameData.totalFreeMatchNum;
        }

        //匹配池服务器回调
        void S2C_Friend_MatchUserPoolCallBack(object o)
        {
            //StopCoroutine(GenerateItem());
            OriginData = new Queue<UserFriend>();
            if (DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList != null)
            {
                debug.Log_yellow(DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList.Count);
                for (int i = 0; i < DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList.Count; i++)
                {
                    OriginData.Enqueue(DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList[i]);
                }
            }
            debug.Log_Blue("可匹配数量： " + OriginData.Count);
            isGetData = true;

            if (OriginData.Count > 0 && !effectPlaying)
            {
                effectPlaying = true;
                InvokeRepeating("GenerateItem", 0, 3);
                //StartCoroutine(GenerateItem());
            }
            //InvokeRepeating("setRandomPiPeiNum", 0, Random.Range(15, 20));

            num_pipei.text = OriginData.Count.ToString();
        }

        WaitForSeconds wait = new WaitForSeconds(3);

        void GenerateItem()
        {
            if (OriginData.Count > 0 && gameObject.activeSelf)
            {
                GameObject Item = GetItemFromPool();
                Item.SetActive(true);
                Item.transform.SetAsLastSibling();
                //每次从数据池中随机选择1——3个
                int randomNum = Random.Range(1, 4);
                Item.AddComponent<UIBFriendExpolerItem>().InitItem(randomNum, GetDataListByOriginData(randomNum));
            }
        }

        //交友大厅 探索 人数刷新 频率15-25秒  
        //void setRandomPiPeiNum()
        //{
        //    num_pipei.text = (DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList.Count).ToString();//+ Random.Range(150, 200)
        //}

        List<UserFriend> randomDataList;
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="num"></param>
        List<UserFriend> GetDataListByOriginData(int num)
        {
            randomDataList = new List<UserFriend>();

            for (int i = 0; i < num; i++)
            {
                if (OriginData.Count > 0)
                {
                    var item = OriginData.Dequeue();
                    randomDataList.Add(item);
                    OriginData.Enqueue(item);
                }
            }
            return randomDataList;
        }

        GameObject GetItemFromPool()
        {
            GameObject obj = null;
            //从池子里取出
            if (ItemPool != null && ItemPool.Count > 0)
            {
                for (int i = 0; i < ItemPool.Count; i++)
                {
                    if (!ItemPool[i].activeSelf)
                    {
                        return ItemPool[i];
                    }
                }
            }
            obj = Instantiate(ItemPrefab, ItemPrefab.transform.parent);
            ItemPool.Add(obj);
            return obj;
        }

        private void PigPingPong(RectTransform trans, float from, float to)
        {
            trans.DOLocalMoveY(to, 2f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    PigPingPong(trans, to, from);
                }
                );
        }

        //打开使用交友卡弹窗
        void OpenPan()
        {
            pan_MaxNum.text = GameData.GetBagCardDataByCardType(CardType.FriendCard).productNum.ToString();
            pan_UseDaoJu.SetActive(true);
            DOTweenMgr.Instance.OpenWindowScale(PanContent, .3f);
        }

        //关闭弹窗
        void ClosePan(System.Action func = null)
        {
            DOTweenMgr.Instance.CloseWindowScale(PanContent, 0.2f, () =>
            {
                pan_UseDaoJu.SetActive(false);
                PanContent.transform.localScale = new Vector3(1, 1, 1);
                if (func != null)
                {
                    func();
                }
            });
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_MatchUserPool, S2C_Friend_MatchUserPoolCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, S2C_Update_UpdateGameDataCallBack);
            ItemPool.Clear();
            CancelInvoke();
            //StopCoroutine(GenerateItem());
        }
    }
}
