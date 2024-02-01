using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnBulletObject : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private ReturnBullet _returnBullet;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _returnBullet = FindObjectOfType<ReturnBullet>();
    }


    void Update()
    {
        transform.position = new Vector2(_returnBullet._player.transform.position.x,
            _returnBullet._player.transform.position.y + _returnBullet.offsetY);
        if (_spriteRenderer.color.a <= 0) Destroy(gameObject);
    }
}
