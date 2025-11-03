using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// 循环列表
    /// </summary>
    public class TableView : MonoBehaviour
    {
        #region 声明
        //子对象预制体
        public GameObject cellItem;

        //可见区域尺寸
        private Vector2 visibleViewSize;

        //面板总尺寸
        private Vector2 totalViewSize;

        //子项尺寸
        private Vector2 cellSize;

        //子项间隔
        private float cellInterval;

        //可滑动的距离
        private float totalScrollDistance;

        //子项数量
        private int totalCellCount;

        //开始结束下标
        private int startIndex;
        private int endIndex;

        //内容有已滑动距离
        private Vector2 contentOffset;

        //可见子项集合
        private Dictionary<int, GameObject> cells;

        //可重用子项集合
        private List<GameObject> reUseCells;

        private ViewDirection currentDir;

        //ScrollRect 组件
        private UnityEngine.UI.ScrollRect scrollRect;

        //遮罩的rectTransform
        private RectTransform viewportRectTransform;

        //内容的rectTransform
        private RectTransform contentRectTransform;

        private bool ItemRechristen;//item重命名

        //public delegate void OnItemRender(int index, Transform trans);
        //Item回调
        //public OnItemRender onItemRender;
        public Action<GameObject> onItemCreated;
        public Action<int, GameObject> onItemRender;

        public enum ViewDirection
        {
            Horizontal,
            Vertical
        }
        #endregion

        #region 初始化
        public bool Init = false;

        public void InitTable(int dataCount, int interval = 20, bool Rechristen = true)
        {
            totalCellCount = dataCount;
            cellInterval = interval; // 子项的间隔
            ItemRechristen = Rechristen;
            InitComponent();
            Init = true;
        }

        public void ReLoadTable(int dataCount, bool resetPos = true)
        {
            totalCellCount = dataCount;

            foreach (KeyValuePair<int, GameObject> pair in cells)
            {
                pair.Value.SetActive(false);
                reUseCells.Add(pair.Value);
            }
            cells.Clear();

            ResetContent(resetPos);
        }

        //固定初始化组件
        private void InitComponent()
        {
            scrollRect = gameObject.GetComponent<UnityEngine.UI.ScrollRect>();
            viewportRectTransform = scrollRect.viewport;
            contentRectTransform = scrollRect.content;
            cellItem = contentRectTransform.GetChild(0).gameObject;
            cellItem.SetActive(false);
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);

            cells = new Dictionary<int, GameObject>();
            reUseCells = new List<GameObject>();
            contentOffset = new Vector2(0, 0);
            visibleViewSize = viewportRectTransform.rect.size;
            cellSize = cellItem.GetComponent<RectTransform>().rect.size;


            if (scrollRect.horizontal)
            {
                currentDir = ViewDirection.Horizontal;

                // 所有锚点设置成一样的
                contentRectTransform.anchorMin = new Vector2(0, 0.5f);
                contentRectTransform.anchorMax = contentRectTransform.anchorMin;
                contentRectTransform.pivot = contentRectTransform.anchorMin;
            }
            else
            {
                currentDir = ViewDirection.Vertical;

                contentRectTransform.anchorMin = new Vector2(0.5f, 1);
                contentRectTransform.anchorMax = contentRectTransform.anchorMin;
                contentRectTransform.pivot = contentRectTransform.anchorMin;
            }
            ResetContent();
        }

        /// <summary>
        /// 重置面板
        /// </summary>
        private void ResetContent(bool resetPos = true)
        {
            int count = 0;
            if (scrollRect.horizontal)
            {
                totalViewSize = new Vector2(cellSize.x * totalCellCount + cellInterval * (totalCellCount - 1), contentRectTransform.sizeDelta.y);

                totalScrollDistance = totalViewSize.x - visibleViewSize.x;

                if (visibleViewSize.x > totalViewSize.x)
                {
                    count = Mathf.FloorToInt((totalViewSize.x + cellInterval) / (cellSize.y + cellInterval));
                }
                else
                {
                    count = Mathf.CeilToInt((visibleViewSize.x + cellInterval) / (cellSize.y + cellInterval));
                }
            }
            else
            {
                totalViewSize = new Vector2(contentRectTransform.sizeDelta.x, cellSize.y * totalCellCount + cellInterval * (totalCellCount - 1));

                totalScrollDistance = totalViewSize.y - visibleViewSize.y;

                if (visibleViewSize.y > totalViewSize.y)
                {
                    count = Mathf.FloorToInt((totalViewSize.y + cellInterval) / (cellSize.y + cellInterval));
                }
                else
                {
                    // count * cellSize + (count - 1) * cellInterval = count * (cellSize + cellInterval) - cellInterval >= visibleViewSize
                    // => count >= (visibleViewSize - cellInterval) / (cellSize + cellInterval)
                    count = Mathf.CeilToInt((visibleViewSize.y + cellInterval) / (cellSize.y + cellInterval));
                }
            }
            // 设置内容面板尺寸
            contentRectTransform.sizeDelta = totalViewSize;
            if (resetPos)
            {
                scrollRect.StopMovement();
                contentRectTransform.anchoredPosition = Vector2.zero;
                for (int i = 0; i < count; ++i)
                {
                    OnCellCreateAtIndex(i);
                }
            }
            else
            {
                CalCellIndex();
            }

        }

        #endregion

        //public void OnDestroy()
        //{
        //    cells.Clear();
        //    reUseCells.Clear();
        //}

        /// <summary>
        /// 直接跳到最上面或者最左边
        /// </summary>
        public void JumpToTop()
        {
            scrollRect.StopMovement();
            if (currentDir == ViewDirection.Horizontal)
            {
                contentRectTransform.anchoredPosition = new Vector2(0, contentRectTransform.anchoredPosition.y);
            }
            else
            {
                contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, 0);
            }
        }
        public void JumpToBottom()
        {
            if (totalScrollDistance > 0)
            {
                scrollRect.StopMovement();
                if (currentDir == ViewDirection.Horizontal)
                {
                    contentRectTransform.anchoredPosition = new Vector2(-totalScrollDistance, contentRectTransform.anchoredPosition.y);
                }
                else
                {
                    contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, totalScrollDistance);
                }
            }
        }

        private void OnCellCreateAtIndex(int index)
        {
            GameObject cell = null;
            if (reUseCells.Count > 0)
            {
                // 有可以重用的
                cell = reUseCells[0];
                reUseCells.RemoveAt(0);
            }
            else
            {
                // 没有的话就从预制体创建
                cell = GameObject.Instantiate(cellItem);
                onItemCreated?.Invoke(cell);
            }
            cell.transform.SetParent(contentRectTransform);
            cell.transform.localScale = Vector3.one;

            RectTransform cellRectTrans = cell.GetComponent<RectTransform>();
            if (currentDir == ViewDirection.Horizontal)
            {
                cellRectTrans.anchorMin = new Vector2(0, 0.5f);
                cellRectTrans.anchorMax = cellRectTrans.anchorMin;
                cellRectTrans.pivot = cellRectTrans.anchorMin;

                float posX = index * cellSize.x + index * cellInterval;
                cellRectTrans.anchoredPosition = new Vector2(posX, 0);
            }
            else
            {
                cellRectTrans.anchorMin = new Vector2(0.5f, 1);
                cellRectTrans.anchorMax = cellRectTrans.anchorMin;
                cellRectTrans.pivot = cellRectTrans.anchorMin;

                float posY = index * cellSize.y + index * cellInterval;
                cellRectTrans.anchoredPosition = new Vector2(0, -posY);
            }
            if (ItemRechristen) cell.name = index.ToString();

            cell.SetActive(true);
            cells.Add(index, cell);
            //回调
            onItemRender(index, cell);
        }

        private void OnScrollValueChanged(Vector2 offset)
        {
            // offset 的x,y都是0-1之间的数 分别代表横向滑出的宽度百分比 纵向。。。
            OnCellScrolling(offset);
        }
        private void OnCellScrolling(Vector2 offset)
        {
            if ((currentDir == ViewDirection.Horizontal && totalViewSize.x <= visibleViewSize.x) ||
                (currentDir == ViewDirection.Vertical && totalViewSize.y <= visibleViewSize.y))
            {
                return;
            }

            offset.x = Mathf.Max(Mathf.Min(offset.x, 1), 0);
            offset.y = Mathf.Max(Mathf.Min(offset.y, 1), 0);

            contentOffset.x = totalScrollDistance * offset.x;
            contentOffset.y = totalScrollDistance * (1 - offset.y);

            CalCellIndex();
        }
        private void CalCellIndex()
        {
            float startOffset = 0f;
            float endOffset = 0f;
            if (currentDir == ViewDirection.Horizontal)
            {
                startOffset = contentOffset.x; // 当前可见区域起始x坐标
                endOffset = contentOffset.x + visibleViewSize.x;

                startIndex = Mathf.CeilToInt((startOffset + cellInterval) / (cellSize.x + cellInterval)) - 1;
                startIndex = Mathf.Max(0, startIndex);

                endIndex = Mathf.CeilToInt((endOffset + cellInterval) / (cellSize.x + cellInterval)) - 1;
                endIndex = Mathf.Min(endIndex, totalCellCount - 1);
            }
            else
            {
                startOffset = contentOffset.y;
                endOffset = contentOffset.y + visibleViewSize.y;

                startIndex = Mathf.CeilToInt((startOffset + cellInterval) / (cellSize.y + cellInterval)) - 1;
                startIndex = Mathf.Max(0, startIndex);

                endIndex = Mathf.CeilToInt((endOffset + cellInterval) / (cellSize.y + cellInterval)) - 1;
                endIndex = Mathf.Min(endIndex, totalCellCount - 1);
            }

            UpdateCells(startIndex, endIndex);
        }
        private void UpdateCells(int startIndex, int endIndex)
        {
            List<int> delList = new List<int>();
            foreach (KeyValuePair<int, GameObject> pair in cells)
            {
                if (pair.Key < startIndex || pair.Key > endIndex)
                {
                    delList.Add(pair.Key);
                    pair.Value.SetActive(false);
                    reUseCells.Add(pair.Value);
                }
            }

            foreach (int index in delList)
            {
                cells.Remove(index);
            }
            for (int i = startIndex; i <= endIndex; ++i)
            {
                if (!cells.ContainsKey(i))
                {
                    OnCellCreateAtIndex(i);
                }
            }
        }
    }
}

