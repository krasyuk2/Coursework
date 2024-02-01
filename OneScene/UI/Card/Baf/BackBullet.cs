using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBullet : MonoBehaviour
{
    private Weapon _weapon;
    

    public void StartBaf()
    {
        _weapon = FindObjectOfType<Weapon>();
        _weapon.fireDelegate += FireBack;
    }
    void FireBack()
    {
        GameObject bullet = _weapon.SpawnFire();
        bullet.GetComponent<Rigidbody2D>().velocity = -_weapon.DirFireAndSpeed();
    }

}
