using HotUpdateScripts.Project.Common;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using My.Msg;
using HotUpdateScripts.Project.BasePrj.Data;
using System;
using UnityEngine.EventSystems;

namespace My.UI.Panel
{
    public class FriendPageBase : MonoBehaviour
    {
        public bool HasInitPage { get; set; }
        public virtual void InitPage()
        {
            HasInitPage = true;
        }

        public virtual void OpenPage()
        {
            //transform.localScale = Vector3.one;
            gameObject.SetActive(true);
        }

        public virtual void ClosePage()
        {
            //transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            HasInitPage = false;
        }
    }
}
