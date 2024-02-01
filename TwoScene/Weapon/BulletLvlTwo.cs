using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLvlTwo : MonoBehaviour
{
    private int _damage = 0;

    private void Start()
    {
        StartCoroutine(DeleteBullet());
    }

    IEnumerator DeleteBullet()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    public void SetDamageWeapon(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyTwoLvl>().TakeDamage(_damage);
            Methods();
        }
    }

    protected virtual void Methods()
    {
        
    }
    
}
