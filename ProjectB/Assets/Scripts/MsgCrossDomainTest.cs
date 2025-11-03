using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MsgCrossDomainTestCode
{
    test0,
    test1
}

public class MsgCrossDomainTest : MsgBaseAPI<MsgCrossDomainTest, object, MsgCrossDomainTestCode>
{
    private static MsgCrossDomainTest instance;
    public static MsgCrossDomainTest Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MsgCrossDomainTest();
            }
            return instance;
        }
    }
}
