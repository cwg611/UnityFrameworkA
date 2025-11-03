using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using JEngine.Core;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.ACommon;
using My.UI;
using My.Msg;
using My.UI.Panel;
using DG.Tweening;
using HotUpdateScripts.Project.Game.GameA.Data;

namespace HotUpdateScripts.Project_Daxigua
{
    public class GameA : MonoBehaviour
    {
        public List<Fruit> fruitPrefabList = new List<Fruit>();
        public GameObject fruitRoot;
        public RectTransform spawnPoint;
        private Vector3 spawnPointPos;
        public Button playButton;
        public Text scoreLabel;
        private Animation ani_Score;

        private Text txt_GoldCoin;
        //private Animation ani_GoldCoin;
        private Transform trans_GoldIcon;
        /// <summary>
        /// 游戏分数
        /// </summary>
        public int score;
        /// <summary>
        /// 游戏金币数
        /// </summary>
        public float todayGoldCoin;
        private GameObject GoldCoinGO;
        private float gameGoldCoin;//本局游戏金币数量
        private Slider slider_GoldCoin;

        private Fruit fruit;
        private int fruidId;
        private static bool isGameOver;

        public GameObject backGround;
        public Button btn_Back;

        /// <summary>
        /// 当前场景中所有的水果对象
        /// </summary>
        private List<Fruit> fruits = new List<Fruit>();

        private bool isSpwan = false;

        private Coroutine coroutine_StSpawnNextFruitop;

        float fruitX;

        bool isClickBack = false;

        private int[] randomRange = { 0, 1, 2, 2, 3, 3, 3, 4, 4, 4 }; //0:10%  1:10% 2:20%  3:20%    4 5:30%

        #region 计算帧率

        private float m_LastUpdateShowTime = 0f;    //上一次更新帧率的时间;

        private float m_UpdateShowDeltaTime = 0.01f;//更新帧率的时间间隔;

        private int m_FrameUpdate = 0;//帧数;

        private float m_FPS = 0;

        private Text FPSText;

        private int timer = 30;

        #endregion

        #region 弹框
        private GameObject mask, btn_Back_window; //    返回按钮时
        private Image icon; //头像
        private Text nickName, txt_Score, txt_num;// 昵称  分数  合成猪的数量
        private int num_Pig = 0;
        private bool isNeedBack = true;
        #endregion

        private void Awake()
        {
            mask = GameTools.GetByName(transform.parent, "mask");
            icon = GameTools.GetByName(transform.parent, "icon").GetComponent<Image>();
            nickName = GameTools.GetByName(transform.parent, "nickName").GetComponent<Text>();
            txt_Score = GameTools.GetByName(transform.parent, "txt_Score").GetComponent<Text>();
            txt_num = GameTools.GetByName(transform.parent, "txt_num").GetComponent<Text>();
            btn_Back_window = GameTools.Instance.AddClickEvent(GameTools.GetByName(transform.parent, "btn_Back_window"), OnClick_btn_Back_window);
            txt_GoldCoin = GameObject.Find("bg_goldCoin/Num").GetComponent<Text>();
            // ani_GoldCoin = txt_GoldCoin.GetComponent<Animation>();

            ani_Score = scoreLabel.transform.GetComponent<Animation>();
            trans_GoldIcon = GameObject.Find("bg_goldCoin/Icon").transform;
            spawnPointPos = spawnPoint.position;
            slider_GoldCoin = GameObject.Find("bg_goldCoin/Slider").GetComponent<Slider>();

            FPSText = GameTools.GetByName(transform, "FpsText").GetComponent<Text>();
            GameTools.Instance.MatchingAnimatorToCamera(backGround, Fill_2D.StretchToFit);

            UIMgr.Instance.Open(UIPath.UIGameAStartPanel);
            for (int i = 0; i < fruitRoot.transform.childCount; i++)
            {
                fruitPrefabList.Add(fruitRoot.transform.GetChild(i).gameObject.GetHotClass<Fruit>());
            }

            playButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                isNeedBack = false;
                isClickBack = true;
                Restart();
            });

            btn_Back.onClick.AddListener(() =>
            {
                isClickBack = true;
                //在GameOver及退出时，判断分数 进行上传
                SaveScoreToServer(this.score);
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Game_SaveGameDXGScore, S2C_Game_SaveGameDXGScoreCallBack);
            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, SetGameStartData);

            Application.targetFrameRate = 60;
        }

        void SaveScoreToServer(int saveScore)
        {
            DataMgr.Instance.dataBGameDXG_SaveScoreReq.userId = GameData.userId;
            DataMgr.Instance.dataBGameDXG_SaveScoreReq.gameName = GameData.GameAName;
            DataMgr.Instance.dataBGameDXG_SaveScoreReq.gameScore = saveScore;
            DataMgr.Instance.dataBGameDXG_SaveScoreReq.loveMoney = this.gameGoldCoin.ToString();
            NetMgr.Instance.C2S_Game_SaveGameDXGScore();
        }

        void S2C_Game_SaveGameDXGScoreCallBack(object o)
        {
            debug.Log_Blue("游戏数据保存成功");
            backGame();
        }

        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{
        //    debug.Log_Blue("更新玩游戏任务状态成功");
        //    GameData.taskInfo = null;
        //}

        void SetGameStartData(object o)
        {
            todayGoldCoin = DataMgr.Instance.dataBUpdataData.todayTotalGetLoveMoneyFromGame == null ?
                0 : float.Parse(DataMgr.Instance.dataBUpdataData.todayTotalGetLoveMoneyFromGame);
            txt_GoldCoin.text = this.todayGoldCoin.ToString();
            slider_GoldCoin.value = todayGoldCoin / 3;
        }

        bool GoldCoinMax()
        {
            return todayGoldCoin >= 3;
        }

        private void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Game_SaveGameDXGScore, S2C_Game_SaveGameDXGScoreCallBack);
            //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Update_UpdateGameData, SetGameStartData);

        }

        //退出时清除全屏,分数清0 
        private void backGame()
        {
            GameData.isGameStart = false;

            isGameOver = true;
            mask.SetActive(false);

            for (int i = 0; i < fruits.Count; i++)
            {
                fruits[i].SetSimulated(false);
                GameObject.Destroy(fruits[i].gameObject);
            }
            CoroutineMgr.Instance.Coroutine_StopAll();
            fruits.Clear();

            fruit = SpawnNextFruit();
            score = 0;
            gameGoldCoin = 0;
            txt_GoldCoin.text = "0";
            scoreLabel.text = "0";
            num_Pig = 0;
            isGameOver = false;
            isSpwan = false;
            UIMgr.Instance.Open(UIPath.UIGameAStartPanel);
            UIGameAStartPanel.refreshGameData();
            isClickBack = false;
        }

        public void Start()
        {
            CoroutineMgr.Instance.Coroutine_Start(Run_Func());
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
        }

        IEnumerator Run_Func()
        {
            yield return 1;
            fruit = SpawnNextFruit();
        }

        public void Update()
        {
            m_FrameUpdate++;
            if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
            {
                m_FPS = m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime);
                m_FrameUpdate = 0;
                m_LastUpdateShowTime = Time.realtimeSinceStartup;
            }

            timer--;
            if (timer < 0)
            {
                timer = 30;
                FPSText.text = "FPS: " + Mathf.Ceil(m_FPS).ToString();
            }

            if (isGameOver || !GameData.isGameStart || GameData.isGameReturn)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (fruit.getIsDrag()) return;
                if (isClickBack)
                {
                    isClickBack = false;
                    return;
                }

                if (fruit.GetComponent<SpriteRenderer>() == null) return;
                fruitX = 20;
            }

            if (Input.GetMouseButton(0))
            {
                if (fruit.getIsDrag()) return;
                if (isClickBack) return;
                if (Input.mousePosition.x > fruitX && Input.mousePosition.x < Screen.width - fruitX)
                {
                    var mousePos = Input.mousePosition;
                    var wolrdPos = Camera.main.ScreenToWorldPoint(mousePos);
                    var fruitPos = new Vector3(wolrdPos.x, spawnPointPos.y, 0);
                    fruit.gameObject.transform.position = Vector3.MoveTowards(fruit.gameObject.transform.position, fruitPos, 250 * Time.deltaTime);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (fruit.getIsDrag()) return;
                if (isClickBack) return;
                fruit.setIsDrag(true);
                fruit.SetSimulated(true);
                fruit.closeAnimation();
                if (!isSpwan)
                {
                    if (coroutine_StSpawnNextFruitop != null)
                    {
                        CoroutineMgr.Instance.Coroutine_Stop(coroutine_StSpawnNextFruitop);
                    }
                    coroutine_StSpawnNextFruitop = CoroutineMgr.Instance.Coroutine_Start(StSpawnNextFruitop());
                }
            }
        }

        private Fruit SpawnNextFruit()
        {
            //按概率取值
            var rand = Random.Range(0, 10);
            var getValue = randomRange[rand];
            if (getValue == 4)
            {
                getValue = Random.Range(0, 2) == 0 ? 4 : 5;
            }
            var prefab = fruitPrefabList[getValue].gameObject;
            var pos = spawnPointPos;
            return SpawnFruit(prefab, pos);
        }

        private Fruit SpawnFruit(GameObject prefab, Vector3 pos)
        {
            GameObject obj = GameObject.Instantiate(prefab, pos, new Quaternion(0, 0, 0, 0));
            Fruit f = obj.GetHotClass<Fruit>();
            f.SetSimulated(false);
            f.id = fruidId++;
            f.OnLevelUp = (a, b) =>
            {
                if (IsFruitExist(a) && IsFruitExist(b))
                {
                    var pos1 = a.gameObject.transform.position;
                    var pos2 = b.gameObject.transform.position;
                    var pos_num = (pos1 + pos2) * 0.5f;
                    RemoveFruit(a);
                    RemoveFruit(b);
                    AddScore(a.score);
                    int rangeValue = Random.Range(0, 2);
                    GameObject m_NextPrefab;
                    if (rangeValue == 0)
                    {
                        m_NextPrefab = a.nextLevelPrefab1;
                    }
                    else
                    {
                        m_NextPrefab = a.nextLevelPrefab2;
                    }
                    var fr = SpawnFruit(m_NextPrefab, pos_num);
                    debug.Log_Red(m_NextPrefab.name);
                    if (!GoldCoinMax())
                    {
                        AddGoldCoin(m_NextPrefab.name);
                    }
                    fr.isGroundPlay = true;
                    fr.SetSimulated(true);
                    if (fr.nextLevelPrefab1.name == fr.name && fr.nextLevelPrefab2.name == fr.name)
                    {
                        Debug.Log("<Color=green>恭喜合成了一个大西瓜</Color>");
                        num_Pig += 1;
                    }
                }
            };

            f.OnGameOver = () =>
            {
                if (isGameOver == true)
                {
                    return;
                }
                OnGameOver();
            };

            fruits.Add(f);
            return f;
        }

        private void OnGameOver()
        {
            isGameOver = true;
            mask.SetActive(true);

            for (int i = 0; i < fruits.Count; i++)
            {
                fruits[i].SetSimulated(false);
                GameObject.Destroy(fruits[i].gameObject);
            }
            CoroutineMgr.Instance.Coroutine_StopAll();
            fruits.Clear();

            //判断是否是任务点击跳转来的
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
            setWindowView();
        }


        //结束弹框赋值
        private void setWindowView()
        {
            //昵称
            nickName.text = GameData.userNickName;
            //分数
            txt_Score.text = this.score.ToString();
            //局内合成猪的数量
            txt_num.text = num_Pig.ToString();
            //头像
            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (icon == null) return;
                icon.sprite = r;
                //GameTools.Instance.MatchImgBySprite(icon);
            }, GameData.userHeadImg);
        }


        public void Restart()
        {
            GameData.isGameStart = true;
            SaveScoreToServer(this.score);
        }

        private void restart_Back()
        {
            mask.SetActive(false);
            fruit = SpawnNextFruit();
            score = 0;
            gameGoldCoin = 0;
            txt_GoldCoin.text = "0";
            scoreLabel.text = "0";
            num_Pig = 0;
            isGameOver = false;
            isSpwan = false;
            if (isNeedBack)
            {
                UIMgr.Instance.Open(UIPath.UIGameAStartPanel);
                UIGameAStartPanel.refreshGameData();
            }
        }

        private void RemoveFruit(Fruit f)
        {
            for (int i = 0; i < fruits.Count; i++)
            {
                if (fruits[i].id == f.id)
                {
                    fruits.Remove(f);
                    GameObject.Destroy(f.gameObject);
                    return;
                }
            }
        }

        private bool IsFruitExist(Fruit f)
        {
            for (int i = 0; i < fruits.Count; i++)
            {
                if (fruits[i].id == f.id)
                {
                    return true;
                }
            }
            return false;
        }

        private void AddScore(int score)
        {
            this.score += score;
            scoreLabel.text = $"{this.score}";
            ani_Score.Play();
        }

        //
        private void AddGoldCoin(string name)
        {
            int a = Random.Range(0, 100);
            //a = 1;
            debug.Log_yellow(a);
            if (GoldCoinGO == null)
            {
                GoldCoinGO = JResource.LoadRes<GameObject>("GameA/GoldCoinGameA.prefab", JResource.MatchMode.Prefab);
                GoldCoinGO.SetActive(false);
            }

            switch (name)
            {
                case "Fruit_6":
                    if (a < 5)
                    {
                        StartCoroutine(AddGoldCoin(0.05f));
                    }
                    break;
                case "Fruit_7":
                    if (a < 10)
                    {
                        StartCoroutine(AddGoldCoin(0.1f));
                    }
                    break;
                case "Fruit_8":
                    if (a < 30)
                    {
                        StartCoroutine(AddGoldCoin(0.1f));
                    }
                    break;
                case "Fruit_9":
                    if (a < 50)
                    {
                        StartCoroutine(AddGoldCoin(0.2f));
                    }
                    break;
                case "Fruit_10":
                    if (a < 70)
                    {
                        StartCoroutine(AddGoldCoin(0.3f));
                    }
                    break;
                case "Fruit_11":
                    StartCoroutine(AddGoldCoin(0.5f));
                    break;
                default:
                    break;
            }
            debug.Log_yellow("金币————" + todayGoldCoin);
        }

        private IEnumerator AddGoldCoin(float num)
        {
            if (todayGoldCoin >= 3)
            {
                yield break;
            }
            todayGoldCoin += num;
            gameGoldCoin += num;
            var go = Instantiate(GoldCoinGO);
            go.SetActive(true);
            AudioMgr.play_effect(AudioConfig.Game_GetGoldCoin, GameData.GameAudioEffectStatus, endPath: ".wav");//播放音效
            Destroy(go, 4);
            yield return new WaitForSeconds(2);
            go.transform.DOScale(0.8f, 2);
            //var pos = GameTools.GetUIToWordPos(trans_GoldIcon);
            go.transform.DOMove(trans_GoldIcon.position, 1).OnComplete(() =>
            {
                var effect = go.transform.GetChild(0).gameObject;
                //effect.transform.position = go.transform.GetChild(1).position;
                effect.SetActive(true);
            });
            float realGoldCoin = todayGoldCoin >= 3 ? 3 : todayGoldCoin;
            txt_GoldCoin.text = realGoldCoin.ToString("F2");
            slider_GoldCoin.value = realGoldCoin / 3;
            //ani_GoldCoin.Play();
        }

        IEnumerator StSpawnNextFruitop()
        {
            isSpwan = true;
            yield return new WaitForSeconds(0.4f);
            fruit = SpawnNextFruit();
            isSpwan = false;
        }


        private void OnClick_btn_Back_window()
        {
            //游戏结束弹框返回按钮事件:返回到游戏开始界面
            isNeedBack = true;
            GameData.isGameReturn = true;
            Restart();
        }
    }

}
