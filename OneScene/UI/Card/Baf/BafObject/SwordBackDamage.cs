using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBackDamage : MonoBehaviour
{
    private SwordBack _swordBack;
    private SwordBackObject _swordBackObject;
    private void Awake()
    {
        _swordBack = FindObjectOfType<SwordBack>();
        _swordBackObject = FindObjectOfType<SwordBackObject>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_swordBackObject.isDamage)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(_swordBack.Damage);
            }
        }
    }
}
