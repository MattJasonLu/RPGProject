using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int grade = 1;
    public int hp = 100;
    public int mp = 100;
    public int coin = 200;

    public int attack = 20;
    public int attack_plus = 0;
    public int defend = 20;
    public int defend_plus = 0;
    public int speed = 20;
    public int speed_plus = 0;

    public int point_remain = 0; // 剩余点数
    

    public void GetCoin(int count)
    {
        coin += count;
    }

}
