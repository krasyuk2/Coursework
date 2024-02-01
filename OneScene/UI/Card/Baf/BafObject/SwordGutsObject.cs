using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGutsObject : MonoBehaviour
{
    private SwordGuts _swordGuts;
    private float _time;
    private Camera _camera;
    private void Awake()
    {
        _swordGuts = FindObjectOfType<SwordGuts>();
        if (Camera.main != null) _camera = Camera.main;
        

    }

    private void Update()
    {
        _time += Time.deltaTime * _swordGuts.speedAngle;

        Rotate();
        
      Vector2  min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
      Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        Vector2 pos = transform.position;
        if (pos.x > max.x + 2 || pos.y > max.y  + 2|| pos.x < min.x -2 || pos.y < min.y -2)
        {
            Destroy(gameObject);
        }
    }

    void Rotate()
    {
        float angle = _time;
        transform.rotation = Quaternion.Euler(0,0,angle);
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(_swordGuts.Damage);
        }
    }
}
