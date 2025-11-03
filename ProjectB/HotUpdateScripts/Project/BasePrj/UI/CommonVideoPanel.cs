using HotUpdateScripts.Project.Common;
using UnityEngine;
using UnityEngine.UI;
using JEngine.Core;
using DG.Tweening;
using HotUpdateScripts.Project.BasePrj.Data;

namespace My.UI.Panel
{
    public class CommonVideoPanel : BasePanel
    {

        private CommonVideoController videoCtrl;

        private Button btn_PlayPause; Image img_PlayPause;
        private Text txt_PlayPause;

        private Slider sld_Position;
        private Slider sld_Preview;

        private Text txt_CurTime;
        private Text txt_TotalTime;

        Button btn_Close;

        //Video渲染到UI上
        RenderTexture videoRT;
        RawImage rImg_DideoPlayer;

        Image img_JiaZai;

        private bool isMute = false;//判断进入视频界面时，是否进行静音


        void Awake()
        {
            videoCtrl = gameObject.AddComponent<CommonVideoController>();

            btn_PlayPause = GameObject.Find("btn_PlayPause").GetComponent<Button>();
            img_PlayPause = btn_PlayPause.GetComponent<Image>();
            txt_PlayPause = GameObject.Find("txt_PlayPause").GetComponent<Text>();

            sld_Position = GameObject.Find("sld_Position").GetComponent<Slider>();
            sld_Preview = GameObject.Find("sld_Preview").GetComponent<Slider>();

            txt_CurTime = GameObject.Find("txt_CurTime").GetComponent<Text>();
            txt_TotalTime = GameObject.Find("txt_TotalTime").GetComponent<Text>();

            btn_Close = transform.Find("btn_Close").GetComponent<Button>();
            btn_Close.onClick.AddListener(
                () =>
                {
                    RenderTexture.ReleaseTemporary(videoRT);
                    UIMgr.Instance.Close(UIPath.CommonVideoPanel);
                }
                );


            rImg_DideoPlayer = GetComponentInChildren<RawImage>();

            videoRT = RenderTexture.GetTemporary(1080, 1920);
            videoCtrl.TargetTexture = videoRT;

            rImg_DideoPlayer.texture = videoRT;

            img_JiaZai = transform.Find("img_JiaZai").GetComponent<Image>();

        }




        public override void InitPanel(object o)
        {
            //if (PlayerPrefs.GetInt(GameData.SystemAudioStatus) == 1)
            //{
            //    isMute = true;
            //    AudioMgr.switch_music(AudioConfig.ProjectB_BGM, GameData.SystemAudioStatus);
            //}
            if (o.ToString() == null)
            {
                //debug.Log_Red("视频链接为 null");
                return;
            }

            videoCtrl.Url = o.ToString();

            videoCtrl.PrepareForUrl(videoCtrl.Url);
        }


        void Start()
        {
            btn_PlayPause.onClick.AddListener(ToggleIsPlaying);
            sld_Position.onValueChanged.AddListener(SliderValueChanged);



            videoCtrl.OnPrepared.AddListener(() =>
            {
                videoCtrl.Play();
            });

            videoCtrl.OnStartedPlaying.AddListener(() =>
            {
                img_JiaZai.gameObject.SetActive(false);


                SetBtnPlayPause("video_pause");
            });

            videoCtrl.OnFinishedPlaying.AddListener(() =>
            {
                sld_Position.value = 0;

                SetBtnPlayPause("video_play");
            });
        }

        private void OnDestroy()
        {
            //if (isMute)
            //{
            //    AudioMgr.switch_music(AudioConfig.ProjectB_BGM, GameData.SystemAudioStatus);
            //}
            btn_PlayPause.onClick.RemoveListener(ToggleIsPlaying);
            sld_Position.onValueChanged.RemoveListener(SliderValueChanged);


            videoCtrl.OnPrepared.RemoveListener(() =>
            {
                videoCtrl.Play();
            });

            videoCtrl.OnStartedPlaying.RemoveListener(() =>
            {
                img_JiaZai.gameObject.SetActive(false);


                SetBtnPlayPause("video_pause");
            });

            videoCtrl.OnFinishedPlaying.RemoveListener(() =>
            {
                sld_Position.value = 0;

                SetBtnPlayPause("video_play");
            });
        }


        void Update()
        {

            //加载中
            if (img_JiaZai != null && img_JiaZai.IsActive())
            {
                img_JiaZai.transform.Rotate(new Vector3(0, 0, -10), 3f);
            }


            if (!videoCtrl.IsPrepared)
                return;

            sld_Preview.value = videoCtrl.NormalizedTime;
            txt_CurTime.text = ConnvTimeFromSToHMS(videoCtrl.Time);

            if (txt_TotalTime.ToString() != ConnvTimeFromSToHMS(videoCtrl.Length))
                txt_TotalTime.text = ConnvTimeFromSToHMS(videoCtrl.Length);
        }


        private void ToggleIsPlaying()
        {

            if (videoCtrl.IsPlaying)
            {
                videoCtrl.Pause();
                txt_PlayPause.text = "Play";

                SetBtnPlayPause("video_play");
            }
            else
            {
                videoCtrl.Play();
                txt_PlayPause.text = "Pause";

                SetBtnPlayPause("video_pause");
            }
        }

        private void SliderValueChanged(float value)
        {
            videoCtrl.Seek(value);
        }




        /// <summary>
        /// 秒 -> 时分秒
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        string ConnvTimeFromSToHMS(float time)
        {
            float h = Mathf.FloorToInt(time / 3600f);
            float m = Mathf.FloorToInt(time / 60f - h * 60f);
            float s = Mathf.FloorToInt(time - m * 60f - h * 3600f);

            if (h.ToString("00") == "00")
                return m.ToString("00") + ":" + s.ToString("00");

            return h.ToString("00") + ":" + m.ToString("00") + ":" + s.ToString("00");
        }


        void SetBtnPlayPause(string btnName)
        {
            JResource.LoadResAsync<Sprite>("Common/Video/" + btnName + ".png",
                (sp) =>
                {
                    img_PlayPause.sprite = sp;
                    img_PlayPause.SetNativeSize();
                },
                JResource.MatchMode.UI);
        }

    }
}