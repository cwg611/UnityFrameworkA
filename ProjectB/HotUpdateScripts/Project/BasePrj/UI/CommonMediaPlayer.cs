using HotUpdateScripts.Project.Common;
using RenderHeads.Media.AVProVideo;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class CommonMediaPlayer : BasePanel
    {
        Button btn_Close;
        MediaPlayer mediaPlayer;
        RectTransform rectTransform;


        public override void InitPanel(object o)
        {
            mediaPlayer = transform.Find("MediaPlayer").GetComponent<MediaPlayer>();
            mediaPlayer.OpenMedia(MediaPathType.AbsolutePathOrURL, o.ToString(), true);

            btn_Close = transform.Find("btn_Close").GetComponent<Button>();
            btn_Close.onClick.AddListener(
                () =>
                {
                    UIMgr.Instance.Close(UIPath.CommonMediaPlayer);
                }
                );


            transform.Find("Controls/BottomRow/ButtonOptions").gameObject.SetActive(false);


            rectTransform = transform.GetComponent<RectTransform>();
            //适配
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);

            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);

            rectTransform.localScale = Vector3.one;
        }




    }
}