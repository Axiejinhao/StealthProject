using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LaserTwinkle : MonoBehaviour
{
    [Header("闪烁时间")]
    public float interval = 1f;

    private float timer = 0;
    //原始位置
    private Vector3 originPos;
    //判断是否显示
    private bool isShow;

    private MeshRenderer mesh;
    private AudioSource aud;
    private Light lt;
    private BoxCollider box;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        aud = GetComponent<AudioSource>();
        lt = GetComponent<Light>();
        box = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        originPos = transform.position;
    }

    private void Update()
    {
        TwinkleByMove();
    }

    private void TwinkleByMove()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            isShow = !isShow;
            //计时器归零
            timer = 0;
        }

        if (isShow)
        {
            //原始位置
            transform.position = originPos;
        }
        else
        {
            transform.position = Vector3.up * 1000;
        }
    }

    private void TwinkleByEnable()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            mesh.enabled = !mesh.enabled;
            aud.enabled = !aud.enabled;
            lt.enabled = !lt.enabled;
            box.enabled = !box.enabled;
            //计时器归零
            timer = 0;
        }
    }
}
