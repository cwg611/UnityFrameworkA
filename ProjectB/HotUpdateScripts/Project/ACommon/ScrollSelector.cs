using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using HotUpdateScripts.Project.Common;

namespace HotUpdateScripts.Project.ACommon
{
    /// <summary>
    /// 滚动选择器
    /// </summary>
    //[RequireComponent(typeof(BoxCollider2D))]
    public class ScrollSelector : MonoBehaviour//, UnityEngine.EventSystems.IPointerDownHandler, UnityEngine.EventSystems.IPointerUpHandler
    {
        public event Action<string> SelectorChooseCallBack;
        private int ItemNum = 5;//须奇数
        private List<string> dataList;
        private GameObject ItemPrefab;
        private Transform ItemParent;
        private List<GameObject> prefabList;

        private Color NormalColor = new Color32(176, 179, 188, 255);
        private Color ChooseColor = new Color32(22, 173, 205, 255);

        private int SelectIndex;
        private bool isDown = false;
        private float moveDis = 50;
        Vector3 vo;
        Vector3 vn;

        float dis = 0;
        private float po;

        private RectTransform rectTransform;

        public void Init(List<string> strList, Action<string> chooseCallBack, int itemNum = 7)
        {
            SelectIndex = 0;
            dataList = new List<string>();
            ItemPrefab = transform.GetChild(0).gameObject;
            ItemPrefab.SetActive(false);
            ItemParent = transform.GetChild(1);
            if (rectTransform == null) rectTransform = GetComponent<RectTransform>();
            if (prefabList == null) prefabList = new List<GameObject>();
            for (int i = 0; i < prefabList.Count; i++)
            {
                prefabList[i].SetActive(false);
            }
            po = rectTransform.localPosition.y;
            this.ItemNum = itemNum;
            if (strList.Count < itemNum && strList.Count > 0)
            {
                this.ItemNum = strList.Count > 7 ? 7 : strList.Count > 4 ? 5 : strList.Count > 2 ? 3 : 1;
                //this.ItemNum = strList.Count < 7 ? strList.Count < 5 ? strList.Count < 3 ? 5 : 3 : 1:0;
            }
            this.dataList = strList;
            SelectorChooseCallBack = chooseCallBack;
            int realValue;
            for (int i = 0; i < ItemNum; i++)
            {
                realValue = -(ItemNum / 2) + 1;
                SpawnItem(i);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(ItemParent.rectTransform());
            RefreshData();
        }

        GameObject SpawnItem(int index)
        {
            if (index < prefabList.Count)
            {
                prefabList[index].SetActive(true);
                return prefabList[index];
            }
            else
            {
                GameObject item = Instantiate(ItemPrefab);
                item.SetActive(true);
                item.transform.SetParent(ItemParent);
                item.transform.localScale = Vector3.one;
                item.transform.localEulerAngles = Vector3.zero;
                prefabList.Add(item);
                return item;
            }

        }

        private void RefreshData()
        {
            for (int i = 0; i < ItemNum; i++)
            {
                int realIndex = -(ItemNum / 2) + i;
                realIndex += SelectIndex;
                realIndex = (realIndex + dataList.Count) % dataList.Count;
                Text curtext = ItemParent.GetChild(i).GetComponentInChildren<Text>();
                curtext.text = dataList[realIndex];
                curtext.color = i == ItemNum / 2 ? ChooseColor : NormalColor;
            }
            SelectorChooseCallBack(dataList[SelectIndex]);
        }

        private void OnDestroy()
        {
            foreach (var item in prefabList) Destroy(item);
            prefabList.Clear();
        }

        GraphicRaycaster[] graphicRaycasters;
        public bool SelectCurrentGo()
        {
            GameObject obj = null;

            graphicRaycasters = FindObjectsOfType<GraphicRaycaster>();
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;
            List<RaycastResult> list = new List<RaycastResult>();
            
            if (graphicRaycasters.Length > 0)
            {
                for (int i = 0; i < graphicRaycasters.Length; i++)
                {
                    if (graphicRaycasters[i].transform.name== "Canvas")
                    {
                        graphicRaycasters[i].Raycast(eventData, list);
                        break;
                    }
                }
            }
            if (list.Count > 0)
            {
                obj = list[0].gameObject;
            }
            return obj == gameObject;
        }

        void Update()
        {
            if (!gameObject.activeSelf) return;

            if (Input.GetMouseButtonDown(0) && IsPointerOverGameObject())
            {
                if (SelectCurrentGo())
                {
                    vo = Input.mousePosition;
                    isDown = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDown = false;
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, po);
            }
            if (isDown)
            {
                vn = Input.mousePosition;
                dis = (vn.y - vo.y);
                vo = vn;

                rectTransform.localPosition += new Vector3(0, dis, 0);

                if (Mathf.Abs(po - rectTransform.localPosition.y) >= moveDis)
                {
                    if (po - rectTransform.localPosition.y < 0)
                    {
                        SelectIndex++;
                    }
                    else
                    {
                        SelectIndex--;
                    }
                    SelectIndex = (SelectIndex + dataList.Count) % dataList.Count;
                    RefreshData();
                    rectTransform.localPosition =
                        new Vector3(rectTransform.localPosition.x, po);
                }
            }

        }

        public bool IsPointerOverGameObject()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                return EventSystem.current.IsPointerOverGameObject();
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        //该方法可以判断触摸点是否在UI上
                        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
                            Debug.Log("鼠标处于UI上");
                            return true;
                        }

                        else
                        {
                            Debug.Log("鼠标不处于UI上");
                            return false;
                        }

                    }
                }
                return false;
            }
        }
    }
}
