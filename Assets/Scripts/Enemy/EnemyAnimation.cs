using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EnemyAnimation : MonoBehaviour
{
    [Header("死角度数")]
    public float deathZone = 4f;

    private NavMeshAgent nav;
    private Animator ani;
    private EnemySenses enemySenses;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        enemySenses = GetComponent<EnemySenses>();
    }

    private void Start()
    {
    }

    /// <summary>
    /// 角色通过动画发生移动或旋转时执行,每帧一次
    /// </summary>
    private void OnAnimatorMove()
    {
        //动画位移/每帧时间
        nav.velocity = ani.deltaPosition / Time.deltaTime;
        //旋转使用动画的根旋转
        transform.rotation = ani.rootRotation;
    }

    private void Update()
    {
        //nav.SetDestination(PlayerBag.instance.transform.position);

        //计算机器人的期望向量在机器人前方的投影向量
        Vector3 projection = Vector3.Project(nav.desiredVelocity, transform.forward);
        //设置速度参数
        ani.SetFloat(GameConsts.SPEED_PARAM, projection.magnitude, 0.2f, Time.deltaTime);
        //求期望速度向量与机器人前方向量的夹角
        float angle = Vector3.Angle(transform.forward, nav.desiredVelocity);

        //期望速度等于零时,实际期望速度向量为(0,0,1),angle不为零
        if (nav.desiredVelocity == Vector3.zero)
        {
            angle = 0;
        }
        if (angle < deathZone && enemySenses.playerInSight)
        {
            angle = 0;
            //通过LookAt转向玩家
            transform.LookAt(PlayerBag.instance.transform);
        }

        //前方与期望速度向量的法向量
        Vector3 normal = Vector3.Cross(transform.forward, nav.desiredVelocity);
        //叉乘,a*b,a在右朝上,a在左朝下
        if (normal.y < 0)
        {
            angle *= -1;
        }

        //角度转弧度
        angle = Mathf.Deg2Rad * angle;
        //设置角速度
        ani.SetFloat(GameConsts.ANGULARSPEED_PARAM, angle, 0.2f, Time.deltaTime);
    }
}
