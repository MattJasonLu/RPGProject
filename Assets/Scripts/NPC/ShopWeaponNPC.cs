using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeaponNPC : NPC
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AudioSource>().Play();
            ShopWeapon._instance.TransformState();
        }
    }
}
