using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AlarmTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConsts.PLAYER))
        {
            //触发警报,设置玩家位置
            AlarmSystem.instance.alarmPosition = other.transform.position;
        }
    }
}
