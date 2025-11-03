using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using HotUpdateScripts.Project.BasePrj.Data;
using My.Msg;
using System.Collections.Generic;
using System.Linq;
using System;

namespace My.UI.Panel
{
    public class UIBShopRecordPanel : BasePanel
    {
        private Button btn_Close, btn_Bg;
        public static ImgQiehuan imgQiehuan;
        private Button[] btns;
        private GameObject ItemPrefab, ScrollViewOne, ScrollViewTwo;
        private Transform ContentOne, ContentTwo;

        void Awake()
        {
            btn_Close = GameTools.GetByName(transform, "btn_Close").GetComponent<Button>();
            btn_Bg = GameTools.GetByName(transform, "btn_Bg").GetComponent<Button>();
            imgQiehuan = GameTools.GetByName(transform, "QieHuan").GetComponent<ImgQiehuan>();

            btns = new Button[2] { GameTools.GetByName(transform, "btn_one").GetComponent<Button>(), GameTools.GetByName(transform, "btn_two").GetComponent<Button>() };
            ContentOne = GameTools.GetByName(transform, "ContentOne").transform;
            ContentTwo = GameTools.GetByName(transform, "ContentTwo").transform;

            ScrollViewOne = GameTools.GetByName(transform, "ScrollViewOne");
            ScrollViewTwo = GameTools.GetByName(transform, "ScrollViewTwo");

            ItemPrefab = GameTools.GetByName(transform, "ItemPrefab");
            ItemPrefab.SetActive(false);

            btn_Close.onClick.AddListener(OnBtnClockA);
            btn_Bg.onClick.AddListener(OnBtnClockA);

            for (int i = 0; i < btns.Length; i++)
            {
                int index = i;
                btns[index].onClick.AddListener(() =>
                {
                    for (int k = 0; k < btns.Length; k++)
                    {
                        if (k == index) imgQiehuan.SetImg(1, btns[k].gameObject);
                        else imgQiehuan.SetImg(0, btns[k].gameObject);
                    }

                    if (index == 0)
                    {
                        debug.Log_Blue("第一个");
                        ScrollViewOne.gameObject.SetActive(true);
                        ScrollViewTwo.gameObject.SetActive(false);
                    }
                    else if (index == 1)
                    {
                        debug.Log_Blue("第二个");
                        ScrollViewOne.gameObject.SetActive(false);
                        ScrollViewTwo.gameObject.SetActive(true);
                    }
                });
            }
            MsgCenter.RegisterMsg(null, MsgCode.S2C_Shop_GetShopRecordList, S2C_Shop_GetShopRecordListCallBack);
            NetMgr.Instance.C2S_Shop_GetShopRecordList();
        }

        void Start()
        {
            if (GameData.isClickVirtual)
            {
                btns[1].onClick.Invoke();
            }
            else btns[0].onClick.Invoke();
        }

        //根据时间排序
        private List<UserConversionRecord> _sortByTime(List<UserConversionRecord> list)
        {
            List<UserConversionRecord> _list = list;
            _list.Sort((a, b) =>
            {
                int result = 0;
                int _a = GameTools.TmGetTimeStamp(DateTime.Parse(a.coinTime));
                int _b = (GameTools.TmGetTimeStamp(DateTime.Parse(b.coinTime)));
                if (_a - _b >= 0)
                {
                    result = -1;
                }
                else
                {
                    result = 1;
                }
                return result;
            });
            return _list;
        }

        void S2C_Shop_GetShopRecordListCallBack(object o)
        {
            List<UserConversionRecord> recordList = DataMgr.Instance.dataBShopRecordListRes.conversionRecordList;
            if (recordList == null) return;

            List<UserConversionRecord> virtualRecordList = new List<UserConversionRecord>();
            List<UserConversionRecord> PhysicalRecordList = new List<UserConversionRecord>();

            virtualRecordList.AddRange(recordList.Where(t => t.isVirtual == 0).ToList());
            PhysicalRecordList.AddRange(recordList.Where(t => t.isVirtual == 1).ToList());
            _sortByTime(virtualRecordList);
            _sortByTime(PhysicalRecordList);

            debug.Log_Blue("virtualShopData " + virtualRecordList.Count);
            debug.Log_Blue("PhysicalShopData " + PhysicalRecordList.Count);

            for (int i = 0; i < PhysicalRecordList.Count; i++)
            {
                GameObject Item = Instantiate(ItemPrefab, ContentOne);
                Item.SetActive(true);
                Item.AddComponent<UIBShopRecordItem>().InitItem(PhysicalRecordList[i]);
            }

            for (int i = 0; i < virtualRecordList.Count; i++)
            {
                GameObject Item = Instantiate(ItemPrefab, ContentTwo);
                Item.SetActive(true);
                Item.AddComponent<UIBShopRecordItem>().InitItem(virtualRecordList[i]);
            }
        }

        void OnBtnClockA()
        {
            UIMgr.Instance.Close(UIPath.UIBShopRecordPanel);
        }

        void OnDestroy()
        {
            MsgCenter.UnRegisterMsg(null, MsgCode.S2C_Shop_GetShopRecordList, S2C_Shop_GetShopRecordListCallBack);
        }
    }
}

