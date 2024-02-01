using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBulletActive : MonoBehaviour
{
    public float timeRate;
    private float _time;
    public GameObject bulletPrefab;
    public float bulletForce;
    private GameObject _player;
    public int countBullet;
    public float radius;
    public bool piercing;
    public int damage;
    public int lvl;
    public bool isLvlTwo;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void OneBaf()
    {
        piercing = true;
    }

    public void TwoBaf()
    {
        isLvlTwo = true;
    }

    public void TreeBaf()
    {
        countBullet += 15;
        damage += 10;
    }


    public (float,float) Baf()
    {
        if (_time <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartCoroutine(Spawn());
                _time = timeRate;
            }
        }
        else
        {
            _time -= Time.deltaTime;
        }

        return (_time, timeRate);
    }
    IEnumerator Spawn()
    {
        for (int i = 0; i < countBullet; i++)
        {
            var x = Mathf.Cos((360f * Mathf.Deg2Rad / countBullet) * i) * radius;
            var y = Mathf.Sin((360f * Mathf.Deg2Rad / countBullet) * i) * radius;
            Vector2 temp = new Vector2(x, y) + (Vector2)_player.transform.position;
            Vector2 dir = temp - (Vector2)_player.transform.position;
            GameObject bullet = Instantiate(bulletPrefab, temp, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletForce;
            yield return new WaitForSeconds(0.1f / (countBullet / 10f));

        }
    }
}
