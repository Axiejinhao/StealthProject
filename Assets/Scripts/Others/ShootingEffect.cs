using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShootingEffect : MonoBehaviour
{
    [Header("射击的声音片段")]
    public AudioClip shootingClip;

    private LineRenderer lineRenderer;
    private Light lt;
    private Transform player;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lt = GetComponent<Light>();
    }

    private void Start()
    {
        player = PlayerBag.instance.transform;
    }

    /// <summary>
    /// 播放射击特效
    /// </summary>
    public void PlayShootingEffect()
    {
        //激光
        //设置激光端点
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.position + Vector3.up * GameConsts.ENEMY_SHOOT_OFFSET);
        //灯光
        lt.enabled = true;
        //声音
        AudioSource.PlayClipAtPoint(shootingClip, transform.position);

        Invoke("DelayClose", 0.3f);
    }

    /// <summary>
    /// 延时关闭
    /// </summary>
    public void DelayClose()
    {
        //设置端点个数
        lineRenderer.positionCount = 0;
        lt.enabled = false;
    }
}
