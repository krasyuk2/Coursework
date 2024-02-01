using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlTwoKatana : MonoBehaviour
{
   private Katana _katana;
   public Vector2 pos;
   private void Awake()
   {
      _katana = FindObjectOfType<Katana>();
   }

   private void Start()
   {
      GetComponent<Rigidbody2D>().velocity = pos * _katana.bulletForceLvlTwo;
      StartCoroutine(Delete());

   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Enemy"))
      {
         other.GetComponent<Enemy>().TakeDamage(_katana.bulletDamage);
      }
   }

   IEnumerator Delete()
   {
      yield return new WaitForSeconds(4f);
      Destroy(gameObject.transform.parent.gameObject);
   }
}
