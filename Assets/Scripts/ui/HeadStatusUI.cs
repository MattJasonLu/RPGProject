﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStatusUI : MonoBehaviour
{
    public static HeadStatusUI _instance;

    private UILabel name;
    private UISlider hpBar;
    private UISlider mpBar;
    private UILabel hpLabel;
    private UILabel mpLabel;
    private PlayerStatus ps;

    private void Awake()
    {
        _instance = this;
        name = transform.Find("Name").GetComponent<UILabel>();
        hpBar = transform.Find("HP").GetComponent<UISlider>();
        mpBar = transform.Find("MP").GetComponent<UISlider>();
        hpLabel = transform.Find("HP/Thumb/Label").GetComponent<UILabel>();
        mpLabel = transform.Find("MP/Thumb/Label").GetComponent<UILabel>();
    }

    private void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateShow(ps);
    }

    public void UpdateShow(PlayerStatus ps)
    {
        name.text = "Lv." + ps.level + " " + ps.name;
        hpBar.value = (float)ps.hp_remain / (float)ps.hp;
        mpBar.value = (float)ps.mp_remain / (float)ps.mp;
        hpLabel.text = ps.hp_remain + "/" + ps.hp;
        mpLabel.text = ps.mp_remain+ "/" + ps.mp;
    }
}
