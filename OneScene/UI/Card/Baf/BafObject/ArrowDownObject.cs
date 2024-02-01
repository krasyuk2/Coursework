using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class ArrowDownObject : MonoBehaviour
{
   private Animator _animator;
   private void Awake()
   {
      _animator = GetComponent<Animator>();
   }

   IEnumerator Start()
   {
      yield return new WaitForSeconds(Random.Range(0.8f, 2f));
      _animator.SetBool(Animator.StringToHash("End"),true);
      yield return new WaitForSeconds(0.1f);
      Destroy(gameObject);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Enemy"))
      {
         Destroy(gameObject);
      }
   }
}
