using System;
using UnityEngine;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.BasePrj.Data;

namespace HotUpdateScripts.Project_Daxigua
{
    public class Fruit : MonoBehaviour
    {
        public int id;
        public int score;
        public GameObject nextLevelPrefab1;
        public GameObject nextLevelPrefab2;
        public Action<Fruit, Fruit> OnLevelUp;
        public Action OnGameOver;
        private Rigidbody2D rigid;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        /// <summary>
        /// 是否碰到红线
        /// </summary>
        private bool isTouchRedline;
        /// <summary>
        /// 和红线接触的时间
        /// </summary>
        private float timer;

        public bool isDrag = false;

        public bool isGroundPlay = false;


        public void setIsDrag(bool drag)
        {
            isDrag = drag;
        }

        public bool getIsDrag()
        {
            return isDrag;
        }

        public void closeAnimation()
        {
            if (animator == null) return;
            animator.enabled = false;
            transform.localScale = new Vector3(1f, 1f, 1f);
            spriteRenderer.material.shader = Shader.Find("Sprites/Default");
        }
        void Awake()
        {
            rigid = gameObject.GetComponent<Rigidbody2D>();
            animator = transform.GetComponent<Animator>();
            spriteRenderer = transform.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (isTouchRedline == false)
            {
                return;
            }
            timer += Time.deltaTime;
            if (timer > 3)
            {
                OnGameOver?.Invoke();
            }
        }
        public void SetSimulated(bool b)
        {
            if (rigid == null)
            {
                return;
            }
            rigid.simulated = b;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var obj = collision.gameObject;
            var fruit = obj.GetComponent<Fruit>();
            if (obj.name.StartsWith("Fruit"))
            {
                if (obj.name == gameObject.name || (obj.name == "Fruit_4(Clone)" && gameObject.name == "Fruit_5(Clone)") || (obj.name == "Fruit_5(Clone)" && gameObject.name == "Fruit_4(Clone)"))
                {
                    if (nextLevelPrefab1.name != transform.name && nextLevelPrefab2.name != transform.name)
                    {
                        OnLevelUp?.Invoke(this, fruit);
                        AudioMgr.play_effect(AudioConfig.Au_combine,GameData.GameAudioEffectStatus);//播放音效
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var obj = collision.gameObject;
            if (obj.name.StartsWith("Redline"))
            {
                isTouchRedline = true;
            }
            else if (obj.name.StartsWith("Bottom") && !isGroundPlay)
            {
                AudioMgr.play_effect(AudioConfig.Au_drop, GameData.GameAudioEffectStatus);//播放音效
                isGroundPlay = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var obj = collision.gameObject;
            if (obj.name.StartsWith("Redline"))
            {
                isTouchRedline = false;
                timer = 0;
            }
        }

    }
}
