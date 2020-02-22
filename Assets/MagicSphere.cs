using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphere : MonoBehaviour
{
    public int attack = 0;

    private List<WolfBaby> wolfList = new List<WolfBaby>();

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.enemy)
        {
            WolfBaby baby = other.GetComponent<WolfBaby>();
            if (!wolfList.Contains(baby))
            {
                baby.TakeDamage(attack);
                wolfList.Add(baby);
            }
        }
    }
}
