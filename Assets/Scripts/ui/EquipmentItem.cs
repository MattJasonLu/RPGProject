using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour
{
    private UISprite sprite;
    private bool isHover = false;
    public int id;

    private void Awake()
    {
        sprite = GetComponent<UISprite>();
    }

    private void Update()
    {
        if (isHover)
        {
            // 在鼠标右键点击，表示卸下
            if (Input.GetMouseButtonDown(1))
            {
                EquipmentUI._instance.TakeOff(id, this.gameObject);

            }
        }
    }

    public void SetId(int id)
    {
        this.id = id;
        ObjectInfo info = ObjectsInfo._instance.GetObjectInfoById(id);
        SetInfo(info);
    }

    public void SetInfo(ObjectInfo info)
    {
        this.id = info.id;
        sprite.spriteName = info.icon_name;
    }

    public void OnHover(bool isOver)
    {
        isHover = isOver;
    }
}
