﻿using UnityEngine;

/// <summary>
/// 游戏常数类
/// </summary>
public class GameConsts
{
    #region GameTags
    public const string MAINLIGHT = "MainLight";
    public const string ALARMLIGHT = "AlarmLight";
    public const string SIREN = "Siren";
    public const string PLAYER = "Player";
    #endregion

    #region Virtual Button & Axis
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string SNEAK = "Sneak";
    public const string SHOUT = "Shout";
    #endregion

    #region Animation Parameters & States
    //速度参数
    public static int SPEED_PARAM;
    //潜行参数
    public static int SNEAK_PARAM;
    //喊叫参数
    public static int SHOUT_PARAM;
    //运动状态
    public static int LOCOMOTION_STATE;
    #endregion

    #region Game Paramters
    //摄像机头顶观察时的偏移量
    public const float WATCH_OFFSET = 0f;
    //玩家身体偏移量
    public const float PLAYER_BODY_OFFSET = 1f;
    #endregion

    #region Static Constructor
    static GameConsts()
    {
        SPEED_PARAM = Animator.StringToHash("Speed");
        SNEAK_PARAM = Animator.StringToHash("Sneak");
        SHOUT_PARAM = Animator.StringToHash("Shout");
        LOCOMOTION_STATE = Animator.StringToHash("Locomotion");
        //Debug.Log("static");
    }
    #endregion
}
