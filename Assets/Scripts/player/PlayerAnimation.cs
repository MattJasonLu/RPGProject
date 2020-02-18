using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private Animation animation;
    private PlayerAttack attack;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMove>();
        animation = GetComponent<Animation>();
        attack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (attack.state == PlayerState.ControlWalk)
        {
            if (move.playerState == ControlWalkState.Moving)
            {
                PlayAnim("Run");
            }
            else if (move.playerState == ControlWalkState.Idle)
            {
                PlayAnim("Idle");
            }
            else
            {
                PlayAnim("Idle");
            }
        } 
        else if (attack.state == PlayerState.NormalAttack)
        {
            if (attack.attack_state == AttackState.Moving)
            {
                PlayAnim("Run");
            }
        }
    }

    void PlayAnim(string animName)
    {
        animation.CrossFade(animName);
    }
}
