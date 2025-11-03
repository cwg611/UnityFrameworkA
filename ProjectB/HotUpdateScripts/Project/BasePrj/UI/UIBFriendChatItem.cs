using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    /// <summary>
    /// 聊天信息Item
    /// </summary>
    public class UIBFriendChatItem : IrregularTableViewCell
    {
        private Image img_HeadIcon;
        private RectTransform rect_MyChatBg, rect_FriendChatBg, rect_MyChatText, rect_FriendChatText;
        private ContentSizeFitter fit_MyChatText, fit_FriendChatFit;
        private long m_userId;
        private Text txt_CommonMeaasge;

        UIExtenEmojiText txt_MyMessage, txt_FriendMessage;

        UIExtenEmojiText1 txt_Emoji;//大表情

        LayoutElement layout_Emoji, layout_MyText, layout_FriendText, layout_CommonText;//子物体能否被控制

        VerticalLayoutGroup layoutGroup;

        public void InitView()
        {
            layoutGroup = GetComponent<VerticalLayoutGroup>();
            img_HeadIcon = GameTools.GetByName(transform, "HeadIcon").GetComponent<Image>();

            rect_MyChatBg = GameTools.GetByName(transform, "MyChatBg").GetComponent<RectTransform>();
            rect_FriendChatBg = GameTools.GetByName(transform, "FriendChatBg").GetComponent<RectTransform>();
            layout_MyText = rect_MyChatBg.GetComponent<LayoutElement>();
            layout_FriendText = rect_FriendChatBg.GetComponent<LayoutElement>();

            txt_CommonMeaasge = GameTools.GetByName(transform, "CommonText").GetComponent<Text>();
            layout_CommonText = txt_CommonMeaasge.GetComponent<LayoutElement>();

            GameObject myChatTextGo = GameTools.GetByName(transform, "MyChatText");
            rect_MyChatText = myChatTextGo.GetComponent<RectTransform>();
            fit_MyChatText = myChatTextGo.GetComponent<ContentSizeFitter>();
            txt_MyMessage = myChatTextGo.AddComponent<UIExtenEmojiText>();


            GameObject friendChatTextGo = GameTools.GetByName(transform, "FriendChatText");
            rect_FriendChatText = friendChatTextGo.GetComponent<RectTransform>();
            fit_FriendChatFit = friendChatTextGo.GetComponent<ContentSizeFitter>();
            txt_FriendMessage = friendChatTextGo.AddComponent<UIExtenEmojiText>();

            GameObject emojiGo = GameTools.GetByName(transform, "EmojiText");
            txt_Emoji = emojiGo.GetComponent<UIExtenEmojiText1>();
            if (txt_Emoji == null) txt_Emoji = emojiGo.AddComponent<UIExtenEmojiText1>();

            layout_Emoji = emojiGo.GetComponent<LayoutElement>();

            img_HeadIcon.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameData.curFriendUserId = m_userId;
                UIMgr.Instance.Open(UIPath.UIBFriendProfilePanel);
            });

            if (txt_MyMessage != null)
            {
                txt_MyMessage.font = UIBFriendChatPanel.defaultFont;
                txt_MyMessage.fontStyle = FontStyle.Normal;
                txt_MyMessage.fontSize = 46;
                txt_MyMessage.color = new Color(75f / 255, 105f / 255, 119f / 255, 1);
                txt_MyMessage.lineSpacing = 1.4f;
                txt_MyMessage.alignment = TextAnchor.MiddleLeft;
                txt_MyMessage.supportRichText = true;
                txt_MyMessage.material = UIBFriendChatPanel.defaultMaterial;
            }
            if (txt_FriendMessage != null)
            {
                txt_FriendMessage.font = UIBFriendChatPanel.defaultFont;
                txt_FriendMessage.fontStyle = FontStyle.Normal;
                txt_FriendMessage.fontSize = 46;
                txt_FriendMessage.color = new Color(75f / 255, 105f / 255, 119f / 255, 1);
                txt_FriendMessage.lineSpacing = 1.4f;
                txt_FriendMessage.alignment = TextAnchor.MiddleLeft;
                txt_FriendMessage.supportRichText = true;
                txt_FriendMessage.material = UIBFriendChatPanel.defaultMaterial;
            }
            if (txt_Emoji != null)
            {
                txt_Emoji.font = UIBFriendChatPanel.defaultFont;
                txt_Emoji.fontStyle = FontStyle.Normal;
                txt_Emoji.fontSize = 300;
                txt_Emoji.alignment = TextAnchor.MiddleLeft;
                txt_Emoji.material = UIBFriendChatPanel.defaultMaterial2;
            }
        }

        string headUrl = "";
        ChatMessages data;
        public override void SetData(object mdata)
        {
            base.SetData(mdata);
            data = mdata as ChatMessages;

            SetItemView(data.fromUserId, data.messageType);

            if (data.messageType == MessageType.SensitiveRemind)
            {
                if (data.fromUserId == GameData.userId)//自己
                {
                    txt_CommonMeaasge.text = "你发送的消息包含违规内容，请注意言行。";
                }
                else
                {
                    txt_CommonMeaasge.text = "对方发送消息可能存在违规内容，你可以举报或拉黑对方。";
                }
            }
            else if (data.messageType == MessageType.SystemMeaasge)
            {
                txt_CommonMeaasge.text = data.messageInfo;
            }
            else
            {
                if (data.fromUserId == GameData.userId)//自己
                {
                    m_userId = GameData.userId;
                    //头像
                    if (GameData.userHeadSprite != null)
                    {
                        img_HeadIcon.sprite = GameData.userHeadSprite;
                    }

                    if (data.messageType == MessageType.commonType)
                    {
                        if (txt_MyMessage != null)
                        {
                            txt_MyMessage.font = UIBFriendChatPanel.defaultFont;
                            txt_MyMessage.fontStyle = FontStyle.Normal;
                            txt_MyMessage.fontSize = 46;
                            txt_MyMessage.color = new Color(75f / 255, 105f / 255, 119f / 255, 1);
                            txt_MyMessage.lineSpacing = 1.41f;
                            txt_MyMessage.alignment = TextAnchor.MiddleLeft;
                            txt_MyMessage.supportRichText = true;
                        }
                        txt_MyMessage.text = data.messageInfo;

                        fit_MyChatText.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                        fit_MyChatText.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                        fit_MyChatText.SetLayoutHorizontal();
                        if (rect_MyChatText.transform.GetComponent<RectTransform>().sizeDelta.x >= 624)
                        {
                            rect_MyChatText.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(624, rect_MyChatText.transform.GetComponent<RectTransform>().sizeDelta.y);
                            fit_MyChatText.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                            fit_MyChatText.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                        }
                        fit_MyChatText.SetLayoutHorizontal();
                        fit_MyChatText.SetLayoutVertical();
                    }
                    else
                    {
                        if (txt_Emoji != null)
                        {
                            txt_Emoji.font = UIBFriendChatPanel.defaultFont;
                            txt_Emoji.fontStyle = FontStyle.Normal;
                            txt_Emoji.fontSize = 300;
                            txt_Emoji.alignment = TextAnchor.MiddleLeft;
                        }
                        txt_Emoji.text = data.messageInfo;
                    }
                }
                else
                {
                    m_userId = GameData.curFriendId;
                    headUrl = GameData.curFriendHeadImg;
                    if (GameData.chatFriendHeadSprite != null)
                    {
                        img_HeadIcon.sprite = GameData.chatFriendHeadSprite;
                    }
                    else
                    {
                        NetMgr.Instance.DownLoadHeadImg(r =>
                        {
                            if (img_HeadIcon == null) return;
                            img_HeadIcon.sprite = r;
                            //GameTools.Instance.MatchImgBySprite(img_HeadIcon);
                        }, headUrl);
                    }

                    if (data.messageType == MessageType.commonType || data.messageType == 0)
                    {
                        if (txt_FriendMessage != null)
                        {
                            txt_FriendMessage.font = UIBFriendChatPanel.defaultFont;
                            txt_FriendMessage.fontStyle = FontStyle.Normal;
                            txt_FriendMessage.fontSize = 46;
                            txt_FriendMessage.color = new Color(75f / 255, 105f / 255, 119f / 255, 1);
                            txt_FriendMessage.lineSpacing = 1.4f;
                            txt_FriendMessage.alignment = TextAnchor.MiddleLeft;
                            txt_FriendMessage.supportRichText = true;
                        }
                        txt_FriendMessage.text = data.messageInfo;

                        fit_FriendChatFit.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                        fit_FriendChatFit.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                        fit_FriendChatFit.SetLayoutHorizontal();
                        if (rect_FriendChatText.transform.GetComponent<RectTransform>().sizeDelta.x >= 624)
                        {
                            rect_FriendChatText.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(624, rect_FriendChatText.transform.GetComponent<RectTransform>().sizeDelta.y);
                            fit_FriendChatFit.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                            fit_FriendChatFit.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                        }
                        fit_FriendChatFit.SetLayoutHorizontal();
                        fit_FriendChatFit.SetLayoutVertical();
                    }
                    else
                    {
                        if (txt_Emoji != null)
                        {
                            txt_Emoji.font = UIBFriendChatPanel.defaultFont;
                            txt_Emoji.fontStyle = FontStyle.Normal;
                            txt_Emoji.fontSize = 300;
                            txt_Emoji.alignment = TextAnchor.MiddleLeft;
                        }
                        txt_Emoji.text = data.messageInfo;
                    }
                }
            }



            //然后刷新
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect_MyChatBg);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect_FriendChatBg);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect_MyChatText);
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect_FriendChatText);
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        private void SetItemView(long fromUserId, MessageType messageType)
        {
            bool isCurrentUser = fromUserId == GameData.userId;
            bool isEmojiMessage = messageType == MessageType.ImageTypeOne;

            //commonMessage
            if (data.messageType == MessageType.SensitiveRemind || data.messageType == MessageType.SystemMeaasge)
            {
                img_HeadIcon.transform.localScale = Vector3.zero;
                layoutGroup.childAlignment = TextAnchor.MiddleCenter;
                layoutGroup.padding = new RectOffset(0, 0, 0, 50);
                layout_CommonText.transform.localScale = Vector3.one;
                layout_CommonText.ignoreLayout = false;
                layout_Emoji.ignoreLayout = true;
                layout_MyText.ignoreLayout = true;
                layout_FriendText.ignoreLayout = true;
                rect_FriendChatBg.transform.localScale = Vector3.zero;
                rect_MyChatBg.localScale = Vector3.zero;
                txt_Emoji.transform.localScale = Vector3.zero;
            }
            else
            {
                img_HeadIcon.transform.localScale = Vector3.one;
                layout_CommonText.ignoreLayout = true;
                layout_CommonText.transform.localScale = Vector3.zero;
                if (isCurrentUser)//自己的信息
                {
                    layoutGroup.childAlignment = TextAnchor.UpperRight;
                    layoutGroup.padding = new RectOffset(0, 180, 0, 50);

                    img_HeadIcon.rectTransform().anchorMin = new Vector2(1, 1);
                    img_HeadIcon.rectTransform().anchorMax = new Vector2(1, 1);
                    img_HeadIcon.rectTransform().anchoredPosition = new Vector2(-90, -60);

                    layout_FriendText.ignoreLayout = true;
                    rect_FriendChatBg.transform.localScale = Vector3.zero;
                    if (isEmojiMessage)
                    {
                        layout_MyText.ignoreLayout = true;
                        rect_MyChatBg.localScale = Vector3.zero;
                        layout_Emoji.ignoreLayout = false;
                        txt_Emoji.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        layout_MyText.ignoreLayout = false;
                        rect_MyChatBg.localScale = Vector3.one;
                        layout_Emoji.ignoreLayout = true;
                        txt_Emoji.transform.localScale = Vector3.zero;
                    }
                }
                else
                {
                    layoutGroup.childAlignment = TextAnchor.UpperLeft;
                    layoutGroup.padding = new RectOffset(180, 0, 0, 50);

                    img_HeadIcon.rectTransform().anchorMin = new Vector2(0, 1);
                    img_HeadIcon.rectTransform().anchorMax = new Vector2(0, 1);
                    img_HeadIcon.rectTransform().anchoredPosition = new Vector2(90, -60);

                    layout_MyText.ignoreLayout = true;
                    rect_MyChatBg.localScale = Vector3.zero;
                    if (isEmojiMessage)
                    {
                        layout_FriendText.ignoreLayout = true;
                        rect_FriendChatBg.localScale = Vector3.zero;
                        layout_Emoji.ignoreLayout = false;
                        txt_Emoji.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        layout_FriendText.ignoreLayout = false;
                        rect_FriendChatBg.localScale = Vector3.one;
                        layout_Emoji.ignoreLayout = true;
                        txt_Emoji.transform.localScale = Vector3.zero;
                    }
                }
            }


        }

        void OnDestroy()
        {
            if (img_HeadIcon != null) img_HeadIcon.sprite = null;
        }

    }
}
