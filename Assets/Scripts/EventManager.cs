using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ReceivePoints(GameObject obj, int pts);
    public static event ReceivePoints Points;

    public delegate void ReceivePowerUp(Assets.Scripts.PowerUp powerUp);
    public static event ReceivePowerUp PowerUp;

    internal static void FirePointsEvent(GameObject obj, int pts)
    {
        Points?.Invoke(obj, pts);
    }

    internal static void FirePowerUpEvent(Assets.Scripts.PowerUp powerUp)
    {
        PowerUp?.Invoke(powerUp);
    }

}
