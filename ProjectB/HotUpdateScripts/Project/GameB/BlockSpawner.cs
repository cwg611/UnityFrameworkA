using HotUpdateScripts.Project.GameB.Data;
using JEngine.Core;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HotUpdateScripts.Project.GameB
{
    /// <summary>
    /// 砖块生成控制
    /// </summary>
    public class BlockSpawner : MonoBehaviour
    {
        private List<GameObject> blockGoList;//当前主题砖块

        private List<GameObject> spawnerGoList;

        public float spawnFrequency = 1.2f;
        private float elapsedTime = 0;
        private float yPos;
        private float yOffset;//砖块厚度

        private int CustomCounter = 0;//

        private List<List<float>> blockCommonList = new List<List<float>>() {
            new List<float>(){1.2f,  1.1f,  1.1f,  1.0f,  1.2f,  1.1f,  0.9f,  1.0f,  1.1f,  1.0f},

            new List<float>(){1.2f,  1.0f,  1.0f,  0.9f, 0.9f,  0.9f,  0.9f,  0.8f,  0.9f,  1.0f },
            new List<float>(){1.2f,  1.0f, 1.0f,  0.9f, 0.9f,  0.9f,  0.9f,  0.8f,  0.9f,  1.0f },

        };

        private List<List<float>> blockMiddleList = new List<List<float>>() {
            new List<float>(){1.0f,  0.9f, 0.9f,  0.9f,  0.8f,  0.8f,  0.9f,  0.8f,  0.9f,  1.0f},
            new List<float>(){0.9f,  0.9f,  0.8f,  0.7f,  0.8f, 0.8f,  0.8f,  0.9f,  1.0f, 0.9f },
            new List<float>(){0.9f,  0.9f, 0.9f, 0.9f,  0.8f,  0.8f,  0.8f,  0.8f,  0.8f,  0.9f },
        };

        private List<List<float>> blockFastList = new List<List<float>>() {
            new List<float>(){0.8f,  0.7f,  0.7f,  0.7f,  0.7f,  0.8f,  0.7f,  0.7f,  0.7f,  0.7f},
            new List<float>(){ 0.8f, 0.8f,  0.7f,  0.7f,  0.8f,  0.8f,  0.8f,  0.8f,  0.8f,  0.8f },
        };

        public void RefreshData()
        {
            if (blockGoList == null)
            {
                blockGoList = new List<GameObject>();
            }
            else
            {
                blockGoList.Clear();
            }
            if (spawnerGoList == null)
            {
                spawnerGoList = new List<GameObject>();
            }
            else
            {
                spawnerGoList.Clear();
            }
            int blockCount = GameBData.ThemeId == 2 ? 5 : 4;
            for (int i = 0; i < blockCount; i++)
            {
                //GameB/Box/01/Block01.prefab
                blockGoList.Add(JResource.LoadRes<GameObject>("GameB/Box/0" + GameBData.ThemeId.ToString() + "/Block0" + (i + 1).ToString() + ".prefab", JResource.MatchMode.Prefab));
            }

            yOffset = 1;
            if (GameBData.ThemeId == 2 || GameBData.ThemeId == 4)
            {
                yOffset = 0.8f;
            }

        }

        public void ShowAllBlocks()
        {
            if (spawnerGoList != null)
            {
                for (int i = 0; i < spawnerGoList.Count; i++)
                {
                    spawnerGoList[i].SetActive(true);
                }
            }
        }

        public void HideAllBlocks()
        {
            if (spawnerGoList != null)
            {
                for (int i = 0; i < spawnerGoList.Count; i++)
                {
                    spawnerGoList[i].SetActive(false);
                }
            }
        }

        public void CleanBlocks()
        {
            if (spawnerGoList != null)
            {
                for (int i = 0; i < spawnerGoList.Count; i++)
                {
                    Destroy(spawnerGoList[i]);
                }
                spawnerGoList.Clear();
            }
            yPos = 0;
            CustomCounter = 0;
        }

        void Update()
        {
            if (elapsedTime < spawnFrequency && GameBManager.Instance.IsGaming())
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= spawnFrequency)
                {
                    SpawnBlock();
                    elapsedTime = 0f;
                    //spawnFrequency = Random.Range(0.8f, 2f);
                    if (GameBManager.Instance.IsRotatingJump)
                    {
                        spawnFrequency = 0.9f;
                    }
                    else if (GameBManager.Instance.IsPerfaceJump)
                    {
                        spawnFrequency = 1.1f;
                    }
                    else
                    {
                        spawnFrequency = 1.3f;
                    }

                }
            }
        }

        List<float> currentSpeedList;
        private void SpawnBlock()
        {
            int randomIndex = Random.Range(0, blockGoList.Count);
            GameObject blockSpawn = Instantiate(blockGoList[randomIndex]);

            blockSpawn.transform.parent = transform;
            blockSpawn.transform.position = new Vector3(blockSpawn.transform.position.x, yPos, blockSpawn.transform.position.z);
            yPos += yOffset;
            spawnerGoList.Add(blockSpawn);

            Block block = blockSpawn.AddComponent<Block>();
            bool isLeft = Random.value >= 0.5;//随机一个方向
            block.startLeft = isLeft;
            GameBManager.Instance.nextDirection = isLeft ? 1 : 2;

            int curIndex = CustomCounter % 10;
            if (curIndex == 0)
            {
                if (GameBManager.Instance.IsRotatingJump || GameBManager.Instance.CurrentScore > 300)
                {
                    int ran = Random.Range(0, blockFastList.Count);
                    currentSpeedList = blockFastList[ran];
                }
                else if (GameBManager.Instance.IsPerfaceJump||GameBManager.Instance.CurrentScore > 150)
                {
                    int ran = Random.Range(0, blockMiddleList.Count);
                    currentSpeedList = blockMiddleList[ran];
                }
                else
                {
                    int ran = Random.Range(0, blockCommonList.Count);
                    currentSpeedList = blockCommonList[ran];
                }
            }
            block.rotateTime *= currentSpeedList[curIndex];//砖块速度控制
            blockSpawn.SetActive(true);
            GameBManager.Instance.stackCount++;
            block.StartMove();
            CustomCounter++;
        }

    }
}
