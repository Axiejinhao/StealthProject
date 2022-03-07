using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LiftController : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 0.5f;
    [HideInInspector]
    public bool beginMove;

    private AudioSource aud;
    private float timer = 0f;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    public void BeginMove()
    {
        beginMove = true;
        aud.Play();
    }

    private void Update()
    {
        if (beginMove)
        {
            timer += Time.deltaTime;

            //电梯上升
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            //玩家上升
            PlayerBag.instance.transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            if (timer > aud.clip.length)
            {
                //重新加载场景
                SceneManager.LoadScene(0);
            }
        }
    }
}
