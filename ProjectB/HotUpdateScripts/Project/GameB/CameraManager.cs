using System;
using System.Collections;
using UnityEngine;

namespace HotUpdateScripts.Project.GameB
{
    /// <summary>
    /// 相机控制
    /// </summary>
    public class CameraManager : MonoBehaviour
    {
        private Vector3 InitialPos = new Vector3(0.74f, 1.367f, -6.7f);
        private Vector3 GameStartPos = new Vector3(0f, 3.5f, -14f);

        private Vector3 GameOverPos1 = new Vector3(0, 2f, -7);//拉近位置
        public Vector3 GameOverPos2 = new Vector3(0, 8, -20);

        private Vector3 RestartPos = new Vector3(1.1f, 1, -6.7f);

        private float moveTime = 0.5f;
        private float CharacterYPos;
        private float CameraPosY;
        private float CameraOffset;
        Coroutine coroMove;

        public bool StartGame = false;
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        public void Init()
        {
            transform.localPosition = InitialPos;
            CameraPosY = transform.position.y;
            CharacterYPos = 0;
            CameraOffset = CharacterYPos - CameraPosY;
        }

        //111开始游戏
        public void OnStartGame()
        {
            StartCoroutine(MoveToTargetPosition(transform.localPosition, GameStartPos, 1));
        }

        private IEnumerator MoveToTargetPosition(Vector3 startPos, Vector3 targetPos, float moveTime, Action callBack = null)
        {
            float elapsedTime = 0;
            while (elapsedTime < moveTime)
            {
                elapsedTime += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / moveTime);
                yield return wait;
            }
            callBack?.Invoke();
        }

        //222跳跃之后移动
        public void BeginMove()
        {
            if (coroMove != null)
            {
                StopCoroutine(coroMove);
            }
            coroMove = StartCoroutine(MoveUp());
        }

        private IEnumerator MoveUp()
        {
            float elapsedTime = 0;
            CharacterYPos = GameBManager.Instance.GetCharacter().transform.position.y;
            CameraPosY = transform.position.y;
            if (CameraPosY < CharacterYPos - CameraOffset)
            {
                while (elapsedTime < moveTime)
                {
                    elapsedTime += Time.deltaTime;
                    Vector3 newPos = transform.position;
                    newPos.y = Mathf.Lerp(CameraPosY, CharacterYPos - CameraOffset, elapsedTime / moveTime);
                    transform.position = newPos;

                    yield return wait;
                }
            }
            coroMove = null;
        }

        //GameOver镜头移动
        public void OnGameOver(Action callBack)
        {
            if (coroMove != null)
            {
                StopCoroutine(coroMove);
            }
            if (GameBManager.Instance.GetStackCount() < 5)
            {
                StartCoroutine(MoveToTargetPosition(transform.localPosition, GameOverPos1, 2, () =>
                {
                    StartCoroutine(DelaySeconds(1, callBack));
                }));
            }
            else
            {
                StartCoroutine(MoveToTargetPosition(transform.localPosition, GameOverPos2, 3f, () =>
                {
                    StartCoroutine(DelaySeconds(1, callBack));
                }));
            }
        }

        public void Restart(Action callBack)
        {
            StartCoroutine(MoveToTargetPosition(RestartPos, InitialPos, 0.5f, callBack));
        }

        private IEnumerator DelaySeconds(int delaySeconts, Action callBack)
        {
            yield return new WaitForSeconds(delaySeconts);
            callBack?.Invoke();
        }

    }

}
