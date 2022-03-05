using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BigDoorController : MonoBehaviour
{
    [Header("开门声音")]
    public AudioClip doorAud;
    [Header("提示声音")]
    public AudioClip refuseAud;
    //外面大门
    private Animator ani;
    //里面大门
    private Animator innerDoorAni;
    //标记玩家是否有钥匙
    private bool playerHasKey = false;
    //玩家与大门的方向向量
    private Vector3 dir;
    private SphereCollider sphereCollider;
    //电梯控制器
    private LiftController liftController;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        innerDoorAni = GameObject.FindWithTag(GameConsts.INNERDOOR).GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        liftController = GameObject.FindWithTag(GameConsts.LIFT).GetComponent<LiftController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConsts.PLAYER))
        {
            //if (other.transform.eulerAngles.y > 80 || other.transform.eulerAngles.y < -80)
            //{
            //    return;
            //}
            //如果有钥匙
            if (PlayerBag.instance.hasKey)
            {
                //外门开门动画
                ani.SetBool(GameConsts.DOOROPEN_PARAM, true);
                //内门开门动画
                innerDoorAni.SetBool(GameConsts.DOOROPEN_PARAM, true);
                //播放开门声音
                AudioSource.PlayClipAtPoint(doorAud, transform.position);
            }
            else
            {
                //播放提示声音
                AudioSource.PlayClipAtPoint(refuseAud, transform.position);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConsts.PLAYER))
        {
            //如果门是开着的
            if (ani.GetBool(GameConsts.DOOROPEN_PARAM) == true)
            {
                ani.SetBool(GameConsts.DOOROPEN_PARAM, false);
                innerDoorAni.SetBool(GameConsts.DOOROPEN_PARAM, false);
                //播放关门声音
                AudioSource.PlayClipAtPoint(doorAud, transform.position);

                dir = other.transform.position - transform.position;
                //玩家从电梯里触发离开事件
                if (dir.z > 0)
                {
                    //sphereCollider.enabled = false;
                    sphereCollider.isTrigger = false;
                    //电梯启动
                    liftController.BeginMove();
                }
            }
        }
    }
}
