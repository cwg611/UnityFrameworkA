using DG.Tweening;
using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using HotUpdateScripts.Project.GameB;
using HotUpdateScripts.Project.GameB.Data;
using JEngine.Core;
using My.Msg;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    /// <summary>
    /// 跳跳乐主页
    /// </summary>
    public class UIGameBStartPanel : BasePanel
    {
        private int CurrentThemeId;
        //效果
        private Transform LogoTrans;
        private Transform ScoreTextTrans;
        private Transform StartTrans;

        private Transform SwitchTrans;
        private Transform RankingTrans;

        private Transform SettingTrans;
        private Transform BackBtnTrans;

        private Transform MusicToggleTrans;
        private Toggle Tog_Music;

        private Text txt_Score;
        private Image[] PanelImages;

        private Transform HaveNewLockedSign;

        public override void InitPanel(object o)
        {
            base.InitPanel(o);

            SettingTrans = transform.Find("SettingBtn");
            BackBtnTrans = transform.Find("BackBtn");
            LogoTrans = transform.Find("LogoBg");
            ScoreTextTrans = transform.Find("ScoreText");
            SwitchTrans = transform.Find("SwitchBtn");
            RankingTrans = transform.Find("RankingBtn");
            StartTrans = transform.Find("StartBtn");
            MusicToggleTrans = transform.Find("SettingBtn/MusicToggle");
            HaveNewLockedSign = transform.Find("SwitchBtn/UnLockedSign");
            Tog_Music = MusicToggleTrans.GetComponent<Toggle>();

            Tog_Music.isOn = !AudioMgr.GetIsMuteByKey(GameData.FunJumpEffectStatus);
            OnOpen();

            txt_Score = ScoreTextTrans.GetComponent<Text>();
            PanelImages = new Image[5] { LogoTrans.GetChild(0).GetComponent<Image>(),
            SwitchTrans.GetChild(0).GetComponent<Image>(),
            StartTrans.GetChild(0).GetComponent<Image>(),
            RankingTrans.GetChild(0).GetComponent<Image>(),
            HaveNewLockedSign.GetComponent<Image>()
            };

            SettingTrans.GetComponent<Button>().onClick.AddListener(() =>
            {
                MusicToggleTrans.localScale = MusicToggleTrans.localScale == Vector3.one ? Vector3.zero : Vector3.one;
            });
            SwitchTrans.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClose(() =>
                {
                    UIMgr.Instance.Close(this.name);
                    UIMgr.Instance.Open(UIPath.UIGameBThemePanel);
                });
            });
            RankingTrans.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnClose(() =>
                {
                    UIMgr.Instance.Close(this.name);
                    UIMgr.Instance.Open(UIPath.UIGameBRankingPanel);
                });
            });
            StartTrans.GetComponent<Button>().onClick.AddListener(() =>
            {
                UIMgr.Instance.Close(this.name);
                GameBManager.Instance.cameraManager.OnStartGame();
                GameBManager.Instance.SetGameStart();
            });

            BackBtnTrans.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameTools.SetLoadingActive(true);
                JResource.LoadSceneAsync("Game.unity", () =>
                {
                    UIMgr.Instance.Close(UIPath.UIGameBStartPanel);
                    CoroutineMgr.Instance.Coroutine_StopAll();
                    CoroutineMgr.Instance.Coroutine_Start(OnLoadedScene());
                });
            });

            Tog_Music.onValueChanged.AddListener((open) =>
            {
                AudioMgr.switch_effect(GameData.FunJumpEffectStatus);
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_Jump_Enter_Homepage, SetGameStartData);


            NetMgr.Instance.C2S_Game_Jump_Enter_Homepage();

            SwitchTheme();
        }

        public override void ReleasePanel()
        {
            base.ReleasePanel();
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_Jump_Enter_Homepage, SetGameStartData);

        }

        public void SetGameStartData(object o)
        {
            DataJumpGame dataJumpGame = o as DataJumpGame;
            txt_Score.text = dataJumpGame.topScore.ToString();
            HaveNewLockedSign.localScale = dataJumpGame.hasNewUnlock ? Vector3.one : Vector3.zero;
        }

        private void SwitchTheme()
        {
            if (CurrentThemeId != GameBData.ThemeId)
            {
                CurrentThemeId = GameBData.ThemeId;
                for (int i = 0; i < PanelImages.Length; i++)
                {
                    PanelImages[i].color = GameBData.ThemeColors[CurrentThemeId - 1];
                }
            }
        }

        IEnumerator OnLoadedScene()
        {
            yield return new WaitForSeconds(1f);
            //AudioMgr.PlayMusic(AudioConfig.ProjectB_BGM, GameData.SystemAudioStatus);
            GameTools.SetLoadingActive(false);
            UIMgr.Instance.Open(UIPath.UIBGameHomePanel);
        }

        void OnOpen()
        {
            MusicToggleTrans.localScale = Vector3.zero;

            LogoTrans.localScale = Vector3.zero;
            ScoreTextTrans.localScale = Vector3.zero;
            StartTrans.localScale = Vector3.zero;

            SwitchTrans.localScale = Vector3.zero;
            RankingTrans.localScale = Vector3.zero;

            SettingTrans.localScale = Vector3.zero;
            BackBtnTrans.localScale = Vector3.zero;

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(LogoTrans.DOScale(Vector3.one, 0.2f));
            mySequence.Insert(0, ScoreTextTrans.DOScale(Vector3.one, 0.2f));
            mySequence.Insert(0, StartTrans.DOScale(Vector3.one, 0.2f));
            mySequence.Append(SwitchTrans.DOScale(Vector3.one, 0.1f));
            mySequence.Insert(0, RankingTrans.DOScale(Vector3.one, 0.1f));
            mySequence.Append(SettingTrans.DOScale(Vector3.one, 0.1f));
            mySequence.Insert(0, BackBtnTrans.DOScale(Vector3.one, 0.1f));

        }

        void OnClose(Action func = null)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(LogoTrans.DOScale(Vector3.zero, 0.15f));
            mySequence.Insert(0, ScoreTextTrans.DOScale(Vector3.zero, 0.15f));
            mySequence.Insert(0, StartTrans.DOScale(Vector3.zero, 0.15f));
            mySequence.Insert(0, SwitchTrans.DOScale(Vector3.zero, 0.15f));
            mySequence.Insert(0, RankingTrans.DOScale(Vector3.zero, 0.15f));
            mySequence.Insert(0, SettingTrans.DOScale(Vector3.zero, 0.15f));
            mySequence.Insert(0, BackBtnTrans.DOScale(Vector3.zero, 0.15f));
            mySequence.OnComplete(() => func());
        }

    }
}
