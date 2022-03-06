using UnityEngine;

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
    public const string ENEMY = "Enemy";
    public const string INNERDOOR = "InnerDoor";
    public const string LIFT = "Lift";
    #endregion

    #region Virtual Button & Axis
    public const string HORIZONTAL = "Horizontal";
    public const string VERTICAL = "Vertical";
    public const string SNEAK = "Sneak";
    public const string SHOUT = "Shout";
    public const string SWITCH = "Switch";
    #endregion

    #region Animation Parameters & States
    //速度参数
    public static int SPEED_PARAM;
    //潜行参数
    public static int SNEAK_PARAM;
    //喊叫参数
    public static int SHOUT_PARAM;
    //开关门参数
    public static int DOOROPEN_PARAM;

    //运动状态
    public static int LOCOMOTION_STATE;
    //角速度参数
    public static int ANGULARSPEED_PARAM;
    #endregion

    #region Game Paramters
    //摄像机头顶观察时的偏移量
    public const float WATCH_OFFSET = 0f;
    //玩家身体偏移量
    public const float PLAYER_BODY_OFFSET = 1f;
    //玩家眼睛偏移量
    public const float ENEMY_EYES_OFFSET = 1.8f;
    #endregion

    #region Static Constructor
    static GameConsts()
    {
        SPEED_PARAM = Animator.StringToHash("Speed");
        ANGULARSPEED_PARAM = Animator.StringToHash("AngularSpeed");
        SNEAK_PARAM = Animator.StringToHash("Sneak");
        SHOUT_PARAM = Animator.StringToHash("Shout");
        LOCOMOTION_STATE = Animator.StringToHash("Locomotion");
        DOOROPEN_PARAM = Animator.StringToHash("DoorOpen");
        //Debug.Log("static");

    }
    #endregion
}
