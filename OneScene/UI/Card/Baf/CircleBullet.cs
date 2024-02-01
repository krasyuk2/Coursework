using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : MonoBehaviour
{
    private GameObject _player;
    public int countBullet;
    public float radius;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float TimeRate;
    private float _time;
    public bool isStart;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void StartBaf()
    {
        isStart = true;
    }
    private void Update()
    {
        if (isStart)
        {
            if (_time <= 0)
            {
                StartCoroutine(Spawn());
                _time = TimeRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
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
            yield return new WaitForSeconds(0.1f);

        }
    }
}
