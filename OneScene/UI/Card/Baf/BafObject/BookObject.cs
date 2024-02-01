using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookObject : MonoBehaviour
{
    public GameObject bulletPrefabTree;
    public GameObject bulletPrefabTwo;
    public GameObject bulletPrefabOne;
    private int Count;
    private Book _book;
    

    private void Awake()
    {
        _book = FindObjectOfType<Book>();
    }

    void Start()
    {
      
        switch (_book.indexLvl)
        {
            case 0:
                Count = 1;
                break;
            case 1:
                Count = 5;
                break;
            case 2:
                Count = 12;
                break;
        }
        StartCoroutine(SpawnBullet());
    }
    

    IEnumerator SpawnBullet()
    {
        for (int i = 0; i < Count; i++)
        {
            switch (_book.indexLvl)
            {
                case 0:
                    Instantiate(bulletPrefabOne, transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(bulletPrefabTwo, transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefabTree, transform.position, Quaternion.identity);
                    break;
            }
           
            yield return new WaitForSeconds(0.2f);
        }
    }
}
