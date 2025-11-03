using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HotUpdateScripts.Project.BasePrj.Ctrl
{
    public class CtrlBAI_QE : MonoBehaviour
    {

        enum StateQE
        {
            daiji = 0,
            zou,
            pao,
            fei//3
        }

        StateQE state_QE;
        Animator anit_QE;

        Transform trans_QE;
        Transform trans_Path;
        List<Transform> list_Point = new List<Transform>();

        int index_CurPoint;
        float sped = 5;


        void Awake()
        {
            trans_QE = transform.Find("QE");
            trans_Path = transform.Find("Path");
            anit_QE = trans_QE.GetComponent<Animator>();

            for (int i = 0; i < trans_Path.transform.childCount; i++)
            {
                list_Point.Add(trans_Path.GetChild(i));
            }
        }


        void Update()
        {
            Transform tar = list_Point[index_CurPoint];
            float dis = Vector3.Distance(trans_QE.localPosition, tar.localPosition);
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
            if (state_QE == StateQE.daiji)
            {
                anit_QE.Play("daiji");

                //!!! 等待改变状态
                DJTimeRandom();
            }
            else
            {
                if (state_QE == StateQE.zou)
                {
                    anit_QE.Play("zou");
                }
                else if (state_QE == StateQE.pao)
                {
                    anit_QE.Play("pao");
                }
                else if (state_QE == StateQE.fei)
                {
                    anit_QE.Play("fei");
                }
                PosAndRot(tar);
            }

        }

        void PosAndRot(Transform tar)
        {
            //移动
            trans_QE.localPosition = Vector3.MoveTowards(trans_QE.localPosition, tar.localPosition, Time.deltaTime * sped);

            //旋转
            trans_QE.localRotation = Quaternion.Slerp(
                Quaternion.Euler(trans_QE.localEulerAngles),
                Quaternion.Euler(new Vector3(-180, tar.localEulerAngles.y, 0)),
                0.8f);
            /*if (trans_QE.localEulerAngles.y != tar.localEulerAngles.y)
            {
                trans_QE.localEulerAngles = new Vector3(-180, tar.localEulerAngles.y, 0);
            }*/
        }


        //状态随机
        void QEStateRandom()
        {
            int n = Random.RandomRange(0, 4);//前包后不包
            if (n == 0)
                state_QE = StateQE.daiji;
            if (n == 1)
                state_QE = StateQE.zou;
            if (n == 2)
                state_QE = StateQE.pao;
            if (n == 3)
                state_QE = StateQE.fei;
        }


        //待机时间随机
        int djNum = 0;
        int djTarNum = 500;
        void DJTimeRandom()
        {
            djNum++;
            if (djNum > djTarNum)
            {
                state_QE = StateQE.zou;
                djNum = 0;

                djTarNum = UnityEngine.Random.RandomRange(300, 500);
            }
        }


    }
}