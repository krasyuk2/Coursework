using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSword : MonoBehaviour
{
   private SwordWeapon _swordWeapon;
   private Camera _camera;
   private void Awake()
   {
      _swordWeapon = FindObjectOfType<SwordWeapon>();
      if (Camera.main != null) _camera = Camera.main;
   }

   private void Update()
   {
      Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
       Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
      Vector2 pos = transform.position;
      if (pos.x > max.x || pos.y > max.y || pos.x < min.x || pos.y < min.y)
      {
         Destroy(gameObject);
      }
   }

   void OnTriggerEnter2D(Collider2D other)
   {
     
      if (other.CompareTag("Enemy"))
      {
         other.gameObject.GetComponent<Enemy>().TakeDamage(_swordWeapon.Damage);
      }
   }
}
