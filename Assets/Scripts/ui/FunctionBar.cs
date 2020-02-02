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
        EquipmentUI._instance.TransformState();
    }

    public void OnSkillBtnClick()
    {
        SkillUI._instance.TransformState();
    }

    public void OnSettingBtnClick()
    {

    }
}
