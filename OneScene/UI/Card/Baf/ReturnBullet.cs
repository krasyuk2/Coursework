using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ReturnBullet : MonoBehaviour
{
    public bool startBaf;
    public GameObject prefabReturnBullet;
    [HideInInspector]
    public GameObject _player;
    public float offsetY;
    public int countChance = 10;
    
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartBaf()
    {
        startBaf = true;
    }

    public void AddChance()
    {
        countChance -= 1;
    }
   
    public bool RandomReturnBullet()
    {
        
        int randomValue = Random.Range(0, countChance); 
        print(randomValue);
        if (randomValue == 2)
        {
            Instantiate(prefabReturnBullet,
                new Vector3(_player.transform.position.x, _player.transform.position.x + offsetY), Quaternion.identity);
            return true;
        }

        return false;
    }
}
