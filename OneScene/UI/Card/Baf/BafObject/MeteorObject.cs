using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorObject : MonoBehaviour
{
    public GameObject prefabBullet;
    public GameObject prefabBulletOneAndTwo;
    private Meteor _meteor;

    private void Awake()
    {
        _meteor = FindObjectOfType<Meteor>();
    }

    public void Start()
    {
        switch (_meteor.indexLvl)
        {
            case 0:
                _meteor.CountBullet = 5;
                _meteor.CountBulletOneSec = 1;
                break;
            case 1:
                _meteor.CountBullet = 10;
                _meteor.CountBulletOneSec = 3;
                break;
            case 2:
                _meteor.CountBullet = 15;
                _meteor.CountBulletOneSec = 5;
                break;
        }
        StartCoroutine(Move());
        
    }

    IEnumerator Move()
    {
        for (float i = 0; i < 3.4; i += 0.02f )
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.02f);
            yield return new WaitForSeconds(0.002f);
        }

        yield return new WaitForSeconds(0.3f);
        
        for (int i = 0; i < _meteor.CountBullet; i++)
        {
            for (int j = 0; j < _meteor.CountBulletOneSec; j++)
            {
                switch (_meteor.indexLvl)
                {
                    case 0:
                        Instantiate(prefabBulletOneAndTwo, transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(prefabBulletOneAndTwo, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(prefabBullet, transform.position, Quaternion.identity);
                        break;
                }
            }
            yield return new WaitForSeconds(0.4f);
        }
        Destroy(gameObject);
    }

   
}
