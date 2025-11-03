using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UISettingPanel : BasePanel
    {
        private GameObject tipWindow;
        private Button btnClose, btnAudioOne, btnAudioTwo, btn_Reset, btn_Suggest;

        void Awake()
        {
            tipWindow = GameTools.GetByName(transform, "tipWindow");
            btnClose = GameTools.GetByName(transform, "btnClose").GetComponent<Button>();
            btnAudioOne = GameTools.GetByName(transform, "btnAudioOne").GetComponent<Button>();
            btnAudioTwo = GameTools.GetByName(transform, "btnAudioTwo").GetComponent<Button>();
            btn_Reset = GameTools.GetByName(transform, "btn_Reset").GetComponent<Button>();
            btn_Suggest = GameTools.GetByName(transform, "SuggestBtn").GetComponent<Button>();



            DOTweenMgr.Instance.OpenWindowScale(tipWindow, 0.3f);
            btnClose.onClick.AddListener(() =>
            {
                DOTweenMgr.Instance.CloseWindowScale(tipWindow, 0.2f, () =>
                {
                    UIMgr.Instance.Close(UIPath.UISettingPanel);
                });
            });

            //setStatusView();
            //btnAudioOne.onClick.AddListener(() =>
            //{
            //    AudioMgr.switch_music(AudioConfig.ProjectB_BGM,GameData.SystemAudioStatus);
            //    setStatusView();
            //    Debug.Log("关闭背景音乐");
            //});
            //btnAudioTwo.onClick.AddListener(() =>
            //{
            //    AudioMgr.switch_music(AudioConfig.ProjectB_BGM, GameData.SystemAudioStatus);
            //    setStatusView();
            //    Debug.Log("打开背景音乐");
            //});

            //重置数据（测试数据）
            btn_Reset.onClick.AddListener(() =>
            {
                NetMgr.Instance.C2S_Init_UserInfo(GameData.userId.ToString());
            });

            //优化建议
            btn_Suggest.onClick.AddListener(() => 
            {
                GameTools.SetTip("暂未开放，敬请期待！");
                //UIMgr.Instance.Open(UIPath.UIBSuggestionPanel);
            });
        }

        //void setStatusView()
        //{
        //    btnAudioOne.gameObject.SetActive(PlayerPrefs.GetInt(GameData.SystemAudioStatus) == 1);
        //    btnAudioTwo.gameObject.SetActive(PlayerPrefs.GetInt(GameData.SystemAudioStatus) == 0);
        //}
    }
}
