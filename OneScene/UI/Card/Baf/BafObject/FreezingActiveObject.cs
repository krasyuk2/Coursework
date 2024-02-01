using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingActiveObject : MonoBehaviour
{
   public GameObject posEnd;
   public float Speed = 1;
   private FreezingActive _freezingActive;
   private Move _move;
   private void Awake()
   {
      _freezingActive = FindObjectOfType<FreezingActive>();
      _move = _freezingActive._player.GetComponent<Move>();
   }

   private void Start()
   {
      _dinDictionaryFreezeList = new Dictionary<GameObject, float>();
      _dinDictionaryFreezeList.Clear();
   }
   float angle = 0;
   private float _time;
   private float _timeRate = 2;
   private void Update()
   {
      transform.position = Vector2.MoveTowards(transform.position, posEnd.transform.position, Time.deltaTime * Speed);


      if (_freezingActive.isLvlTree)
      {
         angle += Time.deltaTime * _freezingActive.speedAngleLvlTree;
         float temp = Convert.ToInt32(angle) * Mathf.Deg2Rad;
       
         var x = Mathf.Cos (temp) * 3f;
         var y = Mathf.Sin (temp) * 3f;
         transform.position = new Vector2(x, y) + (Vector2)_freezingActive._player.transform.position;

         if (_time < 0)
         {
            _move.TakeHeal(2);
            _time = _timeRate;
         }
         else
         {
            _time -= Time.deltaTime;
         }
      }
      
    
      
   }

   private Dictionary<GameObject, float> _dinDictionaryFreezeList;
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Enemy"))
      {
         if (!_dinDictionaryFreezeList.ContainsKey(other.gameObject))
         {
            _dinDictionaryFreezeList.Add(other.gameObject,other.GetComponent<Enemy>().speed);
         }
         StartCoroutine(Freeze());

      }
   }
   IEnumerator Freeze()
   {
    
      foreach (var i in _dinDictionaryFreezeList)
      {
         if (i.Key != null)
         {
            i.Key.GetComponent<Enemy>().speed = 0;
         }
      }
      yield return new WaitForSeconds(3f);
      foreach (var i in _dinDictionaryFreezeList)
      {
         if (i.Key != null)
         {
            i.Key.GetComponent<Enemy>().speed = i.Value;
         }
      }
   }

   public void OnTriggerExit2D(Collider2D other)
   
   {
      if (_freezingActive.isDamage)
      {
         if (other.CompareTag("Enemy"))
         {
            other.GetComponent<Enemy>().TakeDamage(10);
         }
      }
   }
}
