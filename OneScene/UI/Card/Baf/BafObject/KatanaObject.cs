using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class KatanaObject : MonoBehaviour
{
    public GameObject backGroundPrefab;

  
    private GameObject _canvas;
    private GameObject _player;
    public GameObject prefabFire;
    private Katana _katana;

    private void Awake()
    {
        _katana = FindObjectOfType<Katana>();

        _canvas = GameObject.FindGameObjectWithTag("Canvas");
        _player = GameObject.FindGameObjectWithTag("Player");
    }



    private void Update()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y - 0.2f,
            _player.transform.position.z);
        transform.localScale = new Vector3(_player.transform.localScale.x * -1, 1, 1);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        if (_katana.spawnBackGroundPrefab)
        {
            GameObject backGround = Instantiate(backGroundPrefab, _canvas.transform);
            yield return new WaitForSeconds(0.2f);
            Destroy(backGround);
            _katana.spawnBackGroundPrefab = false;
        }
        Fire();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        
    }

    private List<GameObject> _listFire = new List<GameObject>();

    void Fire()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(_player.transform.position, enemy.transform.position) < _katana.Distance)
            {
                enemy.GetComponent<Enemy>().TakeDamage(_katana.Damage);
                var fire = Instantiate(prefabFire, enemy.transform.position,
                    Quaternion.Euler(0, 0, Random.Range(0, 360)));
                _listFire.Add(fire);
            }
        }

  

        StartCoroutine(DeleteFire());
    }

    IEnumerator DeleteFire()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (var fire in _listFire)
        {
            Destroy(fire);
        }
   
    }
}
