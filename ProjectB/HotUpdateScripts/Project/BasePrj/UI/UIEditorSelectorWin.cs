using HotUpdateScripts.Project.ACommon;
using HotUpdateScripts.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdateScripts.Project.BasePrj.UI
{
    public enum SelectorType
    {
        LablesSelector,//标签选择页
        SingleSelector,//单选
        ProvinceCitySelector//省市选择
    }
    /// <summary>
    /// 资料编辑选择器弹窗
    /// </summary>
    public class UIEditorSelectorWin : MonoBehaviour
    {
        private GameObject LablesSelector;
        private GameObject SingleSelector;
        private GameObject AreaSelector;

        private SelectorType selectorType;
        private ItemList LablesItemList;
        private int lableMinNum, lableMaxNum;

        private ScrollSelector singleScrollSelector, provinceScrollSelector, cityScrollSelector;

        public event Action<string> ChooseCallBack;

        private string ChoosedStr;

        public void InitUIEditorSelectWin()
        {
            LablesSelector = GameTools.GetByName(transform, "LablesSelectorView");
            SingleSelector = GameTools.GetByName(transform, "SingleSelectorView");
            AreaSelector = GameTools.GetByName(transform, "AreaSelectorView");

            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "CancelBtn"), () =>
            {
                gameObject.SetActive(false);
            });
            GameTools.Instance.AddClickEvent(GameTools.GetByName(transform, "EnsureBtn"), () =>
            {
                if (selectorType == SelectorType.LablesSelector)
                {
                    if (selectLables.Count< lableMinNum)
                    {
                        GameTools.SetTip(string.Format("请至少添加{0}个标签",lableMinNum));
                        return;
                    }
                    ChoosedStr = GetLablesContact(selectLables);
                }
                if (!string.IsNullOrEmpty(ChoosedStr)) ChooseCallBack?.Invoke(ChoosedStr);
                ChoosedStr = "";
                ChooseCallBack = null;
                gameObject.SetActive(false);
            });

        }

        public void InitSelector(SelectorType type)
        {

            switch (type)
            {
                case SelectorType.LablesSelector:
                    break;
                case SelectorType.SingleSelector:
                    break;
                case SelectorType.ProvinceCitySelector:
                    break;
                default:
                    break;
            }
        }

        List<bool> btnsStatue = new List<bool>(); //记录数组顺序对应的状态
        List<string> selectLables = new List<string>();//已选标签
        List<Button>  buttons = new List<Button>();//标签按钮
        Action buttonAction = () => { };
        //标签选择
        public void InitLablesSelector(string[] lableList, string[] existLables, int limitMin=3, int limitMax=10, Action<string> action=null)
        {
            if (lableList == null) return;
            selectorType = SelectorType.LablesSelector;
            ChooseCallBack = action;
            LablesSelector.SetActive(true);
            SingleSelector.SetActive(false);
            AreaSelector.SetActive(false);
            lableMinNum = limitMin;
            lableMaxNum = limitMax;

            if (LablesItemList == null) LablesItemList = GameTools.GetByName(LablesSelector.transform, "LablesItemList").
                    AddComponent<ItemList>();

           
            btnsStatue.Clear();
            selectLables.Clear();
            buttons.Clear();
            LablesItemList.Reset();
            for (int i = 0; i < lableList.Length; i++)
            {
                LablesItemList.Refresh(i, lableList[i], (p, o) =>
                {
                    bool status = existLables.Contains(p) ? true : false;
                    if (status) selectLables.Add(p);
                    btnsStatue.Add(status);//加入所有标签状态
                    o.transform.GetChild(0).localScale= status?Vector3.one:Vector3.zero;
                    o.GetComponentInChildren<Text>().text = p;
                    buttons.Add(o.GetComponent<Button>());
                });
            }
            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;
                buttons[index].onClick.RemoveAllListeners();
                buttons[index].onClick.AddListener(() =>
                {
                    if (btnsStatue[index])//取消选中
                    {
                        btnsStatue[index] = false;
                        selectLables.Remove(lableList[index]);
                        buttons[index].transform.GetChild(0).localScale = Vector3.zero;
                    }
                    else
                    {
                        if (selectLables.Count >= lableMaxNum)
                        {
                            GameTools.SetTip(string.Format("最多可以添加{0}个标签", lableMaxNum));
                            return;
                        }
                        btnsStatue[index] = true;
                        selectLables.Add(lableList[index]); //数据添加
                        buttons[index].transform.GetChild(0).localScale = Vector3.one;
                    }
                });

            }
            gameObject.SetActive(true);
        }

        //单选
        public void InitSingleSelector(List<string> param, Action<string> action, int ItemNum = 7)
        {
            if (param == null) return;
            LablesSelector.SetActive(false);
            SingleSelector.SetActive(true);
            AreaSelector.SetActive(false);

            ChooseCallBack = action;
            selectorType = SelectorType.SingleSelector;
            SingleSelector.transform.Find("LinePos").localPosition = ItemNum % 2 == 0?new Vector3(0,-40,0):Vector3.zero;
            if (singleScrollSelector == null) singleScrollSelector = GameTools.GetByName(SingleSelector.transform, "SingleSelector").
                    AddComponent<ScrollSelector>();
            var lables = param;
            singleScrollSelector.Init(lables, (s) =>
            {
                ChoosedStr = s;
            }, ItemNum);

            gameObject.SetActive(true);
        }

        //地区选择
        public void InitAreaSelector(List<CityHelper.Province> provinceList, Action<string> action, int ItemNum = 7)
        {
            if (provinceList == null) return;
            selectorType = SelectorType.ProvinceCitySelector;
            LablesSelector.SetActive(false);
            SingleSelector.SetActive(false);
            AreaSelector.SetActive(true);

            ChooseCallBack = action;


            if (provinceScrollSelector == null) provinceScrollSelector = GameTools.GetByName(AreaSelector.transform, "ProvinceSelector").
                    AddComponent<ScrollSelector>();
            if (cityScrollSelector == null) cityScrollSelector = GameTools.GetByName(AreaSelector.transform, "CitySelector").
           AddComponent<ScrollSelector>();
            Dictionary<string, List<CityHelper.City>> selectorDic = new Dictionary<string, List<CityHelper.City>>();
            for (int i = 0; i < provinceList.Count; i++)
            {
                selectorDic.Add(provinceList[i].provinceName, provinceList[i].city);
            }
            List<string> proviceStrs = new List<string>(selectorDic.Keys);
            List<string> cityStrs = new List<string>();
            provinceScrollSelector.Init(proviceStrs, (s) =>
            {
                if (string.IsNullOrEmpty(s)) return;
                //ChoosedStr = s;
                var citys = selectorDic[s];
                cityStrs.Clear();
                for (int i = 0; i < citys.Count; i++)
                {
                    cityStrs.Add(citys[i].cityName);
                }
                cityScrollSelector.Init(cityStrs, (t) =>
                {
                    ChoosedStr=s+(" " + t);
                }, ItemNum);
            }, ItemNum);

            gameObject.SetActive(true);
        }

        //标签字符串拼接
        string GetLablesContact(List<string> _list)
        {
            string str = "";
            for (int i = 0; i < _list.Count; i++)
            {
                str += _list[i] + ",";
            }
            return str.Substring(0, str.Length - 1);
        }

    }
}
