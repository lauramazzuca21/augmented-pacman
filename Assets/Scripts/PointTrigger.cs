using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrigger : MonoBehaviour
{
    [SerializeField]
    private int points = 100;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Inside trigger");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit by player!");
            EventManager.FirePointsEvent(points);
            Destroy(this);
        }
    }
}
