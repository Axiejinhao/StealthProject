using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [Header("玩家血量")]
    public float playerHP = 100f;
    [Header("死亡音效")]
    public AudioClip deadClip;

    private Animator ani;

    private void Awake()
    {
        instance = this;
        ani = GetComponent<Animator>();
    }

    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        //玩家已死亡
        if (playerHP <= 0)
        {
            return;
        }
        //扣血
        playerHP -= damage;

        if (playerHP <= 0)
        {
            ani.SetTrigger(GameConsts.DEAD_PARAM);
            //游戏结束声音
            AudioSource.PlayClipAtPoint(deadClip, transform.position);
            AlarmSystem.instance.alarmPosition = AlarmSystem.instance.safePosition;
            this.tag = "Untagged";
            //延时重启游戏
            Invoke("ReStart", 4.5f);
        }
    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    public void ReStart()
    {
        SceneManager.LoadScene("MainInterface");
    }
}
