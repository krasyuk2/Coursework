using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMeteroOneAndTwo : MonoBehaviour
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
        _rigidbody2D.velocity = (new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized * BulletForce);
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
