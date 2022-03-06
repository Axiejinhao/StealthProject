using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [Header("滞留时间")]
    public float waitTime = 1f;
    [Header("追捕速度")]
    public float chasingSpeed = 5f;
    [Header("巡逻速度")]
    public float patrolingSpeed = 3f;
    [Header("巡逻点")]
    public Transform[] wayPoints;

    //计时器
    private float timer = 0;
    //巡逻点计数器
    private int wayPointIndex = 0;

    private EnemySenses enemySenses;
    private PlayerHealth playerHealth;
    private NavMeshAgent nav;

    private void Awake()
    {
        enemySenses = GetComponent<EnemySenses>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        playerHealth = PlayerBag.instance.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        //射击
        if (enemySenses.playerInSight && playerHealth.playerHP > 0)
        {
            Shooting();
        }
        //追捕
        else if (enemySenses.personAlarmPosition != AlarmSystem.instance.safePosition)
        {
            Chasing();
        }
        //巡逻
        else
        {
            Patroling();
        }
    }

    /// <summary>
    /// 射击
    /// </summary>
    private void Shooting()
    {
        //暂停导航
        nav.isStopped = true;
    }

    /// <summary>
    /// 追捕
    /// </summary>
    private void Chasing()
    {
        //恢复导航
        nav.isStopped = false;
        //追捕速度
        nav.speed = chasingSpeed;
        //设置制动距离
        nav.stoppingDistance = 1;
        //导航到私人警报位置
        nav.SetDestination(enemySenses.personAlarmPosition);

        ////*难度降低:玩家可以在房间里发出声音,需开启动态路障(NavMeshObstacle)
        ////存在路障,无法到达
        //if (nav.pathStatus == NavMeshPathStatus.PathPartial)
        //{
        //    //解除警报(警报系统)
        //    AlarmSystem.instance.alarmPosition = AlarmSystem.instance.safePosition;
        //    //解除警报(私人警报)
        //    enemySenses.personAlarmPosition = AlarmSystem.instance.safePosition;
        //}

        //判断是否到达目标
        if (nav.remainingDistance - nav.stoppingDistance <= 0.05f)
        {
            timer += Time.deltaTime;
            //计时结束
            if (timer >= waitTime)
            {
                //解除警报(警报系统)
                AlarmSystem.instance.alarmPosition = AlarmSystem.instance.safePosition;
                //解除警报(私人警报)
                enemySenses.personAlarmPosition = AlarmSystem.instance.safePosition;
                timer = 0;
            }
        }
        else
        {
            timer = 0;
        }
    }

    /// <summary>
    /// 巡逻
    /// </summary>
    private void Patroling()
    {
        //恢复导航
        nav.isStopped = false;
        //巡逻速度
        nav.speed = patrolingSpeed;
        //设置制动距离
        nav.stoppingDistance = 0;
        //设置导航目标
        nav.SetDestination(wayPoints[wayPointIndex].position);
        if (nav.remainingDistance - nav.stoppingDistance <= 0.05f)
        {
            timer += Time.deltaTime;
            //计时结束
            if (timer >= waitTime)
            {
                wayPointIndex++;
                wayPointIndex %= wayPoints.Length;

                timer = 0;
            }
        }
    }
}
