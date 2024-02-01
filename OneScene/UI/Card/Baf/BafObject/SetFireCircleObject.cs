using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFireCircleObject : MonoBehaviour
{
    private SetFireTo _setFireTo;
    private SetFireCircle _setFireCircle;
    private GameObject _player;
    private void Awake()
    {
        _setFireTo = FindObjectOfType<SetFireTo>();
        _setFireCircle = FindObjectOfType<SetFireCircle>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position = _player.transform.position;
    }

    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(UpSize());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _setFireTo.Damage = _setFireCircle.Damage;
            _setFireTo.SetFire(other.gameObject);
        }
    }

    IEnumerator UpSize()
    {
        for (float i = 0; i < 2.14; i += 0.02f)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.015f);
        }
        for (float i = 1; i > 0; i -= 0.023f)
        {
            GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.005f);
        }
        Destroy(gameObject);
    }
}
