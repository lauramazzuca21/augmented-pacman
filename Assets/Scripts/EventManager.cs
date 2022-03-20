using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ReceivePoints(GameObject gameObject, int pts);//definisce la funzione che voglio (interfaccia funzione evento)
    public static event ReceivePoints Points;

    public delegate void ReceiveSound(string objTag);
    public static event ReceiveSound Sound;

    public delegate void ReceiveArrestedPlayer();
    public static event ReceiveArrestedPlayer ArrestedPlayer;

    public delegate void ReceivePowerUpBegin(Assets.Scripts.PowerUp powerUp);
    public static event ReceivePowerUpBegin PowerUpBegin;

    public delegate void ReceivePowerUpEnd(Assets.Scripts.PowerUp powerUp);
    public static event ReceivePowerUpBegin PowerUpEnd;

    public delegate void ReceivePoliceCaught(GameObject policeCar);
    public static event ReceivePoliceCaught PoliceCaught;

    internal static void FirePointsEvent(GameObject gameObject, int pts)
    {
        Points?.Invoke(gameObject, pts);
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
    
    internal static void FireArrestedPlayerEvent()
    {
        ArrestedPlayer?.Invoke();
    }

    internal static void FirePoliceCaughtEvent(GameObject gameObject)
    {
        PoliceCaught?.Invoke(gameObject);
    }
}
