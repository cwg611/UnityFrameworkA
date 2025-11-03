using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Game.GameA.Data;
using My.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdateScripts.Project.Common
{
    public class BroadCastComponent : MonoBehaviour
    {
        //公告栏
        private Text txt_BroadNotice;
        private Vector3 Speed = new Vector3(130 * Time.deltaTime, 0, 0);
        private float MoveEndPoint;
        private Vector2 MoveStartPoint;
        private float ParentWidth;
        private float TextWidth;

        private const string FriendBroadCastStr = "文明交友，注意言行，做堂堂正正牧原人。（禁止私聊交换微信等联系方式，可通过“信息交换”正规方式获得彼此联系方式）";
        private const string DonateBroadCastStr = "“{0}” 在《希望灯塔》——为“{1}”项目捐献了“{2}”颗爱心，获得了“{3}”个金币。";
        private const string GameBroadCastStr = "“{0}” 在《游戏大厅》——《{1}》游戏中刷新了记录，现居排行第一名，家人们快来挑战吧。";
        private const string ConversionBroadCastStr = "恭喜 “{0}” 在《兑换商城》花费了“{1}”个金币，兑换了“{2}”奖品，各位家人加油呀。";
        float movingTime = 0;
        public float RollingTime = 5;

        Queue<BroadCastItem> ItemQueue;
        bool StartPlay;
        bool Playing;

        void Awake()
        {
            StartPlay = Playing = false;
            //广播栏设置
            ParentWidth = transform.GetComponent<RectTransform>().rect.width;
            txt_BroadNotice = transform.GetChild(0).GetComponent<Text>();
            MoveStartPoint = new Vector2(ParentWidth / 2, 0);
            transform.localScale = Vector3.zero;

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Main_System_Broadcast, S2C_Main_System_BroadcastCallBack);
            
        }

        void Update()
        {
            if (!StartPlay || ItemQueue == null || ItemQueue.Count <= 0) return;

            if (!Playing)
            {
                var curMessage = ItemQueue.Dequeue();
                ItemQueue.Enqueue(curMessage);
                SetStyle(curMessage);
                transform.localScale = Vector3.one;
                Playing = true;
            }
            if (Playing)
            {
                // 公告移动
                if (transform.localScale == Vector3.one)
                {
                    txt_BroadNotice.transform.localPosition -= Speed;
                    if (txt_BroadNotice.transform.localPosition.x <= MoveEndPoint)
                    {
                        txt_BroadNotice.transform.localPosition = MoveStartPoint;
                        transform.localScale = Vector3.zero;
                    }
                }
                else
                {
                    movingTime += Time.deltaTime;
                    if (movingTime >= RollingTime)
                    {
                        movingTime = 0;
                        Playing = false;
                    }
                }
            }
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Main_System_Broadcast, S2C_Main_System_BroadcastCallBack);
        }

        BroadCastItem gameAItem, gameBItem;
        void S2C_Main_System_BroadcastCallBack(object o)
        {
            ItemQueue = new Queue<BroadCastItem>();
            DataBroadcast dataBroadcast = DataMgr.Instance.dataBroadcast;
            List<DonateRecord> donateList = dataBroadcast.donateList;
            List<UserConversionRecord> conversionList = dataBroadcast.conversionList;

            BroadCastItem frinedItem = new BroadCastItem(FriendBroadCastStr, BroadCastType.Friend);//
            DataGameA dataGameA = dataBroadcast.gameTop;
            DataGameA dataGameB = dataBroadcast.topJumpGameScore;

            if (dataGameA != null)
            {
                gameAItem = new BroadCastItem(string.Format(GameBroadCastStr, dataGameA.nickName, "12生肖大合成"), BroadCastType.Game);
            }
            else
            {
                gameAItem = null;
            }
            if (dataGameB != null)
            {
                gameBItem = new BroadCastItem(string.Format(GameBroadCastStr, dataGameB.nickName, "跳跳乐"), BroadCastType.Game);
            }
            else
            {
                gameBItem = null;
            }

            int circleIndex = 1;
            if (donateList != null && conversionList != null)
            {
                if (donateList.Count != 0 || conversionList.Count != 0)
                {
                    circleIndex = donateList.Count > conversionList.Count ? donateList.Count : conversionList.Count;
                }
            }
            else if (donateList != null && donateList.Count > 0)
            {
                circleIndex = donateList.Count;
            }
            else if (conversionList != null && conversionList.Count > 0)
            {
                circleIndex = conversionList.Count;
            }

            for (int i = 0; i < circleIndex; i++)
            {
                ItemQueue.Enqueue(frinedItem);//

                if (donateList != null && i < donateList.Count)
                {
                    var donate = donateList[i];
                    ItemQueue.Enqueue(new BroadCastItem(string.Format(DonateBroadCastStr, donate.nickName, donate.projectTitle,
                        donate.donateNum, donate.getLoveMoneyNum), BroadCastType.Donate));
                }
                if (gameAItem != null)
                {
                    ItemQueue.Enqueue(gameAItem);
                }
                if (gameBItem != null)
                {
                    ItemQueue.Enqueue(gameBItem);
                }
                if (conversionList != null && i < conversionList.Count)
                {
                    var conversion = conversionList[i];
                    ItemQueue.Enqueue(new BroadCastItem(string.Format(ConversionBroadCastStr, conversion.nickName, conversion.spendLoveMoney,
                        conversion.productName), BroadCastType.Conversion));
                }
            }
            StartPlay = true;
            transform.localScale = Vector3.one;
        }

        private void SetStyle(BroadCastItem scrollItem)
        {
            if (scrollItem == null) return;

            txt_BroadNotice.text = scrollItem.Content;
            switch (scrollItem.ItemType)
            {
                case BroadCastType.Friend:
                    txt_BroadNotice.color = Color.white;
                    break;
                case BroadCastType.Donate:
                    txt_BroadNotice.color = Color.yellow;
                    break;
                case BroadCastType.Game:
                    txt_BroadNotice.color = Color.blue;
                    break;
                case BroadCastType.Conversion:
                    txt_BroadNotice.color = Color.green;
                    break;
                default:
                    break;
            }
            TextWidth = txt_BroadNotice.preferredWidth;
            MoveEndPoint = -ParentWidth / 2 - TextWidth;
        }

        public enum BroadCastType
        {
            Friend,
            Donate,
            Game,
            Conversion
        }

        class BroadCastItem
        {
            public BroadCastItem(string str, BroadCastType itemType, int times = 1)
            {
                Content = str;
                RepeatTimes = times;
                ItemType = itemType;
            }
            public string Content;
            public int RepeatTimes;
            public BroadCastType ItemType;
        }
    }
}
