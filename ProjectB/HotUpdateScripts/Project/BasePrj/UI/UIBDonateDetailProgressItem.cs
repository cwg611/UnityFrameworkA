using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace My.UI.Panel
{
    public class UIBDonateDetailProgressItem : MonoBehaviour
    {
        private Text Title, Timer, Desc;
        private Transform ImgGroup;
        private GameObject ImgPrefab;
        private bool isFirst = true;
        private Coroutine coroutine_setView;


        void setObj()
        {
            Title = GameTools.GetByName(transform, "Title").GetComponent<Text>();
            Timer = GameTools.GetByName(transform, "Timer").GetComponent<Text>();
            Desc = GameTools.GetByName(transform, "Desc").GetComponent<Text>();
            ImgGroup = GameTools.GetByName(transform, "ImgGroup").transform;
            ImgPrefab = GameTools.GetByName(transform, "ImgPrefab");
            ImgPrefab.gameObject.SetActive(false);
        }

        public void InitItem(ProjectProgress data)
        {
            if (isFirst)
            {
                setObj();
                isFirst = false;
            }
            Title.text = data.projectProgressTitle;
            Timer.text = data.projectProgressTime;
            Desc.text = data.projectProgressWord;
            string[] s = data.projectProgressImg.Split(new char[] { '^' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (coroutine_setView != null) StopCoroutine(coroutine_setView);
            coroutine_setView = StartCoroutine(setView(s));
        }

        IEnumerator setView(string[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                GameObject Item = Instantiate(ImgPrefab.gameObject, ImgGroup);
                Item.SetActive(true);
                Item.transform.GetChild(0).gameObject.AddComponent<UIBSetImg>().InitItem(s[i], Match_Img.Height);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetComponent<RectTransform>());
            yield return 1;
        }

        void OnDestroy()
        {
            coroutine_setView = null;
        }
    }
}

