using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private Move _move;
    [HideInInspector]
    public int Damage;
    private Vector2 max;
    private Vector2 min;
    private Camera _camera;
    private void Awake()
    {
        _move = FindObjectOfType<Move>();
        if (Camera.main != null) _camera = Camera.main;
    }

    private void Update()
    {
        min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
        max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        Vector2 pos = transform.position;
        if (pos.x > max.x || pos.y > max.y || pos.x < min.x || pos.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _move.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
    
}
