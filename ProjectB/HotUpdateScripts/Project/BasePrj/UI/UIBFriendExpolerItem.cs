using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using HotUpdateScripts.Project.BasePrj.Data;
using System.Collections.Generic;
using JEngine.Core;

namespace My.UI.Panel
{
    public class UIBFriendExpolerItem : MonoBehaviour
    {
        private bool isFirst = true;
        private GameObject xz1, xz2, xz3, IconPrefab;
        private GameObject[] randomXzArray;

        private GameObject[] PosGoArray;
        int[] m_Pos;

        private List<GameObject> cacheObjList = new List<GameObject>();
        private List<UserFriend> m_DataList;

        void SetObj()
        {
            //GetComponent<Animation>().clip = JResource.LoadRes<Animation>("Animation/Ani_FriendItem.anim",JResource.MatchMode.Other);
            xz1 = GameTools.GetByName(transform, "xz1");
            xz2 = GameTools.GetByName(transform, "xz2");
            xz3 = GameTools.GetByName(transform, "xz3");
            IconPrefab = GameTools.GetByName(transform, "Icon");
            randomXzArray = new GameObject[] { xz1, xz2, xz3 };

            PosGoArray = new GameObject[] { GameTools.GetByName(transform, "pos1"), GameTools.GetByName(transform, "pos2"),
                GameTools.GetByName(transform, "pos3"), GameTools.GetByName(transform, "pos4"),GameTools.GetByName(transform, "pos5"),
                GameTools.GetByName(transform, "pos6"), GameTools.GetByName(transform, "pos7") };
        }

        //头像randomNum 1-3  +1图案 2-4
        public void InitItem(int randomNum, List<UserFriend> dataList)
        {
            if (isFirst)
            {
                SetObj();
                isFirst = false;
            }
            m_DataList = dataList;

            //控制动画
            transform.rotation = new Quaternion(0, 0, 0, 0);
            transform.localScale = Vector3.one * 0.2f;
            GetComponent<CanvasGroup>().alpha = 0;

            transform.DORotate(new Vector3(0, 0, -30), 8f).SetEase(Ease.Linear).OnComplete(() =>
            {
                ResetView();
            });
            transform.DOScale(Vector3.one * 0.8f, 8f).SetEase(Ease.Linear);
            var canvasGroup = transform.GetComponent<CanvasGroup>();
            float delay = 6;
            //canvasGroup.DOFade(1, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
            //{
            //    DOTween.To(()=> delay, ()=> delay);

            //});

            //随机Icon
            m_Pos = GetRandomNumber(randomNum + 1);
            if (m_Pos != null && m_Pos.Length == randomNum + 1)
            {
                for (int i = 0; i < randomNum; i++)
                {
                    if (IconPrefab!=null)
                    {
                        GameObject Item = Instantiate(IconPrefab, PosGoArray[m_Pos[i]].transform);
                        if (Item!=null)
                        {
                            Item.SetActive(true);
                            Item.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                            cacheObjList.Add(Item);
                            NetMgr.Instance.DownLoadHeadImg(r =>
                            {
                                if (Item!=null)
                                {
                                    Image Icon = Item.GetComponent<Image>();
                                    Icon.sprite = r;
                                    //GameTools.Instance.MatchImgBySprite(Icon);
                                }
                            }, m_DataList[i].headImgUrl);
                        }
                    }
                }
                var randomObj = randomXzArray[Random.Range(0, randomXzArray.Length)];
                GameObject Item2 = Instantiate(randomObj, PosGoArray[m_Pos[randomNum]].transform);
                Item2.SetActive(true);
                Item2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                cacheObjList.Add(Item2);
            }
        }

        public void ResetView()
        {
            gameObject.SetActive(false);
            //
            if (cacheObjList != null && cacheObjList.Count > 0)
            {
                for (int i = 0; i < cacheObjList.Count; i++)
                {
                    Destroy(cacheObjList[i]);
                }
                cacheObjList.Clear();
            }

        }

        //从0-6中取出number=2-4个组成数组，连续不超过2
        private int[] GetRandomNumber(int Number)
        {
            int[] resultArray = new int[Number];
            int[] iList = new int[] { 0, 1, 3, 4, 6, 0, 2, 3, 5, 6, 1, 2, 4, 5 };
            System.Random rand = new System.Random();//定义一个Random
            int index = rand.Next(0, iList.Length - 1);

            for (int i = 0; i < Number; i++)
            {
                index %= iList.Length;
                resultArray[i] = iList[index];
                index++;
            }
            return resultArray;
        }
    }
}
