using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;
using System.Collections.Generic;

//捐献模块（希望灯塔） 主页面
namespace My.UI.Panel
{
    public class UIBDonateHomePanel : BasePanel
    {
        //View
        private Button btn_CloseA;
        private Button btn_CloseB;
        private Transform sv_DonateContent;
        private GameObject itemPreb;

        private Text randomTip;

        DataBBenefitProjectListRes DataProjectList;

        private bool isFirstOpen = true;
        private List<UIBDonateHomeItem> listItem = new List<UIBDonateHomeItem>();

        private GameObject ExplainView;

        void Awake()
        {
            IsHomePanel = true;
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            }
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_GetDonateList, S2C_Love_GetDonateListCallBack);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f, () =>
             {
                 NetMgr.Instance.C2S_Love_GetDonateList();
             });

            btn_CloseA = GameTools.GetByName(transform, "btn_CloseA").GetComponent<Button>();
            btn_CloseB = GameTools.GetByName(transform, "btn_CloseB").GetComponent<Button>();

            randomTip = GameTools.GetByName(transform, "randomTip").GetComponent<Text>();

            sv_DonateContent = GameTools.GetByName(transform, "sv_DonateContent").GetComponent<Transform>();
            itemPreb = GameTools.GetByName(transform, "Item");
            itemPreb.SetActive(false);
            ExplainView = transform.Find("ExplainPop").gameObject;
            GameTools.Instance.AddClickEvent(transform.Find("Content_bg/Bg/ExplainBtn"), () => { ExplainView.SetActive(true); });
            GameTools.Instance.AddClickEvent(transform.Find("ExplainPop/Bg/CloseBtn"), () => { ExplainView.SetActive(false); });
            btn_CloseA.onClick.AddListener(OnBtn_CloseClick);
            btn_CloseB.onClick.AddListener(OnBtn_CloseClick);

            randomTip.text = GameData.GetRandomTip();

            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_HOPE];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
        }

        //Business
        void OnBtn_CloseClick()
        {
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
            }
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBDonateHomePanel);
            });
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_GetDonateList, S2C_Love_GetDonateListCallBack);
            DataProjectList = null;
            listItem.Clear();
        }

        void S2C_Love_GetDonateListCallBack(object o)
        {
            DataProjectList = DataMgr.Instance.dataBBenefitProjectListRes;
            if (DataProjectList == null) return;
            if (isFirstOpen)
            {
                isFirstOpen = false;
                for (int i = 0; i < DataProjectList.publicBenefitProjectList.Count; i++)
                {
                    GameObject Item = Instantiate(itemPreb, sv_DonateContent);
                    Item.SetActive(true);
                    Item.AddComponent<UIBDonateHomeItem>().setUIView(DataProjectList.publicBenefitProjectList[i]);
                    listItem.Add(Item.GetComponent<UIBDonateHomeItem>());
                }
            }
            else
            {
                for (int i = 0; i < DataProjectList.publicBenefitProjectList.Count; i++)
                {
                    listItem[i].setUIView(DataProjectList.publicBenefitProjectList[i]);
                }
            }
        }
    }
}
