using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeapon : MonoBehaviour
{
    public static ShopWeapon _instance;
    public int[] weaponidArray;
    public UIGrid grid;
    public GameObject weaponItem;
    private TweenPosition tween;
    private bool isShow = false;
    private GameObject numberDialog;
    private UIInput numberInput;
    private int buyId = 0;

    void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
        numberDialog = transform.Find("NumberDialog").gameObject;
        numberInput = transform.Find("NumberDialog/NumberInput").GetComponent<UIInput>();
        numberDialog.SetActive(false);
    }

    private void Start()
    {
        InitShopWeapon();
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

    // 初始化武器商店的信息
    void InitShopWeapon()
    {
        foreach (int id in weaponidArray)
        {
            GameObject itemGo = NGUITools.AddChild(grid.gameObject, weaponItem);
            grid.AddChild(itemGo.transform);
            itemGo.GetComponent<WeaponItem>().SetId(id);
        }
    }

    // 确定按钮
    public void OnOkBtnClick()
    {
        int count = int.Parse(numberInput.value);
        if (count > 0)
        {

            int price = ObjectsInfo._instance.GetObjectInfoById(buyId).price_buy;
            int total_price = price * count;
            bool success = Inventory._instance.GetCoin(total_price);
            if (success)
            {
                Inventory._instance.GetId(buyId, count);
            }
        }
        buyId = 0;
        numberInput.value = "1";
        numberDialog.SetActive(false);
    }

    public void OnBuyClick(int id)
    {
        buyId = id;
        numberDialog.SetActive(true);
        numberInput.value = "1";
    }

    public void OnClick()
    {
        numberDialog.SetActive(false);
    }
}
