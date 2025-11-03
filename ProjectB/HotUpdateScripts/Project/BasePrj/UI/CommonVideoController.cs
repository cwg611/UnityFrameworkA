using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace My.UI.Panel
{
    public class CommonVideoController : MonoBehaviour
    {

        [Header("Optional")]

        [SerializeField]
        private VideoPlayer videoPlayer;

        [SerializeField]
        private AudioSource audioSource;

        [Header("Events")]

        [SerializeField]
        private UnityEvent onPrepared = new UnityEvent();

        [SerializeField]
        private UnityEvent onStartedPlaying = new UnityEvent();

        [SerializeField]
        private UnityEvent onFinishedPlaying = new UnityEvent();

        #region Properties


        public UnityEvent OnPrepared
        {
            get { return onPrepared; }
        }

        public UnityEvent OnStartedPlaying
        {
            get { return onStartedPlaying; }
        }

        public UnityEvent OnFinishedPlaying
        {
            get { return onFinishedPlaying; }
        }

        public ulong Time
        {
            get { return (ulong)videoPlayer.time; }
        }

        public bool IsPlaying
        {
            get { return videoPlayer.isPlaying; }
        }

        public bool IsPrepared
        {
            get { return videoPlayer.isPrepared; }
        }

        public ulong Length
        {
            get { return (ulong)videoPlayer.length; }
        }

        public float NormalizedTime
        {
            get { return (float)(videoPlayer.time / Duration); }
        }

        public ulong Duration
        {
            get { return videoPlayer.frameCount / (ulong)videoPlayer.frameRate; }
        }


        public string Url
        {
            get { return videoPlayer.url; }
            set { videoPlayer.url = value; }
        }

        public RenderTexture TargetTexture
        {
            get { return videoPlayer.targetTexture; }
            set { videoPlayer.targetTexture = value; }
        }

        public float Volume
        {
            get { return audioSource == null ? videoPlayer.GetDirectAudioVolume(0) : audioSource.volume; }
            set
            {
                if (audioSource == null)
                    videoPlayer.SetDirectAudioVolume(0, value);
                else
                    audioSource.volume = value;
            }
        }
        #endregion

        #region Unity Methods

        void Awake()
        {
            videoPlayer = transform.Find("Video Player").GetComponent<VideoPlayer>();
            audioSource = transform.Find("Audio Source").GetComponent<AudioSource>();
        }

        void Start()
        {
            if (videoPlayer == null)
            {
                SubscribeToVideoPlayerEvents();
            }

            //videoPlayer.playOnAwake = false;
        }


        private void OnEnable()
        {
            SubscribeToVideoPlayerEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromVideoPlayerEvents();
        }

        private void OnDestroy()
        {
            UnsubscribeFromVideoPlayerEvents();
        }
        #endregion

        #region Public Methods

        public void PrepareForUrl(string url)
        {
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = url;
            videoPlayer.Prepare();
        }

        public void PrepareForClip(VideoClip clip)
        {
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = clip;
            videoPlayer.Prepare();
        }

        public void Play()
        {
            if (!IsPrepared)
            {
                videoPlayer.Prepare();
                return;
            }

            videoPlayer.Play();
        }

        public void Pause()
        {
            videoPlayer.Pause();
        }


        public void Seek(float time)
        {
            time = Mathf.Clamp(time, 0, 1);
            videoPlayer.time = time * Duration;
        }
        #endregion

        #region Private Methods

        private void OnPrepareCompleted(VideoPlayer source)
        {
            onPrepared.Invoke();
            SetupAudio();

            //Play();
        }

        private void OnStarted(VideoPlayer source)
        {
            onStartedPlaying.Invoke();
        }

        private void OnFinished(VideoPlayer source)
        {
            onFinishedPlaying.Invoke();
        }

        private void OnError(VideoPlayer source, string message)
        {
            Debug.LogError("OnError " + message);
        }

        private void SetupAudio()
        {
            if (videoPlayer.audioTrackCount <= 0)
                return;

            if (audioSource == null && videoPlayer.canSetDirectAudioVolume)
            {
                videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
            }
            else
            {
                videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
                videoPlayer.SetTargetAudioSource(0, audioSource);
            }
            videoPlayer.controlledAudioTrackCount = 1;
            videoPlayer.EnableAudioTrack(0, true);
        }

        /// <summary>
        /// 订阅视频播放器事件
        /// </summary>
        private void SubscribeToVideoPlayerEvents()
        {
            if (videoPlayer == null)
                return;

            videoPlayer.errorReceived += OnError;
            videoPlayer.prepareCompleted += OnPrepareCompleted;
            videoPlayer.started += OnStarted;
            videoPlayer.loopPointReached += OnFinished;
        }

        /// <summary>
        /// 取消订阅视频播放器事件
        /// </summary>
        private void UnsubscribeFromVideoPlayerEvents()
        {
            if (videoPlayer == null)
                return;

            videoPlayer.errorReceived -= OnError;
            videoPlayer.prepareCompleted -= OnPrepareCompleted;
            videoPlayer.started -= OnStarted;
            videoPlayer.loopPointReached -= OnFinished;
        }
        #endregion

    }
}