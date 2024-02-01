using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageRadius : MonoBehaviour
{
    private int Damage= 100;
    public void Dead(int damage)
    {
        Damage = damage;
        TakeDamageList();
    }
    void TakeDamageList()
    {
        foreach (var i in listDamage)
        {
            if (i != null)
            {
                if (i.CompareTag("Player"))
                {
                    i.GetComponent<Move>().TakeDamage(Damage);
                }

                else if (i.CompareTag("Enemy"))
                {
                    i.GetComponent<Enemy>().TakeDamage(Damage);
                }
            }
        }
    }

    private List<GameObject> listDamage = new List<GameObject>();
    

    void OnTriggerStay2D(Collider2D other)
    {
        if (!listDamage.Contains(other.gameObject))
        {
            listDamage.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (listDamage.Contains(other.gameObject))
        {
            listDamage.Remove(other.gameObject);

        }
    }
}
