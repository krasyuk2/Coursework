using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class SphereShieldObject : MonoBehaviour
{
    private GameObject _player;
    private SphereShield _sphereShield;
    public  float angle = 0;
    public float radius = 0.5f;
    public int damage;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _sphereShield = FindObjectOfType<SphereShield>();
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        angle += Time.deltaTime * _sphereShield.speedAngle;
        float temp = Convert.ToInt32(angle) * Mathf.Deg2Rad;
       
        var x = Mathf.Cos (temp) * radius;
        var y = Mathf.Sin (temp) * radius;
        transform.position = new Vector2(x, y) + (Vector2)_player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(_sphereShield.damage);
        }

        if (_sphereShield.indexLvl > 0)
        {
            if (other.CompareTag("EnemyFire"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
