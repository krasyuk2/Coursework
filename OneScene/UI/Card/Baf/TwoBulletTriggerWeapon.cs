using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TwoBulletTriggerWeapon : MonoBehaviour
{
    private CountBulletTriggerWeapon _countBulletTrigger;
    public int Damage = 5;
    public float bulletForce = 10;
    public GameObject prefabBullet;
    private GameObject _player;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player"); 
        _countBulletTrigger = FindObjectOfType<CountBulletTriggerWeapon>();
    }

  
    public void StartBaf()
    {
        _countBulletTrigger.trigger += Baf;
    }

    public void Baf()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            GameObject bullet = Instantiate(prefabBullet, _player.transform.position,Quaternion.identity);
            Vector2 dir = (enemy.transform.position - _player.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletForce;

        }
    }
}
