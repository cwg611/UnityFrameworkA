using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;

namespace My.UI.Panel
{
    public class UIBFriendMatchPanel : BasePanel
    {
        private GameObject Pig, Pig2, Pig3;
        private Animator Ani_Pig, Ani_Pig3;
        private Transform Content_bg;
        private RectTransform Row, ItemPrefab, TargetObj; //箭 目标
        private bool isReadyOver = false, isSheOver = false, isTarget = false;//       是否找到了目标
        private List<UserFriend> dataList = new List<UserFriend>(); //记录随机挑出的 加上 匹配到的
        private Button btn_Back;

        void Awake()
        {
            Pig = GameTools.GetByName(transform, "Pig");
            Pig2 = GameTools.GetByName(transform, "Pig2");
            Pig3 = GameTools.GetByName(transform, "Pig3");

            Ani_Pig = Pig.GetComponent<Animator>();
            Ani_Pig3 = Pig3.GetComponent<Animator>();

            Content_bg = GameTools.GetByName(transform, "Content_bg").transform;

            Row = GameTools.GetByName(transform, "Row").GetComponent<RectTransform>();
            ItemPrefab = GameTools.GetByName(transform, "ItemPrefab").GetComponent<RectTransform>();
            ItemPrefab.gameObject.SetActive(false);
            Row.gameObject.SetActive(false);

            btn_Back = GameTools.GetByName(transform, "btn_Back").GetComponent<Button>();
            btn_Back.onClick.AddListener(() =>
            {
                GameData.ResetFriendListPage = true;
                NetMgr.Instance.C2S_Friend_ChatRelationList(GameData.userId.ToString());
                UIMgr.Instance.Close(UIPath.UIBFriendMatchPanel);
            });

            MsgCenter.RegisterMsg(null, MsgCode.S2C_Friend_MatchUserForChat, S2C_Friend_MatchUserForChatCallBack);
            NetMgr.Instance.C2S_Friend_MatchUserForChat(GameData.userId.ToString());

            //MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
            //if (GameData.taskInfo != null && GameData.taskInfo.taskType == 341)
            //{
            //    DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
            //    DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
            //    DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
            //    NetMgr.Instance.C2S_Love_TaskUpdateItem();
            //}
            //----------------设置任务完成------------
            var task = DataMgr.Instance.GetTaskItemByType(341);
            if (task != null && task.taskStatus == 0)
            {
                DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                DataMgr.Instance.dataBTaskInfoReq.taskId = task.id;
                DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = task.taskCurProgress + 1;
                NetMgr.Instance.C2S_Love_TaskUpdateItem();
            }
            //---------------------------------------
        }

        //void S2C_Love_TaskUpdateItemCallBack(object o)
        //{
        //    debug.Log_Blue("更新匹配任务状态成功");
        //}

        void Start()
        {
            Pig.SetActive(true);
        }

        void Update()
        {
            if (Ani_Pig.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && !isReadyOver)
            {
                isReadyOver = true;
                Pig.SetActive(false);
                Pig2.SetActive(true);
            }
            if (isReadyOver && isTarget)
            {
                Pig2.SetActive(false);
                Pig3.SetActive(true);
            }
            if (Ani_Pig3.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.3f && !isSheOver)
            {
                isSheOver = true;
                debug.Log_Blue("射箭结束");
                Row.gameObject.SetActive(true);
                rowToTarget();
            }
        }

        void S2C_Friend_MatchUserForChatCallBack(object o)
        {
            //所有数据
            List<UserFriend> randomList = getRandomData();
            if (randomList != null)
            {
                for (int i = 0; i < randomList.Count; i++)
                {
                    dataList.Add(randomList[i]);
                }
            }

            dataList.Add(DataMgr.Instance.dataRandomUser.randomUser);
            ////依次取出
            InvokeRepeating("GeneralItem", 0f, 1.5f);
            GameData.curFriendId = DataMgr.Instance.dataRandomUser.randomUser.userId;
            GameData.curFriendName = DataMgr.Instance.dataRandomUser.randomUser.nickName;
            GameData.curFriendHeadImg = DataMgr.Instance.dataRandomUser.randomUser.headImgUrl;
            GameData.isMatchToChat = true;
        }

        void GeneralItem()
        {
            if (dataList.Count <= 0) return;
            GameObject Item = Instantiate(ItemPrefab.gameObject, Content_bg);
            Item.SetActive(true);
            Item.transform.SetAsFirstSibling();
            UserFriend userFriend = GetRandomDataByDatalist();
            Item.AddComponent<UIBFriendMatchItem>().InitItem(userFriend.headImgUrl);
            if (userFriend.userId == DataMgr.Instance.dataRandomUser.randomUser.userId)
            {
                TargetObj = Item.GetComponent<RectTransform>();
                //找到了目标
                debug.Log_purple("--->> 找到了目标");
                Invoke("setTargetStatue", 0.5f);
            }
        }

        void setTargetStatue()
        {
            isTarget = true;
        }

        UserFriend GetRandomDataByDatalist()
        {
            UserFriend Data = new UserFriend();
            int randomNum = Random.Range(0, dataList.Count);
            Data = dataList[randomNum];
            dataList.RemoveAt(randomNum);
            return Data;
        }

        //从池子里随机取出2到3个
        List<UserFriend> getRandomData()
        {
            List<UserFriend> list = new List<UserFriend>();
            int randomNum = Random.Range(2, 4);
            if (DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList.Count < 3)
            {
                return null;
            }
            int[] randoms = GetRandomArray(randomNum, 0, DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList.Count - 1);
            for (int i = 0; i < randoms.Length; i++)
            {
                debug.Log_yellow(randoms[i]);
                if (DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList[randoms[i]].userId != DataMgr.Instance.dataRandomUser.randomUser.userId)
                    list.Add(DataMgr.Instance.dataRandomUserFriendDataList.randomUserFriendDataList[randoms[i]]);
            }
            return list;
        }

        private int[] GetRandomArray(int Number, int minNum, int maxNum, bool isCannotContinuous = false)
        {
            int j;
            int[] b = new int[Number];
            System.Random r = new System.Random();

            for (j = 0; j < Number; j++)
            {
                //先取出第一个随机数
                int i = r.Next(minNum, maxNum + 1);

                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    //如果判断有重复的话，则把j还原，再抽一次
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }

        private void rowToTarget()
        {
            //计算角度
            Vector2 dir = new Vector2(TargetObj.localPosition.x - 150, TargetObj.localPosition.y) - Row.anchoredPosition;
            float angle = Vector2.Angle(Vector2.up, dir);
            if (dir.x > 0)
            {
                angle *= -1;
            }
            Row.localRotation = Quaternion.Euler(0, 0, angle);

            DOTweenMgr.Instance.MovePos(Row.gameObject, Row.localPosition, new Vector3(TargetObj.localPosition.x - 150, TargetObj.localPosition.y, 0), 0.3f, () =>
              {
                  debug.Log_Blue("射中");
                  //添加好友关系
                  DataMgr.Instance.addChatRelationReq.userId = GameData.userId;
                  DataMgr.Instance.addChatRelationReq.randomUserId = DataMgr.Instance.dataRandomUser.randomUser.userId;
                  NetMgr.Instance.C2S_Friend_AddRandomUserRelation();
                  //扣除道具
                  if (GameData.isUserCardToMatchFriend)
                  {
                      UIBFriendExplorePage.act_UserCard();
                  }

                  Row.gameObject.SetActive(false);
                  TargetObj.GetComponent<UIBFriendMatchItem>().setXingPlay();

                  Invoke("ClosePanel", 0.8f);
              });
        }

        private void ClosePanel()
        {
            UIMgr.Instance.Open(UIPath.UIBFriendChatPanel);
            UIMgr.Instance.Close(UIPath.UIBFriendMatchPanel);
            NetMgr.Instance.C2S_Friend_MatchUserPool();
        }

        private void OnDestroy()
        {
            CancelInvoke();
            GameData.isUserCardToMatchFriend = false;
            GameData.taskInfo = null;
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Friend_MatchUserForChat, S2C_Friend_MatchUserForChatCallBack);
            //MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }
    }
}
