
using UnityEngine;
using HotUpdateScripts.Project.Common;

public class sound_scan : MonoBehaviour
{
    void Start()
    {
        //固定一个节奏去扫描，每隔0.5s扫描一次
        this.InvokeRepeating("scan", 0, 0.5f);
    }

    //定时器函数
    void scan()
    {
        AudioMgr.disable_over_audio();
    }
}