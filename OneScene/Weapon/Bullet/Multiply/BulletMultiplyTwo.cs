using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMultiplyTwo : MonoBehaviour
{
    private BulletMultiply _bulletMultiply;

    private void Awake()
    {
        _bulletMultiply = FindObjectOfType<BulletMultiply>();
        
    }

    private void Start()
    {
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_bulletMultiply.Damage);
            Destroy(gameObject);
        }
    }
}
