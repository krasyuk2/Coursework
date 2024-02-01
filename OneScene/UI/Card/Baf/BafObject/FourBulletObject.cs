using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourBulletObject : MonoBehaviour
{
    public int Damage;
    public GameObject start;
    private bool flag;
    private Camera _camera;

    private void Awake()
    {
        if (Camera.main != null) _camera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject != start)
            {
                other.GetComponent<Enemy>().TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
        Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        Vector2 pos = transform.position;
        if (pos.x > max.x + 1 || pos.y > max.y + 1 || pos.x < min.x -1|| pos.y < min.y-1)
        {
            Destroy(gameObject);
        
        }
    }
}
