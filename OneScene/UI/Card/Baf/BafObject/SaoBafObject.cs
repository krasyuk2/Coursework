using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaoBafObject : MonoBehaviour
{
    public Vector2 dir;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private float a = 1;
    private SaoBaf _saoBaf;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _saoBaf = FindObjectOfType<SaoBaf>();
    }

    void Start()
    {
        StartCoroutine(Visible());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(_saoBaf.damage);
        }
    }

    void Update()
    {
        dir.x = Mathf.Lerp(dir.x, 0, Time.deltaTime *1.1f);
        dir.y = Mathf.Lerp(dir.y, 0, Time.deltaTime *1.1f);


        _rigidbody.velocity = dir;
        a = Mathf.Lerp(a, 0, Time.deltaTime);
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, a);
      
    }

    IEnumerator Visible()
    {
        yield return new WaitForSeconds(3f);
        if(transform.parent != null) Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }
}
