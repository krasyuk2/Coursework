using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StartBulletShotgun : BulletLvlTwo
{
    public GameObject prefabBullet;
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        Fire(transform.position,5);
    }

    void Fire(Vector2 spawnPos, int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            var bullet = Instantiate(prefabBullet, spawnPos, Quaternion.identity);

            var rb = bullet.GetComponent<Rigidbody2D>();
          
            rb.velocity = Quaternion.AngleAxis(Random.Range(-7f, 7f), Vector3.forward) * _rigidbody2D.velocity ;
        }
    }

    protected override void Methods()
    {
        Destroy(gameObject);
    }


}
