using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBulletTrigger : MonoBehaviour
{
    public delegate void Method(Enemy enemy);

    public Method method;
    public void AddDelegateTrigger(Enemy enemy)
    {
        method?.Invoke(enemy);
    }
}

