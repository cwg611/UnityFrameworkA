using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using My.Msg;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    /// <summary>
    /// 交友大厅我的页面
    /// </summary>
    public class UIBFriendHomeMyPage : FriendPageBase
    {
        private bool isFirst = true;
        UserFriendData data;

        UserRealInfo userRealInfoData;
        //Top
        private Image img_Icon, img_SexIcon;
        private RectTransform TopInfo;
        private Text txt_NickName, txt_TopAge, txt_TopAddress, txt_MyFocusNum,
            txt_FocusMeNum, txt_Signature;
        private Button btn_Setting, btn_Block,btn_Suggest, btn_MyFocus, btn_FocusMe, btn_PersonalEditor, btn_ExpertEditor;
        private RectTransform SignatureRect;//签名
        private GameObject SettingView;

        private Toggle toggle_DatingInfo, toggle_RealInfo;//ToggleRealInfo, ToggleDatingInfo;
        private GameObject PersonalInfoGB, RealInfoGB;//个人介绍 认证信息
        private RectTransform RefreshContent, PersonalInfoView, MatingInfoView, RealInfoView;

        private ItemList CharacterItemList, ApperanceItemList, MyItemList,
            YCharacterItemList, YApperanceItemList, YourItemList;

        //DatingInfoView
        private Text txt_Age, txt_Height, txt_Weight, txt_Edu, txt_House, txt_Car, txt_Animal, txt_Constellation, txt_Area;
        private ImgQiehuan QieHuan_Sex;
        private bool isFirstOpenRealView = true;
        //MatingInfoView
        private Text txt_ExpAge, txt_ExpHeight, txt_ExpWeight, txt_ExpEdu, txt_ExpHouse, txt_ExpCar;
        //RealInfoView
        private Text txt_Identity, txt_Marriage, txt_RealName, txt_Phone,
            txt_JobNum, txt_NativePlace, txt_Job, txt_College, txt_Profession;
        private Image img_Photo;
        private ScrollRect DatingInfomation, RealInfomation;

        public override void InitPage()
        {
            if (HasInitPage) return;
            //Top
            var Top = GameTools.GetByName(transform, "Top").transform;
            img_Icon = GameTools.GetByName(Top, "Icon").GetComponent<Image>();
            txt_NickName = GameTools.GetByName(Top, "Name").GetComponent<Text>();
            img_SexIcon = GameTools.GetByName(Top, "SexIcon").GetComponent<Image>();
            txt_TopAge = GameTools.GetByName(Top, "Age").GetComponent<Text>();
            txt_TopAddress = GameTools.GetByName(Top, "Address").GetComponent<Text>();
            txt_MyFocusNum = GameTools.GetByName(Top, "MyFocusNum").GetComponent<Text>();
            txt_FocusMeNum = GameTools.GetByName(Top, "FocusMeNum").GetComponent<Text>();
            TopInfo = GameTools.GetByName(Top, "TopInfo").GetComponent<RectTransform>();

            QieHuan_Sex = GameTools.GetByName(Top, "QieHuan_Sex").GetComponent<ImgQiehuan>();
            btn_Setting = GameTools.GetByName(Top, "SettingBtn").GetComponent<Button>();
            SettingView = GameTools.GetByName(transform, "SettingView");
            SettingView.SetActive(false);
            btn_Block = GameTools.GetByName(SettingView.transform, "btn_Block").GetComponent<Button>();
            btn_Suggest = GameTools.GetByName(SettingView.transform, "btn_Suggest").GetComponent<Button>();
            btn_MyFocus = GameTools.GetByName(Top, "MyFocus").GetComponent<Button>();
            btn_FocusMe = GameTools.GetByName(Top, "FocusMe").GetComponent<Button>();

            SignatureRect = GameTools.GetByName(transform, "Signature").GetComponent<RectTransform>();
            txt_Signature = GameTools.GetByName(transform, "SignatureText").GetComponent<Text>();
            //
            toggle_DatingInfo = GameTools.GetByName(transform, "ToggleDatingInfo").GetComponent<Toggle>();
            toggle_RealInfo = GameTools.GetByName(transform, "ToggleRealInfo").GetComponent<Toggle>();
            PersonalInfoGB = GameTools.GetByName(transform, "DatingInfomation");
            RealInfoGB = GameTools.GetByName(transform, "RealInfomation");
            DatingInfomation = PersonalInfoGB.GetComponent<ScrollRect>();
            RealInfomation = RealInfoGB.GetComponent<ScrollRect>();

            #region PersonalInfoView
            RefreshContent = GameTools.GetByName(PersonalInfoGB.transform, "Content").GetComponent<RectTransform>();
            PersonalInfoView = GameTools.GetByName(transform, "PersonalInfoView").GetComponent<RectTransform>();
            btn_PersonalEditor = GameTools.GetByName(transform, "EditorBtnOne").GetComponent<Button>();
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
            #endregion

            //MatingInfo
            MatingInfoView = GameTools.GetByName(transform, "MatingInfoView").GetComponent<RectTransform>();
            btn_ExpertEditor = GameTools.GetByName(transform, "EditorBtnTwo").GetComponent<Button>();
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

            #region Event
            GameObject toggleChooseOne, toggleChooseTwo;
            toggleChooseOne = toggle_DatingInfo.transform.GetChild(0).gameObject;
            toggleChooseTwo = toggle_RealInfo.transform.GetChild(0).gameObject;
            toggle_DatingInfo.onValueChanged.AddListener((p) =>
            {
                toggleChooseOne.SetActive(p);
                toggleChooseTwo.SetActive(!p);
                PersonalInfoGB.SetActive(p);
                RealInfoGB.SetActive(!p);
            });
            toggle_RealInfo.onValueChanged.AddListener((p) =>
            {
                if (p && userRealInfoData == null)
                {
                    NetMgr.Instance.C2S_User_ViewRealInfo(GameData.userId);
                }
                toggleChooseOne.SetActive(!p);
                toggleChooseTwo.SetActive(p);
                PersonalInfoGB.SetActive(!p);
                RealInfoGB.SetActive(p);
            });
            btn_Setting.onClick.AddListener(() =>
            {
                SettingView.SetActive(!SettingView.activeSelf);
            });
            btn_Block.onClick.AddListener(() =>
            {
                SettingView.SetActive(!SettingView.activeSelf);
                UIMgr.Instance.Open(typeof(UIBBlackListPanel).Name);//UIBBlackListPanel
            });
            btn_Suggest.onClick.AddListener(() =>
            {
                SettingView.SetActive(!SettingView.activeSelf);
                GameTools.SetTip("暂未开放，敬请期待。");
            });
            GameTools.Instance.AddClickEvent(SettingView.gameObject, () =>
            {
                SettingView.SetActive(false);
            });
            GameTools.Instance.AddClickEvent(img_Icon.gameObject, () =>
            {
                if (img_Icon.mainTexture == null) return;
                GameData.bigImg = img_Icon.mainTexture;
                UIMgr.Instance.Open(UIPath.UIBBigImgPanel);
            });
            //打开我关注的列表
            btn_MyFocus.onClick.AddListener(() =>
            {
                GameData.isMyFoucs = true;
                GameData.isOpenFocusListPanel = true;
                UIMgr.Instance.Open(UIPath.UIBFriendFocusListPanel);
            });
            //打开关注我的用户列表
            btn_FocusMe.onClick.AddListener(() =>
            {
                GameData.isMyFoucs = false;
                GameData.isOpenFocusListPanel = true;
                UIMgr.Instance.Open(UIPath.UIBFriendFocusListPanel);
            });
            //打开编辑界面
            btn_PersonalEditor.onClick.AddListener(() =>
            {
                if (data == null) return;
                UIMgr.Instance.Open(UIPath.UIBFriendDataEditorPanel, UIEditPanelType.PersonalInfoPanel);
            });
            btn_ExpertEditor.onClick.AddListener(() =>
            {
                if (data == null) return;
                UIMgr.Instance.Open(UIPath.UIBFriendDataEditorPanel, UIEditPanelType.ExpertInfoPanel);
            });

            //交友信息
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_ViewPersonalInfo, S2C_Friend_ViewPersonalInfoCallBack);
            //认证信息
            MsgCenter.RegisterMsg(null, MsgCode.S2C_User_ViewRealInfo, S2C_User_ViewRealInfoCallBack);
            DataMgr.Instance.viewPersonalInfoReq.userId = GameData.userId;
            DataMgr.Instance.viewPersonalInfoReq.viewUserId = GameData.userId;
            NetMgr.Instance.C2S_Friend_ViewPersonalInfo();
            #endregion

            base.InitPage();
        }

        public override void OpenPage()
        {
            base.OpenPage();
            SettingView.SetActive(false);
            DatingInfomation.enabled = true;
            RealInfomation.enabled = true;
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
            return lable.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        private void S2C_Friend_ViewPersonalInfoCallBack(object o)
        {
            data = DataMgr.Instance.dataBViewPersonalInfoRes.userFriendData;
            //我关注
            txt_MyFocusNum.text = DataMgr.Instance.dataBViewPersonalInfoRes.meAttentionNum.ToString();
            //关注我
            txt_FocusMeNum.text = DataMgr.Instance.dataBViewPersonalInfoRes.attentionToMeNum.ToString();

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
            if (GameData.userHeadSprite != null)
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
            SetLablesView();//生成标签
            Invoke("RefreshLsyOut", 0.2f);
        }

        private void S2C_User_ViewRealInfoCallBack(object o)
        {
            var data = userRealInfoData = o as UserRealInfo;
            if (data == null) return;
            if (data.userId != GameData.userId) return;//是否是当前用户
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
                GameTools.Instance.MatchImgBySprite(img_Photo);
            }, data.pic);
            Invoke("RefreshRealInfoLsyOut", 0.2f);
        }

        //刷新布局
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

        public override void ClosePage()
        {
            base.ClosePage();
            GameData.isOpenFriend = false;
            if (DatingInfomation != null) DatingInfomation.enabled = false;
            if (RealInfomation != null) RealInfomation.enabled = false;
        }

        void OnDestroy()
        {
            userRealInfoData = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_ViewPersonalInfo, S2C_Friend_ViewPersonalInfoCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_User_ViewRealInfo, S2C_User_ViewRealInfoCallBack);
        }
    }
}
