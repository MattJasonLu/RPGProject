using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBar : MonoBehaviour
{
    public void OnStatusBtnClick()
    {
        Status._instance.TransformStatus();
    }

    public void OnBagBtnClick()
    {
        Inventory._instance.TransformState();
    }

    public void OnEquipBtnClick()
    {

    }

    public void OnSkillBtnClick()
    {

    }

    public void OnSettingBtnClick()
    {

    }
}
