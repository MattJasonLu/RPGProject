using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem
{
    private UISprite sprite;

    private void Awake()
    {
        base.Awake();
        sprite = GetComponent<UISprite>();
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);

        if (surface != null)
        print(surface.tag);
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        sprite.spriteName = info.icon_name;
    }

    public void SetIconName(string icon_name)
    {
        sprite.spriteName = icon_name;
    }
}
