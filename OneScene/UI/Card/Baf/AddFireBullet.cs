using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFireBullet : MonoBehaviour
{
    private Weapon _weapon;
    public int CountBullet = 0;
    
   public void AddBullet()
    {
       CountBullet++;
    }

   private bool isFlag = true;
    
    private void Update()
    {
        if (FindObjectOfType<Weapon>() != null)
        {
            _weapon = FindObjectOfType<Weapon>();
        }

        if (_weapon != null)
        {
            if (isFlag)
            {
                _weapon.fireDelegate += Fire;
                isFlag = false;
            }
        }
    }
    
    void Fire()
    {
        for (int i = 0; i < CountBullet; i++)
        {
            GameObject bullet = _weapon.SpawnFire();
            Vector2 dir = _weapon.DirFireAndSpeed();
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = dir;
            if (i % 2 == 0)
            {
                rb.velocity = Quaternion.AngleAxis(-i-2.5f, Vector3.forward) * rb.velocity;
            }
            else
            {
                rb.velocity = Quaternion.AngleAxis(i+2.5f, Vector3.forward) * rb.velocity;
            }
        }
      
      
       
    }
}
