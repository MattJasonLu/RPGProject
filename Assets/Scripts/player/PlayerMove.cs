using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlWalkState
{
    Moving,
    Idle,
    Task
}

public class PlayerMove : MonoBehaviour
{
    public float speed = 3;
    public ControlWalkState playerState = ControlWalkState.Idle;
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
        if (playerState != ControlWalkState.Task)
        {
            float distance = Vector3.Distance(dir.targetPosition, transform.position);
            //Debug.Log(distance);
            if (distance > 0.3f)
            {
                isMoving = true;
                playerState = ControlWalkState.Moving;
                controller.SimpleMove(transform.forward * speed);
            }
            else
            {
                isMoving = false;
                playerState = ControlWalkState.Idle;
            }
        }
    }
}
