using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using HotUpdateScripts.Project.GameB.Data;
using JEngine.Core;
using My.Msg;
using My.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.Project.GameB
{
    /// <summary>
    /// 砖块
    /// </summary>
    public class Block : MonoBehaviour
    {
        public Color color = Color.white;
        private float minBrightness = 0, maxBrightness = 0.5f;
        private float autoStart;
        private float _h, _s, _v;  // 色调，饱和度，亮度
        private float _deltaBrightness; // 最低最高亮度差
        private Renderer _renderer;
        private Material _material;
        private readonly string _keyword = "_EMISSION";
        private readonly string _colorName = "_EmissionColor";
        private float rate = 1;
        private Coroutine _glinting;

        public float rotateTime = 2;
        public bool startLeft;
        public bool stopMove;
        private float initialYRot;

        public float eulerAnglesY;

        private void Awake()
        {
            _material = GetComponentInChildren<MeshRenderer>().material;
        }


        public void StartMove()
        {
            if (startLeft)
            {
                transform.Rotate(0, 75, 0);
            }
            else
            {
                transform.Rotate(0, -75, 0);
            }
            initialYRot = transform.localRotation.eulerAngles.y;

            StartCoroutine(RotateIn());
        }


        float time = 0;

        void Update()
        {
            if (gameObject.activeSelf && GameBManager.Instance.IsGaming())
            {
                time += Time.deltaTime;
                if (time > 15)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        //完美跳跃亮光效果
        public void SetBright()
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(Flash());
            }
        }
        IEnumerator Flash()
        {
            Material mt = GetComponentInChildren<MeshRenderer>().material;
            byte colorData = 80;
            mt.SetColor("_ColorGlitter", new Color32(colorData, colorData, colorData, 255));
            yield return new WaitForSeconds(0.05f);
            while (colorData > 0)
            {
                colorData -= 5;
                mt.SetColor("_ColorGlitter", new Color32(colorData, colorData, colorData, 255));
                yield return 0;
            }
            mt.SetColor("_ColorGlitter", Color.black);
        }

        private WaitForEndOfFrame wait = new WaitForEndOfFrame();
        //旋转进入
        private IEnumerator RotateIn()
        {
            float elapsedTime = 0;
            while (elapsedTime < rotateTime && !stopMove && GameBManager.Instance.IsGaming())
            {
                elapsedTime += Time.deltaTime;
                Vector3 eulerAngles = transform.localRotation.eulerAngles;
                eulerAngles.y = Mathf.LerpAngle(initialYRot, 0, elapsedTime / rotateTime);
                eulerAnglesY = startLeft ? 360 - eulerAngles.y : eulerAngles.y;
                transform.localRotation = Quaternion.Euler(eulerAngles);
                yield return wait;
            }
        }

    }
}
