using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using JEngine.Core;
using My.Msg;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBMainBagPanel : BasePanel
    {
        private Button btn_CloseA, btn_CloseB;
        private Transform sv_GridContent;
        private GameObject obj_GoodsDetial;
        private Text txt_JinBiNum;
        DataBag dataBag;
        int gridNum = 16;//格子总数
        private GameObject pan_Content;

        void Awake()
        {
            pan_Content = GameTools.GetByName(transform, "pan_Content");
            DOTweenMgr.Instance.OpenWindowScale(pan_Content, .3f);
            btn_CloseB = transform.Find("btn_CloseB").GetComponent<Button>();
            btn_CloseA = transform.Find("pan_ContentPar/pan_Content/btn_CloseA").GetComponent<Button>();
            sv_GridContent = transform.Find("pan_ContentPar/pan_Content/sv_GridList/Viewport/sv_GridContent");
            obj_GoodsDetial = transform.Find("pan_ContentPar/obj_GoodsDetial").gameObject;
            txt_JinBiNum = transform.Find("pan_ContentPar/pan_Content/pan_JinBi/txt_JinBiNum").GetComponent<Text>();
            btn_CloseB.onClick.AddListener(onBtnCloseClick);
            btn_CloseA.onClick.AddListener(onBtnCloseClick);
            txt_JinBiNum.text = float.Parse(GameData.curLoveMoney).ToString("F2");
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Main_GetBagInfo, S2C_Main_GetBagInfoCallBack);
            NetMgr.Instance.C2S_Main_GetBagInfo(GameData.userId.ToString());
        }

        //获取背包数据
        void S2C_Main_GetBagInfoCallBack(object o)
        {
            dataBag = DataMgr.Instance.dataBag;
            new JPrefab("UI/UIBMainBagItem", (result, prefab) =>
            {
                List<Transform> zeroList = new List<Transform>();
                for (int i = 0; i < gridNum; i++)
                {
                    GameObject grid = Instantiate(prefab.Instance);
                    grid.transform.SetParent(sv_GridContent);
                    grid.transform.localScale = Vector3.one;
                    GameObject obj = grid.transform.GetChild(1).gameObject;
                    for (int j = 0; j < dataBag.prizeStoreList.Count; j++)
                    {
                        if (!DataMgr.Instance.dataBMainHome.AllowMakingFriends && dataBag.prizeStoreList[j].productId == 301)
                            dataBag.prizeStoreList[j].productNum = -1;
                        if (i == j && dataBag.prizeStoreList[j].productNum > 0)
                        {
                            obj.SetActive(true);
                            UIBMainBagItem item = obj.AddComponent<UIBMainBagItem>();
                            item.goodsId = dataBag.prizeStoreList[j].productId;
                            item.goodsDesc = dataBag.prizeStoreList[j].productDesc;
                            item.txt_GoodsName.text = dataBag.prizeStoreList[j].productName;
                            item.txt_GoodsNum.text = "X" + dataBag.prizeStoreList[j].productNum.ToString();
                            item.obj_GoodsDetial = obj_GoodsDetial;
                            /*
                             * 能量卡类 101-199
                             * 加速卡类 201-299
                             * 交友卡类 301-399
                             * 爱心卡类 401-499
                             */
                            string djSpName = "";

                            if (dataBag.prizeStoreList[j].productId == 101)
                            {
                                djSpName = "nengliangka";
                            }
                            else if (dataBag.prizeStoreList[j].productId == 102)
                            {
                                djSpName = "jiasuka";
                            }
                            else if (dataBag.prizeStoreList[j].productId == 103)
                            {
                                djSpName = "jiaoyouka";
                            }
                            else if (dataBag.prizeStoreList[j].productId == 104)
                            {
                                djSpName = "aixinka";
                            }

                            if (!item.gameObject.activeSelf)
                                item.gameObject.SetActive(true);

                            //item.img_GoodsIcon.sprite = JResource.LoadRes<Sprite>("Common/DaoJu/"+ djSpName + ".png", JResource.MatchMode.UI);
                            //item.img_GoodsIcon.SetNativeSize();

                            JResource.LoadResAsync<Sprite>("Common/DaoJu/" + djSpName + ".png",
                                (sp) =>
                                {
                                    item.img_GoodsIcon.sprite = sp;
                                    //item.img_GoodsIcon.SetNativeSize();
                                },
                                JResource.MatchMode.UI);
                        }

                        //先把数量为0的格子记录下来
                        else if (i == j && dataBag.prizeStoreList[j].productNum <= 0)
                        {
                            zeroList.Add(obj.transform.parent);
                        }
                    }
                }

                //再把数量为0的格子依次放到最后
                for (int i = 0; i < zeroList.Count; i++)
                {
                    zeroList[i].SetAsLastSibling();
                }
            });
        }

        void onBtnCloseClick()
        {
            DOTweenMgr.Instance.CloseWindowScale(pan_Content, 0.2f, () =>
            {
                MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Main_GetBagInfo, S2C_Main_GetBagInfoCallBack);
                UIMgr.Instance.Close(UIPath.UIBMainBagPanel);
            });
        }
    }
}