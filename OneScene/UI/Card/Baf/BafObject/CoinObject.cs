using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class CoinObject : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private GameObject _player;
    private Coin _coin;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
        _coin = FindObjectOfType<Coin>();
    }

    private float startPosY;
    private void Start()
    {
        _rigidbody2D.AddForce(Vector2.up * 6.5f,ForceMode2D.Impulse);
        startPosY = transform.position.y;

    }

    private void Update()
    {
        if(transform.position.y < startPosY) Destroy(gameObject);
    }

    private bool isOneShot;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOneShot)
        {
            if (other.CompareTag("Bullet"))
            {
                var bullet = other.GetComponent<Bullet>();
                var enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if (enemies.Length > 0)
                {
                    var enemy = enemies[Random.Range(0, enemies.Length)];
                    bullet.GetComponent<Rigidbody2D>().velocity =
                        (enemy.transform.position - other.gameObject.transform.position).normalized * _coin.BulletForce;

                    bullet.Damage *= 2;
                    var liner = GetComponent<LineRenderer>();
                    liner.positionCount = 3;
                    liner.SetPosition(0, GameObject.FindWithTag("Weapon").transform.position);
                    liner.SetPosition(1, transform.position);
                    liner.SetPosition(2, enemy.transform.position);
                    StartCoroutine(WaitDeleteLine());
                    isOneShot = true;

                }
            }
        }
    }

    IEnumerator WaitDeleteLine()
    {
        yield return new WaitForSeconds(0.12f);
        Destroy(gameObject);

    }



}
