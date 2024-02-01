using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Kunai : MonoBehaviour
{
    public int Damage;
    public float FireRate;
    private float _timeFire;
    public GameObject bulletPrefab;
    public float bulletForce;
    private GameObject _cursor;
    private List<GameObject> _listEnemy = new List<GameObject>();
    private GameObject _player;

    
    private void Awake()
    {
        _cursor = GameObject.FindGameObjectWithTag("Cursor");
        _player = GameObject.FindGameObjectWithTag("Player");
     
    }

    void Start()
    {
        _timeFire = FireRate;
    }

    private Vector2 dir;
    void Update()
    {
        if (_timeFire <= 0)
        {
            
            dir = Vector2.zero;
            transform.position = _player.transform.position;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float Distance = 100;
            foreach (var enemy in enemies)
            {
                float distance = Vector2.Distance(enemy.transform.position, _player.transform.position);
                if (distance < Distance)
                {
                    Distance = distance;
                    dir = (enemy.transform.position - _player.transform.position).normalized;
                }
            }

            if (dir == Vector2.zero)
            {
                if (enemies.Length > 0)
                {
                    dir = enemies[Random.Range(0, enemies.Length)].transform.position - _player.transform.position;
                }
            }

            if (enemies.Length > 0)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, rotZ);
                bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletForce;
            }

            _timeFire = FireRate;
        }
        else
        {
            _timeFire -= Time.deltaTime;
        }


    }
}
