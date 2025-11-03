using HotUpdateScripts.Project.BasePrj.Data;
using JEngine.Core;
using LitJson;
using My.UI;
using My.UI.Panel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using static HotUpdateScripts.Project.BasePrj.Data.GameData;

namespace HotUpdateScripts.Project.Common
{

    public enum Fill_2D
    {
        StretchToFit = 0,
        fillToFit,
    }

    public enum Match_Img
    {
        Width = 1,
        Height,
    }
    /// <summary>
    /// 组件拓展
    /// </summary>
    public static class ComponentExtensions
    {
        public static RectTransform rectTransform(this Component cp)
        {
            return cp.transform as RectTransform;
        }
    }
    /// <summary>
    /// LayoutGroup组件扩展 - 流式布局组件
    /// </summary>
    public class UIExtenFlowLayoutGroup : LayoutGroup
    {
        public float SpacingX = 0f;
        public float SpacingY = 0f;
        public bool ExpandHorizontalSpacing = false;

        public bool ChildForceExpandWidth = false;
        public bool ChildForceExpandHeight = false;

        private float _layoutHeight;

        public override void CalculateLayoutInputHorizontal()
        {

            base.CalculateLayoutInputHorizontal();

            var minWidth = GetGreatestMinimumChildWidth() + padding.left + padding.right;

            SetLayoutInputForAxis(minWidth, -1, -1, 0);

        }

        public override void SetLayoutHorizontal()
        {
            SetLayout(rectTransform.rect.width, 0, false);
        }

        public override void SetLayoutVertical()
        {
            SetLayout(rectTransform.rect.width, 1, false);
        }

        public override void CalculateLayoutInputVertical()
        {
            _layoutHeight = SetLayout(rectTransform.rect.width, 1, true);
        }

        protected bool IsCenterAlign
        {
            get
            {
                return childAlignment == TextAnchor.LowerCenter || childAlignment == TextAnchor.MiddleCenter ||
                    childAlignment == TextAnchor.UpperCenter;
            }
        }

        protected bool IsRightAlign
        {
            get
            {
                return childAlignment == TextAnchor.LowerRight || childAlignment == TextAnchor.MiddleRight ||
                    childAlignment == TextAnchor.UpperRight;
            }
        }

        protected bool IsMiddleAlign
        {
            get
            {
                return childAlignment == TextAnchor.MiddleLeft || childAlignment == TextAnchor.MiddleRight ||
                    childAlignment == TextAnchor.MiddleCenter;
            }
        }

        protected bool IsLowerAlign
        {
            get
            {
                return childAlignment == TextAnchor.LowerLeft || childAlignment == TextAnchor.LowerRight ||
                    childAlignment == TextAnchor.LowerCenter;
            }
        }

        /// <summary>
        /// Holds the rects that will make up the current row being processed
        /// </summary>
        private readonly IList<RectTransform> _rowList = new List<RectTransform>();

        /// <summary>
        /// Main layout method
        /// </summary>
        /// <param name="width">Width to calculate the layout with</param>
        /// <param name="axis">0 for horizontal axis, 1 for vertical</param>
        /// <param name="layoutInput">If true, sets the layout input for the axis. If false, sets child position for axis</param>
        public float SetLayout(float width, int axis, bool layoutInput)
        {
            var groupHeight = rectTransform.rect.height;

            // Width that is available after padding is subtracted
            var workingWidth = rectTransform.rect.width - padding.left - padding.right;

            // Accumulates the total height of the rows, including spacing and padding.
            var yOffset = IsLowerAlign ? (float)padding.bottom : (float)padding.top;

            var currentRowWidth = 0f;
            var currentRowHeight = 0f;

            for (var i = 0; i < rectChildren.Count; i++)
            {

                // LowerAlign works from back to front
                var index = IsLowerAlign ? rectChildren.Count - 1 - i : i;

                var child = rectChildren[index];

                var childWidth = LayoutUtility.GetPreferredSize(child, 0);
                var childHeight = LayoutUtility.GetPreferredSize(child, 1);

                // Max child width is layout group width - padding
                childWidth = Mathf.Min(childWidth, workingWidth);

                // If adding this element would exceed the bounds of the row,
                // go to a new line after processing the current row
                if (currentRowWidth + childWidth > workingWidth)
                {

                    currentRowWidth -= SpacingX;

                    // Process current row elements positioning
                    if (!layoutInput)
                    {

                        var h = CalculateRowVerticalOffset(groupHeight, yOffset, currentRowHeight);
                        LayoutRow(_rowList, currentRowWidth, currentRowHeight, workingWidth, padding.left, h, axis);

                    }

                    // Clear existing row
                    _rowList.Clear();

                    // Add the current row height to total height accumulator, and reset to 0 for the next row
                    yOffset += currentRowHeight;
                    yOffset += SpacingY;

                    currentRowHeight = 0;
                    currentRowWidth = 0;

                }

                currentRowWidth += childWidth;
                _rowList.Add(child);

                // We need the largest element height to determine the starting position of the next line
                if (childHeight > currentRowHeight)
                {
                    currentRowHeight = childHeight;
                }

                // Don't do this for the last one
                if (i < rectChildren.Count - 1)
                    currentRowWidth += SpacingX;
            }

            if (!layoutInput)
            {
                var h = CalculateRowVerticalOffset(groupHeight, yOffset, currentRowHeight);
                currentRowWidth -= SpacingX;
                // Layout the final row
                LayoutRow(_rowList, currentRowWidth, currentRowHeight, workingWidth - (_rowList.Count > 1 ? SpacingX : 0), padding.left, h, axis);
            }

            _rowList.Clear();

            // Add the last rows height to the height accumulator
            yOffset += currentRowHeight;
            yOffset += IsLowerAlign ? padding.top : padding.bottom;

            if (layoutInput)
            {

                if (axis == 1)
                    SetLayoutInputForAxis(yOffset, yOffset, -1, axis);

            }

            return yOffset;
        }

        private float CalculateRowVerticalOffset(float groupHeight, float yOffset, float currentRowHeight)
        {
            float h;

            if (IsLowerAlign)
            {
                h = groupHeight - yOffset - currentRowHeight;
            }
            else if (IsMiddleAlign)
            {
                h = groupHeight * 0.5f - _layoutHeight * 0.5f + yOffset;
            }
            else
            {
                h = yOffset;
            }
            return h;
        }

        protected void LayoutRow(IList<RectTransform> contents, float rowWidth, float rowHeight, float maxWidth, float xOffset, float yOffset, int axis)
        {
            var xPos = xOffset;

            if (!ChildForceExpandWidth && IsCenterAlign)
                xPos += (maxWidth - rowWidth) * 0.5f;
            else if (!ChildForceExpandWidth && IsRightAlign)
                xPos += (maxWidth - rowWidth);

            var extraWidth = 0f;
            var extraSpacing = 0f;

            if (ChildForceExpandWidth)
            {
                extraWidth = (maxWidth - rowWidth) / _rowList.Count;
            }
            else if (ExpandHorizontalSpacing)
            {
                extraSpacing = (maxWidth - rowWidth) / (_rowList.Count - 1);
                if (_rowList.Count > 1)
                {
                    if (IsCenterAlign)
                        xPos -= extraSpacing * 0.5f * (_rowList.Count - 1);
                    else if (IsRightAlign)
                        xPos -= extraSpacing * (_rowList.Count - 1);
                }
            }

            for (var j = 0; j < _rowList.Count; j++)
            {

                var index = IsLowerAlign ? _rowList.Count - 1 - j : j;

                var rowChild = _rowList[index];

                var rowChildWidth = LayoutUtility.GetPreferredSize(rowChild, 0) + extraWidth;
                var rowChildHeight = LayoutUtility.GetPreferredSize(rowChild, 1);

                if (ChildForceExpandHeight)
                    rowChildHeight = rowHeight;

                rowChildWidth = Mathf.Min(rowChildWidth, maxWidth);

                var yPos = yOffset;

                if (IsMiddleAlign)
                    yPos += (rowHeight - rowChildHeight) * 0.5f;
                else if (IsLowerAlign)
                    yPos += (rowHeight - rowChildHeight);

                // 
                if (ExpandHorizontalSpacing && j > 0)
                    xPos += extraSpacing;

                if (axis == 0)
                    SetChildAlongAxis(rowChild, 0, xPos, rowChildWidth);
                else
                    SetChildAlongAxis(rowChild, 1, yPos, rowChildHeight);

                // Don't do horizontal spacing for the last one
                if (j < _rowList.Count - 1)
                    xPos += rowChildWidth + SpacingX;
            }
        }

        public float GetGreatestMinimumChildWidth()
        {
            var max = 0f;

            for (var i = 0; i < rectChildren.Count; i++)
            {
                var w = LayoutUtility.GetMinWidth(rectChildren[i]);

                max = Mathf.Max(w, max);
            }

            return max;
        }
    }

    /// <summary>
    /// EmojiText扩展   支持表情包
    /// </summary>
    public class UIExtenEmojiText : Text
    {
        private const float ICON_SCALE_OF_DOUBLE_SYMBOLE = 0.7f;

        public override float preferredWidth => cachedTextGeneratorForLayout.GetPreferredWidth(emojiText, GetGenerationSettings(rectTransform.rect.size)) / pixelsPerUnit;
        public override float preferredHeight => cachedTextGeneratorForLayout.GetPreferredHeight(emojiText, GetGenerationSettings(rectTransform.rect.size)) / pixelsPerUnit;

        public string emojiText => Regex.Replace(text, "\\[emoji-[0-9]+\\]", "XX");
        private static Dictionary<string, EmojiInfo> m_EmojiIndexDict = null;

        public int TxtIndex = 1;

        //struct EmojiInfo
        //{
        //    public float x;
        //    public float y;
        //    public float size;
        //}

        readonly UIVertex[] m_TempVerts = new UIVertex[4];

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            base.OnPopulateMesh(toFill);
            if (font == null)
            {
                return;
            }
            if (m_EmojiIndexDict == null)
            {
                m_EmojiIndexDict = new Dictionary<string, EmojiInfo>();
                TextAsset emojiContent = JResource.LoadRes<TextAsset>("emoji" + TxtIndex + ".txt", JResource.MatchMode.Other);
                string[] lines = emojiContent.text.Split('\n');
                for (int i = 1; i < lines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(lines[i]))
                    {
                        string[] strs = lines[i].Split('\t');
                        EmojiInfo info = new EmojiInfo();
                        info.x = float.Parse(strs[3]);
                        info.y = float.Parse(strs[4]);
                        info.size = float.Parse(strs[5]);
                        m_EmojiIndexDict.Add(strs[1], info);
                    }
                }
            }

            Dictionary<int, EmojiInfo> emojiDic = new Dictionary<int, EmojiInfo>();

            if (supportRichText)
            {
                int nParcedCount = 0;
                //[1] [123] 替换成#的下标偏移量			
                int nOffset = 0;
                MatchCollection matches = Regex.Matches(text, "\\[emoji-[0-9]+\\]");
                for (int i = 0; i < matches.Count; i++)
                {
                    EmojiInfo info;
                    if (m_EmojiIndexDict.TryGetValue(matches[i].Value, out info))
                    {
                        emojiDic.Add(matches[i].Index - nOffset + nParcedCount, info);
                        nOffset += matches[i].Length - 1;
                        nParcedCount++;
                    }
                }
            }

            m_DisableFontTextureRebuiltCallback = true;

            Vector2 extents = rectTransform.rect.size;

            var settings = GetGenerationSettings(extents);
            cachedTextGenerator.Populate(emojiText, settings);

            Rect inputRect = rectTransform.rect;

            Vector2 textAnchorPivot = GetTextAnchorPivot(alignment);
            Vector2 refPoint = Vector2.zero;
            refPoint.x = Mathf.Lerp(inputRect.xMin, inputRect.xMax, textAnchorPivot.x);
            refPoint.y = Mathf.Lerp(inputRect.yMin, inputRect.yMax, textAnchorPivot.y);

            IList<UIVertex> verts = cachedTextGenerator.verts;
            float unitsPerPixel = 1 / pixelsPerUnit;
            int vertCount = verts.Count;

            if (vertCount <= 0)
            {
                toFill.Clear();
                return;
            }

            Vector2 roundingOffset = new Vector2(verts[0].position.x, verts[0].position.y) * unitsPerPixel;
            roundingOffset = PixelAdjustPoint(roundingOffset) - roundingOffset;
            toFill.Clear();
            if (roundingOffset != Vector2.zero)
            {
                for (int i = 0; i < vertCount; ++i)
                {
                    int tempVertsIndex = i & 3;
                    m_TempVerts[tempVertsIndex] = verts[i];
                    m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
                    m_TempVerts[tempVertsIndex].position.x += roundingOffset.x;
                    m_TempVerts[tempVertsIndex].position.y += roundingOffset.y;
                    if (tempVertsIndex == 3)
                    {
                        toFill.AddUIVertexQuad(m_TempVerts);
                    }
                }
            }
            else
            {
                for (int i = 0; i < vertCount; ++i)
                {
                    EmojiInfo info;
                    int index = i / 4;
                    if (emojiDic.TryGetValue(index, out info))
                    {
                        float emojiSize = 2 * (verts[i + 1].position.x - verts[i].position.x) * ICON_SCALE_OF_DOUBLE_SYMBOLE;

                        float fCharHeight = verts[i + 1].position.y - verts[i + 2].position.y;
                        float fCharWidth = verts[i + 1].position.x - verts[i].position.x;

                        float fHeightOffsetHalf = (emojiSize - fCharHeight) * 0.5f;
                        float fStartOffset = emojiSize * (1 - ICON_SCALE_OF_DOUBLE_SYMBOLE);

                        m_TempVerts[3] = verts[i];//1
                        m_TempVerts[2] = verts[i + 1];//2
                        m_TempVerts[1] = verts[i + 2];//3
                        m_TempVerts[0] = verts[i + 3];//4

                        m_TempVerts[0].position = m_TempVerts[0].position + new Vector3(fStartOffset, -fHeightOffsetHalf, 0);
                        m_TempVerts[1].position = m_TempVerts[1].position + new Vector3(fStartOffset - fCharWidth + emojiSize, -fHeightOffsetHalf, 0);
                        m_TempVerts[2].position = m_TempVerts[2].position + new Vector3(fStartOffset - fCharWidth + emojiSize, fHeightOffsetHalf, 0);
                        m_TempVerts[3].position = m_TempVerts[3].position + new Vector3(fStartOffset, fHeightOffsetHalf, 0);

                        m_TempVerts[0].position = m_TempVerts[0].position * unitsPerPixel;
                        m_TempVerts[1].position = m_TempVerts[1].position * unitsPerPixel;
                        m_TempVerts[2].position = m_TempVerts[2].position * unitsPerPixel;
                        m_TempVerts[3].position = m_TempVerts[3].position * unitsPerPixel;

                        float pixelOffset = emojiDic[index].size / 32 / 2;
                        m_TempVerts[0].uv1 = new Vector2(emojiDic[index].x + pixelOffset, emojiDic[index].y + pixelOffset);
                        m_TempVerts[1].uv1 = new Vector2(emojiDic[index].x - pixelOffset + emojiDic[index].size, emojiDic[index].y + pixelOffset);
                        m_TempVerts[2].uv1 = new Vector2(emojiDic[index].x - pixelOffset + emojiDic[index].size, emojiDic[index].y - pixelOffset + emojiDic[index].size);
                        m_TempVerts[3].uv1 = new Vector2(emojiDic[index].x + pixelOffset, emojiDic[index].y - pixelOffset + emojiDic[index].size);

                        toFill.AddUIVertexQuad(m_TempVerts);

                        i += 4 * 2 - 1;
                    }
                    else
                    {
                        int tempVertsIndex = i & 3;
                        m_TempVerts[tempVertsIndex] = verts[i];
                        m_TempVerts[tempVertsIndex].position = m_TempVerts[tempVertsIndex].position * unitsPerPixel;
                        if (tempVertsIndex == 3)
                        {
                            toFill.AddUIVertexQuad(m_TempVerts);
                        }
                    }

                }

            }
            m_DisableFontTextureRebuiltCallback = false;
        }
    }

    public class UIExtenEmojiText1 : Text
    {
        private const float ICON_SCALE_OF_DOUBLE_SYMBOLE = 0.7f;

        public override float preferredWidth => cachedTextGeneratorForLayout.GetPreferredWidth(emojiText, GetGenerationSettings(rectTransform.rect.size)) / pixelsPerUnit;
        public override float preferredHeight => cachedTextGeneratorForLayout.GetPreferredHeight(emojiText, GetGenerationSettings(rectTransform.rect.size)) / pixelsPerUnit;

        public string emojiText => Regex.Replace(text, "\\[emoji-[0-9]+\\]", "XX");
        //private Dictionary<string, EmojiInfo> m_EmojiIndexDict => GameData.BigEmojiDic;

        public int TxtIndex = 2;

        //struct EmojiInfo
        //{
        //    public float x;
        //    public float y;
        //    public float size;
        //}

        readonly UIVertex[] m_TempVerts = new UIVertex[4];

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            if (font == null || material == null)
            {
                return;
            }

            //base.OnPopulateMesh(toFill);
            if (GameData.BigEmojiDic == null)
            {
                GameData.BigEmojiDic = new Dictionary<string, EmojiInfo>();
                TextAsset emojiContent = JResource.LoadRes<TextAsset>("emoji" + TxtIndex + ".txt", JResource.MatchMode.Other);
                string[] lines = emojiContent.text.Split('\n');
                for (int i = 1; i < lines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(lines[i]))
                    {
                        string[] strs = lines[i].Split('\t');
                        EmojiInfo info = new EmojiInfo();
                        info.x = float.Parse(strs[3]);
                        info.y = float.Parse(strs[4]);
                        info.size = float.Parse(strs[5]);
                        GameData.BigEmojiDic.Add(strs[1], info);
                    }
                }
            }

            Dictionary<int, EmojiInfo> emojiDic = new Dictionary<int, EmojiInfo>();

            if (supportRichText)
            {
                int nParcedCount = 0;
                //[1] [123] 替换成#的下标偏移量			
                int nOffset = 0;
                MatchCollection matches = Regex.Matches(text, "\\[emoji-[0-9]+\\]");
                for (int i = 0; i < matches.Count; i++)
                {
                    EmojiInfo info;
                    if (GameData.BigEmojiDic.TryGetValue(matches[i].Value, out info))
                    {
                        emojiDic.Add(matches[i].Index - nOffset + nParcedCount, info);
                        nOffset += matches[i].Length - 1;
                        nParcedCount++;
                    }
                }
            }

            m_DisableFontTextureRebuiltCallback = true;

            Vector2 extents = rectTransform.rect.size;

            var settings = GetGenerationSettings(extents);
            cachedTextGenerator.Populate(emojiText, settings);

            Rect inputRect = rectTransform.rect;

            Vector2 textAnchorPivot = GetTextAnchorPivot(alignment);
            Vector2 refPoint = Vector2.zero;
            refPoint.x = Mathf.Lerp(inputRect.xMin, inputRect.xMax, textAnchorPivot.x);
            refPoint.y = Mathf.Lerp(inputRect.yMin, inputRect.yMax, textAnchorPivot.y);

            IList<UIVertex> verts = cachedTextGenerator.verts;
            float unitsPerPixel = 1 / pixelsPerUnit;
            int vertCount = verts.Count;

            if (vertCount <= 0)
            {
                toFill.Clear();
                return;
            }
            Vector2 roundingOffset = new Vector2(verts[0].position.x, verts[0].position.y) * unitsPerPixel;
            roundingOffset = PixelAdjustPoint(roundingOffset) - roundingOffset;
            toFill.Clear();
            if (roundingOffset != Vector2.zero)
            {
                for (int i = 0; i < vertCount; ++i)
                {
                    int tempVertsIndex = i & 3;
                    m_TempVerts[tempVertsIndex] = verts[i];
                    m_TempVerts[tempVertsIndex].position *= unitsPerPixel;
                    m_TempVerts[tempVertsIndex].position.x += roundingOffset.x;
                    m_TempVerts[tempVertsIndex].position.y += roundingOffset.y;
                    if (tempVertsIndex == 3)
                    {
                        toFill.AddUIVertexQuad(m_TempVerts);
                    }
                }
            }
            else
            {
                for (int i = 0; i < vertCount; ++i)
                {
                    EmojiInfo info;
                    int index = i / 4;
                    if (emojiDic.TryGetValue(index, out info))
                    {
                        float emojiSize = 2 * (verts[i + 1].position.x - verts[i].position.x) * ICON_SCALE_OF_DOUBLE_SYMBOLE;

                        float fCharHeight = verts[i + 1].position.y - verts[i + 2].position.y;
                        float fCharWidth = verts[i + 1].position.x - verts[i].position.x;

                        float fHeightOffsetHalf = (emojiSize - fCharHeight) * 0.5f;
                        float fStartOffset = emojiSize * (1 - ICON_SCALE_OF_DOUBLE_SYMBOLE);

                        m_TempVerts[3] = verts[i];//1
                        m_TempVerts[2] = verts[i + 1];//2
                        m_TempVerts[1] = verts[i + 2];//3
                        m_TempVerts[0] = verts[i + 3];//4

                        m_TempVerts[0].position = m_TempVerts[0].position + new Vector3(fStartOffset, -fHeightOffsetHalf, 0);
                        m_TempVerts[1].position = m_TempVerts[1].position + new Vector3(fStartOffset - fCharWidth + emojiSize, -fHeightOffsetHalf, 0);
                        m_TempVerts[2].position = m_TempVerts[2].position + new Vector3(fStartOffset - fCharWidth + emojiSize, fHeightOffsetHalf, 0);
                        m_TempVerts[3].position = m_TempVerts[3].position + new Vector3(fStartOffset, fHeightOffsetHalf, 0);

                        m_TempVerts[0].position = m_TempVerts[0].position * unitsPerPixel;
                        m_TempVerts[1].position = m_TempVerts[1].position * unitsPerPixel;
                        m_TempVerts[2].position = m_TempVerts[2].position * unitsPerPixel;
                        m_TempVerts[3].position = m_TempVerts[3].position * unitsPerPixel;

                        float pixelOffset = emojiDic[index].size / 32 / 2;
                        m_TempVerts[0].uv1 = new Vector2(emojiDic[index].x + pixelOffset, emojiDic[index].y + pixelOffset);
                        m_TempVerts[1].uv1 = new Vector2(emojiDic[index].x - pixelOffset + emojiDic[index].size, emojiDic[index].y + pixelOffset);
                        m_TempVerts[2].uv1 = new Vector2(emojiDic[index].x - pixelOffset + emojiDic[index].size, emojiDic[index].y - pixelOffset + emojiDic[index].size);
                        m_TempVerts[3].uv1 = new Vector2(emojiDic[index].x + pixelOffset, emojiDic[index].y - pixelOffset + emojiDic[index].size);

                        toFill.AddUIVertexQuad(m_TempVerts);

                        i += 4 * 2 - 1;
                    }
                    else
                    {
                        int tempVertsIndex = i & 3;
                        m_TempVerts[tempVertsIndex] = verts[i];
                        m_TempVerts[tempVertsIndex].position = m_TempVerts[tempVertsIndex].position * unitsPerPixel;
                        if (tempVertsIndex == 3)
                        {
                            toFill.AddUIVertexQuad(m_TempVerts);
                        }
                    }

                }

            }
            m_DisableFontTextureRebuiltCallback = false;
        }
    }

    /// <summary>
    /// 封装一些工具 不同模块以XX打头区分
    /// </summary>
    public class GameTools : Singleton<GameTools>
    {
        #region UITools
        /// <summary>
        ///找到子级
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetByName(Transform m_trans, string name)
        {
            Transform[] trans = m_trans.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == name)
                {
                    return trans[i].gameObject;
                }
            }
            return null;
        }
        /// <summary>
        ///找到子级
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject GetByName(GameObject m_trans, string name)
        {
            Transform[] trans = m_trans.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == name)
                {
                    return trans[i].gameObject;
                }
            }
            return null;
        }
        /// <summary>
        /// 绑定按钮事件
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="ClickEvent"></param>
        /// <param name="isAni"></param>
        /// <returns></returns>
        public GameObject AddClickEvent(GameObject btn, Action ClickEvent, bool isAni = true)
        {
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ClickEvent();
                if (isAni) { DOTweenMgr.Instance.BtnClickAni(btn, 0.15f); }
            });
            return btn;
        }
        public GameObject AddClickEvent(Transform btn, Action ClickEvent, bool isAni = true)
        {
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ClickEvent();
                if (isAni) { DOTweenMgr.Instance.BtnClickAni(btn.gameObject, 0.15f); }
            });
            return btn.gameObject;
        }
        /// <summary>
        /// 传入世界坐标,计算在对应的UI坐标 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public Vector3 GetUIPos(Vector3 target)
        {
            return Camera.main.WorldToScreenPoint(target);
        }

        /// <summary>
        /// 动态获取Text长度，设置背景宽度
        /// </summary>
        /// <param name="text"></param>
        /// <param name="target"></param>
        /// <param name="delta"></param>
        public void AdjustWidthSizeByText(RectTransform text, RectTransform target, float delta)
        {
            ContentSizeFitter csf = text.GetComponent<ContentSizeFitter>();
            if (csf != null)
            {
                Vector2 size = target.sizeDelta;
                csf.SetLayoutHorizontal();
                csf.SetLayoutVertical();
                size.x = text.sizeDelta.x + delta;
                target.sizeDelta = size;
            }
        }


        /// <summary>
        /// 使sprite拉伸铺满整个屏幕
        /// </summary>
        public void MatchingAnimatorToCamera(GameObject _objanimator, Fill_2D fill)
        {
            SpriteRenderer m_AnimatorSprite = new SpriteRenderer();
            if (_objanimator != null)
            {
                m_AnimatorSprite = _objanimator.GetComponent<SpriteRenderer>();
            }

            Vector3 scale = _objanimator.transform.localScale;
            float cameraheight = Camera.main.orthographicSize * 2;
            float camerawidth = cameraheight * Camera.main.aspect;

            if (fill == Fill_2D.fillToFit)
            {
                if (cameraheight >= camerawidth)
                {
                    scale *= cameraheight / m_AnimatorSprite.bounds.size.y;
                }
                else
                {
                    scale *= camerawidth / m_AnimatorSprite.bounds.size.x;
                }
            }
            else if (fill == Fill_2D.StretchToFit)
            {
                scale.y *= cameraheight / m_AnimatorSprite.bounds.size.y;

                scale.x *= camerawidth / m_AnimatorSprite.bounds.size.x;
            }
            _objanimator.transform.localScale = scale;
            _objanimator.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, _objanimator.transform.position.z);
        }

        /// <summary>
        /// 图片通过屏幕宽度自适应
        /// </summary>
        /// <param name="img"></param>
        public void MatchScreenByImgWidth(RectTransform rect)
        {
            if (Screen.width / rect.sizeDelta.x * rect.sizeDelta.y < Screen.height)
            {
                rect.sizeDelta = new Vector2(Screen.height / rect.sizeDelta.y * rect.sizeDelta.x, Screen.height);
            }
            else
            {
                rect.sizeDelta = new Vector2(Screen.width, Screen.width / rect.sizeDelta.x * rect.sizeDelta.y);
            }
        }

        /// <summary>
        /// 自动通过宽高自适应
        /// </summary>
        /// <param name="img"></param>
        public void MatchImgBySprite(Image img, Match_Img match_Img = 0)
        {
            float widthRect = img.GetComponent<RectTransform>().rect.width;
            float heightRect = img.GetComponent<RectTransform>().rect.height;

            if (match_Img == 0)
            {
                if (img.sprite.texture.width > img.sprite.texture.height)
                {
                    //高固定
                    widthRect = heightRect * img.sprite.texture.width / img.sprite.texture.height;
                }
                else
                {
                    //宽固定
                    heightRect = widthRect * img.sprite.texture.height / img.sprite.texture.width;
                }
            }
            else if (match_Img == Match_Img.Width)
            {
                //宽固定
                heightRect = widthRect * img.sprite.texture.height / img.sprite.texture.width;


            }
            else if (match_Img == Match_Img.Height)
            {
                widthRect = heightRect * img.sprite.texture.width / img.sprite.texture.height;

            }

            img.GetComponent<RectTransform>().sizeDelta = new Vector2(widthRect, heightRect);
        }

        public void MatchImgBySprite(RawImage img, Match_Img match_Img = 0)
        {
            float widthRect = img.GetComponent<RectTransform>().rect.width;
            float heightRect = img.GetComponent<RectTransform>().rect.height;

            if (match_Img == 0)
            {
                if (img.texture.width > img.texture.height)
                {
                    //高固定
                    widthRect = heightRect * img.texture.width / img.texture.height;
                }
                else
                {
                    //宽固定
                    heightRect = widthRect * img.texture.height / img.texture.width;
                }
            }
            else if (match_Img == Match_Img.Width)
            {
                //宽固定
                heightRect = widthRect * img.texture.height / img.texture.width;


            }
            else if (match_Img == Match_Img.Height)
            {
                widthRect = heightRect * img.texture.width / img.texture.height;

            }

            img.GetComponent<RectTransform>().sizeDelta = new Vector2(widthRect, heightRect);
        }


        //---立即强制重建布局 解决 ContentSizeFitter 无法实时刷新问题---
        ///  刷新布局
        public void UIRebuildLayoutImdSig(RectTransform rect)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }

        public void UIRebuildLayoutImdMut(Transform trans)
        {
            ContentSizeFitter[] SSFArr = trans.GetComponentsInChildren<ContentSizeFitter>();
            for (int i = SSFArr.Length - 1; i > 0; i--)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(SSFArr[i].GetComponent<RectTransform>());
            }
        }

        // 根据面板数 处理UI穿透3D
        Transform trasnCanvas;
        bool outpace;//超出1个
        public bool PanelsNumOutpace()//面板数>1 禁止UI穿透3D
        {
            if (trasnCanvas == null)
                trasnCanvas = UIMgr.Instance.UIRoot.transform;

            outpace = false;

            for (int i = 0; i < trasnCanvas.childCount; i++)
            {
                if (trasnCanvas.GetChild(i).childCount > 1)
                {
                    outpace = true;

                    //Debug.Log(trasnCanvas.GetChild(i)+" 有 "+ trasnCanvas.GetChild(i).childCount+" 个面板 ");
                    return outpace;
                }
            }
            return outpace;
        }

        #endregion

        #region TimeTools

        // 获取时间戳Timestamp  

        /// <summary>
        /// 根据DateTime 获取时间戳Timestamp 
        /// </summary>
        /// <param name="dt"> DateTime </param>
        /// <returns></returns>
        public static int TmGetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            int timeStamp = (int)(dt - dateStart).TotalSeconds;
            return timeStamp;
        }

        /// <summary>
        /// 根据DateTime 获取时间戳Timestamp 
        /// </summary>
        /// <param name="dt"> DateTime </param>
        /// <returns></returns>
        public static long TmGetTimeStamp2(DateTime dt)
        {
            long timeStamp = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return timeStamp;
        }

        /// <summary>
        /// 时间戳Timestamp转换成日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime TmGetDateTime(int timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = ((long)timeStamp * 10000000);
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return targetDt;
        }

        /// <summary>
        /// 时间戳Timestamp转换成日期
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime TmGetDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDt = dtStart.Add(toNow);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 将秒数转化为时分秒  00:00:00
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string TimeStampToTime(int duration)
        {
            int hour = 0;
            int minute = 0;
            int second = 0;

            if (duration >= 60)
            {
                minute = duration / 60;
                second = duration % 60;
            }
            else
            {
                second = duration;
            }
            if (minute > 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            return string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
        }
        #endregion

        #region ParticleEffectsTools
        /// <summary>
        /// 设置粒子特效尺寸
        /// </summary>
        /// <param name="ps">  </param>
        /// <param name="psScaleFloat"> 尺寸 </param>
        /// <param name="trans"> 粒子 </param>
        public void PESetPSS(ParticleSystem[] ps, float psScaleFloat, Transform trans)
        {
            foreach (var item in trans.GetComponentsInChildren<ParticleSystem>())
            {
                var main = item.main;
                main.scalingMode = ParticleSystemScalingMode.Local;
                item.transform.localScale = new Vector3(psScaleFloat, psScaleFloat, psScaleFloat);
            }
        }
        #endregion


        #region 字符串处理

        public static int CharStat(string str)
        {
            //str = str.TrimEnd();
            int digitCount = 0;
            int leterCount = 0;
            int spaceCount = 0;
            int chineseLeterCount = 0;
            int ortherLeterCount = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str, i))
                {
                    digitCount++;
                }
                else if (Char.IsWhiteSpace(str, i))
                {
                    spaceCount++;
                }
                else if (Char.ConvertToUtf32(str, i) >= Convert.ToInt32("4e00", 16) && Char.ConvertToUtf32(str, i) <= Convert.ToInt32("9fff", 16))
                {
                    chineseLeterCount++;
                }
                else if (Char.IsLetter(str, i))
                {
                    leterCount++;
                }
                else
                {
                    ortherLeterCount++;
                }
            }
            return digitCount + leterCount + spaceCount + chineseLeterCount * 2 + ortherLeterCount;
        }
        #endregion

        #region 打开弹窗 和 加载场景界面
        public static void SetTip(string str)
        {
            UIMgr.Instance.Open(UIPath.CommonTipPanel, null, UILayer.Layer5);
            CommonTipPanel.SetTip(str);
        }

        public static void Act_setTipWindow(string title, string content, string subTxt, string celtxt, Action act_sub)
        {
            UIMgr.Instance.Open(UIPath.CommonTipPanel, null, UILayer.Layer5);
            CommonTipPanel.Act_setTipWindow(title, content, subTxt, celtxt, act_sub);
        }

        public static void Act_setWindowTip(string title, string content, string subTxt)
        {
            UIMgr.Instance.Open(UIPath.CommonTipPanel, null, UILayer.Layer5);
            CommonTipPanel.Act_setWindowTip(title, content, subTxt);
        }


        public static void SetLoadingActive(bool status)
        {
            if (status)
            {
                UIMgr.Instance.Open(UIPath.UILoadingPanel, null, UILayer.Layer5);
            }
            else
            {
                debug.Log_yellow("Close(UIPath.UILoadingPanel);");

                UIMgr.Instance.Close(UIPath.UILoadingPanel);
            }
        }
        #endregion


        #region 公用方法封装
        /// <summary>
        /// 设置道具UI图标
        /// </summary>
        /// <param name="id"> 道具id </param>
        /// <param name="img"> 要设置icon的Image </param>
        public static void SetDaoJuUIIcon(int id, Image img)
        {
            string djSpName = "";

            if (id == 101)
            {
                djSpName = "nengliang";
            }
            else if (id == 102)
            {
                djSpName = "jiasu";
            }
            else if (id == 103)
            {
                djSpName = "jiaoyou";
            }
            else if (id == 104)
            {
                djSpName = "aixin";
            }

            JResource.LoadResAsync<Sprite>("Common/DaoJu/" + djSpName + ".png",
                (sp) =>
                {
                    img.sprite = sp;
                    img.SetNativeSize();
                },
                JResource.MatchMode.UI);
        }

        /// <summary>
        /// 返回正方形Texture
        /// </summary>
        public static Texture2D GetTextureResizer(Texture2D tex)
        {
            if (tex == null || tex.width == tex.height)
            {
                return tex;
            }
            int size = tex.width > tex.height ? tex.height : tex.width;//取窄边
            Texture2D resultTexture = new Texture2D(size, size, TextureFormat.RGBA4444, true);

            var m_Colors = tex.GetPixels(0, 0, size, size);
            resultTexture.SetPixels(m_Colors);
            resultTexture.Apply();
            return resultTexture;
        }
        #endregion


        /// <summary>
        /// 概率随机数
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static int ProbabilityRandomRumber(int[] rate)
        {
            int total = 0;
            for (int i = 0; i < rate.Length; i++)
            {
                total += rate[i];
            }
            System.Random myRandom = new System.Random();
            int r = myRandom.Next(0, total);
            int t = 0;
            for (int i = 0; i < rate.Length; i++)
            {
                t += rate[i];
                if (r < t)
                    return i;
            }
            return 0;
        }
    }

    /// <summary>
    /// 组件拓展方法
    /// </summary>

    public class CityHelper
    {
        public class Province
        {
            /// <summary>
            /// 省份名称
            /// </summary>
            public string provinceName { get; set; }

            public List<City> city { get; set; }
        }
        public class City
        {
            /// <summary>
            /// 城市名称
            /// </summary>
            public string cityName { get; set; }
        }

        public List<Province> provinces;

        public CityHelper()
        {
            //省份类型provinceType：1-直辖市，2-省，3-自治区，4-特别行政区
            //区域类型areaType：1-直辖市，2-地级市，3-县级市，4-地区，5-自治州，6-盟，7-特别行政区
            //是否是省会isCapital'
            //var json = Resources.Load<TextAsset>("Txt/city").text;
            TextAsset provincecityTxt = JResource.LoadRes<TextAsset>("provincecity.txt", JResource.MatchMode.Other);
            provinces = JsonMapper.ToObject<List<Province>>(provincecityTxt.text);
        }

        /// <summary>
        /// 获取所有的省份信息
        /// </summary>
        /// <returns></returns>
        public List<Province> GetAllProvinces()
        {
            return provinces;
        }

    }

    /// <summary>
    /// 敏感词过滤
    /// </summary>
    public class SensitiveWordFilter
    {
        /// <summary>
        /// 敏感词库 词树
        /// </summary>
        private ItemTree Library;
        /// <summary>
        /// 检测敏感词
        /// </summary>
        /// <param name="text">检测文本</param>
        /// <returns></returns>
        private Dictionary<int, char> WordsCheck(string text)
        {
            if (Library == null)
                return new Dictionary<int, char>();

            Dictionary<int, char> dic = new Dictionary<int, char>();
            ItemTree p = Library;
            List<int> indexs = new List<int>();
            char[] charArray = text.ToCharArray();
            for (int i = 0, j = 0; j < charArray.Length; j++)
            {
                char cha = charArray[j];
                var child = p.Child;
                var node = child.Find(e => e.Item == cha);
                if (node != null)
                {
                    indexs.Add(j);
                    if (node.IsEnd || node.Child == null)
                    {
                        if (node.Child != null)
                        {
                            int k = j + 1;
                            if (k < charArray.Length && node.Child.Exists(e => e.Item == charArray[k]))
                            {
                                p = node;
                                continue;
                            }
                        }

                        foreach (var item in indexs)
                            dic.Add(item, charArray[item]);

                        indexs.Clear();
                        p = Library;
                        i = j;
                        ++i;
                    }
                    else
                        p = node;
                }
                else
                {
                    indexs.Clear();
                    if (p.GetHashCode() != Library.GetHashCode())
                    {
                        j = i;
                        ++i;
                        p = Library;
                    }
                    else
                        i = j;
                }
            }

            return dic;
        }

        /// <summary>
        /// 替换敏感词
        /// </summary>
        /// <param name="text">检测文本</param>
        /// <param name="newChar">替换字符</param>
        /// <returns></returns>
        public bool SensitiveWordsReplace(ref string text, char newChar = '*')
        {
            Dictionary<int, char> dic = WordsCheck(text);
            if (dic != null && dic.Keys.Count > 0)
            {
                char[] charArray = text.ToCharArray();
                foreach (var item in dic)
                    charArray[item.Key] = newChar;

                text = new string(charArray);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找敏感词
        /// </summary>
        /// <param name="text">检测文本</param>
        /// <returns></returns>
        public List<string> FindSensitiveWords(string text)
        {
            Dictionary<int, char> dic = WordsCheck(text);
            if (dic != null && dic.Keys.Count > 0)
            {
                int i = -1;
                string str = "";
                List<string> list = new List<string>();
                foreach (var item in dic)
                {
                    if (i == -1 || i + 1 == item.Key)
                        str += item.Value;
                    else
                    {
                        list.Add(str);
                        str = "" + item.Value;
                    }

                    i = item.Key;
                }
                list.Add(str);

                return list.Distinct().ToList();
            }
            else
                return null;
        }

        /// <summary>
        /// 词库树结构类
        /// </summary>
        public class ItemTree
        {
            public char Item { get; set; }
            public bool IsEnd { get; set; }
            public List<ItemTree> Child { get; set; }
        }
        /// <summary>
        /// 加载 敏感词组，可被重写以自定义 如何加载 敏感词组
        /// </summary>
        public virtual void Init(string[] Words)
        {
            Library = new ItemTree() { Item = 'R', IsEnd = false, Child = CreateTree(Words) };
        }

        /// <summary>
        /// 创建词库树
        /// </summary>
        /// <param name="words">敏感词组</param>
        /// <returns></returns>
        private List<ItemTree> CreateTree(string[] words)
        {
            List<ItemTree> tree = null;

            if (words != null && words.Length > 0)
            {
                tree = new List<ItemTree>();
                char[] charArray;
                foreach (var item in words)
                {
                    if (item.Length > 0)
                    {
                        charArray = item.ToCharArray();
                        ItemTree node = tree.Find(e => e.Item == charArray[0]);
                        if (node != null)
                            AddChildTree(node, item);
                        else
                            tree.Add(CreateSingleTree(item));
                    }
                }
            }

            return tree;
        }

        /// <summary>
        /// 创建单个完整树
        /// </summary>
        /// <param name="word">单个敏感词</param>
        /// <returns></returns>
        private ItemTree CreateSingleTree(string word)
        {
            //根节点，此节点 值为空
            ItemTree root = new ItemTree();
            //移动 游标
            ItemTree p = root;

            char[] charArray = word.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                ItemTree child = new ItemTree() { Item = charArray[i], IsEnd = false, Child = null };
                p.Child = new List<ItemTree>() { child };
                p = child;
            }
            p.IsEnd = true;

            return root.Child.First();
        }

        /// <summary>
        /// 附加分支子树
        /// </summary>
        /// <param name="childTree">子树</param>
        /// <param name="word">单个敏感词</param>
        private void AddChildTree(ItemTree childTree, string word)
        {
            //移动 游标
            ItemTree p = childTree;
            char[] charArray = word.ToCharArray();
            for (int i = 1; i < charArray.Length; i++)
            {
                char cha = charArray[i];
                List<ItemTree> child = p.Child;
                if (child == null)
                {
                    ItemTree node = new ItemTree() { Item = cha, IsEnd = false, Child = null };
                    p.Child = new List<ItemTree>() { node };
                    p = node;
                }
                else
                {
                    ItemTree node = child.Find(e => e.Item == cha);
                    if (node == null)
                    {
                        node = new ItemTree() { Item = cha, IsEnd = false, Child = null };
                        child.Add(node);
                        p = node;
                    }
                    else
                        p = node;
                }
            }
            p.IsEnd = true;
        }
    }

    /// <summary>
    /// 图片切换
    /// </summary>
    public class ImgQiehuan
    {
        public Texture2D sp1;
        public Texture2D sp2;
        public Texture2D sp3;
        public Texture2D sp4;

        public Texture2D sp5;
        public Texture2D sp6;
        public Texture2D sp7;
        public Texture2D sp8;
        public Texture2D sp9;
        public Texture2D sp10;
        public Texture2D sp11;
        public Texture2D sp12;
        public Texture2D sp13;
        public Texture2D sp14;

        public void SetImg(int index, GameObject obj, bool isNative = false)
        {
            Texture2D texture2D = null;
            Sprite sprite = null;
            switch (index)
            {
                case 0:
                    if (sp1 == null) return;
                    texture2D = sp1;
                    break;
                case 1:
                    if (sp2 == null) return;
                    texture2D = sp2;
                    break;
                case 2:
                    if (sp3 == null) return;
                    texture2D = sp3;
                    break;
                case 3:
                    if (sp4 == null) return;
                    texture2D = sp4;
                    break;
                case 4:
                    if (sp5 == null) return;
                    texture2D = sp5;
                    break;
                case 5:
                    if (sp6 == null) return;
                    texture2D = sp6;
                    break;
                case 6:
                    if (sp7 == null) return;
                    texture2D = sp7;
                    break;
                case 7:
                    if (sp8 == null) return;
                    texture2D = sp8;
                    break;
                case 8:
                    if (sp9 == null) return;
                    texture2D = sp9;
                    break;
                case 9:
                    if (sp10 == null) return;
                    texture2D = sp10;
                    break;
                case 10:
                    if (sp11 == null) return;
                    texture2D = sp11;
                    break;
                case 11:
                    if (sp12 == null) return;
                    texture2D = sp12;
                    break;
                case 12:
                    if (sp13 == null) return;
                    texture2D = sp13;
                    break;
                case 13:
                    if (sp14 == null) return;
                    texture2D = sp14;
                    break;
                default:
                    break;
            }

            sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
            if (sprite != null) { obj.GetComponent<Image>().sprite = sprite; }
            if (isNative)
            {
                obj.GetComponent<Image>().SetNativeSize();
            }
        }
    }
}
