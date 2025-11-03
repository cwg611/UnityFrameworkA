using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.Project.BasePrj.Ctrl
{
    public class CtrlBAI_XM : MonoBehaviour
    {

        enum StateXM
        {
            daiji = 0,
            zou,
            pao,
            tiao,
            paiqiu//4
        }


        StateXM state_XM;
        Animator anit_XM;

        Transform trans_XM;
        Transform trans_Path;
        List<Transform> list_Point = new List<Transform>();

        int index_CurPoint;
        float sped = 5;


        void Awake()
        {
            trans_XM = transform.Find("XM");
            trans_Path = transform.Find("Path");
            anit_XM = trans_XM.GetComponent<Animator>();

            for (int i = 0; i < trans_Path.transform.childCount; i++)
            {
                list_Point.Add(trans_Path.GetChild(i));
            }
        }



        Transform tar;
        float dis;
        void Update()
        {
            if (list_Point.Count == 0) return;

            tar = list_Point[index_CurPoint];
            dis = Vector3.Distance(trans_XM.localPosition, tar.localPosition);
            if (dis < 0.1f)
            {
                if (index_CurPoint == list_Point.Count - 1)
                {
                    index_CurPoint = 0;
                }
                else
                {
                    index_CurPoint++;
                }

                //!!! 拐点改变状态
                QEStateRandom();
            }




            //动画状态机
            if (state_XM == StateXM.daiji)
            {
                anit_XM.Play("daiji");

                //!!! 等待改变状态
                DJTimeRandom();
            }

            else if (state_XM == StateXM.tiao)
            {
                anit_XM.Play("tiao");

                //!!! 等待改变状态
                DJTimeRandom();
            }

            else
            {
                if (state_XM == StateXM.zou)
                {
                    anit_XM.Play("zou");
                }
                else if (state_XM == StateXM.pao)
                {
                    anit_XM.Play("pao");
                }

                else if (state_XM == StateXM.paiqiu)
                {
                    anit_XM.Play("paiqiu");
                }

                PosAndRot(tar);
                
            }


        }



        void PosAndRot(Transform tar)
        {
            //移动
            trans_XM.localPosition = Vector3.MoveTowards(trans_XM.localPosition, tar.localPosition, Time.deltaTime * sped);

            //旋转
            trans_XM.localRotation = Quaternion.Slerp(
                Quaternion.Euler(trans_XM.localEulerAngles),
                Quaternion.Euler(new Vector3(tar.localEulerAngles.x, tar.localEulerAngles.y, tar.localEulerAngles.z)),
                0.8f);
        }


        //状态随机
        void QEStateRandom()
        {
            int n = UnityEngine.Random.RandomRange(0, 5);//前包后不包
            if (n == 0)
                state_XM = StateXM.daiji;
            if (n == 1)
                state_XM = StateXM.zou;
            if (n == 2)
                state_XM = StateXM.pao;
            if (n == 3)
                state_XM = StateXM.tiao;
            if (n == 4)
                state_XM = StateXM.paiqiu;
        }


        //待机时间随机
        int djNum = 0;
        int djTarNum = 100;
        void DJTimeRandom()
        {
            djNum++;
            if (djNum > djTarNum)
            {
                state_XM = StateXM.zou;
                djNum = 0;

                djTarNum = UnityEngine.Random.RandomRange(300, 500);
            }
        }





    }
}