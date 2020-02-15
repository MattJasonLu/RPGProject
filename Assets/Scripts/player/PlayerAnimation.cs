using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<PlayerMove>();
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void LateUpdate()
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

    void PlayAnim(string animName)
    {
        animation.CrossFade(animName);
    }
}
