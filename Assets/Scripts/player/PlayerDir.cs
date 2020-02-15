using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour
{
    public GameObject effect_click_prefab;
    public Vector3 targetPosition = Vector3.zero;

    private bool isMoving = false; // 表示鼠标是否按下
    private PlayerMove playerMove;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove.playerState != ControlWalkState.Task)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool isCollier = Physics.Raycast(ray, out hitInfo);
                if (isCollier && hitInfo.collider.tag == Tags.ground)
                {
                    isMoving = true;
                    // 实例化点击效果
                    ShowClickEffect(hitInfo.point);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMoving = false;
            }

            if (isMoving)
            {
                // 得到要移动的目标位置
                // 让主角朝向目标位置
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                bool isCollier = Physics.Raycast(ray, out hitInfo);
                if (isCollier && hitInfo.collider.tag == Tags.ground)
                {
                    LookAtTarget(hitInfo.point);
                }
            }
            else
            {
                if (playerMove.isMoving)
                {
                    LookAtTarget(targetPosition);
                }
            }
        }
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        targetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        this.targetPosition = targetPosition;
        // 朝向目标位置
        transform.LookAt(targetPosition);
    }

    // 实例化点击效果
    void ShowClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        Instantiate(effect_click_prefab, hitPoint, Quaternion.identity);
    }
}
