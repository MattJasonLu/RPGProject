using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : MonoBehaviour
{
    public static ShopWeapon _instance;
    private TweenPosition tween;
    private bool isShow = false;

    void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
    }

    public void TransformState()
    {
        if (!isShow)
        {
            isShow = true;
            tween.PlayForward();
        }
        else
        {
            isShow = false;
            tween.PlayReverse();
        }
    }

    // 关闭按钮
    public void OnCloseBtnClick()
    {
        TransformState();
    }
}
