using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLineObject : MonoBehaviour
{

    private SwordLine _swordLine;
    private GameObject _parent;
    private void Awake()
    {
        _swordLine = FindObjectOfType<SwordLine>();
        _parent = transform.parent.gameObject;
    }

    void Start()
    {
        StartCoroutine(Down());
    }

    private bool isDamage;
    IEnumerator Down()
    {
        for (float i = 1.4f; i >= 0; i -= 0.1f)
        {
            Vector2 pos = transform.position;
            pos.y -= 0.1f;
            transform.position = pos;
            yield return new WaitForSeconds(0.04f);
        }

        isDamage = true;
        yield return new WaitForSeconds(0.02f);
        Destroy(_parent);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
           if(isDamage) other.GetComponent<Enemy>().TakeDamage(_swordLine.Damage);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (isDamage) other.GetComponent<Enemy>().TakeDamage(_swordLine.Damage);
        }
    }
}
