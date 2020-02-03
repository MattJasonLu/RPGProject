using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItemIcon : UIDragDropItem
{
    private int skillId;

    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        skillId = transform.parent.GetComponent<SkillItem>().id;
        transform.parent = transform.root;
        GetComponent<UISprite>().depth = 40;
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        // 当技能拖到快捷方式上
        if (surface != null && surface.tag == Tags.shortcut)
        {
            // 传递技能号
            surface.GetComponent<ShortCutGrid>().SetSkill(skillId);
        }
    }
}
