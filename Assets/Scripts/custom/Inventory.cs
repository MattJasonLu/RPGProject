using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory _instance;
    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public UILabel coinNumberLabel;
    public GameObject inventoryItem;

    private TweenPosition tween;
    private int coinCount = 1000; // 金币数量
    private bool isShow = false;


    void Awake()
    {
        _instance = this;
        tween = GetComponent<TweenPosition>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(1001, 1004));
        }
    }

    // 拾取物品
    public void GetId(int id)
    {
        // 第一步查找是否存在该物品
        // 第二，存在num+1
        // 第三，不存在，查找空格，追加物品
        InventoryItemGrid grid = null;
        foreach (InventoryItemGrid temp in itemGridList)
        {
            // 已存在
            if (temp.id == id)
            {
                grid = temp;
                break;
            }
        }
        // 存在情况
        if (grid != null)
        {
            grid.PlusNumber();
        }
        // 不存在的情况
        else
        {
            foreach (InventoryItemGrid temp in itemGridList)
            {
                if (temp.id == 0)
                {
                    grid = temp;
                    break;
                }
            }
            if (grid != null)
            {
                // 不存在，查找空格，追加物品
                GameObject itemGo = NGUITools.AddChild(grid.gameObject, inventoryItem);
                itemGo.transform.localPosition = Vector3.zero; // 初始位置，确保正中间
                itemGo.GetComponent<UISprite>().depth = 4;
                grid.SetId(id);
            }
        }
    }

    void Show()
    {
        isShow = true;
        tween.PlayForward();
    }

    void Hide()
    {
        isShow = false;
        tween.PlayReverse();
    }

    // 转变状态
    public void TransformState()
    {
        if (!isShow)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
