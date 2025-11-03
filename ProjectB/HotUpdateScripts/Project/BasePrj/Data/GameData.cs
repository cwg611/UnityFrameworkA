using HotUpdateScripts.Project.Common;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.Project.BasePrj.Data
{
    /// <summary>
    /// 服务器类型
    /// </summary>
    public enum ServerType
    {
        LocalServer,
        TestingServer,
        PrePublishingServer,
        OfficialServer
    }

    public enum CardType
    {
        emepty = 0,
        EnergyCard = 101,
        AcculateCard = 102,
        FriendCard = 103,
        LoveCard = 104,
    }

    public enum MessageType
    {
        commonType = 1,
        ImageTypeOne = 2,
        SensitiveRemind = 3,
        SystemMeaasge = 4,
    }
    /// <summary>
    /// 定义用户行为
    /// </summary>
    public enum BehaviorCode
    {
        PROJECT_ENTER_PROJECT = 0,//进入项目
        PROJECT_PLAYGAME_ZodiacMerge,//玩 ZodiacMerge 游戏
        PROJECT_ARCHITECTURE_HOPE,//希望灯塔
        PROJECT_ARCHITECTURE_LOVE, //爱心工坊
        PROJECT_ARCHITECTURE_HONOR,//荣誉展厅
        PROJECT_ARCHITECTURE_GAME,//游戏大厅
        PROJECT_ARCHITECTURE_FRIEND,//交友大厅
        PROJECT_ARCHITECTURE_SHOP,//兑换商场
        PROJECT_ARCHITECTURE_LUCKY,//幸运小屋
        PROJECT_ARCHITECTURE_JUMP,//跳跳乐
    }

    public enum RankingType
    {
        Day = 1,
        Week = 2,
        All = 3
    }


    /// <summary>
    /// 表情对应的名字
    /// </summary>
    public struct EmojiInfo
    {
        public string name;
        public float x;
        public float y;
        public float size;
    }

    public class GameData
    {
        public static ServerType CurrentServer = ServerType.PrePublishingServer;

        public static int taskId_sendToNative = 0; //发送给原生的任务Id

        public static bool isFirstOpenPlanet = true;

        public static long userId = -1; //目前测试使用，后面改为  -1

        public static string userHeadImg; //用户头像

        public static Sprite userHeadSprite;//用户头像的图片

        public static Sprite chatFriendHeadSprite;//当前聊天好友的头像

        public static string userNickName;//用户昵称

        public static int allUnreadChatMessageNum;//未读消息数量

        public static int curLoveNum;//当前爱心数

        public static int curEnergy;//当前能量数

        public static int curDonateLoveNum; //当前总捐献数

        public static int accumulativeGetLoveNum; //累计孵化

        public static string curLoveMoney; //当前爱心币
        //public static int curLoveMoney; //当前爱心币

        public static int isHatching; //是否在孵化中 1：正在孵化 0:没有孵化

        public static int canDraw;////当前是否可抽奖  1可以 0不可

        public static int sendTaskId;//当前领取的任务ID

        public static int consumeOil = 10;//每次消耗10

        public static int cur_ProjectId = -1; //当前点击的捐献项目Id

        public static NewProjectDonateInfo newProjectDonateInfo; //捐献后最新数据

        public static bool isNewDonate; //是否捐赠爱心

        public static bool isGameStart = false; //合成游戏开始

        public static bool isGameReturn = false; //合成游戏返回首页

        public static string GameAName = "ZodiacMerge";//十二生肖游戏名

        public static int GameA_Score;  //十二生肖分数
        public static int GameA_RowNum; //十二生肖排名

        public static bool isOpenFriend = false; //是否点击头像进入交友界面中的 我的

        public static bool isOpenFriendList = false;//是否首页点击未读消息进入 交友大厅  好友列表

        public static Texture bigImg;//点击查看大图的Texture

        public static bool isFirst = true; //是否第一次加载主场景

        public static DataBMainTaskItem taskInfo; //跳转完成的本地任务

        public static string unCollectLoveNum = "unCollectLoveNum"; //待收集的爱心数的Key

        public static string isCollected = "isCollected"; //保存本地爱心工坊爱心领取状态的Key   1:未收集 -1：已收集

        public static string quitTime = "quitTime"; //记录在孵化过程中退出界面时的时间

        public static string isAlreadyAdd = "isAlreadyAdd"; //是否已经加过  1:未加 -1：已加

        public static ConversionShop curShopData;//选择的商品信息

        public static ConversionShop curShop; //当前兑换的商品

        public static bool isClickVirtual = true; //当前点击是否虚拟按钮

        public static string[] donateTip = new string[] { "助人为乐其乐无穷，热心慈善功德无量！", "春风吹遍千万家，慈善温暖你我他！", "人间自有真情在，共为慈善献爱心！", "传播人间至爱真情，浇灌慈善至美之花！", "捐出一份爱心、奉献一片真情！", "阳光雨露及时雨，慈善仁爱人间情！", "乐善好施，功德无量！", "用我们的爱心托起明天的希望。", "爱心点点，温暖人间。" };

        public static CardType cardType;//当前要使用的道具卡

        public static bool isUseCard = false;  //是否使用了加速卡

        public static DataBag BagData; //背包数据

        public static PrizeInfo curPrizeData; //转盘抽奖获得的奖品


        public static int CurTimeDistance; //时间差

        public static bool isMyFoucs = true; //是否点击的是 我关注的

        public static bool isOpenFocusListPanel = false; //是否打开了关注列表界面

        public static long curClickFocusId;//当前操作的userId 

        public static string[] userStatics = new string[] { "enter_project", "playGame_ZodiacMerge", "architecture_hope",
            "architecture_love", "architecture_honor", "architecture_game", "architecture_friend", "architecture_shop",
            "architecture_lucky" ,"architecture_jump"};


        public static Dictionary<int, string> EducationMapDic = new Dictionary<int, string>()
        { {0,"初中及以下" }, { 1, "高中（中专）" },{2, "大专" } ,{3,"本科" }, {4, "研究生" },{5, "博士" } };

        public static List<string> ZodiacList = new List<string> { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };


        public static string[] Provinces = new string[] { "北京市", "天津市", "上海市", "重庆市", "内蒙古", "广西", "西藏", "宁夏", "新疆", "河北", "山西",
        "辽宁","吉林","黑龙江","江苏","浙江","安徽","福建","江西","山东","河南","湖北","湖南","广东","海南","四川","贵州","云南","陕西","甘肃","青海","台湾"};

        public static string[] Lables = new string[] { "可撩", "慢热", "闷骚", "戏精", "杠精", "社恐", "唱歌", "综艺", "运动", "颜控",
        "声控","吃货","学渣","学霸","养猪","搬砖","沙雕","汉服","游戏","相亲","找对象","找闺蜜","找队友","找同城","找同好",
        "夜猫子","铲屎官","拖延症","脑洞大","混饭圈"};
        public static string[] CharacterLables = new string[] { "追求完美", "好胜心强", "温和友善", "自信爆棚", "追求浪漫",
            "风趣幽默", "沉默内向", "乐观向上", "追求新鲜感", "机智"};
        public static string[] AppearanceLables = new string[] { "魁梧", "清秀", "黝黑", "微胖", "潇洒",
            "阳光", "成熟", "时尚", "英气", "清纯", "可爱", "活泼", "质朴", "偏瘦", "娇小"};

        public static string[] XZNames = new string[] { "白羊座", "金牛座", "双子座", "巨蟹座", "狮子座", "处女座", "天秤座", "天蝎座", "射手座", "摩羯座", "水瓶座", "双鱼座" };

        public static bool isUserCardToMatchFriend = false; //是否是 使用道具卡去匹配好友

        public static bool ResetFriendListPage = true;//是否重置好友列表

        public static long curFriendId;//当前聊天的用户Id

        public static string curFriendName;//当前聊天的用户昵称

        public static string curFriendRemark;//当前聊天的用户备注

        public static string curFriendHeadImg;//当前聊天的用户头像

        public static bool isMatchToChat = false;//是否新匹配的进入聊天

        public static long curFriendUserId; //当前查看个人资料的用户Id

        public static bool isLookAttentionProfile = false;//是否是从关注列表查看别人资料

        public static int curFreeMatchNum;//当前免费匹配次数

        public static int totalFreeMatchNum = 5;//每日免费次数上限

        public static Sprite curDonateProjectImg; //当前捐献项目详情的封面

        public static Sprite curDonateResultImg;//当前项目捐献成功的图片

        public static bool isFirstInitAudioMgr = true;

        public static string SystemAudioStatus = "SystemAudioStatus"; //系统背景音乐状态： 0 静音  1开启
        public static string GameAudioBgmStatus = "GameAudioBgmStatus";     //小游戏背景音乐状态    
        public static string GameAudioEffectStatus = "GameAudioEffectStatus";     //小游戏特效音乐状态
                                                                                  //
        public static string FunJumpEffectStatus = "FunJumpEffectStatus";     //跳跳乐音效   

        public const string DefaultHeadImgUrl = "https://ossjuai.oss-cn-beijing.aliyuncs.com/users/profiles/default.jpg";

        public static bool isOpenChatPage = false;//正在聊天窗口界面

        public static bool isOpenTipPanel = false;//正在打开弹窗界面

        public static SensitiveWordFilter SensitiveWordFilter;//敏感词过滤

        public static Dictionary<string, EmojiInfo> LittleEmojiDic, BigEmojiDic;

        //爱心数： 1，爱心工坊数量展示   2，首页希望灯塔权限开启
        /// <summary>
        /// 通过领取状态获取当前实际爱心数量
        /// </summary>
        public static int GetCurLoveNum()
        {
            int _num;
            if (PlayerPrefs.GetInt(isCollected) == 1)
            {
                _num = GameData.curLoveNum - PlayerPrefs.GetInt(GameData.unCollectLoveNum);
            }
            else
            {
                _num = GameData.curLoveNum;
            }
            return _num;
        }

        /// <summary>
        /// 获取当前未领取爱心数
        /// </summary>
        /// <returns></returns>
        public static int GetCurUnCollectNum()
        {
            return PlayerPrefs.GetInt(GameData.unCollectLoveNum);
        }

        /// <summary>
        /// 获取随机Tip
        /// </summary>
        /// <returns></returns>
        public static string GetRandomTip()
        {
            return GameData.donateTip[Random.Range(0, GameData.donateTip.Length)];
        }

        /// <summary>
        /// 获取背包里道具信息
        /// </summary>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public static PrizeStore GetBagCardDataByCardType(CardType cardType)
        {
            PrizeStore prizeStore = null;
            for (int i = 0; i < GameData.BagData.prizeStoreList.Count; i++)
            {
                if (GameData.BagData.prizeStoreList[i].productId == (int)cardType)
                {
                    prizeStore = GameData.BagData.prizeStoreList[i];
                }
            }
            return prizeStore;
        }


        /// <summary>
        /// 根据id获取对应学历
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetUserEducation(int code)
        {
            if (GameData.EducationMapDic.ContainsKey(code)) return GameData.EducationMapDic[code];

            return "";
        }

        public static int GetUserEducation(string eduStr)
        {
            foreach (var item in EducationMapDic)
            {
                if (item.Value == eduStr)
                {
                    return item.Key;
                }
            }
            return 0;
        }

        private static Dictionary<string, Dictionary<string, EmojiInfo>> Dic_Emoji;
        public static string GetEmojiName(string fileName, string emijiText)
        {
            string result = "";
            if (Dic_Emoji == null) Dic_Emoji = new Dictionary<string, Dictionary<string, EmojiInfo>>();
            if (!Dic_Emoji.ContainsKey(fileName))
            {
                Dictionary<string, EmojiInfo> m_EmojiIndexDict = new Dictionary<string, EmojiInfo>();
                TextAsset emojiContent = JEngine.Core.JResource.LoadRes<TextAsset>(fileName, JEngine.Core.JResource.MatchMode.Other);
                string[] lines = emojiContent.text.Split('\n');
                for (int i = 1; i < lines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(lines[i]))
                    {
                        string[] strs = lines[i].Split('\t');
                        EmojiInfo info;
                        info.name = strs[0];
                        info.x = float.Parse(strs[3]);
                        info.y = float.Parse(strs[4]);
                        info.size = float.Parse(strs[5]);
                        m_EmojiIndexDict.Add(strs[1], info);
                    }
                }
                Dic_Emoji.Add(fileName, m_EmojiIndexDict);
            }
            foreach (var item in Dic_Emoji[fileName])
            {
                if (item.Key == emijiText)
                {
                    result = item.Value.name;
                }
            }
            return result;
        }

        //切换账户修改本地存储数据
        public static void SetLocalStorageData(string userId)
        {
            if (PlayerPrefs.HasKey("UserId"))
            {
                if (!PlayerPrefs.GetString("UserId").Equals(userId))
                {
                    PlayerPrefs.SetString("UserId", userId.ToString());
                    PlayerPrefs.DeleteKey("GameBCharacterId");//切换账户删除游戏缓存的选择信息
                    PlayerPrefs.DeleteKey("GameBThemeId");
                }
            }
            else
            {
                PlayerPrefs.SetString("UserId", userId.ToString());
            }
        }
    }
}
