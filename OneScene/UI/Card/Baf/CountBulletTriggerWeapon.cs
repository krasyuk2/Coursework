using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBulletTriggerWeapon : MonoBehaviour
{
    public delegate void Trigger();
    public Trigger trigger;
    
    public void Method()
    {
        trigger?.Invoke();
    }
}
