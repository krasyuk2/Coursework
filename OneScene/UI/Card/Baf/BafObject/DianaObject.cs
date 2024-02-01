using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaObject : MonoBehaviour
{
    private GameObject _player;
    private Diana _diana;
    public  float angle = 0;
    public float speed = 1;
    public float radius = 0.5f;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _diana = FindObjectOfType<Diana>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Move();
        Layer();
      
    }
    void Move()
    {
        angle += Time.deltaTime * speed;
        float temp = Convert.ToInt32(angle) * Mathf.Deg2Rad;
       
        var x = Mathf.Cos (temp) * radius;
        var y = Mathf.Sin (temp) * radius;
        transform.position = new Vector2(x, y) + (Vector2)new Vector2(_player.transform.position.x,_player.transform.position.y + 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(_diana.damage);
            Destroy(gameObject);
        }
    }

    void Layer()
    {
        if (transform.position.y > _player.transform.position.y) _spriteRenderer.sortingOrder = -1;
        else _spriteRenderer.sortingOrder = 0;
    }
}
