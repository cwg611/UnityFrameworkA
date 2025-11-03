using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using My.Msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    /// <summary>
    /// 查看资料页面（个人&好友）
    /// </summary>
    public class UIBFriendProfilePanel : BasePanel
    {
        UserFriendData data;
        UserRealInfo userRealInfo;
        //Top
        private Image img_Icon, img_SexIcon;
        private Text txt_NickName, txt_TopAge, txt_TopAddress, txt_Signature;
        private RectTransform TopInfo;
        private RectTransform SignatureRect;//签名
        private Toggle toggle_DatingInfo, toggle_RealInfo;//ToggleRealInfo, ToggleDatingInfo;
        private GameObject PersonalInfoGB, RealInfoGB;//个人介绍 认证信息
        private ScrollRect RealinfoScrollRect;
        private RectTransform RefreshContent, PersonalInfoView, MatingInfoView, RealInfoView;

        private ItemList CharacterItemList, ApperanceItemList, MyItemList,
            YCharacterItemList, YApperanceItemList, YourItemList;

        //DatingInfoView
        private Text txt_Age, txt_Height, txt_Weight, txt_Edu, txt_House, txt_Car, txt_Animal, txt_Constellation, txt_Area;

        private ImgQiehuan QieHuan_Sex;
        private bool isFirstOpenPage = true;
        //MatingInfoView
        private Text txt_ExpAge, txt_ExpHeight, txt_ExpWeight, txt_ExpEdu, txt_ExpHouse, txt_ExpCar;
        //RealInfoView
        private Text txt_Identity, txt_Marriage, txt_RealName, txt_Phone,
            txt_JobNum, txt_NativePlace, txt_Job, txt_College, txt_Profession;
        private Image img_Photo;
        private GameObject RealInfoMask;

        private GameObject Btn_Focus; //关注按钮
        private GameObject Btn_Chat; //聊天按钮

        private bool isFocus;

        private Text txt_Focus;

        //private GameObject ProfilePage;

        public override void InitPanel(object o)
        {
            #region INIT
            //Top
            var topTransform = GameTools.GetByName(transform, "Top").transform;
            img_Icon = GameTools.GetByName(topTransform, "Icon").GetComponent<Image>();
            txt_NickName = GameTools.GetByName(topTransform, "Name").GetComponent<Text>();
            img_SexIcon = GameTools.GetByName(topTransform, "SexIcon").GetComponent<Image>();
            txt_TopAge = GameTools.GetByName(topTransform, "Age").GetComponent<Text>();
            txt_TopAddress = GameTools.GetByName(topTransform, "Address").GetComponent<Text>();
            txt_Focus = GameTools.GetByName(topTransform, "FocusText").GetComponent<Text>();
            TopInfo = GameTools.GetByName(topTransform, "TopInfo").GetComponent<RectTransform>();

            QieHuan_Sex = GameTools.GetByName(transform, "QieHuan_Sex").GetComponent<ImgQiehuan>();

            SignatureRect = GameTools.GetByName(transform, "Signature").GetComponent<RectTransform>();
            txt_Signature = GameTools.GetByName(transform, "SignatureText").GetComponent<Text>();
            //
            toggle_DatingInfo = GameTools.GetByName(transform, "ToggleDatingInfo").GetComponent<Toggle>();
            toggle_RealInfo = GameTools.GetByName(transform, "ToggleRealInfo").GetComponent<Toggle>();
            PersonalInfoGB = GameTools.GetByName(transform, "DatingInfomation");
            RealInfoGB = GameTools.GetByName(transform, "RealInfomation");
            RealinfoScrollRect = RealInfoGB.GetComponent<ScrollRect>();
            RealinfoScrollRect.enabled = false;
            PersonalInfoGB.SetActive(true);
            RealInfoGB.SetActive(false);

            PersonalInfoView = GameTools.GetByName(transform, "PersonalInfoView").GetComponent<RectTransform>();
            RefreshContent = GameTools.GetByName(PersonalInfoGB.transform, "Content").GetComponent<RectTransform>();
            txt_Age = GameTools.GetByName(PersonalInfoView, "AgeText").GetComponent<Text>();
            txt_Height = GameTools.GetByName(PersonalInfoView, "HeightText").GetComponent<Text>();
            txt_Weight = GameTools.GetByName(PersonalInfoView, "WeightText").GetComponent<Text>();
            txt_Edu = GameTools.GetByName(PersonalInfoView, "EduText").GetComponent<Text>();
            txt_House = GameTools.GetByName(PersonalInfoView, "HouseText").GetComponent<Text>();
            txt_Car = GameTools.GetByName(PersonalInfoView, "CarText").GetComponent<Text>();
            txt_Animal = GameTools.GetByName(PersonalInfoView, "AnimalText").GetComponent<Text>();
            txt_Area = GameTools.GetByName(PersonalInfoView, "AreaText").GetComponent<Text>();
            txt_Constellation = GameTools.GetByName(transform, "ConstellationText").GetComponent<Text>();
            //标签数据
            var constRectOffet = new RectOffset(225, 20, 11, -65);
            float constSpaceX = 16f, constSpaceY = 16.23f;
            //我的标签
            var LayOutObj = GameTools.GetByName(PersonalInfoView, "CharacterLayOut");
            UIExtenFlowLayoutGroup CharacterLayOut = LayOutObj.AddComponent<UIExtenFlowLayoutGroup>();
            CharacterLayOut.SpacingX = constSpaceX;
            CharacterLayOut.SpacingY = constSpaceY;
            CharacterLayOut.padding = constRectOffet;
            CharacterLayOut.CalculateLayoutInputVertical();
            CharacterLayOut.SetLayoutVertical();
            CharacterItemList = LayOutObj.AddComponent<ItemList>();
            LayOutObj = GameTools.GetByName(PersonalInfoView, "ApperanceLayOut");
            UIExtenFlowLayoutGroup ApperanceLayOut = LayOutObj.AddComponent<UIExtenFlowLayoutGroup>();
            ApperanceLayOut.SpacingX = constSpaceX;
            ApperanceLayOut.SpacingY = constSpaceY;
            ApperanceLayOut.padding = constRectOffet;
            ApperanceLayOut.CalculateLayoutInputVertical();
            ApperanceLayOut.SetLayoutVertical();
            ApperanceItemList = LayOutObj.AddComponent<ItemList>();

            LayOutObj = GameTools.GetByName(PersonalInfoView, "MyLayOut");
            UIExtenFlowLayoutGroup Lay_Mylable = LayOutObj.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_Mylable.SpacingX = constSpaceX;
            Lay_Mylable.SpacingY = constSpaceY;
            Lay_Mylable.padding = constRectOffet;
            Lay_Mylable.CalculateLayoutInputVertical();
            Lay_Mylable.SetLayoutVertical();
            MyItemList = LayOutObj.AddComponent<ItemList>();

            //MatingInfo
            MatingInfoView = GameTools.GetByName(transform, "MatingInfoView").GetComponent<RectTransform>();
            txt_ExpAge = GameTools.GetByName(MatingInfoView, "AgeText").GetComponent<Text>();
            txt_ExpHeight = GameTools.GetByName(MatingInfoView, "HeightText").GetComponent<Text>();
            txt_ExpWeight = GameTools.GetByName(MatingInfoView, "WeightText").GetComponent<Text>();
            txt_ExpEdu = GameTools.GetByName(MatingInfoView, "EduText").GetComponent<Text>();
            txt_ExpHouse = GameTools.GetByName(MatingInfoView, "HouseText").GetComponent<Text>();
            txt_ExpCar = GameTools.GetByName(MatingInfoView, "CarText").GetComponent<Text>();
            //TA的标签
            LayOutObj = GameTools.GetByName(MatingInfoView, "CharacterLayOut");
            UIExtenFlowLayoutGroup YCharacterLayOut = LayOutObj.AddComponent<UIExtenFlowLayoutGroup>();
            YCharacterLayOut.SpacingX = constSpaceX;
            YCharacterLayOut.SpacingY = constSpaceY;
            YCharacterLayOut.padding = constRectOffet;
            YCharacterLayOut.CalculateLayoutInputVertical();
            YCharacterLayOut.SetLayoutVertical();
            YCharacterItemList = LayOutObj.AddComponent<ItemList>();

            LayOutObj = GameTools.GetByName(MatingInfoView, "ApperanceLayOut");
            UIExtenFlowLayoutGroup YApperanceLayOut = LayOutObj.AddComponent<UIExtenFlowLayoutGroup>();
            YApperanceLayOut.SpacingX = constSpaceX;
            YApperanceLayOut.SpacingY = constSpaceY;
            YApperanceLayOut.padding = constRectOffet;
            YApperanceLayOut.CalculateLayoutInputVertical();
            YApperanceLayOut.SetLayoutVertical();
            YApperanceItemList = LayOutObj.AddComponent<ItemList>();

            LayOutObj = GameTools.GetByName(MatingInfoView, "YourLayOut");
            UIExtenFlowLayoutGroup Lay_Youlable = LayOutObj.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_Youlable.SpacingX = 9.95f;
            Lay_Youlable.SpacingY = 16.23f;
            Lay_Youlable.padding = new RectOffset(350, 20, 11, -65);
            Lay_Youlable.CalculateLayoutInputVertical();
            Lay_Youlable.SetLayoutVertical();
            YourItemList = LayOutObj.AddComponent<ItemList>();

            //RealInfo
            RealInfoView = GameTools.GetByName(transform, "RealInfoView").GetComponent<RectTransform>();
            txt_Identity = GameTools.GetByName(RealInfoView, "IdentityText").GetComponent<Text>();
            txt_Marriage = GameTools.GetByName(RealInfoView, "MarriageText").GetComponent<Text>();
            txt_RealName = GameTools.GetByName(RealInfoView, "NameText").GetComponent<Text>();
            txt_Phone = GameTools.GetByName(RealInfoView, "PhoneText").GetComponent<Text>();
            txt_JobNum = GameTools.GetByName(RealInfoView, "JobNumText").GetComponent<Text>();
            txt_NativePlace = GameTools.GetByName(RealInfoView, "NativePlaceText").GetComponent<Text>();
            txt_Job = GameTools.GetByName(RealInfoView, "JobText").GetComponent<Text>();
            txt_College = GameTools.GetByName(RealInfoView, "CollegeText").GetComponent<Text>();
            txt_Profession = GameTools.GetByName(RealInfoView, "ProfessionText").GetComponent<Text>();
            img_Photo = GameTools.GetByName(RealInfoView, "PhotoImage").GetComponent<Image>();
            RealInfoMask = GameTools.GetByName(RealInfoGB.transform, "RealInfoMask");
            #endregion

            #region Event
            GameObject toggleChooseOne = toggle_DatingInfo.transform.GetChild(0).gameObject;
            GameObject toggleChooseTwo = toggle_RealInfo.transform.GetChild(0).gameObject;
            toggle_DatingInfo.onValueChanged.AddListener((p) =>
            {
                toggleChooseOne.SetActive(p);
                toggleChooseTwo.SetActive(!p);
                PersonalInfoGB.SetActive(p);
                RealInfoGB.SetActive(!p);
            });
            toggle_RealInfo.onValueChanged.AddListener((p) =>
            {
                if (p && userRealInfo == null)
                {
                    NetMgr.Instance.C2S_User_ViewRealInfo(GameData.curFriendUserId);
                }
                toggleChooseOne.SetActive(!p);
                toggleChooseTwo.SetActive(p);
                PersonalInfoGB.SetActive(!p);
                RealInfoGB.SetActive(p);
            });

            GameTools.Instance.AddClickEvent(img_Icon.gameObject, () =>
            {
                if (img_Icon.mainTexture == null) return;
                GameData.bigImg = img_Icon.mainTexture;
                UIMgr.Instance.Open(UIPath.UIBBigImgPanel);
            });

            Btn_Focus = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_Focus"), () =>
            {
                debug.Log_Blue("---->> 点击关注");
                if (isFocus)
                {
                    //取消关注
                    DataMgr.Instance.UnfollowToUserReq.userId = GameData.userId;
                    DataMgr.Instance.UnfollowToUserReq.toAttentionUser = GameData.curFriendUserId;
                    NetMgr.Instance.C2S_Friend_UnFollowToUser();
                }
                else
                {
                    //去关注
                    DataMgr.Instance.followToUserReq.userId = GameData.userId;
                    DataMgr.Instance.followToUserReq.toAttentionUser = GameData.curFriendUserId;
                    NetMgr.Instance.C2S_Friend_FollowToUser();
                }
            });

            Btn_Chat = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_Chat"), () =>
            {
                debug.Log_Blue("---->> 点击私信");
                debug.Log_purple(GameData.isLookAttentionProfile);
                if (GameData.isLookAttentionProfile)
                {
                    debug.Log_Blue("打开聊天界面");
                    GameData.curFriendId = GameData.curFriendUserId;
                    GameData.curFriendName = data.nickName;
                    GameData.curFriendRemark = data.remark;
                    GameData.curFriendHeadImg = data.headImgUrl;
                    UIMgr.Instance.Open(UIPath.UIBFriendChatPanel);
                }
                ClosePage();
            });

            GameTools.GetByName(transform, "btn_Close").GetComponent<Button>().onClick.AddListener(() =>
            {
                ClosePage();
            });

            //关注
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_FollowToUser, S2C_Friend_FollowToUserCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_UnFollowToUser, S2C_Friend_UnFollowToUserCallBack);
            #endregion
            if (GameData.curFriendUserId == GameData.userId)
            {
                Btn_Focus.SetActive(false);
                Btn_Chat.SetActive(false);
            }
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ViewFriendInfo, S2C_Friend_ViewFriendInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_User_ViewRealInfo, S2C_User_ViewRealInfoCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Attention_UserInfo, S2C_Attention_UserInfoCallBack);

            //关注好友的资料
            if (GameData.isLookAttentionProfile)
            {
                DataMgr.Instance.attentionUserInfoReq.userId = GameData.curFriendUserId;
                NetMgr.Instance.C2S_Attention_UserInfo();
                //NetMgr.Instance.C2S_User_ViewRealInfo(GameData.curFriendUserId);

            }
            else//陌生好友的资料
            {
                DataMgr.Instance.viewPersonalInfoReq.userId = GameData.userId;
                DataMgr.Instance.viewPersonalInfoReq.viewUserId = GameData.curFriendUserId;
                NetMgr.Instance.C2S_Friend_ViewFriendInfo();
                //NetMgr.Instance.C2S_User_ViewRealInfo(GameData.curFriendUserId);
            }

        }

        void ClosePage()
        {
            //DOTweenMgr.Instance.CloseWindowScale(ProfilePage.gameObject, 0.2f, () =>
            //{
            GameData.isLookAttentionProfile = false;
            UIMgr.Instance.Close(UIPath.UIBFriendProfilePanel);
            // });
        }

        //关注回调
        void S2C_Friend_FollowToUserCallBack(object o)
        {
            txt_Focus.text = "已关注";
            isFocus = true;
            QieHuan_Sex.SetImg(3, Btn_Focus);
            //刷新我的资料关注数量
            DataMgr.Instance.viewPersonalInfoReq.userId = GameData.userId;
            DataMgr.Instance.viewPersonalInfoReq.viewUserId = GameData.userId;
            NetMgr.Instance.C2S_Friend_ViewPersonalInfo();
        }

        //取消关注回调
        void S2C_Friend_UnFollowToUserCallBack(object o)
        {
            txt_Focus.text = "关 注";
            isFocus = false;
            QieHuan_Sex.SetImg(2, Btn_Focus);
            //刷新我的资料关注数量
            DataMgr.Instance.viewPersonalInfoReq.userId = GameData.userId;
            DataMgr.Instance.viewPersonalInfoReq.viewUserId = GameData.userId;
            NetMgr.Instance.C2S_Friend_ViewPersonalInfo();
        }

        void S2C_Friend_ViewFriendInfoCallBack(object o)
        {
            data = DataMgr.Instance.dataBViewFriendInfoRes.userFriendData;
            SetView(data);
        }

        void S2C_Attention_UserInfoCallBack(object o)
        {
            data = DataMgr.Instance.dataBViewAttentionInfoRes.userInfo;
            SetView(data);
        }

        private void SetView(UserFriendData data)
        {
            if (data == null) data = new UserFriendData();

            txt_NickName.text = data.nickName;
            QieHuan_Sex.SetImg(data.gender, img_SexIcon.gameObject, true);
            txt_TopAge.text = data.age + "岁";
            txt_TopAddress.text = string.IsNullOrEmpty(data.province) ? "河南省" : data.province;
            txt_Area.text = data.province;
            //个性签名
            if (string.IsNullOrEmpty(data.personalizedSignature)) txt_Signature.text = "<Color=#a2bcc8>这个人很个性，没有签名</Color>";
            txt_Signature.text = data.personalizedSignature;
            //设置背景图及条的高度
            txt_Signature.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            if (!string.IsNullOrEmpty(data.personalizedSignature))
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(SignatureRect);
            }

            txt_Age.text = data.age + "岁";
            txt_Height.text = data.height + "cm";
            txt_Weight.text = data.weight + "kg";
            txt_Edu.text = GameData.GetUserEducation(data.education);
            txt_House.text = data.house == 1 ? "是" : "否";
            txt_Car.text = data.car == 1 ? "是" : "否";
            txt_Animal.text = data.zodiac;
            txt_Constellation.text = data.constellation;
            txt_Area.text = data.province + " " + data.city;

            txt_ExpAge.text = data.expectAge + "岁";
            txt_ExpHeight.text = data.expectHeight + "cm";
            txt_ExpWeight.text = data.expectWeight + "kg";
            txt_ExpEdu.text = GameData.GetUserEducation(data.expectEducation);
            txt_ExpHouse.text = data.expectHouse == 1 ? "是" : "否";
            txt_ExpCar.text = data.expectCar == 1 ? "是" : "否";

            //头像
            if (data.userId == GameData.userId && GameData.userHeadSprite != null)
            {
                img_Icon.sprite = GameData.userHeadSprite;
                GameTools.Instance.MatchImgBySprite(img_Icon);
            }
            else
            {
                NetMgr.Instance.DownLoadHeadImg(r =>
                {
                    if (img_Icon == null) return;
                    img_Icon.sprite = r;
                    //GameTools.Instance.MatchImgBySprite(img_Icon);
                }, data.headImgUrl);
            }

            //是否关注
            isFocus = (data.isBeFollowed == 1);
            txt_Focus.text = isFocus ? "已关注" : "关 注";
            QieHuan_Sex.SetImg(isFocus ? 3 : 2, Btn_Focus);

            SetLablesView();//生成标签
            Invoke("RefreshLsyOut", 0.2f);
        }
        /// <summary>
        /// 查看好友真实信息
        /// </summary>
        /// <param name="o"></param>
        private void S2C_User_ViewRealInfoCallBack(object o)
        {
            RealinfoScrollRect.enabled = false;//认证信息禁用
            UserRealInfo data = userRealInfo = o as UserRealInfo;
            if (data == null)
            {
                return;
            }
            //RealInfoMask.SetActive(data.allowViewRealInfo == 2);
            //RealinfoScrollRect.enabled = data.allowViewRealInfo == 1 || data.userId == GameData.userId;
            RealInfoMask.SetActive(false);
            RealinfoScrollRect.enabled = true;
            txt_Identity.text = "内部用户";
            txt_Marriage.text = "未婚";
            txt_RealName.text = data.userName;
            txt_Phone.text = data.tel;
            txt_JobNum.text = data.jobNo;
            txt_NativePlace.text = data.address;
            txt_Job.text = data.dutyName;
            txt_College.text = data.school;
            txt_Profession.text = data.education;
            //照片
            NetMgr.Instance.DownLoadImg(r =>
            {
                if (img_Photo == null) return;
                img_Photo.sprite = r;
                GameTools.Instance.MatchImgBySprite(img_Icon);
            }, data.pic);
            Invoke("RefreshRealInfoLsyOut", 0.2f);
        }

        void RefreshLsyOut()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(TopInfo);
            LayoutRebuilder.ForceRebuildLayoutImmediate(PersonalInfoView);
            LayoutRebuilder.ForceRebuildLayoutImmediate(MatingInfoView);
            LayoutRebuilder.ForceRebuildLayoutImmediate(RefreshContent);
        }

        //认证信息页面
        void RefreshRealInfoLsyOut()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(RealInfoView);
        }

        void SetLablesView()
        {
            SetLableItemList(data.character, CharacterItemList);
            SetLableItemList(data.appearance, ApperanceItemList);
            SetLableItemList(data.mineLabel, MyItemList);
            SetLableItemList(data.expectCharacter, YCharacterItemList);
            SetLableItemList(data.expectAppearance, YApperanceItemList);
            SetLableItemList(data.expectLabel, YourItemList);
        }
        string[] lables;
        private void SetLableItemList(string lableStr, ItemList itemList)
        {
            lables = GetLables(lableStr);
            if (lables != null)
            {
                itemList.Reset();
                for (int i = 0; i < lables.Length; i++)
                {
                    itemList.Refresh(i, lables[i], (p, o) =>
                    {
                        o.transform.GetChild(0).GetComponent<Text>().text = p;
                    });
                }
            }
        }
        string[] GetLables(string lable)
        {
            if (string.IsNullOrEmpty(lable)) return null;
            string[] lables = lable.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            return lables;
        }

        public override void ReleasePanel()
        {
            userRealInfo = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ViewFriendInfo, S2C_Friend_ViewFriendInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_User_ViewRealInfo, S2C_User_ViewRealInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Attention_UserInfo, S2C_Attention_UserInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_FollowToUser, S2C_Friend_FollowToUserCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_UnFollowToUser, S2C_Friend_UnFollowToUserCallBack);
        }
    }
}
