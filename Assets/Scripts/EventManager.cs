using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ReceivePoints(int pts);
    public static event ReceivePoints Points;

    internal static void FirePointsEvent(int pts)
    {
        Points?.Invoke(pts);
    }

}
