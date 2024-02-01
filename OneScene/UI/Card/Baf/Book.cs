using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject bookPrefab;
    public int Damage;
    public int Radius;
    public float bulletForce;
    private GameObject _player;
    public float TimeRate;
    private float _time;
    private bool isStart;
    public int indexLvl;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _time = TimeRate;

    }

    public void StartBaf()
    {
        isStart = true;
    }

    public void AddDamage()
    {
        Damage += 10;
    }

    public void LowKd()
    {
        TimeRate -= 1.5f;
    }
    public void UpLvl()
    {
        indexLvl++;
    }

    private GameObject book;
    void Update()
    {
        if (isStart)
        {
            Vector2 posSpawn = new Vector2(_player.transform.position.x, _player.transform.position.y - 0.2f);
            if (_time <= 0)
            {
                if( book != null) Destroy(book);
                book = Instantiate(bookPrefab, posSpawn, Quaternion.identity);
                StartCoroutine(DeleteBook());
                _time = TimeRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }

            if (book != null) book.transform.position = posSpawn;
        }
    }

    IEnumerator DeleteBook()
    {
        yield return new WaitForSeconds(3f);
        Destroy(book);
        
    }
}
