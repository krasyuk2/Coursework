using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordObject : MonoBehaviour
{
    private GameObject _player;
    public  float angle = 0;
    public float speed = 300;
    public float speedSlow;
    public float radius = 0.5f;
    private Sword _sword;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _sword = FindObjectOfType<Sword>();

    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        
        angle += Time.deltaTime * speed;

        if (angle >= 360) Destroy(gameObject);
        speed -= Time.deltaTime * speedSlow;
        float temp = Convert.ToInt32(angle) * Mathf.Deg2Rad;
        var x = Mathf.Cos (temp) * radius;
        var y = Mathf.Sin (temp) * radius;
        transform.position = new Vector2(x, y) + (Vector2)_player.transform.position;
        transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_sword.damage);
        }
    }
}
