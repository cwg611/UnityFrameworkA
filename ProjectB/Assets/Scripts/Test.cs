using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Button TestButton;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        TestButton.onClick.AddListener(() =>
        {
            Test0();
        });

        MsgCrossDomainTest.Instance.AddEventListener(MsgCrossDomainTestCode.test1, Test1);
    }

    void Test0()
    {
        MsgCrossDomainTest.Instance.Dispatch(MsgCrossDomainTestCode.test0, this.gameObject.name);//主->热更
        Debug.Log("主->热更");
    }

    void Test1(object s)
    {
        Debug.Log("MsgCrossDomainTestCode test1 接受消息" + s);
    }

    private void OnDestroy()
    {
        MsgCrossDomainTest.Instance.RemoveEventListener(MsgCrossDomainTestCode.test1, Test1);
    }

}
