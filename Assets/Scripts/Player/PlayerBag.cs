using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerBag : MonoBehaviour
{
    public static PlayerBag instance;

    [Header("玩家是否获得钥匙")]
    public bool hasKey = false;

    private void Awake()
    {
        instance = this;
    }
}
