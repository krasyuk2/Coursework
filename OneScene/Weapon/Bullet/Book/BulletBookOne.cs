using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletBookOne : MonoBehaviour
{
    private GameObject _player;
    private Book _book;
    private Rigidbody2D _rigidbody2D;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _book = FindObjectOfType<Book>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private List<GameObject> _listEnemy = new List<GameObject>();
    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, _player.transform.position) < _book.Radius)
            {
                _listEnemy.Add(enemy);
            }
        }

        if (_listEnemy.Count > 0)
        {
            _rigidbody2D.velocity =
                (_listEnemy[Random.Range(0, _listEnemy.Count)].transform.position - transform.position).normalized *
                _book.bulletForce;
        }
        else
        {
            _rigidbody2D.velocity =
                new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * _book.bulletForce;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_book.Damage);
            Destroy(gameObject);
        }
    }
}
