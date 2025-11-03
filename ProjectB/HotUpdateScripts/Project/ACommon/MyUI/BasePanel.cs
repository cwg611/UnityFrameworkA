using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace My.UI
{
    /// <summary>
    /// UI面板基类
    /// </summary>
    public class BasePanel : MonoBehaviour
    {
        public UILayer parentLayer;
        protected bool IsHomePanel { get; set; }
        //Awake
        public virtual void InitPanel(object o)
        {

        }

        //Destroy
        public virtual void ReleasePanel()
        {

        }

        public void OpenPanel(object o, UILayer layer)
        {
            parentLayer = layer;
            Transform tran = GameObject.Find(layer.ToString()).transform;

            transform.SetParent(tran, false);
            gameObject.SetActive(true);
        }

        public bool ClosePanel(bool destroy = true)
        {
            if (gameObject == null)
            {
                return false;
            }
            if (destroy)
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
            if (IsHomePanel)
            {
                HotUpdateScripts.Project.BasePrj.Ctrl.CtrlBPlanet.instance.gameObject.SetActive(true);
            }
            return true;
        }

    }
}
