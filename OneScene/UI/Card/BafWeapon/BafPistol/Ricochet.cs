using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Ricochet : MonoBehaviour
{

    public int countRicochet = 0;
    public float distance = 10f;
    public void StartRicochet()
    {
        
        countRicochet++;
    }
  
    public void RicochetBaf(Collider2D other,int countRicochet,ref int tempRicochet,GameObject bullet, float bulletForce)
    {
      
        if (tempRicochet != countRicochet)
        {
            if (other.CompareTag("Enemy"))
            {
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                Vector2 dir = DirLowDistanceGameObject(other, bulletRb);
                bulletRb.velocity = dir.normalized * bulletForce;
                tempRicochet++;
            }
        }
        else
        {
            Destroy(bullet);
            
        }
    }

    
    Vector2 DirLowDistanceGameObject(Collider2D other, Rigidbody2D bulletRb) // Кидаеи пулю примерно в сторону противника ближайшего но не преследует его 
    {
        List<GameObject> _listEnemy = new List<GameObject>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 startDir = bulletRb.velocity;
       
        GameObject go = null;
        foreach (var enemy in enemies)
        {
            float dis = Vector2.Distance(enemy.transform.position, other.gameObject.transform.position);
            if (dis < distance && enemy != other.gameObject)
            {
                _listEnemy.Add(enemy);
            }
        }

        if (_listEnemy.Count > 0)
        {
            go = _listEnemy[Random.Range(0, _listEnemy.Count - 1)];
        }
        

        return go != null ? go.transform.position - bulletRb.gameObject.transform.position : startDir;
    }
    
}
