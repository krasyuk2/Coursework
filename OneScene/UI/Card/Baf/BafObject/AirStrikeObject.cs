using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrikeObject : MonoBehaviour
{
    private AirStrike _airStrike;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _airStrike = FindObjectOfType<AirStrike>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(LiveTime());
    }

  

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(_airStrike.damage);
        }
    }

    IEnumerator LiveTime()
    {
        for (float i = 1; i > 0f; i -= 0.05f)
        {
            _spriteRenderer.color = new Color(1, 1, 1, i);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
