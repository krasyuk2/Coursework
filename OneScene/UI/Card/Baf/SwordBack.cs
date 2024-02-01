using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBack : MonoBehaviour
{
    public int index = 0;
    public GameObject prefabSword;
    public GameObject runduk;
    public int Damage;

    private GameObject _player;
    public float BulletForce = 10;
  
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }
    public void AddDamage()
    {
        Damage++;
    }

  
    public void StartBaf()
    {
        GameObject sword = Instantiate(prefabSword,_player.transform.position, Quaternion.identity);
        sword.GetComponent<SwordBackObject>().RegIndex(runduk.transform.GetChild(index).gameObject);
        
        index++;
        
        if (index == 7)
        {
            index = 0;
        }


    }
}
