using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class RunPage : MonoBehaviour
{
    public RectTransform GameName;
    public RectTransform Hope_Light;
    public RectTransform Love1;
    public RectTransform Love2;
    public RectTransform Friend;
    public RectTransform Pig;



    void Start()
    {
        GameNameAni();
        LoveAni();
        Hope_LightAni(Hope_Light, 15, -15, 10);
        FriendAni(Friend, Friend.localPosition.y, Friend.localPosition.y + 10, 2);

        //猪
        FriendAni(Pig, Pig.localPosition.y, Pig.localPosition.y + 40, 3);

    }


    void GameNameAni()
    {
        GameName.GetComponent<Image>().DOFade(1,0.6f);

        GameName.DOLocalMoveY(685, 0.7f).SetEase(Ease.Linear).OnComplete(() =>
        {
            //GameName.DOShakeRotation(1f, new Vector3(10, 0, 10), 10, 180, true).SetDelay(0f);
            //GameName.DOShakePosition(1f, new Vector3(20, 0, 20), 10, 180, true);
            //GameName.DOShakePosition(1f, new Vector3(0, 40, 0), 10, 180, false).SetEase(Ease.InOutBounce);

            //endValue：结束位置，jumpPower：跳跃的最大高度，numJumps：跳跃次数，jumpPower：持续时间，snapping：只取整数值（默认为false）
            //GameName.DOLocalJump(new Vector3(-12, 685, 0), 10, 3, 2).SetEase(Ease.InOutSine);

            GameName.DOBlendableLocalMoveBy(new Vector3(0, 20, 0), 2).SetEase(Ease.OutElastic);//.SetLoops(-1, LoopType.Yoyo);
        });


        //GameName.DOBlendableLocalMoveBy(new Vector3(0, 20, 0), 2).SetEase(Ease.OutElastic);

/*        GameName.DOLocalJump(new Vector3(-12, 685, 0), 1, 1, 1).OnComplete(() =>
        {
            //GameName.DOShakePosition(1f, new Vector3(0, 20, 0), 10, 180, true);
            GameName.DOBlendableLocalMoveBy(new Vector3(0, 20, 0), 2).SetEase(Ease.OutElastic);
        });*/

    }


    void LoveAni()
    {
        Sequence mSequence = DOTween.Sequence();
        mSequence.Append(Love1.DOScale(Vector3.one, 5));
        mSequence.Insert(0.1f,Love1.DOLocalMove(new Vector3(-70, 285, 0), 5f));
        mSequence.Insert(0.1f, Love1.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0f),3));

        mSequence.Insert(1,Love2.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 3));
        mSequence.Insert(1, Love2.DOLocalMove(new Vector3(-15, 225, 0), 3f));
        mSequence.Insert(1f, Love2.GetComponent<Image>().DOColor(new Color(0.8f, 0.8f, 0.8f, 0f), 3));

        mSequence.SetLoops(-1);
    }


    void FriendAni(RectTransform trans, float from, float to, float time)
    {
        trans.DOLocalMoveY(to, time).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                FriendAni(trans, to, from, time);
            }
            );
    }


    void Hope_LightAni(RectTransform trans, float from, float to, float time)
    {
        trans.DORotate(new Vector3(0, 0, from), time).OnComplete(() =>
        {
            Hope_LightAni(trans, to, from, time);
        });
    }



}
