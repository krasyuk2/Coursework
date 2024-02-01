using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTutorialWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
