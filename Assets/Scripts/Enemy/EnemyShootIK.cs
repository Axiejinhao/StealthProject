using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EnemyShootIK : MonoBehaviour
{
    [Header("IK动画开关")]
    public bool ikActive = false;

    private Animator ani;
    private Transform player;
    private ShootingEffect shootingEffect;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        shootingEffect = GetComponentInChildren<ShootingEffect>();
    }

    private void Start()
    {
        player = PlayerBag.instance.transform;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!ikActive)
        {
            return;
        }
        //机器人没在射击状态
        if (ani.GetCurrentAnimatorStateInfo(1).shortNameHash != GameConsts.WEAPONSHOOT_STATE
            && ani.GetCurrentAnimatorStateInfo(1).shortNameHash != GameConsts.WEAPONRAISE_STATE)
        {
            return;
        }

        //设置右手权重
        ani.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        //设置眼睛的权重
        ani.SetLookAtWeight(1);

        //设置右手IK位置
        ani.SetIKPosition(AvatarIKGoal.RightHand, player.position + Vector3.up * GameConsts.ENEMY_SHOOT_OFFSET);
        //设置眼睛IK位置
        ani.SetLookAtPosition(player.position + Vector3.up * GameConsts.ENEMY_SHOOT_OFFSET);
    }

    /// <summary>
    /// 帧事件
    /// </summary>
    public void Shoot()
    {
        //播放射击特效
        shootingEffect.PlayShootingEffect();
        PlayerHealth.instance.TakeDamage(60);
    }
}
