using HotUpdateScripts.Project.BasePrj.Data;
using HotUpdateScripts.Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotUpdateScripts.Project.GameB
{
    /// <summary>
    /// 角色控制
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        private float jumpForce = 13;
        private Animator animator;
        public bool isOnGround = true;
        private Rigidbody rigidBody;
        List<Block> perfectBlockList;//完美跳跃记录
        private Transform trans_Eyes;
        private Vector3 eyePosition;
        private Vector3 eyeTargetPos;
        float eyeMoveSpeed = 0.5f;

        private ParticleSystem EffectGround;//地面特效
        private ParticleSystem EffectJumpStar;
        private GameObject EffectPerfaceJump;
        private MeshCollider meshCollider;

        private ParticleSystem.EmissionModule JumpStarEmission;
        private ParticleSystem.Burst JumpStarBurst;
        private ParticleSystem.MinMaxCurve ParticleCount = new ParticleSystem.MinMaxCurve();

        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            meshCollider = GetComponent<MeshCollider>();
            perfectBlockList = new List<Block>();

            trans_Eyes = transform.Find("Base_Armature/Base_Armature_2/Torso/Animal_Eyeball_Round");
            EffectGround = transform.Find("EffectGround").GetComponent<ParticleSystem>();
            EffectJumpStar = transform.Find("EffectJumpStar").GetComponent<ParticleSystem>();
            EffectPerfaceJump = transform.Find("EffectPerfaceJump").gameObject;

            EffectGround.transform.SetParent(transform.parent);
            EffectJumpStar.transform.SetParent(transform.parent);
            EffectGround.transform.position = transform.position;
            EffectJumpStar.transform.position = transform.position;

            JumpStarEmission = EffectJumpStar.emission;
            JumpStarBurst = EffectJumpStar.emission.GetBurst(0);

            eyeTargetPos = eyePosition = trans_Eyes.localPosition;
        }

        void Update()
        {
            if (GameBManager.Instance.IsGaming())
            {
                if (Input.GetMouseButtonDown(0) && isOnGround)
                {
                    isOnGround = false;
                    rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    if (GameBManager.Instance.nextDirection == 1)
                    {
                        AudioMgr.play_effect(AudioConfig.GameB_JumpLeft, GameData.FunJumpEffectStatus, ".wav");//播放音效
                    }
                    else if (GameBManager.Instance.nextDirection == 2)
                    {
                        AudioMgr.play_effect(AudioConfig.GameB_JumpRight, GameData.FunJumpEffectStatus, ".wav");//播放音效
                    }

                    if (GameBManager.Instance.IsRotatingJump)
                    {
                        animator.SetTrigger("PerfectJump");
                    }
                    else
                    {
                        animator.SetTrigger("CommonJump");
                    }

                    animator.speed = 2.5f;
                }
                //上升和下落过程中，手动改变速度
                if (rigidBody.velocity.y < 0)
                {
                    rigidBody.velocity -= 3.2f * Vector3.down * Physics2D.gravity.y * Time.deltaTime;
                }
                else if (rigidBody.velocity.y > 0)
                {
                    rigidBody.velocity += 3.6f * Vector3.up * Physics2D.gravity.y * Time.deltaTime;
                }
                //眼睛
                if (GameBManager.Instance.GetNextDirection() == 1)
                {
                    eyeTargetPos = new Vector3(-.06f, eyeTargetPos.y, eyeTargetPos.z);
                }
                else if (GameBManager.Instance.GetNextDirection() == 2)
                {
                    eyeTargetPos = new Vector3(.06f, eyeTargetPos.y, eyeTargetPos.z);
                }
                else
                {
                    eyeTargetPos = eyePosition;
                }
                trans_Eyes.localPosition = Vector3.MoveTowards(trans_Eyes.localPosition, eyeTargetPos, eyeMoveSpeed * Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            if (EffectGround != null)
            {
                Destroy(EffectGround.gameObject);
            }
            if (EffectJumpStar != null)
            {
                Destroy(EffectJumpStar.gameObject);
            }
        }

        public void SetReset()
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
            transform.localPosition = Vector3.zero;
            isOnGround = true;
            perfectBlockList.Clear();
            trans_Eyes.localPosition = eyePosition;
            rigidBody.constraints = ~RigidbodyConstraints.FreezePositionY;
            EffectPerfaceJump.SetActive(false);
            EffectGround.transform.position = transform.position;
        }

        //落地检测
        private void OnCollisionEnter(Collision collision)
        {
            if (GameBManager.Instance.IsGaming())
            {
                if (collision.gameObject.name == "FloorCollider")
                {
                    GameBManager.Instance.jumpCounter++;
                    isOnGround = true;
                }

                if (collision.gameObject.name.StartsWith("changjing"))
                {
                    GameBManager.Instance.jumpCounter++;
                    isOnGround = true;
                    var block = collision.transform.parent.GetComponent<Block>();
                    if (block == null)
                    {
                        return;
                    }
                    block.stopMove = true;
                    if (!perfectBlockList.Contains(block))
                    {
                        //达成完美
                        if (perfectBlockList.Count > 0 && block.eulerAnglesY == perfectBlockList[perfectBlockList.Count - 1].eulerAnglesY)
                        {
                            GameBManager.Instance.SetPerfectJump(perfectBlockList.Count);
                            perfectBlockList.Add(block);
                            for (int i = 0; i < perfectBlockList.Count; i++)
                            {
                                perfectBlockList[i].SetBright();
                            }
                            if (GameBManager.Instance.IsRotatingJump)
                            {
                                EffectPerfaceJump.SetActive(true);
                            }
                            else
                            {
                                EffectPerfaceJump.SetActive(false);
                            }
                            //星星特效数量
                            if (perfectBlockList.Count <= 1)
                            {
                                ParticleCount.constant = 8;
                            }
                            else if (perfectBlockList.Count <= 3)
                            {
                                ParticleCount.constant = 15;
                            }
                            else if (perfectBlockList.Count <= 6)
                            {
                                ParticleCount.constant = 25;
                            }
                            else if (perfectBlockList.Count <= 10)
                            {
                                ParticleCount.constant = 35;
                            }
                            else
                            {
                                ParticleCount.constant = 50;
                            }
                            JumpStarBurst.count = ParticleCount;
                            JumpStarEmission.SetBurst(0, JumpStarBurst);

                            EffectGround.transform.position = transform.position;
                            EffectGround.Play();
                            EffectJumpStar.transform.position = transform.position;
                            EffectJumpStar.Play();
                        }
                        else
                        {
                            EffectPerfaceJump.SetActive(false);
                            GameBManager.Instance.SetCommonJump();
                            perfectBlockList.Clear();
                            perfectBlockList.Add(block);
                        }
                    }
                }
            }
        }

        //GameOver检测
        private void OnTriggerEnter(Collider other)
        {
            if (GameBManager.Instance.IsGaming())
            {
                GameBManager.Instance.cameraManager.GameOverPos2 = new Vector3(0, transform.position.y - 2, -(transform.position.y * Mathf.Tan(Mathf.PI / 3)));

                GameBManager.Instance.SetGameOver();


                Rigidbody rb = GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;//冻结Z轴移动和Y、X轴旋转
                rb.constraints = RigidbodyConstraints.FreezeRotationX;
                rb.constraints = RigidbodyConstraints.FreezeRotationY;

                EffectGround.Stop();
                EffectJumpStar.Stop();
                EffectPerfaceJump.SetActive(false);

                Vector3 bumpDirection = other.gameObject.name == "Left" ? -transform.right : transform.right;

                rb.AddForce(bumpDirection * 5, ForceMode.Impulse);
                AudioMgr.play_effect(AudioConfig.GameB_Death, GameData.FunJumpEffectStatus, ".wav");//播放音效
                AudioMgr.play_effect(AudioConfig.GameB_HitCharacter, GameData.FunJumpEffectStatus, ".wav");//播放音效
            }
        }
    }
}
