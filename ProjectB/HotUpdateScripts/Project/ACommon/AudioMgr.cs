using HotUpdateScripts.Project.BasePrj.Data;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.Project.Common
{
    public class AudioMgr
    {
        static GameObject sound_play_object;

        static Dictionary<string, AudioSource> musics = null;
        static Dictionary<string, Dictionary<string, AudioSource>> key_effects = null;//每类游戏管理自己的音效   key   url   Audio

        static string endPath = ".mp3";

        public static void init()
        {
            if (!GameObject.Find("Audio_Player"))
            {
                sound_play_object = new GameObject("Audio_Player");
                sound_play_object.AddComponent<sound_scan>();
                GameObject.DontDestroyOnLoad(sound_play_object);
            }

            //第一次初始化   默认音乐为 非静音 状态
            if (!PlayerPrefs.HasKey(GameData.SystemAudioStatus))
            {
                PlayerPrefs.SetInt(GameData.SystemAudioStatus, 1);
            }
            if (!PlayerPrefs.HasKey(GameData.GameAudioBgmStatus))
            {
                PlayerPrefs.SetInt(GameData.GameAudioBgmStatus, 1);
            }
            if (!PlayerPrefs.HasKey(GameData.GameAudioEffectStatus))
            {
                PlayerPrefs.SetInt(GameData.GameAudioEffectStatus, 1);
            }

            musics = new Dictionary<string, AudioSource>();
            key_effects = new Dictionary<string, Dictionary<string, AudioSource>>();

            //初始化播放背景音乐
            AudioMgr.PlayMusic(AudioConfig.ProjectB_BGM, GameData.SystemAudioStatus);
        }

        #region Method
        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="url"></param>
        /// <param name="is_loop"></param>
        /// <param name="key"></param>
        public static void PlayMusic(string url, string key = "", bool is_loop = true)
        {
            return;
            url = AudioConfig.AuSource + url;
            AudioSource audio_source = null;
            if (musics.ContainsKey(url))
            {
                audio_source = musics[url];
            }
            else
            {
                GameObject s = new GameObject(url);
                s.transform.parent = sound_play_object.transform;
                audio_source = s.AddComponent<AudioSource>();
                AudioClip clip = JResource.LoadRes<AudioClip>(url + endPath, JResource.MatchMode.Other);
                audio_source.clip = clip;
                audio_source.loop = is_loop;
                audio_source.playOnAwake = true;
                audio_source.spatialBlend = 0.0f;
                musics.Add(url, audio_source);
            }
            if (string.IsNullOrEmpty(key))
            {
                audio_source.mute = false;
            }
            else
            {
                audio_source.mute = (PlayerPrefs.GetInt(key) == 0);
            }
            audio_source.enabled = true;
            audio_source.Play();
        }

        /// <summary>
        /// 停止播放音乐
        /// </summary>
        /// <param name="url"></param>
        public static void stop_music(string url)
        {
            url = AudioConfig.AuSource + url;
            AudioSource audio_source = null;
            if (!musics.ContainsKey(url))
            {
                return;
            }
            audio_source = musics[url];
            audio_source.Stop();
        }

        /// <summary>
        /// 停止播放所有背景音乐
        /// </summary>
        public static void stop_all_music()
        {
            foreach (AudioSource s in musics.Values)
            {
                s.Stop();
            }
        }

        /// <summary>
        /// 删除指定背景音乐和它的节点
        /// </summary>
        /// <param name="url"></param>
        public static void clear_music(string url)
        {
            url = AudioConfig.AuSource + url;
            AudioSource audio_source = null;
            if (!musics.ContainsKey(url))
            {
                return;
            }
            audio_source = musics[url];
            musics[url] = null;
            GameObject.Destroy(audio_source.gameObject);
        }

        /// <summary>
        /// 指定音乐切换是否静音
        /// </summary>
        /// <returns></returns>
        public static void switch_music(string url, string key)
        {
            url = AudioConfig.AuSource + url;
            bool is_music_mute = (PlayerPrefs.GetInt(key) == 0);
            int value = (is_music_mute) ? 1 : 0;
            PlayerPrefs.SetInt(key, value);
            if (musics.ContainsKey(url))
            {
                musics[url].mute = !is_music_mute;
            }
        }

        /// <summary>
        /// 播放指定音效
        /// </summary>
        /// <param name="url"></param>
        /// <param name="is_loop"></param>
        /// <param name="pitch">音阶高低</param>
        public static AudioSource play_effect(string url, string key, string endPath = ".mp3", bool is_loop = false, float pitch = 1)
        {
            if (GetIsMuteByKey(key)) return null;

            url = AudioConfig.AuSource + url;
            AudioSource audio_source = null;

            if (key_effects.ContainsKey(key))
            {
                if (key_effects[key].ContainsKey(url))
                {
                    audio_source = key_effects[key][url];
                }
                else
                {
                    GameObject s = new GameObject(url);
                    s.transform.parent = sound_play_object.transform;
                    audio_source = s.AddComponent<AudioSource>();
                    AudioClip clip = JResource.LoadRes<AudioClip>(url + endPath, JResource.MatchMode.Other);
                    audio_source.clip = clip;
                    audio_source.loop = is_loop;
                    audio_source.playOnAwake = true;
                    audio_source.spatialBlend = 0.0f;
                    key_effects[key].Add(url, audio_source);
                }
            }
            else
            {
                GameObject s = new GameObject(url);
                s.transform.parent = sound_play_object.transform;
                audio_source = s.AddComponent<AudioSource>();
                AudioClip clip = JResource.LoadRes<AudioClip>(url + endPath, JResource.MatchMode.Other);
                audio_source.clip = clip;
                audio_source.loop = is_loop;
                audio_source.playOnAwake = true;
                audio_source.spatialBlend = 0.0f;
                Dictionary<string, AudioSource> effects = new Dictionary<string, AudioSource>();
                key_effects.Add(key, effects);
                key_effects[key].Add(url, audio_source);
            }
            //if (!string.IsNullOrEmpty(key))
            //{
            //    audio_source.mute = PlayerPrefs.GetInt(key) == 0;
            //}
            //else
            //{
            //    audio_source.mute = false;
            //}
            audio_source.pitch = pitch;
            audio_source.enabled = true;
            audio_source.Play();
            return audio_source;
        }

        /// <summary>
        /// 停止播放指定类音效
        /// </summary>
        public static void stop_all_effect(string key)
        {
            foreach (AudioSource s in key_effects[key].Values)
            {
                s.Stop();
            }
        }

        /// <summary>
        /// 切换 指定类音效静音
        /// </summary>
        public static void switch_effect(string key)
        {
            int value = (PlayerPrefs.GetInt(key) == 0) ? 1 : 0;
            PlayerPrefs.SetInt(key, value);
            //if (!key_effects.ContainsKey(key)) return;
            //foreach (AudioSource s in key_effects[key].Values)
            //{
            //    debug.Log_Blue("xxx");
            //    s.mute = value == 0;
            //}
        }

        public static bool GetIsMuteByKey(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                return PlayerPrefs.GetInt(key) == 0;
            }
            else
            {
                PlayerPrefs.SetInt(key, 1);
                return false;
            }
        }

        /// <summary>
        /// 优化策略接口
        /// </summary>
        public static void disable_over_audio()
        {
            //遍历背景音乐表
            foreach (AudioSource s in musics.Values)
            {
                if (!s.isPlaying)
                {
                    s.enabled = false;
                }
            }
            //遍历音效表
            foreach (Dictionary<string, AudioSource> item in key_effects.Values)
            {
                foreach (AudioSource s in item.Values)
                {
                    if (!s.isPlaying)
                    {
                        s.enabled = false;
                    }
                }
            }
        }
        #endregion
    }
}
