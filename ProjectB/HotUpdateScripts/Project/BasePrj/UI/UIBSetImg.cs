using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBSetImg : MonoBehaviour
    {
        private RawImage img;
        private bool isFirst = true;
        private bool isFinish = false;

        void setObj()
        {
            img = transform.GetComponent<RawImage>();
        }

        public void InitItem(string url, Match_Img match)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
                img.GetComponent<Button>().onClick.AddListener(() =>
                {
                    if (isFinish)
                    {
                        GameData.bigImg = img.texture;
                        UIMgr.Instance.Open(UIPath.UIBBigImgPanel);
                    }
                });
            }
            NetMgr.Instance.DownLoadImgTwo(r =>
            {
                isFinish = true;
                if (img == null) return;
                img.texture = r;
                GameTools.Instance.MatchImgBySprite(img, match);
            }, url);
        }
    }
}
