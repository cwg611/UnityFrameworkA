using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using My.Msg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBFriendChatPanel : BasePanel
    {

        ChatPanelData chatPanelData;
        public IrregularTableView tableView;
        public RectTransform tableViewTrans;

        private RectTransform Top, Bottom;
        private Text txt_NikeName;
        private InputField Input;

        private Button btn_Return, btn_Send, btn_Set, btn_Look, btn_EditorName, btn_ExchangeInfo, btn_Block;
        private GameObject Pan_Set;

        private Coroutine matchContent;

        private string inputStr = "";

        private DataFriendViewChatRecord ChatRecordData = new DataFriendViewChatRecord();//聊天记录Data

        private List<ChatMessages> ChatMessages;//聊天记录信息

        private bool isFirstGeneral = true;

        public static Action<string> UpdateRemark;

        List<string> patten = new List<string>() { @"\p{Cs}" };

        private string FilterEmoji(string str)
        {
            for (int i = 0; i < patten.Count; i++)
            {
                str = Regex.Replace(str, patten[i], "");//屏蔽emoji   
            }
            return str;
        }

        //-------------
        private Button btn_EmojiOne, btn_EmojiTwo;
        private RectTransform ScrollViewOne, ScrollViewTwo;
        private GameObject text_Input;
        Vector2 start_delta;
        private Dictionary<string, EmojiInfo> m_EmojiIndexDict, m_EmojiIndexDict1;

        private GameObject emojiTxtPrefab, emojiTxtPrefabTwo;

        public static Font defaultFont;
        public static Material defaultMaterial, defaultMaterial2;

        private RectTransform EmojiContent, EmojiContentTwo;

        private Button btn_ContentOne, btn_ContentTwo;

        private bool IsFirstGenerateTwo = true;

        private float BottomHeight;

        UIExtenEmojiText emojitext_Input;


        public override void InitPanel(object o)
        {
            #region InitView
            GameData.isOpenChatPage = true;
            //DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .2f, TweenCallBack);
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3(Screen.width, 0, 0), new Vector3(0, 0, 0), .2f, TweenCallBack);

            tableView = GameTools.GetByName(transform, "ChatScrollView").AddComponent<IrregularTableView>();
            tableViewTrans = tableView.rectTransform();
            tableView.CreateItemCallBack = (item) =>
            {
                var chatItem = item.AddComponent<UIBFriendChatItem>();
                chatItem.InitView();
                return chatItem;
            };
            tableView.SetCellCallback = (cell, index) =>
            {
                cell.SetData(ChatMessages[index]);
            };

            //Top
            Top = GameTools.GetByName(transform, "Top").transform.rectTransform();
            btn_Return = GameTools.GetByName(Top, "btn_return").GetComponent<Button>();
            btn_Set = GameTools.GetByName(Top, "btn_set").GetComponent<Button>();
            txt_NikeName = GameTools.GetByName(Top, "nikeName").GetComponent<Text>();
            txt_NikeName.text = string.IsNullOrEmpty(GameData.curFriendRemark) ?
            GameData.curFriendName : GameData.curFriendRemark;
            UpdateRemark = (s) =>
            {
                GameData.curFriendRemark = string.IsNullOrEmpty(s) ? "" : s;
                txt_NikeName.text = string.IsNullOrEmpty(s) ? GameData.curFriendName : s;
            };
            //Pan_Set
            Pan_Set = GameTools.GetByName(transform, "Pan_Set");
            Pan_Set.SetActive(false);
            btn_Look = GameTools.GetByName(Pan_Set.transform, "btn_Look").GetComponent<Button>();
            btn_EditorName = GameTools.GetByName(Pan_Set.transform, "btn_EditorName").GetComponent<Button>();
            btn_ExchangeInfo = GameTools.GetByName(Pan_Set.transform, "btn_ExchangeInfo").GetComponent<Button>();
            btn_Block = GameTools.GetByName(Pan_Set.transform, "btn_Block").GetComponent<Button>();
            /*
            //
            ScrollRectTrans = GameTools.GetByName(transform, "Scroll View").transform.rectTransform();
            ScrollRect = ScrollRectTrans.GetComponent<ScrollRect>();
            Content = GameTools.GetByName(ScrollRectTrans, "Content").transform.rectTransform();
            FriendItem = GameTools.GetByName(ScrollRectTrans, "FriendItem").transform.rectTransform();
            SelfItem = GameTools.GetByName(ScrollRectTrans, "SelfItem").transform.rectTransform();
            FriendItemTwo = GameTools.GetByName(ScrollRectTrans, "FriendItemTwo").transform.rectTransform();
            SelfItemTwo = GameTools.GetByName(ScrollRectTrans, "SelfItemTwo").transform.rectTransform();
            FriendItem.gameObject.SetActive(false);
            SelfItem.gameObject.SetActive(false);
            */

            //Bottom
            Bottom = GameTools.GetByName(transform, "Bottom").transform.rectTransform();
            btn_Send = GameTools.GetByName(Bottom, "btn_send").GetComponent<Button>();
            Input = GameTools.GetByName(Bottom, "Input").GetComponent<InputField>();
            ScrollViewOne = GameTools.GetByName(Bottom, "ScrollViewOne").transform.rectTransform();
            ScrollViewTwo = GameTools.GetByName(Bottom, "ScrollViewTwo").transform.rectTransform();
            var rectTransform = GameTools.GetByName(Bottom, "BottomHeight").transform.rectTransform();
            BottomHeight = rectTransform.sizeDelta.y;
            btn_EmojiOne = GameTools.GetByName(Bottom, "btn_EmojiOne").GetComponent<Button>();
            btn_EmojiTwo = GameTools.GetByName(Bottom, "btn_EmojiTwo").GetComponent<Button>();
            btn_EmojiOne.gameObject.SetActive(true);
            btn_EmojiTwo.gameObject.SetActive(false);
            btn_ContentOne = GameTools.GetByName(Bottom, "btn_ContentOne").GetComponent<Button>();
            btn_ContentTwo = GameTools.GetByName(Bottom, "btn_ContentTwo").GetComponent<Button>();

            EmojiContent = GameTools.GetByName(Bottom, "EmojiContent").GetComponent<RectTransform>();
            EmojiContentTwo = GameTools.GetByName(Bottom, "EmojiContentTwo").GetComponent<RectTransform>();

            //表情设置
            defaultFont = GameTools.GetByName(transform, "Text").GetComponent<Text>().font;
            //defaultFont = Resources.GetBuiltinResource<Font>("Arial.ttf");
            emojiTxtPrefab = GameTools.GetByName(transform, "emojiTxtPrefab");
            emojiTxtPrefabTwo = GameTools.GetByName(transform, "emojiTxtPrefabTwo");

            text_Input = GameTools.GetByName(transform, "text_Input");
            RectTransform rect = text_Input.GetComponent<RectTransform>();
            start_delta = rect.sizeDelta;
            emojitext_Input = text_Input.AddComponent<UIExtenEmojiText>();
            emojitext_Input.font = defaultFont;
            emojitext_Input.fontStyle = FontStyle.Normal;
            emojitext_Input.fontSize = 46;
            emojitext_Input.color = Color.black;
            emojitext_Input.lineSpacing = 1.6f;
            emojitext_Input.alignment = TextAnchor.MiddleLeft;
            emojitext_Input.supportRichText = true;
            defaultMaterial = JResource.LoadRes<Material>("UI_Emoji1.mat", JResource.MatchMode.Material);
            emojitext_Input.material = defaultMaterial;
            defaultMaterial2 = JResource.LoadRes<Material>("UI_Emoji2.mat", JResource.MatchMode.Material);
            GenerateEmojiOne();
            #endregion
            #region InitEvent

            btn_Set.onClick.AddListener(() =>
            {
                if (ChatRecordData.blackList == 5)
                {
                    GameTools.SetTip("您已被拉黑");
                    return;
                }
                Pan_Set.SetActive(!Pan_Set.activeSelf);
            });
            btn_Return.onClick.AddListener(() =>
            {
                //关闭界面时向服务器发送  此人消息状态已读
                DataMgr.Instance.updateChatMessageStateReq.userId = GameData.userId;
                DataMgr.Instance.updateChatMessageStateReq.friendId = GameData.curFriendId;
                NetMgr.Instance.C2S_Friend_UpdateChatMessageState();
                S2C_Friend_UpdateChatMessageStateCallBack(null);
            });
            //监听输入结束
            Input.onEndEdit.AddListener(s =>
            {
                inputStr = FilterEmoji(s);
                emojitext_Input.text = inputStr;
                if (rect.sizeDelta.x < emojitext_Input.preferredWidth)
                {
                    rect.sizeDelta = new Vector2(emojitext_Input.preferredWidth, rect.sizeDelta.y);
                }
                if (emojitext_Input.preferredWidth < rect.sizeDelta.x && rect.sizeDelta.x > start_delta.x)
                {
                    rect.sizeDelta = new Vector2(emojitext_Input.preferredWidth, rect.sizeDelta.y);

                }
                if (emojitext_Input.preferredWidth <= start_delta.x)
                {
                    rect.sizeDelta = start_delta;
                }
            });
            //发送消息按钮
            btn_Send.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(inputStr) || inputStr.Trim().Length == 0) return;
                if (ChatRecordData.blackList == 5)
                {
                    GameTools.SetTip("您已被拉黑");
                    return;
                }
                bool filtered = false;
                if (GameData.SensitiveWordFilter != null)
                {
                    filtered = GameData.SensitiveWordFilter.SensitiveWordsReplace(ref inputStr);
                }

                ChatMessages chatMessages = new ChatMessages();
                chatMessages.toUserId = GameData.curFriendId;
                chatMessages.fromUserId = GameData.userId;
                chatMessages.sendTime = GameTools.TmGetDateTime(TimeMgr.Instance.serverTime).ToString("yyyy-MM-dd HH:mm:ss");
                chatMessages.messageType = MessageType.commonType;
                chatMessages.messageInfo = inputStr;
                DataMgr.Instance.dataSendMessageReq = chatMessages;
                ChatMessages.Add(chatMessages);

                //发送消息
                NetMgr.Instance.C2S_Friend_SendMessage();
                if (filtered)//敏感词提醒消息
                {
                    chatMessages = new ChatMessages();
                    chatMessages.toUserId = GameData.curFriendId;
                    chatMessages.fromUserId = GameData.userId;
                    chatMessages.sendTime = GameTools.TmGetDateTime(TimeMgr.Instance.serverTime).ToString("yyyy-MM-dd HH:mm:ss");
                    chatMessages.messageType = MessageType.SensitiveRemind;
                    //chatMessages.messageInfo = inputStr;
                    DataMgr.Instance.dataSendMessageReq = chatMessages;
                    ChatMessages.Add(chatMessages);
                    NetMgr.Instance.C2S_Friend_SendMessage();
                }
                tableView.Load(ChatMessages.Count);
                Input.text = "";
                inputStr = "";
                emojitext_Input.text = inputStr;
            });


            btn_EmojiOne.onClick.AddListener(() =>
            {
                Bottom.localPosition = new Vector3(Bottom.localPosition.x, Bottom.localPosition.y + BottomHeight);
                tableViewTrans.sizeDelta = new Vector2(tableViewTrans.sizeDelta.x, tableViewTrans.sizeDelta.y - BottomHeight);
                //tableView.GetComponent<ScrollRect>().normalizedPosition = new Vector2(0, 0);
                tableView.Load(ChatMessages.Count);
                btn_EmojiOne.gameObject.SetActive(false);
                btn_EmojiTwo.gameObject.SetActive(true);
            });
            btn_EmojiTwo.onClick.AddListener(() =>
            {
                SetBottomRetract();
            });

            btn_ContentOne.onClick.AddListener(() =>
            {
                btn_ContentOne.transform.parent.GetComponent<Image>().enabled = true;
                btn_ContentTwo.transform.parent.GetComponent<Image>().enabled = false;
                ScrollViewOne.transform.localPosition = new Vector3(0, ScrollViewOne.transform.localPosition.y, 0);
                ScrollViewTwo.transform.localPosition = new Vector3(1200, ScrollViewTwo.transform.localPosition.y, 0);
            });
            btn_ContentTwo.onClick.AddListener(() =>
            {
                if (IsFirstGenerateTwo)
                {
                    IsFirstGenerateTwo = false;
                    GenerateEmojiTwo();
                }
                btn_ContentOne.transform.parent.GetComponent<Image>().enabled = false;
                btn_ContentTwo.transform.parent.GetComponent<Image>().enabled = true;
                ScrollViewOne.transform.localPosition = new Vector3(1200, ScrollViewOne.transform.localPosition.y, 0);
                ScrollViewTwo.transform.localPosition = new Vector3(0, ScrollViewTwo.transform.localPosition.y, 0);
            });
            tableView.GetComponent<Button>().onClick.AddListener(() =>
            {
                SetBottomRetract();
            });

            btn_Look.onClick.AddListener(() =>
            {
                Pan_Set.SetActive(!Pan_Set.activeSelf);
                if (ChatRecordData.blackList == 5)
                {
                    GameTools.SetTip("您已被拉黑");
                    return;
                }
                GameData.curFriendUserId = GameData.curFriendId;
                UIMgr.Instance.Open(UIPath.UIBFriendProfilePanel);
            });
            btn_EditorName.onClick.AddListener(() =>
            {
                Pan_Set.SetActive(!Pan_Set.activeSelf);
                if (ChatRecordData.blackList == 5)
                {
                    GameTools.SetTip("您已被拉黑");
                    return;
                }
                GameData.curFriendUserId = GameData.curFriendId;
                UIMgr.Instance.Open(UIPath.UIBFriendEditorNotePanel);
            });
            //好友拉黑
            btn_Block.onClick.AddListener(() =>
            {
                if (ChatRecordData.blackList == 5)
                {
                    GameTools.SetTip("您已被拉黑");
                    return;
                }
                CommonPopWinPanel.Instance.ShowPopOne("确定要将好友拉黑？", okAction: () =>
                {
                    NetMgr.Instance.C2S_Friend_PullInOrPullOutBlackList(GameData.userId, GameData.curFriendId, 5);
                    Pan_Set.SetActive(!Pan_Set.activeSelf);
                    UIMgr.Instance.Close(UIPath.UIBFriendChatPanel);
                });
            });
            //btn_ExchangeInfo.interactable = userFriendData.AllowApplyExchangeInfo;//不允许交换
            btn_ExchangeInfo.interactable = true;//点击之后禁用
            //交换认证信息
            btn_ExchangeInfo.onClick.AddListener(() =>
            {
                if (ChatRecordData.blackList == 5)
                {
                    GameTools.SetTip("您已被拉黑");
                    return;
                }
                if (chatPanelData.intimacy < 100)
                {
                    GameTools.SetTip("亲密度不足，无法交换认证信息");
                    return;
                }
                if (chatPanelData.allowViewRealInfo == 1)
                {
                    GameTools.SetTip("信息已经交换过啦");
                    return;
                }
                if (chatPanelData.AllowApplyExchangeInfo)
                {
                    CommonPopWinPanel.Instance.ShowPopOne("你请求和" + GameData.curFriendName + "交换认证信息", ok: "勇敢示爱", cancle: "我再想想",
                        okAction: () =>
                        {
                            NetMgr.Instance.C2S_Friend_ApplyViewUserRealInfo(GameData.userId, GameData.curFriendId);
                            Pan_Set.SetActive(!Pan_Set.activeSelf);
                            btn_ExchangeInfo.interactable = false;//点击之后禁用
                        }, popWinType: PopWinType.ExchangeRealInfo);
                }
                else
                {
                    GameTools.SetTip("正在等待对方处理");
                }
            });
            Pan_Set.GetComponent<Button>().onClick.AddListener(() =>
            {
                Pan_Set.SetActive(!Pan_Set.activeSelf);
            });
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ApplyViewUserRealInfo, S2C_Friend_ApplyViewUserRealInfo);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ProcessViewUserRealInfoApply, S2C_Friend_ProcessViewUserRealInfoApply);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ViewChatRecord, S2C_Friend_ViewChatRecordCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_SendMessage, S2C_Friend_SendMessageCallBack); //监听接收消息
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_UpdateChatMessageState, S2C_Friend_UpdateChatMessageStateCallBack); //修改聊天消息状态
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Attention_OpenPrivateLetter, S2C_Attention_OpenPrivateLetter); //修改聊天消息状态
            #endregion



            if (!string.IsNullOrEmpty(GameData.curFriendHeadImg))
            {
                NetMgr.Instance.DownLoadHeadImg(r =>
                {
                    GameData.chatFriendHeadSprite = r;
                }, GameData.curFriendHeadImg);

            }
        }

        void SetBottomRetract()
        {
            if (btn_EmojiTwo.IsActive())
            {
                Bottom.localPosition = new Vector3(Bottom.localPosition.x, Bottom.localPosition.y - BottomHeight);
                tableViewTrans.sizeDelta = new Vector2(tableViewTrans.sizeDelta.x, tableViewTrans.sizeDelta.y + BottomHeight);
                tableView.Load(ChatMessages.Count);
                btn_EmojiOne.gameObject.SetActive(true);
                btn_EmojiTwo.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 生成小表情图
        /// </summary>
        void GenerateEmojiTwo()
        {
            if (m_EmojiIndexDict == null)
            {
                m_EmojiIndexDict = new Dictionary<string, EmojiInfo>();
                TextAsset emojiContent = JResource.LoadRes<TextAsset>("emoji1.txt", JResource.MatchMode.Other);
                string[] lines = emojiContent.text.Split('\n');
                for (int i = 1; i < lines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(lines[i]))
                    {
                        string[] strs = lines[i].Split('\t');
                        EmojiInfo info;
                        info.name = strs[0];
                        info.x = float.Parse(strs[3]);
                        info.y = float.Parse(strs[4]);
                        info.size = float.Parse(strs[5]);
                        m_EmojiIndexDict.Add(strs[1], info);
                    }
                }
                foreach (var item in m_EmojiIndexDict)
                {
                    UIExtenEmojiText Item = Instantiate(emojiTxtPrefabTwo.gameObject, EmojiContentTwo.transform).AddComponent<UIExtenEmojiText>();
                    Item.gameObject.SetActive(true);
                    Item.TxtIndex = 1;
                    Item.font = defaultFont;
                    Item.fontStyle = FontStyle.Normal;
                    Item.fontSize = 86;
                    Item.color = Color.black;
                    Item.lineSpacing = 1.6f;
                    Item.alignment = TextAnchor.MiddleLeft;
                    Item.supportRichText = true;
                    Item.material = defaultMaterial;
                    Item.raycastTarget = false;
                    Item.text = item.Key;

                    Item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
                    {
                        inputStr += item.Key;
                        Input.text = inputStr;
                        emojitext_Input.text = inputStr;
                    });
                }
                LayoutRebuilder.ForceRebuildLayoutImmediate(EmojiContentTwo);
            }
        }

        //void LoadBigEmoji()
        //{
        //    for (int i = 0; i < length; i++)
        //    {
        //        JResource.LoadResAsync<TextAsset>("emoji2.txt", (s) =>
        //        {
        //        });
        //        }
        //}

        //加载并生成大表情图
        void GenerateEmojiOne()
        {
            if (defaultMaterial2 == null) return;

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
                    CreatEmojis();
                });
            }
            else
            {
                CreatEmojis();
            }
        }

        void CreatEmojis()
        {
            foreach (var item in GameData.BigEmojiDic)
            {
                UIExtenEmojiText1 Item = Instantiate(emojiTxtPrefab.gameObject, EmojiContent.transform).AddComponent<UIExtenEmojiText1>();
                //Item.m_EmojiIndexDict = m_EmojiIndexDict1;
                Item.gameObject.SetActive(true);
                Item.font = defaultFont;
                Item.fontStyle = FontStyle.Normal;
                Item.fontSize = 200;
                Item.color = Color.black;
                Item.lineSpacing = 1.6f;
                Item.alignment = TextAnchor.MiddleLeft;
                Item.supportRichText = true;
                Item.material = defaultMaterial2;
                Item.raycastTarget = false;
                Item.text = item.Key;
                string str = item.Value.name;
                str = str.Replace("[", "");
                str = str.Replace("]", "");
                Item.transform.GetChild(1).GetComponent<Text>().text = str;
                Item.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
                {
                    ChatMessages chatMessages = new ChatMessages();
                    chatMessages.toUserId = GameData.curFriendId;
                    chatMessages.fromUserId = GameData.userId;
                    chatMessages.sendTime = GameTools.TmGetDateTime(TimeMgr.Instance.serverTime).ToString("yyyy-MM-dd HH:mm:ss");
                    chatMessages.messageType = MessageType.ImageTypeOne;
                    chatMessages.messageInfo = Item.text;

                    DataMgr.Instance.dataSendMessageReq = chatMessages;
                    //发送消息
                    NetMgr.Instance.C2S_Friend_SendMessage();
                    ChatMessages.Add(chatMessages);
                    tableView.Load(ChatMessages.Count);
                });
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(EmojiContent);
        }

        void TweenCallBack()
        {
            NetMgr.Instance.C2S_Attention_OpenPrivateLetter();
            //获取聊天记录
            DataMgr.Instance.dataViewChatRecordReq.userId = GameData.userId;
            DataMgr.Instance.dataViewChatRecordReq.friendId = GameData.curFriendId;
            NetMgr.Instance.C2S_Friend_ViewChatRecord();
        }

        /// <summary>
        /// 获取聊天记录回调
        /// </summary>
        /// <param name="o"></param>
        void S2C_Friend_ViewChatRecordCallBack(object o)
        {
            ChatRecordData = DataMgr.Instance.dataFriendViewChatRecord;
            ChatMessages = ChatRecordData.chatMessages == null ? new List<ChatMessages>() : ChatRecordData.chatMessages;
            tableView.Load(ChatMessages.Count);
        }

        /// <summary>
        /// 接收消息回调
        /// </summary>
        /// <param name="o"></param>
        void S2C_Friend_SendMessageCallBack(object o)
        {
            ChatMessages data = DataMgr.Instance.dataSendMessage;

            if (data.fromUserId == GameData.curFriendId&&data.toUserId== GameData.userId)
            {
                ChatMessages.Add(data);
                tableView.Load(ChatMessages.Count);
            }
        }
        const string Intimacy100PopKey = "Intimacy100Pop";
        void S2C_Attention_OpenPrivateLetter(object o)
        {
            if (o != null) chatPanelData = o as ChatPanelData;
            if (chatPanelData.AllowApplyExchangeInfo)
            {
                if (!PlayerPrefs.HasKey(Intimacy100PopKey))
                {
                    PlayerPrefs.SetString(Intimacy100PopKey, GameData.curFriendId.ToString() + ",");
                    CommonPopWinPanel.Instance.ShowPopTwo(string.Format("您和{0}亲密度已达100\n(现在可以发起“交换信息”交换联系方式)", txt_NikeName.text), okAction: () =>
                    {


                    }, popWinType: PopWinType.IntimacyPop);
                }
                else
                {
                    string signStr = PlayerPrefs.GetString(Intimacy100PopKey);
                    debug.Log_yellow(signStr);
                    var signArray = signStr.Split(',').ToList();
                    if (!signArray.Contains(GameData.curFriendId.ToString()))
                    {
                        signStr += GameData.curFriendId.ToString() + ",";
                        PlayerPrefs.SetString(Intimacy100PopKey, signStr);
                        CommonPopWinPanel.Instance.ShowPopTwo(string.Format("您和{0}亲密度已达100\n(现在可以发起“交换信息”交换联系方式)", txt_NikeName.text), okAction: () =>
                        {

                        }, popWinType: PopWinType.ExchangeRealInfo);
                    }
                }
            }
        }
        /// <summary>
        /// 修改聊天状态回调
        /// </summary>
        /// <param name="o"></param>
        void S2C_Friend_UpdateChatMessageStateCallBack(object o)
        {
            //DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .1f, () =>
            //{
            //    if (!GameData.isOpenFocusListPanel)
            //    {
            //        UIBFriendHomePanel.SwitchPage.Invoke();
            //    }
            //    UIMgr.Instance.Close(UIPath.UIBFriendChatPanel);
            //});
            if (!GameData.isOpenFocusListPanel)
            {
                UIBFriendHomePanel.SwitchFriendPageAction.Invoke();
            }
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3(0, 0, 0), new Vector3(Screen.width, 0, 0), .1f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBFriendChatPanel);
            });
        }

        //处理认证交换请求
        void S2C_Friend_ApplyViewUserRealInfo(object o)
        {
            ExchangeInfoMessageData dataBFriend = o as ExchangeInfoMessageData;
            if (dataBFriend.applicantId == GameData.curFriendId)//当前好友展示弹窗，否则刷新好友列表
            {
                CommonPopWinPanel.Instance.ShowPopOne("请求和你交换认证信息？", ok: "欣然接受", cancle: "委婉拒绝", okAction: () =>
                    {
                        NetMgr.Instance.C2S_Friend_ProcessViewUserRealInfoApply(GameData.userId, GameData.curFriendId, 1);
                        NetMgr.Instance.C2S_Attention_OpenPrivateLetter();
                    }, cancleAction: () =>
                    {
                        NetMgr.Instance.C2S_Friend_ProcessViewUserRealInfoApply(GameData.userId, GameData.curFriendId, 2);
                        NetMgr.Instance.C2S_Attention_OpenPrivateLetter();
                    }, popWinType: PopWinType.ExchangeRealInfo);
            }
        }

        void S2C_Friend_ProcessViewUserRealInfoApply(object o)
        {
            ExchangeInfoMessageData dataBFriend = o as ExchangeInfoMessageData;
            if (dataBFriend.processId == GameData.curFriendId)
            {
                if (dataBFriend.processState == 1)
                {
                    CommonPopWinPanel.Instance.ShowPopTwo("对方已同意您的申请", okAction: () =>
                    {
                        NetMgr.Instance.C2S_Friend_ConfirmUserRealInfoApply(GameData.userId, GameData.curFriendId);
                    }, popWinType: PopWinType.ExchangeRealInfo);
                }
                else if (dataBFriend.processState == 2)
                {
                    CommonPopWinPanel.Instance.ShowPopTwo("对方拒绝了您的申请", okAction: () =>
                    {
                        NetMgr.Instance.C2S_Friend_ConfirmUserRealInfoApply(GameData.userId, GameData.curFriendId);
                    }, popWinType: PopWinType.ExchangeRealInfo);
                }
            }
            NetMgr.Instance.C2S_Attention_OpenPrivateLetter();
        }

        public override void ReleasePanel()
        {
            GameData.isOpenChatPage = false;
            //刷新聊天列表
            GameData.ResetFriendListPage = false;
            NetMgr.Instance.C2S_Friend_ChatRelationList(GameData.userId.ToString());
            GameData.isMatchToChat = false;
            GameData.curFriendRemark = null;
            GameData.chatFriendHeadSprite = null;
            if (matchContent != null) matchContent = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ApplyViewUserRealInfo, S2C_Friend_ApplyViewUserRealInfo);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ProcessViewUserRealInfoApply, S2C_Friend_ProcessViewUserRealInfoApply);

            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ViewChatRecord, S2C_Friend_ViewChatRecordCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_SendMessage, S2C_Friend_SendMessageCallBack); //监听接收消息
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_UpdateChatMessageState, S2C_Friend_UpdateChatMessageStateCallBack); //修改聊天消息状态
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Attention_OpenPrivateLetter, S2C_Attention_OpenPrivateLetter); //修改聊天消息状态
        }
    }
}


