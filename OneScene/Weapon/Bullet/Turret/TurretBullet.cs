using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    private TurretActive _turretActive;
    

    private void Awake()
    {
        _turretActive = FindObjectOfType<TurretActive>();
        
    }

    private void Start()
    {
        StartCoroutine(DeleteWait());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_turretActive.piercing)
            {
                other.GetComponent<Enemy>().TakeDamage(_turretActive.damage);
            }
            else
            {
                other.GetComponent<Enemy>().TakeDamage(_turretActive.damage);
                Destroy(gameObject);
            }
            
        }
    }

    IEnumerator DeleteWait()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
