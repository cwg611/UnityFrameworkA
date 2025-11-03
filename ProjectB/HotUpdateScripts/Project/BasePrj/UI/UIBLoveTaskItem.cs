using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using LitJson;
using My.Msg;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBLoveTaskItem : MonoBehaviour
    {
        //View
        private GameObject img_Icon,reward_Icon;
        private Text txt_Title, txt_Desc, txt_Reward, txt_Progress;
        private Button btn_Operate;
        //数据
        public DataBMainTaskItem curItem = null;
        private RectTransform pan_Title;
        private Color _Color;
        private Color _commonColor;

        void Awake()
        {
            ColorUtility.TryParseHtmlString("#dcf4ff", out _Color);
            ColorUtility.TryParseHtmlString("#FFFFFF", out _commonColor);
            img_Icon = transform.Find("img_Icon").gameObject;
            reward_Icon = transform.Find("pan_World/pan_Reward/img").gameObject;
            txt_Title = transform.Find("pan_World/pan_Title/txt_Title").GetComponent<Text>();
            txt_Desc = transform.Find("pan_World/txt_Desc").GetComponent<Text>();
            txt_Reward = transform.Find("pan_World/pan_Reward/txt_Reward").GetComponent<Text>();
            txt_Progress = transform.Find("pan_World/pan_Title/txt_Progress").GetComponent<Text>();//Bind
            btn_Operate = transform.Find("btn_Operate").GetComponent<Button>();
            pan_Title = GameTools.GetByName(transform, "pan_Title").GetComponent<RectTransform>();
            GameTools.Instance.AddClickEvent(btn_Operate.gameObject, OnBtn_OperateClick);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItemReward, S2C_Love_TaskUpdateItemRewardCallBack);
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);

            MsgINAVLocalAndHotupt.Instance.AddEventListener(MsgINAVLocalAndHotuptCode.LocalToHotupt, (o) =>
            {
                DataBNativeToUnity dataBNativeToUnity = JsonMapper.ToObject<DataBNativeToUnity>(o.ToString());
                debug.Log_Blue("LocalToHotupt--taskState--" + dataBNativeToUnity.taskState);
                if (int.Parse(dataBNativeToUnity.taskId) == curItem.taskId)
                {
                    if (dataBNativeToUnity.taskState)
                    {
                        DataMgr.Instance.dataBTaskInfoReq.userId = GameData.userId;
                        DataMgr.Instance.dataBTaskInfoReq.taskId = GameData.taskInfo.id;
                        DataMgr.Instance.dataBTaskInfoReq.taskCurProgress = GameData.taskInfo.taskCurProgress + 1;
                        NetMgr.Instance.C2S_Love_TaskUpdateItem();
                    }
                }
            });
        }

        void S2C_Love_TaskUpdateItemCallBack(object o)
        {
            if (curItem.taskId == GameData.taskId_sendToNative)
            {
                //NetMgr.Instance.C2S_Love_TaskGetList(GameData.userId.ToString());
                GameTools.SetTip("更新任务成功");
            }
        }

        void Start()
        {
            InitItem();
        }

        public void InitItem()
        {
            ////任务完成状态， 0 未完成  1 已完成待领取 2 已完成已领取
            //显示权限
            //公益捐献 累计孵化>=5颗爱心 累计捐献<10
            gameObject.SetActive(true);

            if (curItem.taskStatus != 1)
            {
                if (curItem.taskType == 321)
                {
                    if (GameData.accumulativeGetLoveNum >= 5)
                    {
                        gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                //查看荣誉排行 累计捐献>=10颗爱心 
                else if (curItem.taskType == 331)
                {
                    if (GameData.curDonateLoveNum >= 10)
                    {
                        gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                //交友匹配 累计捐献>=20颗爱心
                //else if (curItem.taskType == 341)
                //{
                //    if (GameData.curDonateLoveNum >= 20)
                //    {
                //        gameObject.SetActive(true);
                //    }
                //    else
                //    {
                //        gameObject.SetActive(false);
                //    }
                //}
                //玩合成游戏 累计捐献>=15颗爱心
                else if (curItem.taskType == 351)
                {
                    if (GameData.curDonateLoveNum >= 15)
                    {
                        gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                //查看兑换奖品 累计捐献>=25颗爱心
                else if (curItem.taskType == 361)
                {
                    if (GameData.curDonateLoveNum >= 25)
                    {
                        gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                //幸运小屋抽奖 累计捐献>=30颗爱心
                else if (curItem.taskType == 371)
                {
                    if (GameData.curDonateLoveNum >= 30)
                    {
                        gameObject.SetActive(true);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
            }

            //UI赋值
            txt_Title.text = curItem.taskTitle;
            txt_Desc.text = curItem.taskDesc;
            txt_Reward.text = "x" + curItem.taskRewardNumber.ToString();
            txt_Progress.text = "(" + curItem.taskCurProgress.ToString() + "/" + curItem.taskTargetProgre + ")";

            /*101:连续签到
             * 102每日登录
             * 103特定时段 登录
             * 311爱心充能
             * 321公益捐献
             * 331查看荣誉排行
             * 341交友匹配
             * 351去玩一把游戏
             * 361查看兑换奖品
             * 371来做星球幸运儿
             * 201~230浏览类
             * 251~270下单类
             * 230~250关注类
             * 270~280邀新类
             * 280~290助力类
            */
            #region Task Type Open Icon
            if (curItem.taskType == 102)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(0, img_Icon);
            }
            else if (curItem.taskType == 103)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(1, img_Icon);
            }
            else if (curItem.taskType > 310 && curItem.taskType < 320)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(2, img_Icon);
            }
            else if (curItem.taskType > 320 && curItem.taskType < 330)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(3, img_Icon);
            }
            else if (curItem.taskType > 330 && curItem.taskType < 340)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(4, img_Icon);
            }
            else if (curItem.taskType > 340 && curItem.taskType < 350)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(5, img_Icon);
            }
            else if (curItem.taskType > 350 && curItem.taskType < 360)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(6, img_Icon);
            }
            else if (curItem.taskType > 360 && curItem.taskType < 370)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(7, img_Icon);
            }
            else if (curItem.taskType > 370 && curItem.taskType < 380)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(8, img_Icon);
            }
            else if (curItem.taskType > 200 && curItem.taskType < 230)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(9, img_Icon);
            }
            else if (curItem.taskType > 250 && curItem.taskType < 270)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(10, img_Icon);
            }
            else if (curItem.taskType > 230 && curItem.taskType < 250)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(11, img_Icon);
            }
            else if (curItem.taskType > 270 && curItem.taskType < 280)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(12, img_Icon);
            }
            else if (curItem.taskType > 280 && curItem.taskType < 290)
            {
                UIBLoveTaskPanel.taskQiehuan.SetImg(13, img_Icon);
            }
            #endregion
            UIBLoveTaskPanel.QieHuan.SetImg(curItem.taskStatus, btn_Operate.gameObject);

            //TaskReward
            if (curItem.taskRewardType==201)
            {
                UIBLoveTaskPanel.RewardQiehuan.SetImg(0, reward_Icon.gameObject);
            }
            else if (curItem.taskRewardType == 202)
            {
                UIBLoveTaskPanel.RewardQiehuan.SetImg(1, reward_Icon.gameObject);

            }
            else if (curItem.taskRewardType == 101)
            {
                UIBLoveTaskPanel.RewardQiehuan.SetImg(2, reward_Icon.gameObject);

            }
            else if (curItem.taskRewardType == 102)
            {
                UIBLoveTaskPanel.RewardQiehuan.SetImg(3, reward_Icon.gameObject);
            }
            else if (curItem.taskRewardType == 103)
            {
                UIBLoveTaskPanel.RewardQiehuan.SetImg(4, reward_Icon.gameObject);
            }
            else if (curItem.taskRewardType == 104)
            {
                UIBLoveTaskPanel.RewardQiehuan.SetImg(5, reward_Icon.gameObject);

            }


            //设置底图
            if (curItem.taskStatus == 2)
            {
                transform.GetComponent<Image>().color = _Color;
            }
            else
            {
                transform.GetComponent<Image>().color = _commonColor;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(pan_Title);
        }

        //Business
        public void OnBtn_OperateClick()
        {
            if (curItem.taskStatus == 2) return;
            if (curItem.taskStatus == 0)
            {
                if (curItem.taskType == 311) //爱心充能
                {
                    GameData.taskInfo = curItem;
                    UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
                }
                else if (curItem.taskType == 321) //公益捐献
                {
                    GameData.taskInfo = curItem;
                    UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
                    UIMgr.Instance.Close(UIPath.UIBLoveHomePanel);
                    UIMgr.Instance.Open(UIPath.UIBDonateHomePanel);
                }
                else if (curItem.taskType == 331) //查看荣誉排行
                {
                    GameData.taskInfo = curItem;
                    UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
                    UIMgr.Instance.Close(UIPath.UIBLoveHomePanel);
                    UIMgr.Instance.Open(UIPath.UIBHonorHomePanel);
                }
                else if (curItem.taskType == 341) //交友匹配
                {
                    GameData.taskInfo = curItem;
                    UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
                    UIMgr.Instance.Close(UIPath.UIBLoveHomePanel);
                    UIMgr.Instance.Open(UIPath.UIBFriendHomePanel);
                }
                else if (curItem.taskType == 351) //合成十二生肖
                {
                    if (GameData.curDonateLoveNum >= 15)
                    {
                        GameData.taskInfo = curItem;
                        UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
                        UIMgr.Instance.Close(UIPath.UIBLoveHomePanel);
                        UIMgr.Instance.Open(UIPath.UIBGameHomePanel);
                    }
                    else
                    {
                        GameTools.SetTip("<b>游戏大厅</b> 未解锁 \n 解锁条件：累计捐献≥15颗爱心");
                    }

                }
                else if (curItem.taskType == 361) //查看兑换商品
                {
                    GameData.taskInfo = curItem;
                    UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
                    UIMgr.Instance.Close(UIPath.UIBLoveHomePanel);
                    UIMgr.Instance.Open(UIPath.UIBShopPanel);
                }
                else if (curItem.taskType == 371) //幸运抽奖
                {
                    GameData.taskInfo = curItem;
                    UIMgr.Instance.Close(UIPath.UIBLoveTaskPanel);
                    UIMgr.Instance.Close(UIPath.UIBLoveHomePanel);
                    UIMgr.Instance.Open(UIPath.UIBLuckyRotatePanel);
                }
                else if (curItem.taskType > 200 && curItem.taskType < 210) //浏览专题   subjectId
                {
                    GameData.taskInfo = curItem;
                    GameData.taskId_sendToNative = curItem.taskId;
                    DataBUnityToNative unityToAndroid = new DataBUnityToNative();
                    unityToAndroid.type = "BrowseSubject";
                    unityToAndroid.subjectId = curItem.taskGotoSubject;
                    unityToAndroid.taskId = curItem.taskId.ToString();
                    unityToAndroid.duration = curItem.taskBrowseDuration.ToString();
                    debug.Log_Blue("点击浏览专题");
                    MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.HotuptToLocal, JsonMapper.ToJson(unityToAndroid));
                }
                else if (curItem.taskType > 210 && curItem.taskType < 220) //浏览商品类  productId
                {
                    GameData.taskInfo = curItem;
                    GameData.taskId_sendToNative = curItem.taskId;
                    DataBUnityToNative unityToAndroid = new DataBUnityToNative();
                    unityToAndroid.type = "BrowseProductsNormal";
                    unityToAndroid.shopId = "";
                    unityToAndroid.productId = curItem.taskGotoProduct;
                    unityToAndroid.taskId = curItem.taskId.ToString();
                    if (curItem.taskBrowseDuration == null)
                    {
                        unityToAndroid.duration = "0";
                    }
                    else
                    {
                        unityToAndroid.duration = curItem.taskBrowseDuration.ToString();
                    }
                    debug.Log_Blue("点击浏览商品");
                    MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.HotuptToLocal, JsonMapper.ToJson(unityToAndroid));
                }
                else if (curItem.taskType > 220 && curItem.taskType < 230) //浏览店铺类  shopId
                {
                    GameData.taskInfo = curItem;
                    GameData.taskId_sendToNative = curItem.taskId;
                    DataBUnityToNative unityToAndroid = new DataBUnityToNative();
                    unityToAndroid.type = "BrowseShopNormal";
                    unityToAndroid.shopId = curItem.taskGotoShop;
                    unityToAndroid.productId = "";
                    unityToAndroid.taskId = curItem.taskId.ToString();
                    if (curItem.taskBrowseDuration == null)
                    {
                        unityToAndroid.duration = "0";
                    }
                    else
                    {
                        unityToAndroid.duration = curItem.taskBrowseDuration.ToString();
                    }

                    debug.Log_Blue("点击浏览店铺");
                    MsgINAVLocalAndHotupt.Instance.Dispatch(MsgINAVLocalAndHotuptCode.HotuptToLocal, JsonMapper.ToJson(unityToAndroid));
                }
                else if (curItem.taskType > 250 && curItem.taskType < 260) //下单商品类
                {

                }
                else if (curItem.taskType > 260 && curItem.taskType < 270) //下单店铺类
                {

                }
                else if (curItem.taskType > 230 && curItem.taskType < 240) //收藏商品 
                {

                }
                else if (curItem.taskType > 240 && curItem.taskType < 250) //收藏店铺
                {

                }
                else if (curItem.taskType > 270 && curItem.taskType < 280) //邀新类
                {

                }
                else if (curItem.taskType > 280 && curItem.taskType < 290) //助力类
                {

                }
                else if (curItem.taskType > 290) //系统类
                {

                }
            }
            else if (curItem.taskStatus == 1)
            {
                curItem.taskStatus = 2;
                DataMgr.Instance.DataBReceiveReq.userId = GameData.userId;
                DataMgr.Instance.DataBReceiveReq.taskId = curItem.id;
                GameData.sendTaskId = curItem.id;
                NetMgr.Instance.C2S_Love_TaskUpdateItemReward();
            }
        }

        /// <summary>
        /// 领取奖励回调
        /// </summary>
        /// <param name="o"></param>
        private void S2C_Love_TaskUpdateItemRewardCallBack(object o)
        {
            if (curItem.id == GameData.sendTaskId)
            {
                //NetMgr.Instance.C2S_Love_TaskGetList(GameData.userId.ToString());
                //不用刷新列表数据,直接更改状态即可
                curItem.taskStatus = 2;
                InitItem();
            }
        }

        void OnDestroy()
        {
            btn_Operate.onClick.RemoveListener(OnBtn_OperateClick);
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItemReward, S2C_Love_TaskUpdateItemRewardCallBack);
            MsgINAVLocalAndHotupt.Instance.RemoveEventListener(MsgINAVLocalAndHotuptCode.LocalToHotupt, (o) =>
            {
            });
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Love_TaskUpdateItem, S2C_Love_TaskUpdateItemCallBack);
        }
    }
}
