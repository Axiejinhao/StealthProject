using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    private EnemySenses enemySenses;

    private void Awake()
    {
        enemySenses = GetComponent<EnemySenses>();
    }

    private void Update()
    {
        //射击

        //追捕

        //巡逻
    }
}
