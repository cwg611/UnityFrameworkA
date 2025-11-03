using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using JEngine.Core;
using DG.Tweening;
using My.Msg;

namespace My.UI.Panel
{
    public class UIBFriendItem : MonoBehaviour
    {
        private UserFriend m_Data;
        private Image img_HeadIcon, img_OnLineIcon;
        private Text txt_Title, txt_Time, txt_UnReadNum;
        private Text uIExtenEmojiText;
        //private UIExtenEmojiText uIExtenEmojiText;
        //private GameData hand;//认证关系
        private Image img_Relation;
        private Button btn_ChatClick;
        private GameObject exchangeInfoGo;
        private GameObject UnReadBg;
        private Color color_OnLine, color_OffLine;

        private bool exchangeInfoPending;//交换信息待处理
        public bool ExchangeInfoPending { get { return exchangeInfoPending; } private set { exchangeInfoPending = value; } }
        private bool HasInit = false;

        private int TempIntimacy;
        void InitView()
        {
            img_HeadIcon = GameTools.GetByName(transform, "HeadPortrait").GetComponent<Image>();
            img_OnLineIcon = GameTools.GetByName(img_HeadIcon.transform, "onLineIcon").
                GetComponent<Image>();
            img_Relation = GameTools.GetByName(transform, "RelationImage").GetComponent<Image>();
            //btn_ExchangeInfo = GameTools.GetByName(transform, "ExchangeInfo").GetComponent<Button>();
            exchangeInfoGo = GameTools.GetByName(transform, "ExchangeInfo");
            txt_Title = GameTools.GetByName(transform, "Title").GetComponent<Text>();

            uIExtenEmojiText = GameTools.GetByName(transform, "Desc").GetComponent<Text>();
            //uIExtenEmojiText.font = txt_Title.font;
            //uIExtenEmojiText.fontStyle = FontStyle.Normal;
            //uIExtenEmojiText.fontSize = 30;
            //uIExtenEmojiText.color = Color.white;// new Color(75f / 255, 105f / 255, 119f / 255, 1);
            //uIExtenEmojiText.lineSpacing = 1;
            //uIExtenEmojiText.alignment = TextAnchor.MiddleLeft;
            //uIExtenEmojiText.material = JResource.LoadRes<Material>("UI_Emoji1.mat", JResource.MatchMode.Material);

            //            JResource.LoadResAsync<Material>("UI_Emoji1.mat",
            //(t) =>
            //{
            //    uIExtenEmojiText.material = t;
            //}, JResource.MatchMode.Material);

            btn_ChatClick = GetComponent<Button>();
            txt_Time = GameTools.GetByName(transform, "Time").GetComponent<Text>();
            txt_UnReadNum = GameTools.GetByName(transform, "UnReadNum").GetComponent<Text>();
            UnReadBg = GameTools.GetByName(transform, "UnReadBg");
            ColorUtility.TryParseHtmlString("#b1ff53", out color_OnLine);
            ColorUtility.TryParseHtmlString("#f2f2f2", out color_OffLine);

            btn_ChatClick.onClick.AddListener(()=> {

                if (ExchangeInfoPending)//交换信息待处理
                {
                    ProcessExchangeInfo();
                }
                else
                {
                    OpenChatPage();
                }
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_SendMessage, S2C_Friend_SendMessageCallBack); //监听接收消息
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_GetUnreadMessageNum, S2C_Friend_GetUnreadMessageNumCallBack); //查看好友给自己发送的未读消息数量  
            HasInit = true;
        }

        public void SetItemData(UserFriend data)
        {
            if (!HasInit) InitView();

            m_Data = data;
            //头像
            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (img_HeadIcon == null) return;
                img_HeadIcon.sprite = r;
                //GameTools.Instance.MatchImgBySprite(img_HeadIcon);
            }, data.headImgUrl);

            //昵称或备注
            txt_Title.text = string.IsNullOrEmpty(data.remark) ? data.nickName : data.remark;

            //在线状态
            img_OnLineIcon.color = data.isOnline == 1 ? color_OnLine : color_OffLine;

            if (!string.IsNullOrEmpty(data.lastMessage))
            {
                //SetTextWithEllipsis(Desc, data.lastMessage);
                //uIExtenEmojiText.text = data.lastMessage;
                if (data.messageType == (int)MessageType.commonType || data.messageType == 0)
                {
                    uIExtenEmojiText.text = data.lastMessage;
                }
                else
                {
                    uIExtenEmojiText.text = GameData.GetEmojiName("emoji2.txt", data.lastMessage);
                }
            }
            else
            {
                uIExtenEmojiText.text = "";
            }
            //时间
            txt_Time.text = data.lastMessageSendTime;
            TempIntimacy = data.intimacy;
            img_Relation.fillAmount = TempIntimacy * 0.01f;

            exchangeInfoGo.SetActive(data.allowViewRealInfo == 1);
            ExchangeInfoPending = false;

            if (data.applyState == 3
                && ((!data.isApplicat && data.processState == 0) //被申请人待处理
                || (data.isApplicat && data.processState > 0)))//申请人的待处理
            {
                exchangeInfoGo.SetActive(true);
                ExchangeInfoPending = true;
            }
            else
            {
                ExchangeInfoPending = false;
                //exchangeInfoGo.SetActive(false);
            }
            //未读数量
            if (data.unreadChatMessageNum == 0)
            {
                txt_UnReadNum.text = "";
                UnReadBg.SetActive(false);
            }
            else if (data.unreadChatMessageNum > 99)
            {
                txt_UnReadNum.text = "99+";
                UnReadBg.SetActive(true);
            }
            else
            {
                txt_UnReadNum.text = data.unreadChatMessageNum.ToString();
                UnReadBg.SetActive(true);
            }
            //LayoutRebuilder.ForceRebuildLayoutImmediate(onLineBg);
        }


        private float timer = 0;
        void Update()
        {
            if (ExchangeInfoPending && exchangeInfoGo != null)
            {
                timer += Time.deltaTime;
                if (timer >= .8f)
                {
                    timer = 0;
                    exchangeInfoGo.gameObject.SetActive(!exchangeInfoGo.activeInHierarchy);
                }
            }
        }

        void S2C_Friend_SendMessageCallBack(object o)
        {
            if (m_Data.userId == DataMgr.Instance.dataSendMessage.fromUserId)
            {
                ChatMessages chatMessage = DataMgr.Instance.dataSendMessage;
                //transform.SetAsFirstSibling();
                DataMgr.Instance.getUnreadMessageNumReq.userId = GameData.userId;
                DataMgr.Instance.getUnreadMessageNumReq.friendId = chatMessage.fromUserId;
                NetMgr.Instance.C2S_Friend_GetUnreadMessageNum();
                //刷新对应的数据
                RefreshChatMessage(chatMessage.messageInfo, chatMessage.messageType, chatMessage.sendTime);
            }
        }

        //查看好友给自己发送的未读消息数量  
        void S2C_Friend_GetUnreadMessageNumCallBack(object o)
        {
            if (m_Data.userId == DataMgr.Instance.getUnreadMessageNumRes.fromUserId)
            {
                RefreshUnReadNum(DataMgr.Instance.getUnreadMessageNumRes.UnreadMessageNum);
            }
        }

        //处理交换认证信息
        public void ProcessExchangeInfo()
        {
            if (m_Data.applyState == 3)//待处理
            {
                if (m_Data.isApplicat)//需要自己处理
                {
                    if (m_Data.processState == 1)
                    {
                        CommonPopWinPanel.Instance.ShowPopTwo("对方已同意您的申请", okAction: () =>
                        {
                            ExchangeInfoPending = false;
                            NetMgr.Instance.C2S_Friend_ConfirmUserRealInfoApply(GameData.userId, GameData.curFriendId);
                        }, popWinType: PopWinType.ExchangeRealInfo);
                    }
                    else if (m_Data.processState == 2)
                    {
                        CommonPopWinPanel.Instance.ShowPopTwo("对方拒绝了您的申请", okAction: () =>
                        {
                            ExchangeInfoPending = false;
                            NetMgr.Instance.C2S_Friend_ConfirmUserRealInfoApply(GameData.userId, GameData.curFriendId);
                        }, popWinType: PopWinType.ExchangeRealInfo);
                    }
                }
                else
                {
                    if (m_Data.processState == 0)
                    {
                        CommonPopWinPanel.Instance.ShowPopOne("请求和你交换认证信息？", ok: "欣然接受", cancle: "委婉拒绝", okAction: () =>
                        {
                            ExchangeInfoPending = false;
                            NetMgr.Instance.C2S_Friend_ProcessViewUserRealInfoApply(GameData.userId, m_Data.userId, 1);
                            exchangeInfoGo.SetActive(true);
                        }, cancleAction: () =>
                        {
                            ExchangeInfoPending = false;
                            NetMgr.Instance.C2S_Friend_ProcessViewUserRealInfoApply(GameData.userId, m_Data.userId, 2);
                            exchangeInfoGo.SetActive(false);
                        }, popWinType: PopWinType.ExchangeRealInfo);
                    }
                }
            }
        }

        /// <summary>
        /// 打开聊天界面
        /// </summary>
        public void OpenChatPage()
        {
            GameData.curFriendId = m_Data.userId;
            GameData.curFriendName = m_Data.nickName;
            GameData.curFriendRemark = m_Data.remark;
            GameData.curFriendHeadImg = m_Data.headImgUrl;
            UIMgr.Instance.Open(UIPath.UIBFriendChatPanel, m_Data);
        }

        /// <summary>
        /// 删除聊天关系
        /// </summary>
        public void SendToPb()
        {
            DataMgr.Instance.dataRemoveChatRelationReq.userId = GameData.userId;
            DataMgr.Instance.dataRemoveChatRelationReq.friendId = m_Data.userId;
            NetMgr.Instance.C2S_Friend_RemoveChatRelation();
            NetMgr.Instance.C2S_Friend_MatchUserPool();
        }

        /// <summary>
        /// 刷新消息和时间
        /// </summary>
        /// <param name="message"></param>
        /// <param name="time"></param>
        public void RefreshChatMessage(string message, MessageType Type, string time)
        {
            if (!string.IsNullOrEmpty(message))
            {
                //SetTextWithEllipsis(Desc, message);
                if (Type == MessageType.commonType || Type == 0)
                {
                    uIExtenEmojiText.text = message;
                }
                else
                {
                    uIExtenEmojiText.text = GameData.GetEmojiName("emoji2.txt", message);
                }
            }
            TempIntimacy++;
            img_Relation.fillAmount = TempIntimacy * 0.01f;
            txt_Time.text = time;
        }

        /// <summary>
        /// 刷新未读消息数量
        /// </summary>
        /// <param name="num"></param>
        private void RefreshUnReadNum(int num)
        {
            if (num == 0)
            {
                txt_UnReadNum.text = "";
                UnReadBg.SetActive(false);
            }
            else if (num > 99)
            {
                txt_UnReadNum.text = "99+";
                UnReadBg.SetActive(true);
            }
            else
            {
                txt_UnReadNum.text = num.ToString();
                UnReadBg.SetActive(true);
            }
        }

        void OnDestroy()
        {
            m_Data = null;
            TempIntimacy = 0;
            btn_ChatClick.onClick.RemoveAllListeners();
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_SendMessage, S2C_Friend_SendMessageCallBack); //监听接收消息
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_GetUnreadMessageNum, S2C_Friend_GetUnreadMessageNumCallBack); //查看好友给自己发送的未读消息数量  
        }

        void SetTextWithEllipsis(Text textComponent, string value)
        {
            var generator = new TextGenerator();
            var rectTransform = textComponent.GetComponent<RectTransform>();
            var settings = textComponent.GetGenerationSettings(rectTransform.rect.size);
            generator.Populate(value, settings);
            var characterCountVisible = generator.characterCountVisible;
            var updatedText = value;
            if (value.Length > characterCountVisible)
            {
                updatedText = value.Substring(0, characterCountVisible - 1);
                updatedText += "…";
            }
            textComponent.text = updatedText;
        }
    }
}
