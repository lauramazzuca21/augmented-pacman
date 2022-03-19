using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void ReceivePoints(GameObject money, int pts);//definisce la funzione che voglio (interfaccia funzione evento)
    public static event ReceivePoints Points;

    public delegate void ReceiveSound(string objTag);
    public static event ReceiveSound Sound;

    internal static void FirePointsEvent(GameObject money, int pts)
    {
        Points?.Invoke(money, pts);
    }    
    
    internal static void FireSoundEvent(string objTag)
    {
        Sound?.Invoke(objTag);
    }

}
