using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletCircleBulletActive : MonoBehaviour
{
    private CircleBulletActive _circleBulletActive;
    public GameObject circleDamage;

    private void Update()
    {
        _circleBulletActive = FindObjectOfType<CircleBulletActive>();
    }

    private void Start()
    {
        StartCoroutine(WaitDeleteBullet());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_circleBulletActive.isLvlTwo)
            {
                Instantiate(circleDamage,
                    new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y - 0.5f, 0),
                    Quaternion.identity);
            }
            var enemy = other.GetComponent<Enemy>();
            if (!_circleBulletActive.piercing)
            {
                enemy.TakeDamage(_circleBulletActive.damage);
                Destroy(gameObject);
            }
            else
            {
                enemy.TakeDamage(_circleBulletActive.damage);
            }
        }
    }

    IEnumerator WaitDeleteBullet()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }

 
}
