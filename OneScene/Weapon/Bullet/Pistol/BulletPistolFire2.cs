using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPistolFire2 : Bullet
{



   

    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            int healEnemy = other.gameObject.GetComponent<Enemy>().Heal;
            if (Damage >= healEnemy)
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(healEnemy);
                Damage -= healEnemy;
            }
            else
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
                Destroy(gameObject);
            }

        }
    }
}
