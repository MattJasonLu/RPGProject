using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroType
{
    Swordman,
    Magician,
    Common
}

public class PlayerStatus : MonoBehaviour
{
    public HeroType heroType;
    public int level = 1;
    public string name = "FUCK";
    public int hp = 100; // 最大值
    public int mp = 100;
    public int hp_remain = 100; // 剩余值
    public int mp_remain = 100;

    public int attack = 20;
    public int attack_plus = 0;
    public int defend = 20;
    public int defend_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;

    public int point_remain = 0; // 剩余点数
    
    public void GetDrug(int hp, int mp)
    {
        hp_remain += hp;
        mp_remain += mp;
        if (hp_remain > this.hp)
        {
            hp_remain = this.hp;
        }
        if (mp_remain > this.mp)
        {
            mp_remain = this.mp;
        }
    }

    public bool GetPoint(int point = 1)
    {
        if (point_remain >= point)
        {
            point_remain -= point;
            return true;
        }
        return false;
    }

}
