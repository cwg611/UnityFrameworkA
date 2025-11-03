using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace HotUpdateScripts.Project.Common
{
    /// <summary>
    /// 轮播图工具
    /// </summary>
    public class CarouselComponent : MonoBehaviour
    {
        public class DataCarousel
        {
            public string Name;

            public string Picture;
        }

        public Button Left;
        public Button Right;
        public List<float> posXList;

        public static bool IsCanClickBtn = true;
        public static int panoramaID = 0;
        public Text TextName;
        List<DataCarousel> carouselDatas;
        Transform itemParent;
        int curIndex = 0;//第一张图的数据索引

        public void InitData(List<DataCarousel> dataCarousels)
        {
            carouselDatas = dataCarousels;
            itemParent = transform.Find("Imgs");
            curIndex = 0;
            posXList = new List<float>();
            for (int i = 0; i < itemParent.childCount; i++)
            {
                posXList.Add(itemParent.GetChild(i).rectTransform().anchoredPosition.x);
                SetItem(itemParent.GetChild(i), carouselDatas[i]);
            }

            transform.Find("LeftBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                LeftMoveToRight(true);
            });
            transform.Find("RightBtn").GetComponent<Button>().onClick.AddListener(() =>
            {
                LeftMoveToRight(false);
            });
        }

        private void SetItem(Transform item, DataCarousel dataCarousel)
        {
            SetPitcure(item.GetChild(0).GetComponent<Image>(), dataCarousel.Picture);
            item.GetChild(1).GetComponent<Text>().text = dataCarousel.Name;
        }

        List<Transform> itemsList = new List<Transform>();
        void LeftMoveToRight(bool IsLeft)
        {
            if (carouselDatas == null || carouselDatas.Count == 0)
            {
                return;
            }
            itemsList.Clear();
            for (int i = 0; i < itemParent.childCount; i++)
            {
                itemsList.Add(itemParent.GetChild(i));
            }
            if (IsLeft)
            {
                curIndex = (curIndex + 1) % carouselDatas.Count;
            }
            else
            {
                curIndex = (curIndex - 1 + carouselDatas.Count) % carouselDatas.Count;
            }
            for (int i = 0; i < itemsList.Count; i++)
            {
                Transform item = itemsList[i];
                if (i == 0)//左
                {
                    if (IsLeft)//0-1
                    {
                        MoveItem(item, 1, 190, true);
                        SetItem(item, carouselDatas[(curIndex + 2) % carouselDatas.Count]);
                    }
                    else//0-2
                    {
                        MoveItem(item, 2, 250, true);
                    }
                }
                else if (i == 1)//右
                {
                    if (IsLeft)//1-2
                    {
                        MoveItem(item, 2, 250, true);
                    }
                    else//1-0
                    {
                        MoveItem(item, 0, 190, true);
                        SetItem(item, carouselDatas[curIndex]);
                    }
                }
                else if (i == 2)//中
                {
                    if (IsLeft)//2-0
                    {
                        MoveItem(item, 0, 190, true);
                    }
                    else//2-1
                    {
                        MoveItem(item, 1, 190, true);
                    }
                }
            }
        }


        private void MoveItem(Transform item, int index, int rgbA, bool isbreak)
        {
            if (index == 2)
            {
                item.SetAsLastSibling();
                item.DOScale(1f, 0.2f).SetEase(Ease.Linear);
            }
            else if (index == 0)
            {
                item.SetAsFirstSibling();
                item.DOScale(0.77f, 0.2f).SetEase(Ease.Linear);
            }
            else
            {
                item.DOScale(0.77f, 0.2f).SetEase(Ease.Linear);
            }

            item.DOLocalMoveX(posXList[index], isbreak == true ? 0.2f : 0).SetEase(Ease.Linear);

            //item.DOScale(1f, 0.2f).SetEase(Ease.Linear);
            item.GetComponent<Image>().color = new Color(1, 1, 1, rgbA / 255f);
        }

        void SetPitcure(Image image, string url)
        {
            NetMgr.Instance.DownLoadImg(r =>
            {
                if (image == null) return;
                image.sprite = r;
                //GameTools.Instance.MatchImgBySprite(image, Match_Img.Height);
            }, url);
        }
    }
}
