using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushActive : MonoBehaviour
{
    public float distance;
    public float force;
    public GameObject prefab;
    public GameObject prefabLvlUpOne;
    public GameObject prefabLvlUpTwoGrenade;
    public float distanceGrenadeAttraction;
    public float grenadeForce;
    private int _damage;
    public float timeRate;
    private float _time;
    private GameObject _player;
    private GameObject _cursor;

    private bool _isTwoBaf;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _cursor = GameObject.FindWithTag("Cursor");
    }
    
    // Должно быть изменинение анимации + новая механика -> отталкивание + урон
    public void OneBaf()
    {
        prefab = prefabLvlUpOne;
        _damage += 10;
    }
    
    // Должно быть изменинение анимации + новая механика отталкивание и отбрасывание притяжных гранат
    public void TwoBaf()
    {
        _isTwoBaf = true;
    }
    
    // Тожеа самое но только лучше 
    public void TreeBaf()
    {
        distance += 2f;
        force += 3000;
    }


    private GameObject pushActivePrefab;

    public (float,float) Baf()
    {

        if (pushActivePrefab != null) pushActivePrefab.transform.position = _player.transform.position;
        if (_time <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pushActivePrefab = Instantiate(prefab, _player.transform.position,Quaternion.identity);
                StartCoroutine(WaitDelete(pushActivePrefab));
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (var enemy in enemies)
                {
                    if (Vector2.Distance(enemy.transform.position, _player.transform.position) < distance)
                    {
                        TakeDamageEnemy(enemy.GetComponent<Enemy>(),_damage);
                        Vector2 dir = (enemy.transform.position - _player.transform.position).normalized;
                        enemy.GetComponent<Rigidbody2D>().AddForce(dir * force);
                    }
                }

                if (_isTwoBaf)
                {
                    SpawnGrenade();
                    
                }
                _time = timeRate;
            }
                
         
        }
        else
        {
            _time -= Time.deltaTime;
        }
     
   
        return (_time, timeRate);
    }

    IEnumerator WaitDelete(GameObject go)
    {
        yield return new WaitForSeconds(1f);
        Destroy(go);
    }

    void TakeDamageEnemy(Enemy enemy, int damage) => enemy.TakeDamage(damage);
    
    
    private void SpawnGrenade()
    {
        Vector2 posCursor = _cursor.transform.position;
        Vector2 dir = (posCursor - (Vector2)_player.transform.position).normalized;
        Vector2 posEnd = posCursor;
        GameObject grenade = Instantiate(prefabLvlUpTwoGrenade, _player.transform.position, Quaternion.identity);
        grenade.GetComponent<Rigidbody2D>().velocity = dir * grenadeForce;
        grenade.GetComponent<PushActiveGrenadeObject>().posEnd = posEnd;

    }

}
  
    

