using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ReceivePoints(GameObject money, int pts);//definisce la funzione che voglio (interfaccia funzione evento)
    public static event ReceivePoints Points;

    public delegate void ReceiveSound(string objTag);
    public static event ReceiveSound Sound;

    public delegate void ManageArrestedPlayer();
    public static event ManageArrestedPlayer ArrestedPlayer;

    public delegate void ReceivePowerUpBegin(Assets.Scripts.PowerUp powerUp);
    public static event ReceivePowerUpBegin PowerUpBegin;

    public delegate void ReceivePowerUpEnd(Assets.Scripts.PowerUp powerUp);
    public static event ReceivePowerUpBegin PowerUpEnd;
    internal static void FirePointsEvent(GameObject obj, int pts)
    {
        Points?.Invoke(obj, pts);
    }

    internal static void FirePowerUpBeginEvent(Assets.Scripts.PowerUp powerUp)
    {
        PowerUpBegin?.Invoke(powerUp);
    }
    internal static void FirePowerUpEndEvent(Assets.Scripts.PowerUp powerUp)
    {
        PowerUpEnd?.Invoke(powerUp);
    }

    internal static void FireSoundEvent(string objTag)
    {
        Sound?.Invoke(objTag);
    }    
    
    internal static void FireArrestedPlayerEvent(string objTag)
    {
        Sound?.Invoke(objTag);
    }

}
