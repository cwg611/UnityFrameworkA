using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.BasePrj.UI;
using System.Collections;

namespace My.UI.Panel
{
    public class UIBFriendDataEditorPanel : BasePanel
    {
        #region data
        private DataEditorReq m_Data; //本地记录数据

        private Transform PersonalDetail;
        private Transform PersonalInfoView;
        private Text txt_Age, txt_Height, txt_Weight, txt_Edu, txt_House, txt_Car,
txt_Animal, txt_Constellation, txt_Area;
        private ItemList CharacterItemList, ApperanceItemList, MyItemList;

        //
        private Transform ExpectDetail;
        private Transform MatingInfoView;
        private Text txt_ExpAge, txt_ExpHeight, txt_ExpWeight, txt_ExpEdu, txt_ExpHouse, txt_ExpCar;
        private ItemList YCharacterItemList, YApperanceItemList, YourItemList;

        private UIEditorSelectorWin editorSelectorWin;
        private RectTransform Bottom;


        #endregion

        #region 初始化

        UIEditPanelType panelType = UIEditPanelType.PersonalInfoPanel;
        public override void InitPanel(object o)
        {
            DOTweenMgr.Instance.OpenWindowScale(gameObject, .3f);

            if (o != null) this.panelType = (UIEditPanelType)o;

            editorSelectorWin = GameTools.GetByName(transform, "EditWindow").AddComponent<UIEditorSelectorWin>();
            editorSelectorWin.InitUIEditorSelectWin();
            editorSelectorWin.gameObject.SetActive(false);
            PersonalDetail = GameTools.GetByName(transform, "PersonalDetail").transform;
            ExpectDetail = GameTools.GetByName(transform, "ExpectDetail").transform;
            if (panelType == UIEditPanelType.PersonalInfoPanel)
            {
                Bottom = PersonalDetail.rectTransform();
                PersonalDetail.localPosition = Vector3.zero;
                ExpectDetail.localPosition = Vector3.one * 2000;
            }
            else
            {
                Bottom = ExpectDetail.rectTransform();
                PersonalDetail.localPosition = Vector3.one * 2000;
                ExpectDetail.localPosition = Vector3.zero;

            }
            InitUIView();
            InitEvent();
            SetMyData();
            #region OLDCODE
            //isFirst = true;
            //isFirstOpenDQ = true;
            //isFirstOpenMyLable = true;
            //isFirstOpenYouLable = true;
            //isFirstOpenXz = true;

            //编辑
            //InputName = GameTools.GetByName(transform, "InputName").GetComponent<InputField>();
            /*
            AddressText = GameTools.GetByName(transform, "AddressText").GetComponent<Text>();

            Btn_UnOpen_DQ = GameTools.GetByName(transform, "Btn_UnOpen_DQ").GetComponent<Button>();
            Btn_Open_DQ = GameTools.GetByName(transform, "Btn_Open_DQ").GetComponent<Button>();
            Bg_DQ = GameTools.GetByName(transform, "Bg_DQ").transform;

            LayOut_DQ = GameTools.GetByName(transform, "LayOut_DQ").transform;
            //地区选择标签
            UIExtenFlowLayoutGroup Lay_DQ = LayOut_DQ.gameObject.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_DQ.SpacingX = 36.9f;
            Lay_DQ.SpacingY = 29.46f;
            Lay_DQ.padding = new RectOffset(21, 0, 29, 34);
            Lay_DQ.CalculateLayoutInputVertical();

            AddressPrefab = GameTools.GetByName(transform, "AddressPrefab");

            //InputSign = GameTools.GetByName(transform, "InputSign").GetComponent<InputField>();

            LabelOne = GameTools.GetByName(transform, "LabelOne");

            LayOut_Mylabel = GameTools.GetByName(transform, "LayOut_Mylabel").transform;
            //我的标签选择
            UIExtenFlowLayoutGroup Lay_Mylable = LayOut_Mylabel.gameObject.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_Mylable.SpacingX = 22.26f;
            Lay_Mylable.SpacingY = 18.52f;
            Lay_Mylable.padding = new RectOffset(259, 0, 10, -42);
            Lay_Mylable.CalculateLayoutInputVertical();

            Bg_Mylabel = GameTools.GetByName(transform, "Bg_Mylabel");
            Content_Mylabel = GameTools.GetByName(transform, "Content_Mylabel").transform;
            //我的标签
            UIExtenFlowLayoutGroup Lay_ContentMylable = Content_Mylabel.gameObject.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_ContentMylable.SpacingX = 34.9f;
            Lay_ContentMylable.SpacingY = 23.12f;
            Lay_ContentMylable.padding = new RectOffset(21, 0, 29, 34);
            Lay_ContentMylable.CalculateLayoutInputVertical();

            Btn_UnOpen_Mylabel = GameTools.GetByName(transform, "Btn_UnOpen_Mylabel").GetComponent<Button>();
            Btn_Open_Mylabel = GameTools.GetByName(transform, "Btn_Open_Mylabel").GetComponent<Button>();

            LayOut_Youlabel = GameTools.GetByName(transform, "LayOut_Youlabel").transform;
            //TA的标签选择
            UIExtenFlowLayoutGroup Lay_Youlable = LayOut_Youlabel.gameObject.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_Youlable.SpacingX = 9.95f;
            Lay_Youlable.SpacingY = 16.23f;
            Lay_Youlable.padding = new RectOffset(367, 0, 11, -28);
            Lay_Youlable.CalculateLayoutInputVertical();

            Bg_Youlabel = GameTools.GetByName(transform, "Bg_Youlabel");
            Content_Youlabel = GameTools.GetByName(transform, "Content_Youlabel").transform;
            //TA的标签
            UIExtenFlowLayoutGroup Lay_ContentYoulable = Content_Youlabel.gameObject.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_ContentYoulable.SpacingX = 34.9f;
            Lay_ContentYoulable.SpacingY = 23.12f;
            Lay_ContentYoulable.padding = new RectOffset(21, 0, 29, 34);
            Lay_ContentYoulable.CalculateLayoutInputVertical();

            Btn_UnOpen_Youlabel = GameTools.GetByName(transform, "Btn_UnOpen_Youlabel").GetComponent<Button>();
            Btn_Open_Youlabel = GameTools.GetByName(transform, "Btn_Open_Youlabel").GetComponent<Button>();

            LabelXZ = GameTools.GetByName(transform, "LabelXZ").GetComponent<Text>();
            Btn_UnOpen_XZ = GameTools.GetByName(transform, "Btn_UnOpen_XZ").GetComponent<Button>();
            Btn_Open_XZ = GameTools.GetByName(transform, "Btn_Open_XZ").GetComponent<Button>();

            Bg_XZ = GameTools.GetByName(transform, "Bg_XZ");
            LayOut_XZ = GameTools.GetByName(transform, "LayOut_XZ").transform;

            //星座选择标签
            UIExtenFlowLayoutGroup Lay_XZ = LayOut_XZ.gameObject.AddComponent<UIExtenFlowLayoutGroup>();
            Lay_XZ.SpacingX = 21.3f;
            Lay_XZ.SpacingY = 18.98f;
            Lay_XZ.padding = new RectOffset(21, 0, 29, 34);
            Lay_XZ.CalculateLayoutInputVertical();

            Label = GameTools.GetByName(transform, "Label");

            QieHuan_Sex = GameTools.GetByName(transform, "QieHuan_Sex").GetComponent<ImgQiehuan>();

            Bottom = GameTools.GetByName(transform, "Bottom").GetComponent<RectTransform>();
            Bottom.gameObject.SetActive(false);
            //编辑昵称
            //InputName.onEndEdit.AddListener(str =>
            //{
            //    m_Data.nickName = str;
            //    Name.text = str;
            //});
            //编辑性别
            //Btn_Nan.onClick.AddListener(() =>
            //{
            //    m_Data.gender = 1;
            //    setGender(m_Data.gender);
            //});
            //Btn_Nv.onClick.AddListener(() =>
            //{
            //    m_Data.gender = 0;
            //    setGender(m_Data.gender);
            //});
            */
            #endregion
        }

        //标签数据
        RectOffset constRectOffet = new RectOffset(225, 20, 11, -65);
        float constSpaceX = 16f, constSpaceY = 16.23f;
        private void InitUIView()
        {
            if (panelType == UIEditPanelType.PersonalInfoPanel)
            {
                PersonalInfoView = PersonalDetail.Find("PersonalInfoView");
                txt_Age = PersonalInfoView.Find("AgeItem/AgeText").GetComponent<Text>();
                txt_Height = PersonalInfoView.Find("HeightItem/HeightText").GetComponent<Text>();
                txt_Weight = PersonalInfoView.Find("WeightItem/WeightText").GetComponent<Text>();
                txt_Edu = PersonalInfoView.Find("EducationItem/EduText").GetComponent<Text>();
                txt_House = PersonalInfoView.Find("HouseItem/HouseText").GetComponent<Text>();
                txt_Car = PersonalInfoView.Find("CarItem/CarText").GetComponent<Text>();
                txt_Animal = PersonalInfoView.Find("AnimalItem/AnimalText").GetComponent<Text>();
                txt_Constellation = PersonalInfoView.Find("Constellation/ConstellationText").GetComponent<Text>();
                txt_Area = PersonalInfoView.Find("AreaItem/AreaText").GetComponent<Text>();
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
            }
            else
            {
                MatingInfoView = ExpectDetail.Find("MatingInfoView");
                txt_ExpAge = MatingInfoView.Find("AgeItem/AgeText").GetComponent<Text>();
                txt_ExpHeight = MatingInfoView.Find("HeightItem/HeightText").GetComponent<Text>();
                txt_ExpWeight = MatingInfoView.Find("WeightItem/WeightText").GetComponent<Text>();
                txt_ExpEdu = MatingInfoView.Find("EducationItem/EduText").GetComponent<Text>();
                txt_ExpHouse = MatingInfoView.Find("HouseItem/HouseText").GetComponent<Text>();
                txt_ExpCar = MatingInfoView.Find("CarItem/CarText").GetComponent<Text>();
                //TA的标签
                var LayOutObj = GameTools.GetByName(MatingInfoView, "CharacterLayOut");
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
            }
        }

        private void InitEvent()
        {
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "ConcelBtn"), () =>
            {
                //ResetView();
                DOTweenMgr.Instance.CloseWindowScale(gameObject, 0.2f, () =>
                {
                    m_Data = null;
                    UIMgr.Instance.Close(UIPath.UIBFriendDataEditorPanel);
                });
            });
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "Btn_Submit"), () =>
            {
                DataMgr.Instance.dataEditorReq = m_Data;
                NetMgr.Instance.C2S_Friend_CompilePersonalInfo();
            });

            if (panelType == UIEditPanelType.PersonalInfoPanel)
            {
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "AgeItem"), () =>
                {
                    List<string> ageStrs = new List<string>();
                    for (int i = 18; i < 51; i++)
                    {
                        ageStrs.Add(i.ToString());
                    }
                    editorSelectorWin.InitSingleSelector(ageStrs, (p) => { m_Data.age = int.Parse(p); SetUIData(); });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "HeightItem"), () =>
                {
                    List<string> strs = new List<string>();
                    for (int i = 150; i < 191; i++)
                    {
                        strs.Add(i.ToString() + "cm");
                    }
                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        var str = p.ToString();
                        str = str.Substring(0, str.Length - 2);
                        m_Data.height = int.Parse(str);
                        SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "WeightItem"), () =>
                {
                    List<string> strs = new List<string>();
                    for (int i = 40; i < 101; i++)
                    {
                        strs.Add(i.ToString() + "kg");
                    }
                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        var str = p.ToString();
                        str = str.Substring(0, str.Length - 2);
                        m_Data.weight = int.Parse(str);
                        SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "EducationItem"), () =>
                {
                    List<string> strs = new List<string>();
                    for (int i = 0; i < GameData.EducationMapDic.Count; i++)
                    {
                        strs.Add(GameData.EducationMapDic[i]);
                    }
                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        m_Data.education = GameData.GetUserEducation(p);
                        SetUIData();
                    }, 5);
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "HouseItem"), () =>
                {
                    List<string> strs = new List<string>() { "是", "否" };

                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        m_Data.house = p.Equals("是") ? 1 : 0;
                        SetUIData();
                    }, 2);
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "CarItem"), () =>
                {
                    List<string> strs = new List<string>() { "是", "否" };

                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        m_Data.car = p.Equals("是") ? 1 : 0;
                        SetUIData();
                    }, 2);
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "AnimalItem"), () =>
                {
                    editorSelectorWin.InitSingleSelector(GameData.ZodiacList, (p) => { m_Data.zodiac = p; SetUIData(); });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "Constellation"), () =>
                {
                    List<string> Strs = new List<string>();
                    for (int i = 0; i < GameData.XZNames.Length; i++)
                    {
                        Strs.Add(GameData.XZNames[i]);
                    }
                    editorSelectorWin.InitSingleSelector(Strs, (p) =>
                    {
                        m_Data.constellation = p; SetUIData();
                    });
                });

                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "AreaItem"), () =>
                {
                    CityHelper cityHelper = new CityHelper();
                    var provinces = cityHelper.GetAllProvinces();
                    editorSelectorWin.InitAreaSelector(provinces, (p) =>
                    {
                        if (!string.IsNullOrEmpty(p))
                        {
                            string[] provincecity = p.Split(' ');
                            if (provincecity.Length > 1)
                            {
                                m_Data.province = provincecity[0];
                                m_Data.city = provincecity[1];
                            }
                        }
                        SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "CharacterLables"), () =>
                {
                    string[] lables = GetLables(m_Data.character);
                    editorSelectorWin.InitLablesSelector(GameData.CharacterLables, lables, 3, 5, (p) =>
                    {
                        m_Data.character = p; SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "ApperanceLables"), () =>
                {
                    string[] lables = GetLables(m_Data.appearance);
                    editorSelectorWin.InitLablesSelector(GameData.AppearanceLables, lables, 3, 5, (p) =>
                    {
                        m_Data.appearance = p; SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(PersonalInfoView, "Mylabels"), () =>
                {
                    string[] lables = GetLables(m_Data.mineLabel);
                    editorSelectorWin.InitLablesSelector(GameData.Lables, lables, 3, 10, (p) =>
                     {
                         m_Data.mineLabel = p; SetUIData();
                     });
                });
            }
            else
            {
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "AgeItem"), () =>
                {
                    List<string> ageStrs = new List<string>();
                    for (int i = 18; i < 51; i++)
                    {
                        ageStrs.Add(i.ToString());
                    }
                    editorSelectorWin.InitSingleSelector(ageStrs, (p) => { m_Data.expectAge = int.Parse(p); SetUIData(); });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "HeightItem"), () =>
                {
                    List<string> strs = new List<string>();
                    for (int i = 150; i < 191; i++)
                    {
                        strs.Add(i.ToString() + "cm");
                    }
                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        var str = p.ToString();
                        str = str.Substring(0, str.Length - 2);
                        m_Data.expectHeight = int.Parse(str);
                        SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "WeightItem"), () =>
                {
                    List<string> strs = new List<string>();
                    for (int i = 40; i < 101; i++)
                    {
                        strs.Add(i.ToString() + "kg");
                    }
                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        var str = p.ToString();
                        str = str.Substring(0, str.Length - 2);
                        m_Data.expectWeight = int.Parse(str);
                        SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "EducationItem"), () =>
                {
                    List<string> strs = new List<string>();
                    for (int i = 0; i < GameData.EducationMapDic.Count; i++)
                    {
                        strs.Add(GameData.EducationMapDic[i]);
                    }
                    debug.Log_yellow("click");
                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        m_Data.expectEducation = GameData.GetUserEducation(p);
                        SetUIData();
                    }, 5);
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "HouseItem"), () =>
                {
                    List<string> strs = new List<string>() { "是", "否" };

                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        m_Data.expectHouse = p.Equals("是") ? 1 : 0;
                        SetUIData();
                    }, 2);
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "CarItem"), () =>
                {
                    List<string> strs = new List<string>() { "是", "否" };

                    editorSelectorWin.InitSingleSelector(strs, (p) =>
                    {
                        m_Data.expectCar = p.Equals("是") ? 1 : 0;
                        SetUIData();
                    }, 2);
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "CharacterLables"), () =>
                {
                    string[] lables = GetLables(m_Data.expectCharacter);
                    editorSelectorWin.InitLablesSelector(GameData.CharacterLables, lables, 3, 5, (p) =>
                     {
                         m_Data.expectCharacter = p; SetUIData();
                     });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "ApperanceLables"), () =>
                {
                    string[] lables = GetLables(m_Data.expectAppearance);
                    editorSelectorWin.InitLablesSelector(GameData.AppearanceLables, lables, 3, 5, (p) =>
                    {
                        m_Data.expectAppearance = p; SetUIData();
                    });
                });
                GameTools.Instance.AddClickEvent(GameTools.GetByName(MatingInfoView, "Yourlabels"), () =>
                {
                    string[] lables = GetLables(m_Data.expectLabel);
                    editorSelectorWin.InitLablesSelector(GameData.Lables, lables, 3, 10, (p) =>
                    {
                        m_Data.expectLabel = p; SetUIData();
                    });
                });
            }

            #region OLDCODE
            //编辑地区
            //Btn_UnOpen_DQ.onClick.AddListener(() =>
            //{
            //    Btn_Open_DQ.gameObject.SetActive(true);
            //    Btn_UnOpen_DQ.gameObject.SetActive(false);

            //    Bg_DQ.gameObject.SetActive(true);
            //    Btn_Open_Mylabel.onClick.Invoke();
            //    Btn_Open_Youlabel.onClick.Invoke();
            //    Btn_Open_XZ.onClick.Invoke();
            //    setDQView();
            //});
            //Btn_Open_DQ.onClick.AddListener(() =>
            //{
            //    Btn_Open_DQ.gameObject.SetActive(false);
            //    Btn_UnOpen_DQ.gameObject.SetActive(true);
            //    Bg_DQ.gameObject.SetActive(false);
            //});
            //编辑签名
            //InputSign.onEndEdit.AddListener(str =>
            //{
            //    m_Data.personalizedSignature = str;
            //});
            //编辑我的标签
            //Btn_UnOpen_Mylabel.onClick.AddListener(() =>
            //{
            //    Btn_Open_Mylabel.gameObject.SetActive(true);
            //    Btn_UnOpen_Mylabel.gameObject.SetActive(false);

            //    Bg_Mylabel.gameObject.SetActive(true);
            //    Btn_Open_DQ.onClick.Invoke();
            //    Btn_Open_Youlabel.onClick.Invoke();
            //    Btn_Open_XZ.onClick.Invoke();
            //    setMyLableView();
            //});
            //Btn_Open_Mylabel.onClick.AddListener(() =>
            //{
            //    Btn_Open_Mylabel.gameObject.SetActive(false);
            //    Btn_UnOpen_Mylabel.gameObject.SetActive(true);
            //    Bg_Mylabel.gameObject.SetActive(false);
            //});

            //编辑TA的标签
            //Btn_UnOpen_Youlabel.onClick.AddListener(() =>
            //{
            //    Btn_Open_Youlabel.gameObject.SetActive(true);
            //    Btn_UnOpen_Youlabel.gameObject.SetActive(false);

            //    Bg_Youlabel.gameObject.SetActive(true);
            //    Btn_Open_DQ.onClick.Invoke();
            //    Btn_Open_Mylabel.onClick.Invoke();
            //    Btn_Open_XZ.onClick.Invoke();
            //    setYouLableView();

            //});
            //Btn_Open_Youlabel.onClick.AddListener(() =>
            //{
            //    Btn_Open_Youlabel.gameObject.SetActive(false);
            //    Btn_UnOpen_Youlabel.gameObject.SetActive(true);
            //    Bg_Youlabel.gameObject.SetActive(false);

            //});

            //编辑星座
            //Btn_UnOpen_XZ.onClick.AddListener(() =>
            //{
            //    Btn_Open_XZ.gameObject.SetActive(true);
            //    Btn_UnOpen_XZ.gameObject.SetActive(false);

            //    Bg_XZ.gameObject.SetActive(true);
            //    Btn_Open_DQ.onClick.Invoke();
            //    Btn_Open_Mylabel.onClick.Invoke();
            //    Btn_Open_Youlabel.onClick.Invoke();
            //    setXZView();
            //});
            //Btn_Open_XZ.onClick.AddListener(() =>
            //{
            //    Btn_Open_XZ.gameObject.SetActive(false);
            //    Btn_UnOpen_XZ.gameObject.SetActive(true);
            //    Bg_XZ.gameObject.SetActive(false);
            //});
            #endregion
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_CompilePersonalInfo, S2C_Friend_CompilePersonalInfoCallBack);
        }

        private void SetMyData()
        {
            UserFriendData data = DataMgr.Instance.dataBViewPersonalInfoRes.userFriendData;
            //本地数据赋值
            m_Data = new DataEditorReq();
            m_Data.userId = GameData.userId;
            m_Data.age = data.age;
            m_Data.height = data.height;
            m_Data.weight = data.weight;
            m_Data.education = data.education;
            m_Data.house = data.house;
            m_Data.car = data.car;
            m_Data.zodiac = data.zodiac;
            m_Data.constellation = data.constellation;
            m_Data.province = data.province;
            m_Data.city = data.city;
            m_Data.character = data.character;
            m_Data.appearance = data.appearance;
            m_Data.mineLabel = data.mineLabel;
            m_Data.expectAge = data.expectAge;
            m_Data.expectHeight = data.expectHeight;
            m_Data.expectWeight = data.expectWeight;
            m_Data.expectEducation = data.expectEducation;
            m_Data.expectHouse = data.expectHouse;
            m_Data.expectCar = data.expectCar;
            m_Data.expectCharacter = data.expectCharacter;
            m_Data.expectAppearance = data.expectAppearance;
            m_Data.expectLabel = data.expectLabel;
            SetUIData();
        }


        #endregion

        private void SetUIData()
        {
            if (panelType == UIEditPanelType.PersonalInfoPanel)
            {
                txt_Age.text = m_Data.age + "岁";
                txt_Height.text = m_Data.height + "cm";
                txt_Weight.text = m_Data.weight + "kg";
                txt_Edu.text = GameData.GetUserEducation(m_Data.education);
                txt_House.text = m_Data.house == 1 ? "是" : "否";
                txt_Car.text = m_Data.car == 1 ? "是" : "否";
                txt_Animal.text = m_Data.zodiac;
                txt_Constellation.text = m_Data.constellation;
                txt_Area.text = m_Data.province + " " + m_Data.city;
                SetLableItemList(m_Data.character, CharacterItemList);
                SetLableItemList(m_Data.appearance, ApperanceItemList);
                SetLableItemList(m_Data.mineLabel, MyItemList);
            }
            else
            {
                txt_ExpAge.text = m_Data.expectAge + "岁";
                txt_ExpHeight.text = m_Data.expectHeight + "cm";
                txt_ExpWeight.text = m_Data.expectWeight + "kg";
                txt_ExpEdu.text = GameData.GetUserEducation(m_Data.expectEducation);
                txt_ExpHouse.text = m_Data.expectHouse == 1 ? "是" : "否";
                txt_ExpCar.text = m_Data.expectCar == 1 ? "是" : "否";
                SetLableItemList(m_Data.expectCharacter, YCharacterItemList);
                SetLableItemList(m_Data.expectAppearance, YApperanceItemList);
                SetLableItemList(m_Data.expectLabel, YourItemList);
            }
            Invoke("SetBottom", 0.2f);
        }

        private void SetBottom()
        {
            if (panelType == UIEditPanelType.PersonalInfoPanel)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(PersonalInfoView.rectTransform());
            }
            else
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(MatingInfoView.rectTransform());
            }
        }

        /// <summary>
        /// 提交资料修改 回调
        /// </summary>
        /// <param name="o"></param>
        void S2C_Friend_CompilePersonalInfoCallBack(object o)
        {
            debug.Log_purple("--->>  提交成功");
            //刷新我的资料界面
            DataMgr.Instance.viewPersonalInfoReq.userId = GameData.userId;
            DataMgr.Instance.viewPersonalInfoReq.viewUserId = GameData.userId;
            NetMgr.Instance.C2S_Friend_ViewPersonalInfo();
            UIMgr.Instance.Close(UIPath.UIBFriendDataEditorPanel);
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

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_CompilePersonalInfo, S2C_Friend_CompilePersonalInfoCallBack);
        }

        //标签拆解
        string[] GetLables(string lable)
        {
            if (string.IsNullOrEmpty(lable)) lable = "";
            string[] lables = lable.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            return lables;
        }
    }
}
