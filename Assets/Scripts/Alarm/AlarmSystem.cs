using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AlarmSystem : MonoBehaviour
{
    public static AlarmSystem instance;
    [Header("过渡速度")]
    public float turnSpeed = 8f;
    [HideInInspector]
    //警报坐标
    public Vector3 safePosition = new Vector3(0, 1000, 0);
    [HideInInspector]
    //安全坐标
    public Vector3 alarmPosition = new Vector3(0, 1000, 0);
    //警报灯光脚本
    private AlarmLight alarmLight;
    //主灯光
    private Light mainLight;
    //正常的背景音乐
    private AudioSource normalAud;
    //警报时的背景音乐
    private AudioSource panicAud;
    //喇叭对象数组
    private GameObject[] sirensObj;
    //喇叭AudioSource数组
    private AudioSource[] sirensAud;

    private void Awake()
    {
        //单例脚本赋初值
        instance = this;

        alarmLight = GameObject.FindWithTag(GameConsts.ALARMLIGHT).GetComponent<AlarmLight>();
        mainLight = GameObject.FindWithTag(GameConsts.MAINLIGHT).GetComponent<Light>();
        normalAud = GetComponent<AudioSource>();
        panicAud = transform.GetChild(0).GetComponent<AudioSource>();
        sirensObj = GameObject.FindGameObjectsWithTag(GameConsts.SIREN);
    }

    private void Start()
    {
        //实例化喇叭AudioSource数组
        sirensAud = new AudioSource[sirensObj.Length];
        //遍历获取所有喇叭的AudioSource组件
        for (int i = 0; i < sirensAud.Length; i++)
        {
            sirensAud[i] = sirensObj[i].GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        ////没有警报
        //if (alarmPosition == safePosition)
        //{
        //    AlarmSystemOperation(false);
        //}
        ////触发警报
        //else
        //{
        //    AlarmSystemOperation(true);
        //}

        //简化判断是否触发警报
        AlarmSystemOperation(alarmPosition != safePosition);
    }

    /// <summary>
    /// 警报系统的操作
    /// </summary>
    /// <param name="alarmOn">true开启,false关闭</param>
    private void AlarmSystemOperation(bool alarmOn)
    {
        float value = 0f;
        if (alarmOn)
        {
            value = 1f;
        }

        //操作警报灯光
        alarmLight.alarmOn = alarmOn;
        //操作主灯光
        mainLight.intensity = Mathf.Lerp(mainLight.intensity, 1 - value, Time.deltaTime * turnSpeed);
        //平滑操作普通背景音乐
        normalAud.volume = Mathf.Lerp(normalAud.volume, 1 - value, Time.deltaTime * turnSpeed);
        //平滑操作启警报时的背景音乐
        panicAud.volume = Mathf.Lerp(panicAud.volume, value, Time.deltaTime * turnSpeed);
        //平滑操作喇叭声音
        for (int i = 0; i < sirensAud.Length; i++)
        {
            sirensAud[i].volume = Mathf.Lerp(sirensAud[i].volume, value, Time.deltaTime * turnSpeed);
        }
    }
}
