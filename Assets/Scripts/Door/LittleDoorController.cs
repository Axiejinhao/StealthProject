using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LittleDoorController : MonoBehaviour
{
    [Header("开关门的声音片段")]
    public AudioClip doorAud;

    //触发器里的人数
    private float counter = 0f;
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.isTrigger);
        if (other.isTrigger)
        {
            return;
        }
        //如果是玩家或是机器人
        if (other.CompareTag(GameConsts.PLAYER) || other.CompareTag(GameConsts.ENEMY))
        {
            //判断是否是第一个人进入
            if (++counter == 1)
            {
                //开门
                ani.SetBool(GameConsts.DOOROPEN_PARAM, true);
                //播放开门声音
                AudioSource.PlayClipAtPoint(doorAud, transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }
        //如果是玩家或是机器人
        if (other.CompareTag(GameConsts.PLAYER) || other.CompareTag(GameConsts.ENEMY))
        {
            //判断是否是最后一个人离开
            if (--counter == 0)
            {
                //关门
                ani.SetBool(GameConsts.DOOROPEN_PARAM, false);
                //播放关门声音
                AudioSource.PlayClipAtPoint(doorAud, transform.position);
            }
        }
    }

    private void Update()
    {

    }
}
