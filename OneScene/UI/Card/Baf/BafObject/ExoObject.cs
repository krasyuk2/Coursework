using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExoObject : MonoBehaviour
{
    private Animator _animator;

    private ExoBaf _exoBaf;
    public float TimeRate;
    private float _time;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _exoBaf = FindObjectOfType<ExoBaf>();
    }

    private void Update()
    {
        if (_time <= 0)
        {
            StartCoroutine(Fire());
            if (_listEnemy.Count > 0)
            {
               
                foreach (var enemy in _listEnemy)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(_exoBaf.Damage);
                }
            }
            
        }
        else
        {
            _time -= Time.deltaTime;
        }
    }

    IEnumerator Fire()
    {
        _animator.SetBool(Animator.StringToHash("Fire"),true);
        _time = TimeRate;
        yield return new WaitForSeconds(1f);
        _animator.SetBool(Animator.StringToHash("Fire"),false);
  
   
    }

    private List<GameObject> _listEnemy = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(!_listEnemy.Contains(other.gameObject)) _listEnemy.Add(other.gameObject);
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(_listEnemy.Contains(other.gameObject)) _listEnemy.Remove(other.gameObject);
    }
}
