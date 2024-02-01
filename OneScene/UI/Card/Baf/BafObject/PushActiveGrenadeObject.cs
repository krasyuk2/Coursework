using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushActiveGrenadeObject : MonoBehaviour
{
    public Vector2 posEnd;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private PushActive _pushActive;
    public GameObject plus;

    private bool StartActive;
    
    private Vector2 _posAttraction;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pushActive = FindObjectOfType<PushActive>();
        
    }

    private void Update()
    {
        if (!_Flag)
        {
            StopGrenade();
        }
        if (_enemyAttraction.Count > 0)
        {
            foreach (var enemy in _enemyAttraction)
            {
                
                enemy.transform.position =
                    Vector2.MoveTowards(enemy.transform.position, _posAttraction, Time.deltaTime * 3.4f);
            }
        }

    
    }

    private bool _Flag;
    private GameObject _plus;
    private void StopGrenade()
    {
        if (Vector2.Distance(transform.position, posEnd) < 0.4f)
        {
            _rigidbody2D.velocity = Vector2.zero;
            _spriteRenderer.enabled = false;
            StartActive = true;
            _posAttraction = transform.position;
            if (!_Flag)
            {
                _plus = Instantiate(plus, _posAttraction, Quaternion.identity);
                Attraction();
            }

            

        }
    }

    public List<GameObject> _enemyAttraction = new List<GameObject>();
    
    void Attraction()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, _posAttraction) < _pushActive.distanceGrenadeAttraction)
            {
                _enemyAttraction.Add(enemy);
            }
        }

        StartCoroutine(WaitDelete());
        _Flag = true;
        
        
    }

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
        Destroy(_plus);
    }
}
