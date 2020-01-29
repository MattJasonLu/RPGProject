using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem
{
    private UISprite sprite;
    private bool isHover = false;
    private int id;

    private void Awake()
    {
        base.Awake();
        sprite = GetComponent<UISprite>();
    }

    private void Update()
    {
        if (isHover)
        {
            // 显示提示信息
            InventoryDes._instance.Show(id);
        }
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            // 拖放到空格子
            if (surface.tag == Tags.inventory_item_grid)
            {
                // 拖放到自己的格子
                if (surface == this.transform.parent.gameObject)
                {
                }
                else
                {
                    InventoryItemGrid oldParent = transform.parent.GetComponent<InventoryItemGrid>();
                    this.transform.parent = surface.transform;
                    InventoryItemGrid newParent = surface.GetComponent<InventoryItemGrid>();
                    newParent.SetId(oldParent.id, oldParent.num);
                    oldParent.ClearInfo();
                }
            }
            else if (surface.tag == Tags.inventory_item)
            {
                InventoryItemGrid grid1 = this.transform.parent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid grid2 = surface.transform.parent.GetComponent<InventoryItemGrid>();
                int id = grid1.id;
                int num = grid1.num;
                grid1.SetId(grid2.id, grid2.num);
                grid2.SetId(id, num);
            }
        }
        ResetPosition();
    }

    void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        sprite.spriteName = info.icon_name;
    }

    public void SetIconName(int id, string icon_name)
    {
        this.id = id;
        sprite.spriteName = icon_name;
    }

    public void OnHoverOver()
    {
        isHover = true;
    }

    public void OnHoverOut()
    {
        isHover = false;
    }
}
