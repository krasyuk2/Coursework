using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cultist : Enemy
{
    public GameObject deadCultist;
    public GameObject prefabAttack;
    private float FireRate = 5f;
    private float _time;
    private bool isFire;
    public float DistanceFire = 8f;
    new void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    new void Update()
    {
        base.Update();
        Dead();
        if (Vector2.Distance(transform.position, _player.transform.position) <= DistanceFire)
        {
            if (_time <= 0)
            {
                Fire();
                _time = FireRate;
            }
            else
            {
                _time -= Time.deltaTime;
            }
        }
        else
        {
            _time = FireRate;
        }
    }

    new void FixedUpdate()
    {
        if(isLocalScale)  LocalScale();
        Move();
    }

    void Move()
    {
        if (!isFire)
        {
            Vector2 pos = (_player.transform.position - transform.position).normalized; // не сталкиваеться 
            _rigidbody2D.velocity = pos * speed;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

 

    private  void Dead()
    {
        if (Heal <= 0)
        {
          
            Vector2 posDead = transform.position;
            GameObject dead = Instantiate(deadCultist, posDead, Quaternion.identity);
            dead.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            Destroy(gameObject);
        }
    }

    void Fire()
    {
       
        StartCoroutine(WaitFire());
    }

    IEnumerator WaitFire()
    {
        isFire = true;
        _animator.SetBool(Animator.StringToHash("Fire"),true);
        yield return new WaitForSeconds(0.5f);
        Instantiate(prefabAttack, new Vector2(_player.transform.position.x, _player.transform.position.y - 0.5f),
            Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool(Animator.StringToHash("Fire"), false);
        isFire = false;
    }
}
