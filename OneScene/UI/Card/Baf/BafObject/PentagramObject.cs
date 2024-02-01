using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramObject : MonoBehaviour
{
    private Pentagram _pentagram;
    
    private void Awake()
    {
        _pentagram = FindObjectOfType<Pentagram>();
    }

    void Start()
    {
        StartCoroutine(DamageStart());
    }


    private List<GameObject> _damageList = new List<GameObject>();
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!_damageList.Contains(other.gameObject))
            {
                _damageList.Add(other.gameObject);
            }
        }
    }

    IEnumerator DamageStart()
    {
        yield return new WaitForSeconds(0.8f);
        foreach (var enemy in _damageList)
        {
            if(enemy!= null)enemy.GetComponent<Enemy>().TakeDamage(_pentagram.Damage);
            
        }
        Destroy(gameObject);
    }
}
