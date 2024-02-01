using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
   public int damage;
   private Animator _animator;
   public float rateAttack;
   private float _time;
   public void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   private void Update()
   {
      if (_time > 0)
      {
         
         _time -= Time.deltaTime;
      }
   }

   private void OnTriggerStay2D(Collider2D other)
   {
      if (other.CompareTag("Enemy"))
      {
         Attack(other.GetComponent<Enemy>());
      }
   }

   private void Attack(Enemy enemy)
   {
      if (_time <= 0)
      {
         _animator.SetBool(Animator.StringToHash("Attack"),true);
         enemy.TakeDamage(damage);
         StartCoroutine(WaitOffAnimation());
         _time = rateAttack;
      }
   }

   IEnumerator WaitOffAnimation()
   {
      yield return new WaitForSeconds(0.2f);
      _animator.SetBool(Animator.StringToHash("Attack"),false);
   }
}
