using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoBulletTrigger : MonoBehaviour
{
    private CountBulletTrigger _countBulletTrigger;
    public int Damage = 5;

    private void Awake()
    {
        _countBulletTrigger = FindObjectOfType<CountBulletTrigger>();
    }
    
    public void StartBaf()
    {
        _countBulletTrigger.method += Baf;
    }

    public void Baf(Enemy enemy)
    {
        enemy.TakeDamage(Damage);
    }
}

