using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWeapon : MonoBehaviour
{
    public GameObject prefabSphere;
    public float fireRate = 1f;
    public int Damage;
    public float bulletForce = 10;
    public bool isPiercing;
    
    public void StartPiercing()
    {
        isPiercing = true;
    }
    

    public void AddDamage()
    {
        Damage += 10;
    }

    public void LowFireKd()
    {
        fireRate -= 0.2f;
    }
    public void StartSphereWeapon()
    {
        Instantiate(prefabSphere);
    }
}
