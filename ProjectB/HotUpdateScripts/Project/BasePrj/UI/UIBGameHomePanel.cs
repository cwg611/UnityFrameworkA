using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using UnityEngine;
using UnityEngine.UI;


namespace My.UI.Panel
{
    /// <summary>
    /// 游戏大厅主页面
    /// </summary>
    public class UIBGameHomePanel : BasePanel
    {
        private Button btn_CloseA, Game_Combine, btn_JumpGame, btn_Bg;
        private GameObject ExplainView;
        void Awake()
        {
            //IsHomePanel = true;
            //if (IsHomePanel)
            //{
            //    HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(false);
            //}
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, -Screen.height, 0), new Vector3(0, 0, 0), .5f);

            btn_CloseA = GameTools.GetByName(transform, "btn_CloseA").GetComponent<Button>();
            btn_Bg = GameTools.GetByName(transform, "btn_Bg").GetComponent<Button>();

            btn_CloseA.onClick.AddListener(OnBtnCloseClick);
            btn_Bg.onClick.AddListener(OnBtnCloseClick);

            Game_Combine = GameTools.GetByName(transform, "Game_Combine").GetComponent<Button>();
            btn_JumpGame = GameTools.GetByName(transform, "JumpGameBtn").GetComponent<Button>();
            Game_Combine.onClick.AddListener(() =>
            {
                AudioMgr.stop_music(AudioConfig.ProjectB_BGM);
                GameTools.SetLoadingActive(true);
                //UIMgr.Instance.Close(UIPath.UIBGameHomePanel);
                JResource.LoadSceneAsync("GameA/GameA.unity", () =>
                {
                    Invoke("_GotoScene", 1f);
                });
            });
            btn_JumpGame.onClick.AddListener(() =>
            {
                AudioMgr.stop_music(AudioConfig.ProjectB_BGM);
                GameTools.SetLoadingActive(true);
                JResource.LoadSceneAsync("FunJump.unity", () =>
                {
                    Invoke("_GotoScene", 1f);
                });
            });
            ExplainView = transform.Find("ExplainPop").gameObject;
            GameTools.Instance.AddClickEvent(transform.Find("Content_bg/Bg/ExplainBtn"), () => { ExplainView.SetActive(true); });
            GameTools.Instance.AddClickEvent(transform.Find("ExplainPop/Bg/CloseBtn"), () => { ExplainView.SetActive(false); });
            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_GAME];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
        }

        private void _GotoScene()
        {
            GameTools.SetLoadingActive(false);
            AudioMgr.PlayMusic(AudioConfig.GameA_BGM, GameData.GameAudioBgmStatus);
            UIMgr.Instance.Close(UIPath.UIBGameHomePanel);
            UIMgr.Instance.Close(UIPath.UIBMainHomePanel, false);
        }

        void OnBtnCloseClick()
        {
            //if (IsHomePanel)
            //{
            //    HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
            //}
            DOTweenMgr.Instance.DoFadeQ(gameObject, 0, .3f);
            DOTweenMgr.Instance.MovePos(GameTools.GetByName(transform, "Content_bg"), new Vector3(0, 0, 0), new Vector3(0, -Screen.height, 0), .3f, () =>
            {
                UIMgr.Instance.Close(UIPath.UIBGameHomePanel);
            });
        }
    }
}
