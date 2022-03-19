using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside trigger");
        int pts;
        if (other.gameObject.tag == Constants.T_CREDITCARD)
            EventManager.FirePowerUpEvent(PowerUp.PIMP);
        if (Constants.Points.TryGetValue(other.gameObject.tag, out pts))
            EventManager.FirePointsEvent(other.gameObject, pts);
    }
}
