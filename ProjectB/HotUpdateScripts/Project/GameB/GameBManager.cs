using DG.Tweening;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.Game.GameA.Data;
using HotUpdateScripts.Project.GameB.Data;
using JEngine.Core;
using My.Msg;
using My.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace HotUpdateScripts.Project.GameB
{
    /// <summary>
    /// 跳跳乐游戏
    /// </summary>
    public class GameBManager : MonoBehaviour
    {
        public static GameBManager Instance { get; set; }

        public CameraManager cameraManager;

        public BlockSpawner blockSpawner;

        public GameObject CharacterGo;

        public List<GameObject> BoxList;

        private GameBStatus GameStatus;

        public bool IsRotatingJump;//旋转跳跃

        public bool IsPerfaceJump;//完美跳跃

        public int CharacterId;

        public int ThemeId;

        public int CurrentScore = 0;//当前得分

        public int stackCount = 0;//当前砖块数量

        public int jumpCounter = 0;

        private int perfectTimes = 0;

        private Text txt_Score, txt_ScoreBg;

        //private MeshRenderer BGRenderer;//主题背景
        private Skybox ThemeSkyBox;//主题背景

        private GameObject SceneModelGo;

        private Text txt_AddScore;

        public int nextDirection = 0;//0无 1 左  2右

        private float todayGoldCoin;//金币数量

        private float gameGoldCoin;//本局游戏金币数量

        private GameObject GoldCoinGo;

        private GameObject GoldCoinTextGo;

        private Transform trans_GoldIcon;

        private Text txt_GoldCoin;

        private Text txt_GoldCoin1;

        private Slider slider_GoldCoin;

        Tween textTween;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            cameraManager = GameObject.FindGameObjectWithTag("MainCamera").AddComponent<CameraManager>();
            cameraManager.enabled = false;
            cameraManager.Init();

            ThemeSkyBox = cameraManager.gameObject.GetComponent<Skybox>();
            //BGRenderer = GameObject.Find("BackGround").GetComponent<MeshRenderer>();

            blockSpawner = GameObject.Find("Spawned").AddComponent<BlockSpawner>();

            txt_Score = GameObject.Find("Canvas/ScoreText").GetComponent<Text>();
            txt_ScoreBg = GameObject.Find("Canvas/ScoreTextBg").GetComponent<Text>();
            txt_ScoreBg.text = txt_Score.text = "";

            GoldCoinTextGo = GameObject.Find("Canvas/GoldIconBg");
            GoldCoinTextGo.SetActive(false);
            trans_GoldIcon = GoldCoinTextGo.transform.GetChild(3);
            txt_GoldCoin = GoldCoinTextGo.transform.GetChild(0).GetComponent<Text>();
            txt_GoldCoin1 = GoldCoinTextGo.transform.GetChild(1).GetComponent<Text>();
            txt_AddScore = GameObject.Find("AddScoreCanvas/Text").GetComponent<Text>();

            slider_GoldCoin = GoldCoinTextGo.transform.Find("Slider").GetComponent<Slider>();
            txt_AddScore.text = "";
            txt_AddScore.transform.position = Vector3.one * 1000;
            //textTween = txt_AddScore.transform.DOMove(txt_AddScore.transform.position + Vector3.right, 0.5f);
            //textTween = txt_AddScore.DOFade(0,0.5f);

            CharacterId = GameBData.CharacterId;
            ThemeId = GameBData.ThemeId;

            OnSwitchCharacter(null);
            OnSwitchTheme(null);

            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);

            MsgCenter.RegisterMsg(null, MsgCode.GameB_SwitchCharacter, OnSwitchCharacter);

            MsgCenter.RegisterMsg(null, MsgCode.GameB_SwitchTheme, OnSwitchTheme);

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, SetGameStartData);


            UIMgr.Instance.Open(UIPath.UIGameBStartPanel);
        }

        void OnDestroy()
        {
            if (CharacterGo != null)
            {
                Destroy(CharacterGo);
            }
            if (SceneModelGo != null)
            {
                Destroy(SceneModelGo);
            }


            MsgCenter.UnRegisterMsg(null, MsgCode.GameB_SwitchCharacter, OnSwitchCharacter);

            MsgCenter.UnRegisterMsg(null, MsgCode.GameB_SwitchTheme, OnSwitchTheme);

            //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);

            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, SetGameStartData);
        }

        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{
        //    debug.Log_Blue("更新玩游戏任务状态成功");
        //    GameData.taskInfo = null;
        //}

        void SetGameStartData(object o)
        {
            debug.Log_yellow("todayGoldCoin--");
            todayGoldCoin = DataMgr.Instance.dataBUpdataData.todayTotalGetLoveMoneyFromGame == null ?
                0 : float.Parse(DataMgr.Instance.dataBUpdataData.todayTotalGetLoveMoneyFromGame);
            txt_GoldCoin1.text = txt_GoldCoin.text = this.todayGoldCoin.ToString();
            slider_GoldCoin.value = todayGoldCoin / 3;
        }

        /// <summary>
        /// 切换角色
        /// </summary>
        /// <param name="id">1-9</param>
        public void OnSwitchCharacter(object o)
        {
            int characterId = GameBData.CharacterId;
            if (CharacterGo != null)
            {
                Destroy(CharacterGo);
            }
            CharacterGo = Instantiate(JResource.LoadRes<GameObject>("GameB/Character/Animal_Pig_" + characterId + ".prefab", JResource.MatchMode.Prefab));
            CharacterGo.AddComponent<PlayerController>();
            CharacterGo.transform.localPosition = Vector3.zero;
            CharacterGo.name = "Character";
        }

        /// <summary>
        /// 切换主题
        /// </summary>
        /// <param name="themeId">1-6</param>
        public void OnSwitchTheme(object o)
        {
            int themeId = GameBData.ThemeId;
            if (ThemeSkyBox != null)
            {
                if (ThemeSkyBox.material = null)
                {
                    Destroy(ThemeSkyBox.material);
                }
                ThemeSkyBox.material = JResource.LoadRes<Material>("GameB/BackGround/changjing" + themeId + ".mat", JResource.MatchMode.Material);
            }
            if (SceneModelGo != null)
            {
                Destroy(SceneModelGo);
            }
            SceneModelGo = Instantiate(JResource.LoadRes<GameObject>("GameB/Theme/ThemeModel0" + themeId + ".prefab", JResource.MatchMode.Prefab));
            SceneModelGo.transform.localPosition = Vector3.zero;

            blockSpawner.RefreshData();
        }

        public GameObject GetCharacter()
        {
            return CharacterGo;
        }

        public int GetNextDirection()
        {
            return nextDirection;
        }

        bool GoldCoinMax()
        {
            return todayGoldCoin >= 3;
        }

        public void SetPerfectJump(int count)
        {
            IsPerfaceJump = true;
            //if (perfectTimes < count)
            //{
            //    perfectTimes = count;
            //}
            perfectTimes++;
            switch (count)
            {
                case 1:
                    AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1f);
                    AddScore(1);
                    break;
                case 2:
                    AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1.1f);
                    AddScore(2);
                    break;
                case 3:
                    AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1.2f);
                    AddScore(2);
                    break;
                case 4:
                    AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1.3f);
                    AddScore(3);
                    break;
                case 5:
                    AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1.4f);
                    AddScore(3);
                    break;
                case 6:
                    AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1.5f);
                    AddScore(3);
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1.6f);
                    AddScore(4);
                    break;
                default:
                    break;
            }
            if (count == 10)
            {
                if (Random.value < 0.5f)
                {
                    StartCoroutine(AddGoldCoin(0.5f));
                }
            }
            if (count > 10)
            {
                AudioMgr.play_effect(AudioConfig.GameB_PerfectOnLand, GameData.FunJumpEffectStatus, ".wav", pitch: 1.7f);
                AddScore(6);
                IsRotatingJump = true;
            }

            cameraManager.BeginMove();
            //加分特效移动
            Vector3 screenPos = (Camera.main.WorldToScreenPoint(new Vector3(CharacterGo.transform.position.x + 3, CharacterGo.transform.position.y - 0.6f, CharacterGo.transform.position.z)));
            Vector3 pos = Camera.main.ScreenToWorldPoint(screenPos);
            txt_AddScore.transform.position = pos;
            txt_AddScore.transform.DOMove(txt_AddScore.transform.position + Vector3.right * 1.2f, 0.8f).OnComplete(() => { txt_AddScore.transform.position = Vector3.one * 1000; });
        }

        public void SetCommonJump()
        {
            cameraManager.BeginMove();
            AddScore();
            IsRotatingJump = false;
            IsPerfaceJump = false;
            AudioMgr.play_effect(AudioConfig.GameB_OnLand, GameData.FunJumpEffectStatus, ".wav");//播放音效
        }

        private void AddScore(int score = 0)
        {
            CurrentScore += (1 + score);
            if (score > 0)
            {
                txt_AddScore.text = "+" + score.ToString();
            }
            txt_ScoreBg.text = txt_Score.text = CurrentScore.ToString();
            if (!GoldCoinMax())
            {
                AddGoldCoin(CurrentScore);
            }
        }

        int AddGoldSign = 0;
        //1——20分 5%+0.05
        //21——50分 10%+0.05
        //51—— 100分 10%+0.1
        //101——200分 5%+0.2
        //201——~~分 10%+0.3
        //完美跳连10个 50%+0.5
        private void AddGoldCoin(int score)
        {
            int a = Random.Range(0, 100);

            if (GoldCoinGo == null)
            {
                GoldCoinGo = JResource.LoadRes<GameObject>("GameB/GoldCoinGameB.prefab", JResource.MatchMode.Prefab);
                GoldCoinGo.SetActive(false);
            }
            if (score > 0 && AddGoldSign == 0)
            {
                AddGoldSign = 1;
                if (a < 5) StartCoroutine(AddGoldCoin(0.05f));

            }
            else if (score >= 20 && AddGoldSign == 1)
            {
                AddGoldSign = 2;
                if (a < 10) StartCoroutine(AddGoldCoin(0.05f));
            }
            else if (score >= 50 && AddGoldSign == 2)
            {
                AddGoldSign = 3;
                if (a < 10) StartCoroutine(AddGoldCoin(0.1f));
            }
            else if (score >= 100 && AddGoldSign == 3)
            {
                AddGoldSign = 4;
                if (a < 5) StartCoroutine(AddGoldCoin(0.2f));
            }
            else if (score >= 200 && AddGoldSign == 4)
            {
                AddGoldSign = 5;
                if (a < 10) StartCoroutine(AddGoldCoin(0.3f));
            }

        }

        private IEnumerator AddGoldCoin(float num)
        {
            if (todayGoldCoin >= 3)
            {
                yield break;
            }
            gameGoldCoin += num;
            todayGoldCoin += num;
            var go = Instantiate(GoldCoinGo);
            go.transform.position = GetCharacter().transform.position + Vector3.up * 2.5f;
            go.SetActive(true);
            AudioMgr.play_effect(AudioConfig.Game_GetGoldCoin, GameData.FunJumpEffectStatus, endPath: ".wav");//播放音效
            Destroy(go, 3.5f);
            yield return new WaitForSeconds(2);
            go.transform.DOScale(0.8f, 1);
            var pos = Camera.main.ScreenToWorldPoint(new Vector3(trans_GoldIcon.position.x, trans_GoldIcon.position.y, 13));
            go.transform.DOMove(pos + Vector3.up * 0.5f, 1).OnComplete(() =>
               {
                   var effect = go.transform.GetChild(0).gameObject;
                   effect.SetActive(true);
               });
            float realGoldCoin = todayGoldCoin >= 3 ? 3 : todayGoldCoin;
            txt_GoldCoin.text = realGoldCoin.ToString("F2");
            txt_GoldCoin1.text = realGoldCoin.ToString("F2");
            slider_GoldCoin.value = realGoldCoin / 3;
        }


        public int GetStackCount()
        {
            return stackCount;
        }

        //游戏开始
        public void SetGameStart()
        {
            GoldCoinTextGo.SetActive(true);
            GameStatus = GameBStatus.Gaming;
            GetCharacter().GetComponent<PlayerController>().SetReset();
            DataMgr.Instance.dataBProject.behaviorCode = GameData.userStatics[(int)BehaviorCode.PROJECT_ARCHITECTURE_JUMP];
            NetMgr.Instance.C2S_Project_UserBehaviorStatistics();
        }

        //开始游戏状态
        public bool IsGaming()
        {
            return GameStatus == GameBStatus.Gaming;
        }

        public void SetGameOver()
        {
            debug.Log_yellow("Game Over");
            GameStatus = GameBStatus.GameOver;
            DataMgr.Instance.dataJumpGameRecord.gameScoreOfThisBureau = CurrentScore;
            DataMgr.Instance.dataJumpGameRecord.jumpTimesOfThisBureau = jumpCounter;
            DataMgr.Instance.dataJumpGameRecord.perfectJumpTimesOfThisBureau = perfectTimes;
            DataMgr.Instance.dataJumpGameRecord.gameOverTimeOfThisBureau = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DataMgr.Instance.dataJumpGameRecord.loveMoney = this.gameGoldCoin.ToString();

            NetMgr.Instance.C2S_Game_Jump_Upload_Score();//上传本局游戏数据

            //if (GameData.taskInfo != null && GameData.taskInfo.taskType == 351)
            //{
            //    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
            //    DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
            //    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
            //    NetMgr.Instance.C2S_Love_TaskUpdateItem();
            //}
            //----------------设置任务完成------------
            var task = DataMgr.Instance.GetTaskItemByType(351);
            if (task != null && task.taskStatus == 0)
            {
                DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                DataMgr.Instance.dataBTaskInfoReq.taskId = task.id;
                DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = task.taskCurProgress + 1;
                NetMgr.Instance.C2S_Love_TaskUpdateItem();
            }
            //---------------------------------------

            IsRotatingJump = false;
            IsPerfaceJump = false;
            txt_ScoreBg.text = txt_Score.text = "";
            nextDirection = 0;

            blockSpawner.ShowAllBlocks();
            cameraManager.OnGameOver(() =>
            {
                blockSpawner.CleanBlocks();
                CharacterGo.GetComponent<PlayerController>().SetReset();
                SetResetData();
                cameraManager.Restart(() =>
                {
                    UIMgr.Instance.Open(UIPath.UIGameBStartPanel);
                });

            });
            CurrentScore = stackCount = perfectTimes= jumpCounter = 0;
        }


        private void SetResetData()
        {
            GoldCoinTextGo.SetActive(false);
            AddGoldSign = 0;
            gameGoldCoin = 0;
            txt_GoldCoin.text = "0";
            txt_GoldCoin1.text = "0";
        }
    }
}
