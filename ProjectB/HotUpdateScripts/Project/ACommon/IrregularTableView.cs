using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace HotUpdateScripts.Project.ACommon
{
    /// 不规则循环列表， 单元大小
    [RequireComponent(typeof(ScrollRect))]
    public class IrregularTableView : MonoBehaviour
    {
        public enum CellViewStatus
        {

            InSide = 0, // 在显示框中

            OnTop = 1, // 在显示框上

            DownBelow = -1 // 在显示框下
        }

        public ScrollRect ScrollRect;

        public Action<IrregularTableViewCell, int> SetCellCallback;

        public delegate IrregularTableViewCell CreateItemDelegate(GameObject item);

        public CreateItemDelegate CreateItemCallBack;

        private RectTransform.Edge rectEdge = RectTransform.Edge.Bottom;

        private float top = 0;

        private float bottom = 0;

        private float spacing = 0;

        private GameObject cellPrefab;

        /// <summary>
        /// 滚动框大小
        /// </summary>
        private Vector2 viewPortSize = Vector2.zero;

        private Vector2 cellMinSize = new Vector2(750, 162);

        private readonly List<IrregularTableViewCell> showCells = new List<IrregularTableViewCell>();
        private int CellCount => showCells.Count;

        private IrregularTableViewCell[] cellPool;

        private int dataCount;

        public int MoveDir => ScrollRect.velocity.y > 0 ? 1 : -1;

        /// 头索引
        private int frontId;

        /// 尾索引
        private int rearId;

        private void Awake()
        {
            ScrollRect = GetComponent<ScrollRect>();
            cellPrefab = ScrollRect.content.GetChild(0).gameObject;
            cellPrefab.transform.localScale = Vector3.zero;
            ScrollRect.onValueChanged.AddListener(OnValueChanged);
        }

        /// 修改布局
        private void Start()
        {
            ScrollRect.horizontal = false;
            ScrollRect.vertical = true;
            ScrollRect.content.SetInsetAndSizeFromParentEdge(rectEdge, 0, 0);
            SetContent();
        }

        private void SetContent()
        {

            if (rectEdge == RectTransform.Edge.Top)
            {
                ScrollRect.content.pivot = new Vector2(0.5f, 1);
                cellPrefab.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
                cellPrefab.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
                cellPrefab.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1);
            }
            else
            {
                ScrollRect.content.pivot = new Vector2(0.5f, 0);
                cellPrefab.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
                cellPrefab.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0);
                cellPrefab.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
            }
        }

        private void OnValueChanged(Vector2 pos)
        {
            if (dataCount == 0 && ScrollRect.velocity.y == 0) return;
            UpdateCellList();
            UpdateContentSize();
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        /// <param name="count"></param>
        /// <param name="reLoad"></param>
        /// <param name="resetWrap"></param>
        public void Load(int count, bool reLoad = true, bool resetWrap = true)
        {
            dataCount = count;

            if (cellPool == null) InitCellPool();//初始化单元池

            if (resetWrap)
            {
                ScrollRect.StopMovement();

                ScrollRect.content.anchoredPosition = Vector2.zero;
            }

            if (reLoad)
            {
                ResetCells();

                AddShowCells();

                return;
            }

            UpdateCellList();

            UpdateContentSize();
        }

        private float GetContentLength()
        {
            var lenght = 0f;
            if (rectEdge == RectTransform.Edge.Top)
            {
                var cell = showCells[rearId];

                if (cell.DataIndex == dataCount - 1)
                {
                    lenght = -cell.RectTransform.anchoredPosition.y;

                    lenght += cell.GetSize().y;

                    lenght += top + bottom;
                    return lenght;
                }
            }
            else if (rectEdge == RectTransform.Edge.Bottom)
            {
                var cell = showCells[frontId];

                if (cell.DataIndex == 0)
                {
                    lenght = cell.RectTransform.anchoredPosition.y;

                    lenght += cell.GetSize().y;

                    lenght += top + bottom;
                    return lenght;
                }
            }


            return int.MaxValue;
        }

        /// <summary>
        /// 更新滚动条长度
        /// </summary>
        private void UpdateContentSize()
        {
            var pos = ScrollRect.content.anchoredPosition;
            pos = new Vector2(pos.x, Mathf.Abs(pos.y));

            var size = pos + viewPortSize;

            var contentLength = GetContentLength();

            if (size.y > contentLength)
            {
                size.y = contentLength;

            }
            ScrollRect.content.sizeDelta = size;
        }

        /// <summary>
        /// 刷新单元列表
        /// </summary>
        public void UpdateCellList()
        {
            if (showCells == null || CellCount == 0)
            {
                return;
            }
            if (MoveDir > 0)//向下滑动
            {
                var cell = showCells[frontId];
                var cellViewStatus = GetCellViewStatus(cell);

                if (cell.DataIndex + CellCount < dataCount && cellViewStatus == CellViewStatus.OnTop) // 向上移出
                {
                    cell.DataIndex += CellCount;

                    if (SetCellCallback != null)
                        SetCellCallback(cell, cell.DataIndex);

                    if (rectEdge == RectTransform.Edge.Top)
                    {
                        cell.RectTransform.anchoredPosition = showCells[rearId].RectTransform.anchoredPosition - GetItemHeight(showCells[rearId]);
                    }
                    else if (rectEdge == RectTransform.Edge.Bottom)
                    {
                        cell.RectTransform.anchoredPosition = showCells[rearId].RectTransform.anchoredPosition - GetItemHeight(cell);
                    }

                    frontId = (cell.Id + 1) % CellCount;

                    rearId = cell.Id;
                }
            }
            else
            {
                var cell = showCells[rearId];
                var cellViewStatus = GetCellViewStatus(cell);

                if (cell.DataIndex >= CellCount && cellViewStatus == CellViewStatus.DownBelow)
                {
                    cell.DataIndex -= CellCount;

                    if (SetCellCallback != null)
                        SetCellCallback(cell, cell.DataIndex);


                    if (rectEdge == RectTransform.Edge.Top)
                    {
                        cell.RectTransform.anchoredPosition = showCells[frontId].RectTransform.anchoredPosition + GetItemHeight(cell);
                    }
                    else if (rectEdge == RectTransform.Edge.Bottom)
                    {
                        cell.RectTransform.anchoredPosition = showCells[frontId].RectTransform.anchoredPosition + GetItemHeight(showCells[frontId]);
                    }


                    frontId = cell.Id;

                    rearId = cell.Id - 1;

                    rearId = rearId < 0 ? CellCount + rearId : rearId;
                }
            }
        }

        private Vector2 GetItemHeight(IrregularTableViewCell cell)
        {
            return Vector2.up * (cell.GetSize().y + spacing);
        }

        /// <summary>
        /// 单元状态
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public CellViewStatus GetCellViewStatus(IrregularTableViewCell cell)
        {
            if (rectEdge == RectTransform.Edge.Top)
            {
                var realPos = cell.RectTransform.anchoredPosition + ScrollRect.content.anchoredPosition;

                if (realPos.y > cell.RectTransform.sizeDelta.y)
                {
                    return CellViewStatus.OnTop;
                }
                else if (realPos.y < -viewPortSize.y)
                {
                    return CellViewStatus.DownBelow;
                }
            }
            else if (rectEdge == RectTransform.Edge.Bottom)
            {
                var realPos = cell.RectTransform.anchoredPosition + ScrollRect.content.anchoredPosition;

                if (realPos.y > viewPortSize.y)
                {
                    return CellViewStatus.OnTop;
                }
                else if (realPos.y < -cell.RectTransform.sizeDelta.y)
                {
                    return CellViewStatus.DownBelow;
                }
            }


            return CellViewStatus.InSide;
        }

        /// 创建cell
        private IrregularTableViewCell CreateViewCell()
        {
            var cellObj = Instantiate(cellPrefab);

            IrregularTableViewCell cell = CreateItemCallBack?.Invoke(cellObj);

            cellObj.transform.SetParent(ScrollRect.content, false);

            return cell;
        }

        /// <summary>
        /// 初始化单元池
        /// </summary>
        private void InitCellPool()
        {
            var corners = new Vector3[4];

            transform.GetComponent<RectTransform>().GetLocalCorners(corners);

            viewPortSize = corners[2] - corners[0];
            //cellMinSize
            //var cellMinSize = cellPrefab.GetComponent<RectTransform>().sizeDelta; // 假设所有单元都是最小size

            var cellCount = (int)(viewPortSize.y / cellMinSize.y) + 1;

            cellPool = new IrregularTableViewCell[cellCount];

            for (int i = 0; i < cellCount; ++i)
            {
                var cell = CreateViewCell();

                cell.Id = i;

                cell.transform.localScale = Vector3.zero;

                cellPool[i] = cell;
            }
        }

        private void ResetCells()
        {
            //ResetCells
            for (var i = 0; i < cellPool.Length; ++i)
            {
                cellPool[i].transform.localScale = Vector3.zero;
            }
            showCells.RemoveAll(c => c != null);

            if (dataCount < cellPool.Length && rectEdge != RectTransform.Edge.Top)//不满一页的时候设置从上方开始显示
            {
                rectEdge = RectTransform.Edge.Top;
                SetContent();
                InitCellPool();//重置
            }
            else if (dataCount >= cellPool.Length && rectEdge != RectTransform.Edge.Bottom)
            {
                rectEdge = RectTransform.Edge.Bottom;
                SetContent();
                InitCellPool();//重置
            }
        }

        private void AddShowCells()
        {
            if (rectEdge == RectTransform.Edge.Top)
            {
                //显示的数量
                var count = dataCount < cellPool.Length ? dataCount : cellPool.Length;//数量超过一页，数量不满一页

                for (var i = count; i < showCells.Count; ++i)//隐藏多余cell
                {
                    showCells[i].transform.localScale = Vector3.zero;

                    showCells.RemoveAt(i);
                }

                var curCellPos = Vector2.down * top;//上方间隔

                for (var i = 0; i < count; ++i)
                {
                    var cell = cellPool[i];

                    if (cell.transform.localScale == Vector3.zero)
                    {
                        cell.DataIndex = i;//数据编号

                        cell.transform.localScale = Vector3.one;

                        SetCellCallback?.Invoke(cell, cell.DataIndex);

                        cell.RectTransform.anchoredPosition = curCellPos;

                        curCellPos -= GetItemHeight(cell);

                        showCells.Add(cell);
                    }
                }

                frontId = 0;

                rearId = showCells.Count - 1;
                ScrollRect.verticalNormalizedPosition = 1;
            }
            else if (rectEdge == RectTransform.Edge.Bottom)//初始在最下方
            {
                var count = dataCount < cellPool.Length ? dataCount : cellPool.Length;

                for (var i = count; i < showCells.Count; ++i)
                {
                    showCells[i].transform.localScale = Vector3.zero;

                    showCells.RemoveAt(i);
                }

                var curCellPos = Vector2.up * bottom;
                List<IrregularTableViewCell> cellTemp = new List<IrregularTableViewCell>();
                for (int i = 0; i < count; i++)
                {
                    var cell = cellPool[count - i - 1];

                    //if (!cell.gameObject.activeSelf)
                    if (cell.transform.localScale == Vector3.zero)
                    {
                        cell.DataIndex = dataCount - 1 - i;

                        cell.transform.localScale = Vector3.one;

                        SetCellCallback?.Invoke(cell, cell.DataIndex);

                        cell.RectTransform.anchoredPosition = curCellPos;

                        curCellPos += GetItemHeight(cell);

                        cellTemp.Add(cell);
                    }
                }
                for (int i = cellTemp.Count - 1; i >= 0; i--)
                {
                    showCells.Add(cellTemp[i]);
                }


                frontId = 0;

                rearId = showCells.Count - 1;
                ScrollRect.verticalNormalizedPosition = 0;

            }


        }
    }

    /// <summary>
    /// 不规则循环列表Item
    /// </summary>
    public class IrregularTableViewCell : MonoBehaviour
    {
        public int Id;//Cell编号

        public int DataIndex;

        public RectTransform RectTransform;

        public virtual void SetData(object data)
        {
            if (RectTransform == null) RectTransform = GetComponent<RectTransform>();
        }

        public Vector2 GetSize()
        {
            return RectTransform.sizeDelta;
        }
    }
}
