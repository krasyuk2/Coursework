using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulltBowFire2Bullet : MonoBehaviour
{
    private Weapon _weapon;
    public GameObject enemy;
    private Camera _camera;
    private void Awake()
    {
        _weapon = FindObjectOfType<Weapon>();
        if(Camera.main != null) _camera = Camera.main;
        
    }

    public void RegEnemy(GameObject target)
    {
        enemy = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && enemy != other.gameObject)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_weapon.Damage);
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
