using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletMeteor : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float BulletForce;
    private Meteor _meteor;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _meteor = FindObjectOfType<Meteor>();
        
    }

    void Start()
    {
        _rigidbody2D.AddForce(new Vector2(Random.Range(-0.75f,0.75f),1) * (BulletForce * Random.Range(1,1.5f)), ForceMode2D.Impulse);
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(Random.Range(1.8f, 2.8f));
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(_meteor.Damage);
            Destroy(gameObject);
        }
    }

    
}
