using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pentagram : MonoBehaviour
{
    public GameObject prefabPentagram;
    public int Damage;
    public float radius;
    public float TimeRate;
    private GameObject _player;
    private float _time;
    public bool isStart;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void StartBaf()
    {
        isStart = true;
        
    }
    
    private float size;
    
    public void AddSizeAndAddDamageAndLowKd()
    {
        size += 0.35f;
        Damage += 10;
        TimeRate -= 2f;
        radius += 2f;
    }
    void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                Spawn();
                _time = TimeRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
    }

    void Spawn()
    {
        float randomAngle = Random.Range(0, 359f);
        var x = Mathf.Cos(randomAngle * Mathf.Deg2Rad) * radius;
        var y = Mathf.Sin(randomAngle * Mathf.Deg2Rad) * radius;
        var posSpawn = (Vector2)_player.transform.position + new Vector2(x, y);
        var prefabPenta = Instantiate(prefabPentagram, posSpawn, Quaternion.identity);
        prefabPenta.transform.localScale += new Vector3(size, size, size);
    }
}
