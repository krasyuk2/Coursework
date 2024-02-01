using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordWeapon : MonoBehaviour
{
    private Animator _animator;
    public int Damage;
    public GameObject prefabBullet;
    public float bulletForce;
    public float fireRate;
    public float FireTime;
    public float CoolDawnFire2Rate;
    public float CoolDawnFire2Time;
    private List<GameObject> _listEnemy = new List<GameObject>();
    private GameObject _cursor;
    
    public bool isFire2 = true;
    public bool isFire1 = true;
  
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _cursor = GameObject.FindGameObjectWithTag("Cursor");
      
    }

    private bool isFalgFire1 = true;
    private void Update()
    {
        if (isFire1)
        {
            if (FireTime <= 0)
            {
                if (isFalgFire1)
                {
                    Fire();
                   
                }
               
            }
            else
            {
                _animator.SetBool(Animator.StringToHash("Fire"), false);

                FireTime -= Time.deltaTime;
            }
        }

        if (isFire2)
        {
            if (CoolDawnFire2Time <= 0)
            {
                Fire2();
            }
            else
            {
                _animator.SetBool(Animator.StringToHash("Fire2"), false);
                CoolDawnFire2Time -= Time.deltaTime;
            }
        }
    }

    void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetBool(Animator.StringToHash("Fire"),true);

            StartCoroutine(WaitFire());
            isFalgFire1 = false;

        }
    }

    private bool flag = true;
    IEnumerator WaitFire()
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (var enemy in _listEnemy)
            {
                if (enemy != null)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(10);
                }
            }
            if (flag)
            {
                yield return new WaitForSeconds(0.35f);
                flag = false;
            }
        }
        flag = true;
        isFalgFire1 = true;
        FireTime = fireRate;
    }

    void Fire2()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _animator.SetBool(Animator.StringToHash("Fire2"),true);
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = (_cursor.transform.position - transform.position).normalized * bulletForce;
            bullet.transform.rotation = transform.rotation;
            CoolDawnFire2Time = CoolDawnFire2Rate;
           
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (!_listEnemy.Contains(other.gameObject))
            {
                _listEnemy.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_listEnemy.Contains(other.gameObject))
            {
                _listEnemy.Remove(other.gameObject);
            }
        }
    }
}
