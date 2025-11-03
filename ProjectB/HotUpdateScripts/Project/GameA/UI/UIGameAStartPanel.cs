using UnityEngine;
using JEngine.Core;
using HotUpdateScripts.Project.Common;
using System.Collections;
using System;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.ACommon;
using My.Msg;
using HotUpdateScripts.Project.Game.GameA.Data;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIGameAStartPanel : BasePanel
    {
        private GameObject btn_Start, btn_Leaderboard, btn_Back;
        public static Action refreshGameData;

        private Button btn_Audio, EffectAudioOne, EffectAudioTwo;
        private GameObject AudioPanel;
        private Image  btn_EffectAudioBg;
        //private Color color_Open;
        //private Color color_Close;

        void startGame()
        {
            GameData.isGameStart = true;
            GameData.isGameReturn = false;
        }

        private void Awake()
        {
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_GetGameDXGScore, S2C_Game_GetGameDXGScoreCallBack);
            //---音乐设置----
            btn_Audio = GameTools.GetByName(transform, "btn_Audio").GetComponent<Button>();
            AudioPanel = GameTools.GetByName(transform, "AudioPanel");

            ////BgAudioOne = GameTools.GetByName(transform, "BgAudioOne").GetComponent<Button>();
            //BgAudioTwo = GameTools.GetByName(transform, "BgAudioTwo").GetComponent<Button>();
            EffectAudioOne = GameTools.GetByName(transform, "EffectAudioOne").GetComponent<Button>();
            EffectAudioTwo = GameTools.GetByName(transform, "EffectAudioTwo").GetComponent<Button>();
            //btn_BgAudioBg = GameTools.GetByName(transform, "btn_BgAudioBg").GetComponent<Image>();
            btn_EffectAudioBg = GameTools.GetByName(transform, "btn_EffectAudioBg").GetComponent<Image>();

            SetAudioView();

            btn_Audio.onClick.AddListener(() =>
            {
                AudioPanel.SetActive(!AudioPanel.activeSelf);
            });

            //BgAudioOne.onClick.AddListener(() =>
            //{
            //    AudioMgr.switch_music(AudioConfig.GameA_BGM,GameData.GameAudioBgmStatus);
            //    SetAudioView();
            //    debug.Log_Blue("关闭背景音乐");
            //});

            //BgAudioTwo.onClick.AddListener(() =>
            //{
            //    AudioMgr.switch_music(AudioConfig.GameA_BGM, GameData.GameAudioBgmStatus);
            //    SetAudioView();
            //    debug.Log_Blue("打开背景音乐");
            //});

            EffectAudioOne.onClick.AddListener(() =>
            {
                AudioMgr.switch_effect(GameData.GameAudioEffectStatus);
                SetAudioView();
                debug.Log_Blue("关闭音效");
            });

            EffectAudioTwo.onClick.AddListener(() =>
            {
                AudioMgr.switch_effect(GameData.GameAudioEffectStatus);
                SetAudioView();
                debug.Log_Blue("打开音效");
            });
        }

        void SetAudioView()
        {

            //BgAudioOne.gameObject.SetActive(PlayerPrefs.GetInt(GameData.GameAudioBgmStatus) == 1);
            //BgAudioTwo.gameObject.SetActive(PlayerPrefs.GetInt(GameData.GameAudioBgmStatus) == 0);
            EffectAudioOne.gameObject.SetActive(PlayerPrefs.GetInt(GameData.GameAudioEffectStatus) == 1);
            EffectAudioTwo.gameObject.SetActive(PlayerPrefs.GetInt(GameData.GameAudioEffectStatus) == 0);
        }

        private void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_GetGameDXGScore, S2C_Game_GetGameDXGScoreCallBack);

        }

        void S2C_Game_GetGameDXGScoreCallBack(object o)
        {
            DataGameA data = DataMgr.Instance.dataBUserGameScoreRes.userGameScore;
            if (data == null)
            {
                return;
            }
            GameData.GameA_Score = DataMgr.Instance.dataBUserGameScoreRes.userGameScore.gameScore;
            GameData.GameA_RowNum = DataMgr.Instance.dataBUserGameScoreRes.userGameScore.rowNum;
            GameData.userHeadImg = DataMgr.Instance.dataBUserGameScoreRes.userGameScore.headImgUrl;
            GameData.userNickName = DataMgr.Instance.dataBUserGameScoreRes.userGameScore.nickName;
        }

        public override void InitPanel(object o)
        {
            btn_Start = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Start"), () =>
            {
                UIMgr.Instance.Close(UIPath.UIGameAStartPanel, false);
                Invoke("startGame", .5f);
                DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_PLAYGAME_ZodiacMerge];
                NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
            });

            btn_Leaderboard = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Leaderboard"), () =>
            {
                UIMgr.Instance.Close(UIPath.UIGameAStartPanel, false);
                UIMgr.Instance.Open(UIPath.UILeaderBorderPanel);
            });

            btn_Back = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "btn_Back"), () =>
            {
                AudioMgr.stop_music(AudioConfig.GameA_BGM);
                GameTools.SetLoadingActive(true);
                JResource.LoadSceneAsync("Game.unity", () =>
                {
                    UIMgr.Instance.Close(UIPath.UIGameAStartPanel);
                    CoroutineMgr.Instance.Coroutine_StopAll();
                    CoroutineMgr.Instance.Coroutine_Start(_GotoScene());
                });
            });
            InitPage();
            refreshGameData = InitPage;
        }

        private void InitPage()
        {
            //获取游戏分数
            DataMgr.Instance.dataBGetGameDXGScoreReq.userId = GameData.userId;
            DataMgr.Instance.dataBGetGameDXGScoreReq.gameName = GameData.GameAName;
            NetMgr.Instance.C2S_Game_GetGameDXGScore();
        }

        IEnumerator _GotoScene()
        {
            yield return new WaitForSeconds(1f);
            AudioMgr.PlayMusic(AudioConfig.ProjectB_BGM, GameData.SystemAudioStatus);
            GameTools.SetLoadingActive(false);
            UIMgr.Instance.Open(UIPath.UIBGameHomePanel);
        }
    }
}
