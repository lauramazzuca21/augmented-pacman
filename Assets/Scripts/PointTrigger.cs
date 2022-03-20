using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    public PlayerController playerController;
    private void OnTriggerEnter(Collider other)
    {
        bool getPoints = true;
        if (other.gameObject.tag == Constants.T_ENEMY)
        {
            if (playerController.IsPowerupActive)
                EventManager.FirePoliceCaughtEvent(other.gameObject);
            else
            {
                EventManager.FireArrestedPlayerEvent();
                getPoints = false;
            }
        }

        if (other.gameObject.tag == Constants.T_POWERUP)
            EventManager.FirePowerUpBeginEvent(PowerUp.PIMP);

        int pts;
        if (Constants.Points.TryGetValue(other.gameObject.tag, out pts) && getPoints)
            EventManager.FirePointsEvent(other.gameObject, pts);

        string audio;
        if (Constants.Sounds.TryGetValue(other.gameObject.tag, out audio))
            EventManager.FireSoundEvent(audio);
    }
}
