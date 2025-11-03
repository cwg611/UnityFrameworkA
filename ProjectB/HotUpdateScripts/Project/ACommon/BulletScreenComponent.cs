using DG.Tweening;
using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Game.GameA.Data;
using My.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace HotUpdateScripts.Project.Common
{
    public class Bullet : MonoBehaviour
    {
        private Text txt_Content;
        private Vector3 moveSpeed = new Vector2(200*Time.deltaTime, 0);

        private float ScreenWidth;
        private float TextWidth;
        private float MoveEndPoint;
        public void Init(float screenWidth)
        {
            txt_Content = GetComponentInChildren<Text>();
            ScreenWidth = screenWidth;
            gameObject.SetActive(false);
        }

        public void SetBullet(string content, Vector2 MoveStartPoint)
        {
            txt_Content.text = content;
            transform.localPosition = MoveStartPoint;
            TextWidth = txt_Content.preferredWidth + 60;
            MoveEndPoint = -ScreenWidth / 2 - TextWidth;
            gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(transform.rectTransform());
        }
        void Update()
        {
            if (!gameObject.activeSelf) return;

            transform.localPosition -= moveSpeed;
            if (transform.localPosition.x <= MoveEndPoint)
            {
                gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 弹幕组件
    /// </summary>
    public class BulletScreenComponent : MonoBehaviour
    {
        float delayTime = 3.5f;
        private GameObject bulletPrefab;
        float screenWidth;
        List<Bullet> bullets;

        Queue<string> contentStrs;

        private bool haveInit;

        public void InitData(List<string> strs, int poolSize,float delayTime=3.5f)
        {
            if (strs == null) return;
            this.delayTime = delayTime;
            contentStrs = new Queue<string>();
            for (int i = 0; i < strs.Count; i++)
            {
                contentStrs.Enqueue(strs[i]);
            }
            bulletPrefab = transform.GetChild(0).gameObject;
            screenWidth = transform.GetComponent<RectTransform>().rect.width;
            CreatPool(poolSize);

            CreatBullet();

        }

        public void CreatBullet()
        {
            for (int i = 0; i < 3; i++)
            {
                var Item = GetBullet();
                var content = contentStrs.Dequeue();
                Item.SetBullet(content, new Vector2(Random.Range(screenWidth / 2, screenWidth / 2 + 200), 80 * (i - 1)));
                contentStrs.Enqueue(content);
            }
            haveInit = true;
        }


        float time = 0;
        private void Update()
        {
            if (!haveInit) return;
            time += Time.deltaTime;
            if (time >= delayTime)
            {
                time = 0;
                CreatBullet();
            }
        }

        void OnDestroy()
        {
            if (bullets!=null)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    DestroyImmediate(bullets[i].gameObject);
                }
            }
        }

        private Bullet GetBullet()
        {
            if (bullets == null)
            {
                CreatPool(10);
            }
            if (bullets != null)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (!bullets[i].gameObject.activeSelf)
                    {
                        return bullets[i];
                    }
                }
            }
            Bullet bullet = Instantiate(bulletPrefab).AddComponent<Bullet>();
            bullet.Init(screenWidth);
            bullets.Add(bullet);
            return bullet;
        }

        private void CreatPool(int poolSize)
        {
            if (bulletPrefab != null)
            {
                bullets = new List<Bullet>();

                for (int i = 0; i < poolSize; i++)
                {
                    var bullet = Instantiate(bulletPrefab, this.transform).AddComponent<Bullet>();
                    bullet.Init(screenWidth);
                    bullets.Add(bullet);
                }
            }
        }

    }
}
