using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDrugNPC : NPC
{
    public void OnMouseOver()
    {
        // 弹出药品购买列表
        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
            ShopDrug._instance.TransformState();
        }
    }
}
