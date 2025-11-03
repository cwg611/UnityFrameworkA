using HotUpdateScripts.Project.BasePrj.Data;
using My.UI;
using UnityEngine;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// UI界面Prefab配置
    /// </summary>
    public class UIPath
    {
        //主工程 BasePrj UIB开头
        public const string UIBMainHomePanel = "UIBMainHomePanel";
        public const string UIBMainBagPanel = "UIBMainBagPanel";
        public const string UIBMainBeginnerGuidePanel = "UIBMainBeginnerGuidePanel";
        public const string UIBActivityPanel = "UIBActivityPanel";
        public const string UIBSuggestionPanel = "UIBSuggestionPanel";

        public const string UIBLoveHomePanel = "UIBLoveHomePanel";
        public const string UIBLoveTaskPanel = "UIBLoveTaskPanel";
        public const string UIBDonateHomePanel = "UIBDonateHomePanel";
        public const string UIBDonateDetailPanel = "UIBDonateDetailPanel";
        public const string UIBHonorHomePanel = "UIBHonorHomePanel";

        public const string UIBGameHomePanel = "UIBGameHomePanel";
        public const string UIBFriendHomePanel = "UIBFriendHomePanel";
        public const string UIBFriendChatPanel = "UIBFriendChatPanel";

        public const string CommonTipPanel = "CommonTipPanel";

        public const string UILoadingPanel = "UILoadingPanel";

        public const string UIGameAStartPanel = "UIGameAStartPanel";

        public const string UIGameBStartPanel = "UIGameBStartPanel";

        public const string UIGameBThemePanel = "UIGameBThemePanel";

        public const string UIBUnLockProgressPanel = "UIBUnLockProgressPanel";

        public const string UIGameBRankingPanel = "UIGameBRankingPanel";

        public const string UILeaderBorderPanel = "UILeaderBorderPanel";

        public const string UIBShopPanel = "UIBShopPanel";

        public const string UIBBigImgPanel = "UIBBigImgPanel";

        public const string UILoginPanel = "UILoginPanel";

        public const string UIBLuckyRotatePanel = "UIBLuckyRotatePanel";

        public const string UIBShopRecordPanel = "UIBShopRecordPanel";

        public const string UIBLoveWindowPanel = "UIBLoveWindowPanel";

        public const string UIBDonateResultPanel = "UIBDonateResultPanel";

        public const string UIBShopDescPanel = "UIBShopDescPanel";

        public const string UIBShopReallyDescPanel = "UIBShopReallyDescPanel";

        public const string UIBGetRewardPanel = "UIBGetRewardPanel";

        public const string UIBFriendFocusListPanel = "UIBFriendFocusListPanel";

        public const string UIBBlackListPanel = "UIBBlackListPanel";

        public const string UIBFriendDataEditorPanel = "UIBFriendDataEditorPanel";

        public const string UIBFriendMatchPanel = "UIBFriendMatchPanel";

        public const string UIBFriendProfilePanel = "UIBFriendProfilePanel"; //好友资料展示界面

        public const string CommonNetErrorPanel = "CommonNetErrorPanel"; //网络重连界面


        public const string CommonVideoPanel = "CommonVideoPanel"; //播放器界面 Unity VideoPlayer
        public const string CommonMediaPlayer = "CommonMediaPlayer";//播放器界面 AVProVideo

        public const string UIBFriendEditorNotePanel = "UIBFriendEditorNotePanel"; //修改备注界面

        public const string UISettingPanel = "UISettingPanel"; //设置界面

        public const string UIBRedPackagePanel = "UIBRedPackagePanel"; //红包界面

        //子工程 ChildPrjA UICA开头

    }

    public class GameInit : MonoBehaviour
    {

        private void Awake()
        {
            NetInit();
            UIInit();
        }

        private void Start()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            //Application.runInBackground = true;
        }


        #region 各种初始化,游戏启动进入主工程执行

        void UIInit()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            //初始化音频管理器
            if (GameData.isFirstInitAudioMgr)
            {
                GameData.isFirstInitAudioMgr = false;
                AudioMgr.init();
            }

            //UI面板 初始化
            UIMgr.Instance.Init();
            if (GameData.isFirst)
            {
                GameData.isFirst = false;
                //UIMgr.Instance.Open(UIPath.UILoginPanel, null, UILayer.Layer3);
            }
            else
            {
                UIMgr.Instance.Open(UIPath.UIBMainHomePanel, null, UILayer.Layer3);
            }
            //UIMgr.Instance.Open(UIPath.CommonTipPanel, null, UILayer.Layer5);
            //UIMgr.Instance.Open(UIPath.UILoadingPanel, null, UILayer.Layer5);

        }

        void NetInit()
        {
            NetMgr.Instance.Init();
        }
        #endregion
    }
}
