using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Moving,
    Idle
}

public class PlayerMove : MonoBehaviour
{
    public float speed = 3;
    public PlayerState playerState = PlayerState.Idle;
    private PlayerDir dir;
    private CharacterController controller;
    public bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        dir = GetComponent<PlayerDir>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(dir.targetPosition, transform.position);
        Debug.Log(distance);
        if (distance > 0.1f)
        {
            isMoving = true;
            playerState = PlayerState.Moving;
            controller.SimpleMove(transform.forward * speed);
        }
        else
        {
            isMoving = false;
            playerState = PlayerState.Idle;
        }
    }
}
