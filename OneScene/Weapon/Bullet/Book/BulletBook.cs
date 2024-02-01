using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletBook : MonoBehaviour
{
    private List<GameObject> _enemyList = new List<GameObject>();

    private GameObject _player;
    private Book _book;
    private Rigidbody2D _rigidbody2D;
    private Vector2 ifNullEnemy;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _book = FindObjectOfType<Book>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ifNullEnemy = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
       
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, _player.transform.position) < _book.Radius)
            {
                _enemyList.Add(enemy);
            }
        }
    }

    private GameObject enemyTarget;
    private bool cool = true;
    private float temp;

    void Move()
    {
        temp += Time.deltaTime /2;
        if (cool)
        {   
            if(_enemyList.Count > 0)  enemyTarget = _enemyList[Random.Range(0, _enemyList.Count)];
           
            if (enemyTarget != null)
            {
                cool = false;
            }
        }
        if (enemyTarget != null)
        {
            Ray ray = new Ray(_player.transform.position,  enemyTarget.transform.position - _player.transform.position );
            Debug.DrawRay(ray.origin,ray.direction,Color.cyan);
            Vector3 posCenter =
                ray.GetPoint(Vector2.Distance(_player.transform.position, enemyTarget.transform.position) / 2);
            transform.position = Vector3.Slerp(transform.position - posCenter, enemyTarget.transform.position - posCenter, temp) +
                                 posCenter;
        }
        else
        {
            _rigidbody2D.velocity = ifNullEnemy * 10f;
        }
       
        
    }
    private void Update()
    {
            Move();
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
