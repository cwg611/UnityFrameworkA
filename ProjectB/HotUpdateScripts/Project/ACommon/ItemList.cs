using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// 标签或列表预制体生成组件
    /// </summary>
    public class ItemList : MonoBehaviour
    {
        private readonly List<GameObject> mItemList = new List<GameObject>();
        public GameObject ItemTemplate;

        public int Count => mItemList.Count;
        public GameObject this[int index] => mItemList[index];

        public void Reset()
        {
            if (ItemTemplate == null)
            {
                ItemTemplate = transform.GetChild(0).gameObject;
                ItemTemplate.SetActive(false);
            }
            for (int i = 0; i < mItemList.Count; i++)
            {
                mItemList[i].SetActive(false);
            }
        }

        public void Reset(int maxValue)
        {
            for (int i = 0; i < mItemList.Count; i++)
            {
                mItemList[i].SetActive(i + 1 <= maxValue);
            }
        }

        public void ResetDestroy()
        {
            for (int i = mItemList.Count; i > 0; i--)
            {
                DestroyImmediate(mItemList[i - 1]);
            }
            mItemList.Clear();
        }

        public void Refresh<T>(int index, T data, Action<T, GameObject> draw, bool isActive = true)
        {
            if (mItemList == null) return;

            if (index < mItemList.Count)
            {
                mItemList[index].SetActive(isActive);
                draw(data, mItemList[index]);
            }
            else
            {
                if (ItemTemplate == null)
                {
                    ItemTemplate = transform.GetChild(0).gameObject;
                    ItemTemplate.SetActive(false);
                }
                var newItem = Instantiate(ItemTemplate);
                newItem.SetActive(isActive);
                newItem.transform.SetParent(ItemTemplate.transform.parent, false);
                mItemList.Add(newItem);
                draw(data, newItem);
            }
        }

        public void Refresh<T>(int index, T data, Action<T, GameObject, object[]> draw, bool isActive = true, params object[] followData)
        {
            if (mItemList == null) return;

            if (index < mItemList.Count)
            {
                mItemList[index].SetActive(isActive);
                draw(data, mItemList[index], followData);
            }
            else
            {
                if (ItemTemplate == null)
                {
                    ItemTemplate = transform.GetChild(0).gameObject;
                    ItemTemplate.SetActive(false);
                }
                var newItem = Instantiate(ItemTemplate);
                newItem.SetActive(isActive);
                newItem.transform.SetParent(ItemTemplate.transform.parent, false);
                mItemList.Add(newItem);
                draw(data, newItem, followData);
            }
        }
        public void RefreshData<T>(int index, T data, Action<int, T, GameObject> draw, bool isActive = true)
        {
            if (mItemList == null) return;

            if (index < mItemList.Count)
            {
                mItemList[index].SetActive(isActive);
                draw(index, data, mItemList[index]);
            }
            else
            {
                if (ItemTemplate == null)
                {
                    ItemTemplate = transform.GetChild(0).gameObject;
                    ItemTemplate.SetActive(false);
                }
                var newItem = Instantiate(ItemTemplate);
                newItem.SetActive(isActive);
                newItem.transform.SetParent(ItemTemplate.transform.parent, false);
                mItemList.Add(newItem);
                draw(index, data, newItem);
            }
        }

        public GameObject GetGoItem(int index)
        {
            if (index >= mItemList.Count) return null;

            return mItemList[index];
        }

        public List<GameObject> GetItemList()
        {
            return mItemList;
        }

        private void OnDestroy()
        {
            foreach (var item in mItemList) Destroy(item);
            mItemList.Clear();
        }
    }
}
