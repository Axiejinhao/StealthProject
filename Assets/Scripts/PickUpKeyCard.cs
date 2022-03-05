using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PickUpKeyCard : MonoBehaviour
{
    [Header("拾取卡片的音效")]
    public AudioClip pickupClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConsts.PLAYER))
        {
            //标记玩家获得钥匙
            PlayerBag.instance.hasKey = true;
            //播放音效
            AudioSource.PlayClipAtPoint(pickupClip, transform.position);
            Destroy(gameObject);
        }
    }
}
