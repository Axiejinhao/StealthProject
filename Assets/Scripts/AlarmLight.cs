using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AlarmLight : MonoBehaviour
{
    [Header("是否开启警报灯")]
    public bool alarmOn;
    [Header("闪烁速度")]
    public float turnSpeed = 8f;

    //高光强
    private float highIntencity = 4f;
    //低光强
    private float lowIntencity = 0f;
    //目标光强
    private float targetIntencity;
    private Light lt;
    private void Awake()
    {
        lt = GetComponent<Light>();
    }

    private void Start()
    {
        //赋初值
        targetIntencity = highIntencity;
    }

    private void Update()
    {
        //开启警报灯
        if (alarmOn)
        {
            //光强到达目标
            if (Mathf.Abs(lt.intensity - targetIntencity) < 0.05f)
            {
                //切换目标
                if (targetIntencity == highIntencity)
                {
                    targetIntencity = lowIntencity;
                }
                else
                {
                    targetIntencity = highIntencity;
                }
            }
            //平滑更改光强
            lt.intensity = Mathf.Lerp(lt.intensity, targetIntencity, Time.deltaTime * turnSpeed);
        }
        else
        {
            //平滑过渡到低光强
            lt.intensity = Mathf.Lerp(lt.intensity, lowIntencity, Time.deltaTime * turnSpeed);
            //光强到达目标
            if (Mathf.Abs(lt.intensity - targetIntencity) < 0.05f)
            {
                lt.intensity = lowIntencity;
            }
        }
    }
}
