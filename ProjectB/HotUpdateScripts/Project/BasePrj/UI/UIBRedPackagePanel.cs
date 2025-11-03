using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.BasePrj.Data;

namespace My.UI.Panel
{
    /// <summary>
    /// 红包
    /// </summary>
    public class UIBRedPackagePanel : BasePanel
    {
        private Transform ShowPage;
        private Transform OpenPage;
        private Transform GoldenLight;

        private GameObject RedPackage1;
        private GameObject RedPackage2;
        private GameObject RedPackage3;

        private Text txt_Money;

        private Button btn_Open, btn_Close;

        private Animator ani_Open;

        private string audioName;

        void Awake()
        {
            ShowPage = transform.Find("ShowRedPackage");
            OpenPage = transform.Find("OpenRedPackage");

            OpenPage = transform.Find("OpenRedPackage");
            ShowPage.localScale = Vector3.zero;
            OpenPage.localScale = Vector3.zero;

            GoldenLight = transform.Find("ShowRedPackage/GoldenLight");
            RedPackage1 = transform.Find("ShowRedPackage/RedPackage1").gameObject;
            RedPackage2 = transform.Find("ShowRedPackage/RedPackage2").gameObject;
            RedPackage3 = transform.Find("ShowRedPackage/RedPackage3").gameObject;

            DOTweenMgr.Instance.DoScale(ShowPage.gameObject, Vector3.zero, Vector3.one, .3f, () =>
            {
 
            });
            DOTweenMgr.Instance.MovePos(RedPackage1, Vector3.zero, new Vector3(-420, 236), 0.5f);
            DOTweenMgr.Instance.MovePos(RedPackage2, Vector3.zero, new Vector3(403, 254), 0.5f);
            DOTweenMgr.Instance.MovePos(RedPackage3, Vector3.zero, new Vector3(387, 454), 0.5f);

            DOTweenMgr.Instance.DoScale(RedPackage1, Vector3.one * 0.3f, Vector3.one, 0.5f);
            DOTweenMgr.Instance.DoScale(RedPackage2, Vector3.one * 0.3f, Vector3.one, 0.5f);
            DOTweenMgr.Instance.DoScale(RedPackage3, Vector3.one * 0.3f, Vector3.one, 0.5f);

            ani_Open = transform.Find("ShowRedPackage/GoldCoin").GetComponent<Animator>();
            ani_Open.enabled = false;
            btn_Open = transform.Find("ShowRedPackage/OpenButton").GetComponent<Button>();
            btn_Close = transform.Find("OpenRedPackage/CloseBtn").GetComponent<Button>();
            txt_Money = transform.Find("OpenRedPackage/MoneyNumText").GetComponent<Text>();

            btn_Open.onClick.AddListener(() =>
            {
                StartCoroutine(DelayOpenPackage());
            });

            btn_Close.onClick.AddListener(() =>
            {
                UIMgr.Instance.Close(UIPath.UIBRedPackagePanel);
                UIMgr.Instance.OnCloseRedPackagePanel?.Invoke();
                UIMgr.Instance.OnCloseRedPackagePanel = null;
            });

            txt_Money.text = DataMgr.Instance.DataRedPackage.redQuota.ToString();
            audioName = DataMgr.Instance.DataRedPackage.redQuota.ToString();

        }

        void Update()
        {
            if (ShowPage.localScale == Vector3.one)
            {
                GoldenLight.rotation *= Quaternion.Euler(0, 0, 1);
            }
        }

        private IEnumerator DelayOpenPackage()
        {
            btn_Open.gameObject.SetActive(false);
            ani_Open.enabled = true;
            yield return new WaitForSeconds(1);
            StartCoroutine(DelayPlayAudio());
            ShowPage.localScale = Vector3.zero;
            DOTweenMgr.Instance.DoScale(OpenPage.gameObject, Vector3.zero, Vector3.one, .15f, null);
        }

        private AudioSource audioSource;
        private IEnumerator DelayPlayAudio()
        {
            audioSource = AudioMgr.play_effect("RedPackage/openRedpackage", GameData.GameAudioEffectStatus, ".mp3");
            yield return new WaitUntil(() => !audioSource.isPlaying);
            audioSource = AudioMgr.play_effect("RedPackage/account", GameData.GameAudioEffectStatus, ".mp3");
            yield return new WaitUntil(() => !audioSource.isPlaying);
            audioSource = AudioMgr.play_effect("RedPackage/" + audioName, GameData.GameAudioEffectStatus, ".mp3");
            yield return new WaitUntil(() => !audioSource.isPlaying);
            audioSource = AudioMgr.play_effect("RedPackage/comejuaixingqiu", GameData.GameAudioEffectStatus, ".mp3");
        }


    }
}
