using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemyTwoLvl : Enemy
{
    public List<Animator> _AnimatorBodyParts = new List<Animator>();
    private StatisticsLvlTwo _statisticsLvlTwo;

    new void Awake()
    {
        base.Awake();
        _statisticsLvlTwo = FindObjectOfType<StatisticsLvlTwo>();
    }



    public new void OnTriggerEnter2D(Collider2D other)
    {
        if (Heal > 0)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Bullet"))
            {
                Body();
            }
        }

        
    }

    new void Update()
    {
        if(_rigidbody2D.IsSleeping())_rigidbody2D.WakeUp();
        
        if (Heal <= 0)
        {
         
            StartCoroutine(WaitForDead());
            if (_AnimatorBodyParts.Count > 0)
            {
                foreach (var anim in _AnimatorBodyParts)
                {
                    anim.SetBool("Damage",true);
                }
            }
              _animator.SetBool(Animator.StringToHash("Dead"),true);
        }
    }

 
    IEnumerator WaitForDead()
    {
        yield return new WaitForSeconds(0.5f);
        _statisticsLvlTwo.coin ++;
        Destroy(gameObject);
    }
    void Body()
    {
        if (_AnimatorBodyParts.Count > 0)
        {
            int random = Random.Range(0, _AnimatorBodyParts.Count);
            _AnimatorBodyParts[random].SetBool(Animator.StringToHash("Damage"), true);
            _AnimatorBodyParts.RemoveAt(random);
        }
    }

    private float timeDamage = 0;
    protected override void OnTriggerStay2D(Collider2D other)
    {
        base.OnTriggerStay2D(other);
        if (other.CompareTag("Block"))
        {
            if (timeDamage <= 0)
            {
                print("damage");
                other.GetComponent<Block>().TakeDamage(1);
                timeDamage = 1f;
            }
            else
            {
                timeDamage -= Time.deltaTime;
            }
        }
        if (other.CompareTag("Turret"))
        {
            if (timeDamage <= 0)
            {
                print("damage");
                other.GetComponent<TurretBlock>().TakeDamage(1);
                timeDamage = 1f;
            }
            else
            {
                timeDamage -= Time.deltaTime;
            }
        }
    }
}
