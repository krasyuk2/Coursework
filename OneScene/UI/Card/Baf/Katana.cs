using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Katana : MonoBehaviour
{
    public GameObject prefabKatana;
    public int Damage = 10;
    public float spawnRate;
    public float Distance = 8;
    [HideInInspector] public bool spawnBackGroundPrefab = true;
    private GameObject _player;
    public GameObject prefabLvlTwo;
    private float _time;
    public bool isStart;
    public int lvl = 1;
    public float bulletForceLvlTwo = 10f;
    public int bulletDamage = 10;


    public void StartBaf()
    {
        isStart = true;
    }

    public void LvlUp()
    {
        lvl += 1;
    }

    public void AddDamage()
    {
        Damage += 10;
    }

    public void AddDistance()
    {
        Distance += 3;
    }

    public void LowKd()
    {
        spawnRate -= 2;
    }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                Instantiate(prefabKatana, _player.transform.position, Quaternion.identity);
               
                if (lvl > 1)
                {
                    StartCoroutine(WaitForSecondSpawnLvlTwoPrefab());
                }

             
                _time = spawnRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
    }

    private IEnumerator WaitForSecondSpawnLvlTwoPrefab()
    {
        yield return new WaitForSeconds(0.4f);
        Instantiate(prefabLvlTwo, _player.transform.position, Quaternion.identity);
    }
  
}
