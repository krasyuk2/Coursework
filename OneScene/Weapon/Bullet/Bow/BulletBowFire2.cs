using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletBowFire2 : MonoBehaviour
{
   public GameObject prefabBullet;
   public int CountBullet = 2;
   private Weapon _weapon;
   private Camera _camera;
   private BowBulletFire2Add _bowBulletFire2Add;
   private void Awake()
   {
      _weapon = FindObjectOfType<Weapon>();
      _bowBulletFire2Add = FindObjectOfType<BowBulletFire2Add>();
      if(Camera.main != null) _camera = Camera.main;
      
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Enemy"))
      {
         other.gameObject.GetComponent<Enemy>().TakeDamage(_weapon.Damage);
         Multiply(other.gameObject);
      }
   }

   private void Update()
   {
      Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
      Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
      Vector2 pos = transform.position;
      if (pos.x > max.x + 1 || pos.y > max.y + 1 || pos.x < min.x -1|| pos.y < min.y-1)
      {
         Destroy(gameObject);
      }
   }


   void Rotate(Vector2 dir, GameObject current)
   {

      Vector2 diff =  dir;
      diff.Normalize();

      float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
      current.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
   }
   
   private void Multiply(GameObject enemy)
   {
      for (int i = 0; i < _bowBulletFire2Add.BulletCount; i++)
      {
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            dir.Normalize();
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<BulltBowFire2Bullet>().RegEnemy(enemy);
            Rotate(dir,bullet);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * _weapon.bulletForceFire2;
         
      }
   }
   
}
