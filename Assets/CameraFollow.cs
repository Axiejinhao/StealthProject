using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [Header("总挡位数")]
    public int gear = 5;
    [Header("移动速度")]
    public float moveSpeed = 1f;
    [Header("旋转速度")]
    public float turnSpeed = 1f;
    //跟随目标
    private Transform followTarget;
    //摄像机指向跟随目标的方向向量
    private Vector3 dir;
    //射线碰撞检测器
    private RaycastHit hit;

    private void Awake()
    {
        followTarget = GameObject.FindWithTag(GameConsts.PLAYER).transform;
    }

    private void Start()
    {
        //计算初始方向向量
        dir = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - transform.position;
    }

    private void Update()
    {
        //最佳位置
        Vector3 bestPos = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - dir;
        //最差位置
        Vector3 worstPos = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET + Vector3.up * (dir.magnitude + GameConsts.WATCH_OFFSET);
        //观察点位置数组
        Vector3[] watchPositions = new Vector3[gear];
        //观察起点和终点
        watchPositions[0] = bestPos;
        watchPositions[watchPositions.Length - 1] = worstPos;
        //设置中间观察点位置
        for (int i = 1; i < watchPositions.Length - 1; i++)
        {
            //watchPositions[i] = bestPos + (bestPos - worstPos) * i * 1f / (watchPositions.Length - 1);
            watchPositions[i] = Vector3.Lerp(bestPos, worstPos, i * 1f / (watchPositions.Length - 1));
        }

        //挑选合适的位置
        Vector3 fitPos = bestPos;
        for (int i = 0; i < watchPositions.Length; i++)
        {
            //可以看见玩家
            if (CanSeeTarget(watchPositions[i]))
            {
                fitPos = watchPositions[i];
                break;
            }
        }

        //插值移动
        transform.position = Vector3.Lerp(transform.position, fitPos, Time.deltaTime * moveSpeed);
        //摄像机旋转看向玩家
        Vector3 lookDir = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - transform.position;
        Quaternion targetQua = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, Time.deltaTime * turnSpeed);
        //欧拉角修正
        Vector3 eularAngles = transform.eulerAngles;
        //设置y和z为0,保证视角不会偏移
        eularAngles.y = 0;
        eularAngles.z = 0;
        transform.eulerAngles = eularAngles;
    }

    private bool CanSeeTarget(Vector3 pos)
    {
        //现在的方向向量
        Vector3 currentDir = followTarget.position + Vector3.up * GameConsts.PLAYER_BODY_OFFSET - pos;
        //射线检测
        if (Physics.Raycast(pos, currentDir, out hit))
        {
            //被检测的是玩家
            if (hit.collider.CompareTag(GameConsts.PLAYER))
            {
                return true;
            }
        }
        return false;
    }
}
