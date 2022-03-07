using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("从站立到跑步的过渡时间")]
    [Range(0.1f, 1)]
    public float dempTime = 0.2f;
    [Header("转身速度")]
    public float turnSpeed = 30f;
    [Header("喊叫声音片段")]
    public AudioClip shoutClip;

    //虚拟轴
    private float hor, ver;
    //虚拟按键
    private bool sneak, shout;
    private Animator ani;
    private AudioSource aud;

    //方向向量
    private Vector3 dir;
    //目标四元数
    private Quaternion targetQua;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        dir = Vector3.zero;
    }

    private void Update()
    {
        if (PlayerHealth.instance.playerHP <= 0)
        {
            return;
        }

        hor = Input.GetAxis(GameConsts.HORIZONTAL);
        ver = Input.GetAxis(GameConsts.VERTICAL);
        sneak = Input.GetButton(GameConsts.SNEAK);
        shout = Input.GetButtonDown(GameConsts.SHOUT);

        //设置向量的值
        dir.x = hor;
        dir.z = ver;

        //按下方向键
        if (hor != 0 || ver != 0)
        {
            //平滑设置动态参数
            ani.SetFloat(GameConsts.SPEED_PARAM, 5.67f, dempTime, Time.deltaTime);

            //将方向向量转换成四元数
            Quaternion targetQua = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, Time.deltaTime * turnSpeed);
        }
        else
        {
            ani.SetFloat(GameConsts.SPEED_PARAM, 0.1f, dempTime / 4, Time.deltaTime);
        }

        //潜行
        ani.SetBool(GameConsts.SNEAK_PARAM, sneak);

        //按下喊叫键
        if (shout)
        {
            ani.SetTrigger(GameConsts.SHOUT_PARAM);
        }

        AudioSetup();
    }

    /// <summary>
    /// 声音配置
    /// </summary>
    private void AudioSetup()
    {
        //判断当前运动状态
        bool isLocomotion = ani.GetCurrentAnimatorStateInfo(0).shortNameHash == GameConsts.LOCOMOTION_STATE;
        if (isLocomotion)
        {
            if (!aud.isPlaying)
            {
                //播放音乐
                aud.Play();
            }
        }
        else
        {
            //停止音乐
            aud.Stop();
        }
    }

    /// <summary>
    /// 播放喊叫声音
    /// </summary>
    public void PlayShoutAudio()
    {
        AudioSource.PlayClipAtPoint(shoutClip, transform.position);
    }
}
