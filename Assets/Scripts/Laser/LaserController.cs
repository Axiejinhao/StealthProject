using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LaserController : MonoBehaviour
{
    [Header("被控制的激光")]
    public GameObject controllerLaser;
    [Header("激光关闭声音")]
    public AudioClip switchAud;
    [Header("屏幕解锁的材质")]
    public Material unlockMat;

    //屏幕网格渲染器
    private MeshRenderer screenMeshrender;

    private void Awake()
    {
        screenMeshrender = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameConsts.PLAYER) && Input.GetButtonDown(GameConsts.SWITCH) && controllerLaser.activeSelf)
        {
            //关闭控制的激光
            controllerLaser.SetActive(false);
            //播放声音
            AudioSource.PlayClipAtPoint(switchAud, transform.position);
            //更换材质
            screenMeshrender.material = unlockMat;
        }
    }
}
