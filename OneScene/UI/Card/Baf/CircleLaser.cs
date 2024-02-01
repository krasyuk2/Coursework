using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleLaser : MonoBehaviour
{
    public GameObject line;
    private LineRenderer _lineRenderer;
    private GameObject _player;
    private Camera _camera;
    public float Distance;
    public int CountAttack;
    private int _countAttack;
    public int Damage;
    public float spawnRate = 10f;
    private float _time;
    public bool isStart;
    
    private void Awake()
    {
        _lineRenderer = line.GetComponent<LineRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = FindObjectOfType<Camera>();
    }

    public void StartBaf()
    {
        isStart = true;
    }

    public void AddCountAttack()
    {
        CountAttack += 5;
    }

    public void LowKd()
    {
        spawnRate -= 1f;
    }

    private List<GameObject> _listEnemy = new List<GameObject>();

    private void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                _lineRenderer.positionCount = 2;
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies) 
                {
                    if (Vector2.Distance(_player.transform.position, enemy.transform.position) <= Distance)
                    {
                        if (!_listEnemy.Contains(enemy))
                        {
                            
                            _listEnemy.Add(enemy);
                        }

                    }
                }

                if (_listEnemy.Count > 0)
                {
                    StartCoroutine(Attack());
                    _time = spawnRate;
                }
                else
                {
                    _lineRenderer.positionCount = 0;
                }
                
            }
            else
            {
                _time -= Time.deltaTime;
            }
            if (_lineRenderer.positionCount > 1) _lineRenderer.SetPosition(0, _player.transform.position);
        }


    }

    IEnumerator Attack()
    {
        _countAttack = CountAttack;
        if (_countAttack > _listEnemy.Count) _countAttack = _listEnemy.Count;
        for (int i = 0; i < _countAttack; i++)
        {
            _lineRenderer.SetPosition(1,_listEnemy[i].transform.position);
            if (_listEnemy[i] != null)
            {
                _listEnemy[i].GetComponent<Enemy>().TakeDamage(Damage);
            }
            yield return new WaitForSeconds(0.15f);
        }
        
        _listEnemy.Clear();
        
        _lineRenderer.positionCount = 0;


    }
}
  
