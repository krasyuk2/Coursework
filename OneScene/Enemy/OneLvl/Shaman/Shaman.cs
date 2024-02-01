using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : Enemy
{
   public GameObject prefabArrow;
   public GameObject prefabDead;
   public float moveDistance;
   private bool isFire;
   public float bafRate = 5f;
   private float _time;
   public int countAddDamage = 40;
   public int countAddHeal = 50;
   new void Update()
   {
      base.Update();
      
      if (arrow != null && enemy != null)
      {
         arrow.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + 1.5f);
      }

      if (enemy == null)
      {
         if (arrow != null) Destroy(arrow);
         isFire = false;
      }

      if (!isFire && enemy == null)
      {
         if (_time <= 0)
         {
            StartCoroutine(Baf());
            _time = bafRate;
         }
         else
         {
            _time -= Time.deltaTime;
         }
      }
      Dead();
   }
   new void FixedUpdate()
   {
      if(isLocalScale) LocalScale();
      Move();
   }

   void Move()
   {
      if (!isFire && enemy == null)
      {
         if (Vector2.Distance(_player.transform.position, transform.position) > moveDistance)
         {
            Vector2 pos = (_player.transform.position - transform.position).normalized;
            _rigidbody2D.velocity = pos * speed;
         }
        
      }
      else
      {
         _rigidbody2D.velocity = Vector2.zero;
      }
   }

   private GameObject arrow;
   private GameObject enemy;
   private int startHeal;
   private int startDamage;
   IEnumerator Baf()
   {
      isFire = true;
      _animator.SetBool(Animator.StringToHash("Baf"),true);
      yield return new WaitForSeconds(1.5f);
      enemy = FindEnemy();
      if (enemy != null)
      {
         arrow = Instantiate(prefabArrow, new Vector2(enemy.transform.position.x, enemy.transform.position.y + 1.5f),
            Quaternion.identity);
         arrow.transform.SetParent(enemy.transform);
         var en = enemy.GetComponent<Enemy>();
         startHeal = en.Heal;
         startDamage = en.Damage;
         
         en.Heal += countAddHeal;
         en.Damage += countAddDamage;

      }
      _animator.SetBool(Animator.StringToHash("Baf"),false);
      

   }

   public float distanceEnemyFind = 5f;

   private bool isArrow;
   GameObject FindEnemy()
   {
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      foreach (var enemy in enemies)
      {
         isArrow = false;
         if (enemy.name != "Shaman" && enemy.name != "Shaman(Clone)")
         {
            for (int i = 0; i < enemy.transform.childCount; i++)
            {
               if (enemy.transform.GetChild(i).name == "Arraw(Clone)")
               {
                  isArrow = true;
               }
            }

            if (!isArrow)
            {
               if (Vector2.Distance(enemy.transform.position, transform.position) < distanceEnemyFind)
               {
                  return enemy;
               }
            }
         }
      }

      return null;
   }
   private void Dead()
   {
      if (Heal <= 0)
      {
         if(arrow != null) Destroy(arrow);
         if (enemy != null)
         {
            enemy.GetComponent<Enemy>().Heal = startHeal;
            enemy.GetComponent<Enemy>().Damage = startDamage;
         }
         Vector2 posDead = transform.position;
         GameObject dead = Instantiate(prefabDead, posDead, Quaternion.identity);
         dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
         Destroy(gameObject);
      }
   }

}
