using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    private int id;
    private ObjectInfo info;
    private UISprite icon_sprite;
    private UILabel name_label;
    private UILabel effect_label;
    private UILabel pricesell_label;

    private void Awake()
    {
        icon_sprite = transform.Find("icon").GetComponent<UISprite>();
        name_label = transform.Find("name").GetComponent<UILabel>();
        effect_label = transform.Find("effect").GetComponent<UILabel>();
        pricesell_label = transform.Find("price_sell").GetComponent<UILabel>();
    }

    // 更新显示装备
    public void SetId(int id)
    {
        this.id = id;
        info = ObjectsInfo._instance.GetObjectInfoById(id);
        icon_sprite.spriteName = info.icon_name;
        name_label.text = info.name;
        if (info.attack > 0)
        {
            effect_label.text = "+伤害 " + info.attack;
        }
        else if (info.defend > 0)
        {
            effect_label.text = "+防御 " + info.defend;
        }
        else if (info.speed > 0)
        {
            effect_label.text = "+速度 " + info.speed;
        }
        pricesell_label.text = info.price_sell.ToString();
    }
}
