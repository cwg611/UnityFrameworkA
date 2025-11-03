using DG.Tweening;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBFriendMatchItem : MonoBehaviour
    {
        private GameObject xing;
        private Image Icon;
        private bool isFirst = true;
        private RectTransform tras;

        void setObj()
        {
            xing = GameTools.GetByName(transform, "xing");
            Icon = GameTools.GetByName(transform, "Icon").GetComponent<Image>();
            tras = transform.GetComponent<RectTransform>();
        }

        public void InitItem(string headImg)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            DOTweenMgr.Instance.MovePos(gameObject, new Vector3((Screen.width / 2 + 100), 690, 0), new Vector3((-Screen.width / 2 - 400), 690, 0), 5f, () =>
             {
                 Destroy(gameObject);
             });
            NetMgr.Instance.DownLoadHeadImg(r =>
            {
                if (Icon == null) return;
                Icon.sprite = r;
                //GameTools.Instance.MatchImgBySprite(Icon);
            }, headImg);
        }

        //当被射中时。星星显示
        public void setXingPlay()
        {
            xing.SetActive(true);
        }

        void OnDestroy()
        {

        }
    }
}
