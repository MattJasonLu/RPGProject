using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrug : MonoBehaviour
{
    public static ShopDrug _instance;
    private TweenPosition tween;
    private bool isShow = false;

    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
    }

    public void TransformState()
    {
        if (!isShow)
        {
            tween.PlayForward();
            isShow = true;
        }
        else
        {
            tween.PlayReverse();
            isShow = false;
        }
    }

    public void OnCloseBtnClick()
    {
        TransformState();
    }

    public void OnBuyId1001()
    {

    }

    public void OnBuyId1002()
    {

    }

    public void OnBuyId1003()
    {

    }

    public void OnOkBtnClick()
    {

    }
}
