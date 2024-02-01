using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SwordLine : MonoBehaviour
{
   private GameObject _player;
   private GameObject _cursor;
   public GameObject SwordPrefab;
   public int Damage;
   public float TimeRate = 1f;
   private float _timeCurrent;
   private bool _isStart;
   public int countSpawn = 5;
   private void Awake()
   {
       _player = GameObject.FindWithTag("Player");
       _cursor = GameObject.FindWithTag("Cursor");
   }

   public void StartBaf()
   {
       _isStart = true;
   }

   public void AddDamage()
   {
       Damage += 10;
   }

   public void AddCountSwordLine()
   {
       countSpawn++;
   }

   private void Update()
   {
       if (_isStart)
       {
           if (_timeCurrent <= 0)
           {
               StartCoroutine(Spawn());
               _timeCurrent = TimeRate;
           }
           else
           {
               _timeCurrent -= Time.deltaTime;
           }
       }
   }

   IEnumerator Spawn()
   {
       Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f));
       for (int i = 2; i < countSpawn + 2; i++)
       {
           GameObject sword = Instantiate(SwordPrefab);
           sword.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sortingOrder = -i;
           sword.transform.position = (Vector2)_player.transform.position + dir.normalized * (i + 0.4f);
           yield return new WaitForSeconds(0.04f);
       }
   }
}
