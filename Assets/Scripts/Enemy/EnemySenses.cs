using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// 敌人的视觉和听觉,位置消息的接收
/// </summary>
public class EnemySenses : MonoBehaviour
{
    [Header("视野范围")]
    public float fieldOfView = 100f;
    [HideInInspector]
    public bool playerInSight = false;

    //机器人私人的警报位置
    [HideInInspector]
    public Vector3 personAlarmPosition;
    //上一帧的警报位置
    private Vector3 previousAlarmPosition;

    //机器人指向玩家的方向向量
    private Vector3 dir;
    //玩家的Transform
    private Transform player;
    //射线碰撞检测器
    private RaycastHit hit;
    //玩家动画系统
    private Animator playerAni;
    //导航组件
    private NavMeshAgent nav;
    //路径对象
    private NavMeshPath path;
    //圆形碰撞体
    private SphereCollider sphereCollider;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        //通过单例脚本找到玩家
        player = PlayerBag.instance.transform;
        playerAni = player.GetComponent<Animator>();
        //赋初值
        personAlarmPosition = AlarmSystem.instance.safePosition;
        previousAlarmPosition = AlarmSystem.instance.safePosition;
    }

    private void Update()
    {
        CheckAlarmPositionChange();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(GameConsts.PLAYER))
        {
            return;
        }

        if (PlayerHealth.instance.playerHP <= 0)
        {
            return;
        }

        //视觉检测
        Sighting();
        //听觉检测
        Hearing();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(GameConsts.PLAYER))
        {
            return;
        }
        //离开触发器,则看不到玩家
        playerInSight = false;
    }

    /// <summary>
    /// 检测警报位置是否更新
    /// </summary>
    private void CheckAlarmPositionChange()
    {
        //当前帧与上一帧的警报位置做对比
        if (previousAlarmPosition != AlarmSystem.instance.alarmPosition)
        {
            //将全局警报位置发送给机器人
            personAlarmPosition = AlarmSystem.instance.alarmPosition;
        }

        //保存当前帧的全局警报,供下一帧使用
        previousAlarmPosition = AlarmSystem.instance.alarmPosition;
    }

    /// <summary>
    /// 视觉检测
    /// </summary>
    private void Sighting()
    {
        //默认为false
        playerInSight = false;
        dir = player.position - transform.position;
        //计算夹角
        float angle = Vector3.Angle(dir, transform.forward);
        //玩家不在范围内
        if (angle > fieldOfView / 2)
        {
            return;
        }

        //向玩家发射物理射线
        if (Physics.Raycast(transform.position + Vector3.up * GameConsts.ENEMY_EYES_OFFSET, dir, out hit))
        {
            //如果射线检测到的是玩家
            if (hit.collider.CompareTag(GameConsts.PLAYER))
            {
                playerInSight = true;
                //触发全局警报
                AlarmSystem.instance.alarmPosition = player.position;
            }
        }
    }

    /// <summary>
    /// 听觉检测
    /// </summary>
    private void Hearing()
    {
        bool isLocomotion = playerAni.GetCurrentAnimatorStateInfo(0).shortNameHash == GameConsts.LOCOMOTION_STATE;
        bool isShout = playerAni.GetCurrentAnimatorStateInfo(1).shortNameHash == GameConsts.SHOUT_PARAM;
        //Debug.Log(isLocomotion + "  " + isShout);
        //如果没有喊叫和走路声音  
        if (!isLocomotion && !isShout)
        {
            return;
        }
        //能否找到玩家
        bool canArrive = nav.CalculatePath(player.position, path);

        ////*难度降低:玩家可以在房间里发出声音,需开启动态路障(NavMeshObstacle)
        ////查看路径的状态(能到达,有路障无法到达,路径不存在)
        //if (path.status == NavMeshPathStatus.PathPartial)
        //{
        //    //无法到达,存在路障
        //    return;
        //}

        if (canArrive)
        {
            Vector3[] points = new Vector3[path.corners.Length + 2];
            points[0] = transform.position;
            points[points.Length - 1] = player.position;
            for (int i = 1; i < points.Length - 1; i++)
            {
                points[i] = path.corners[i - 1];
            }
            float dis = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                dis += Vector3.Distance(points[i + 1], points[i]);
            }
            //如果导航路径足够短,则能听到声音
            if (dis <= sphereCollider.radius)
            {
                personAlarmPosition = player.position;
            }
        }
    }
}
