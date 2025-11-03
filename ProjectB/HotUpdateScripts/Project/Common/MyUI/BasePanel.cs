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

        public virtual void InitPanel(object o)
        {
        }

        public virtual void ReleasePanel()
        {

        }

        public void OpenPanel(object o, UILayer layer)
        {
            parentLayer = layer;
            Transform tran = GameObject.Find(layer.ToString()).transform;

            transform.SetParent(tran ,false);
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

            return true;
        }
                
    }
}
