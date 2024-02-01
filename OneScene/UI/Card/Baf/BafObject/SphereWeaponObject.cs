using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereWeaponObject : MonoBehaviour
{
   public GameObject prefabBullet;
   public float xOffset;
   public float yOffset;

   private SphereWeapon _sphereWeapon;

   
   
   private GameObject _player;
   private float _timeFire = 1f;
   private Animator _animator;
   
   private void Awake()
   {
      _player = GameObject.FindWithTag("Player");
      _animator = GetComponent<Animator>();
      _sphereWeapon = FindObjectOfType<SphereWeapon>();

   }

   private void Update()
   {
      transform.position =
         _player.transform.TransformPoint(1 * _player.transform.localScale.x + xOffset, 1 + yOffset, 0);

      Fire();
   }

 
   void Fire()
   {
      if (_timeFire <= 0)
      {  
         
         GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
         float distance = 10f;
         GameObject go = null;
         
         foreach (var enemy in enemies)
         {
            float dis = Vector2.Distance(enemy.transform.position, transform.position);
            if (dis < distance)
            {
               distance = dis;
               go = enemy;
            }
         }
         
         if (go != null)
         {
            Vector2 dir = go.transform.position - transform.position;
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            _animator.SetBool(Animator.StringToHash("Fire"),true);
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * _sphereWeapon.bulletForce;
         }
         
         _timeFire = _sphereWeapon.fireRate;
      }
      else
      {
         _animator.SetBool(Animator.StringToHash("Fire"),false);
         _timeFire -= Time.deltaTime;
      }
   }
}
