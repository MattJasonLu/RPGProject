using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrug : MonoBehaviour
{
    public static ShopDrug _instance;
    private TweenPosition tween;
    private bool isShow = false;
    private GameObject numberDialog;
    private UIInput numberInput;
    private int buyId;

    private void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
        numberDialog = transform.Find("NumberDialog").gameObject;
        numberInput = transform.Find("NumberDialog/NumberInput").GetComponent<UIInput>();
        numberDialog.SetActive(false);
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
        Buy(1001);
    }

    public void OnBuyId1002()
    {
        Buy(1002);
    }

    public void OnBuyId1003()
    {
        Buy(1003);
    }

    public void Buy(int id)
    {
        ShowNumberDialog();
        buyId = id;
    }

    public void OnOkBtnClick()
    {
        int count = int.Parse(numberInput.value);
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(buyId);
        int price = info.price_buy;
        int price_total = price * count;
        bool success = Inventory._instance.GetCoin(price_total);
        if (success)
        {
            if (count > 0)
            {
                Inventory._instance.GetId(buyId, count);
            }
        }
        else
        {

        }
        numberDialog.SetActive(false);
    }

    void ShowNumberDialog()
    {
        numberDialog.SetActive(true);
        numberInput.value = "1";
    }
}
