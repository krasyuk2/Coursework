using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
   public GameObject[] prefabDeath;
   private Camera _camera;
   private List<GameObject> _enemyList = new List<GameObject>();
   private List<int> _listIndex = new List<int>() { 0, 1, 2, 3, 4 };
   private List<int> _indexDead = new List<int>();
   private int index = 0;
   private bool isTrue;
   private GameObject _player;
   public int Damage;
   public float DamageDistance;
   
   public void Awake()
   {
      if (Camera.main != null) _camera = Camera.main;
   }
   

   private void Update()
   {
      if(index == 5)
      {
         foreach (var enemy in _enemyList)
         {
            if (enemy == null) {  
               if (!_indexDead.Contains(_enemyList.IndexOf(enemy))) // Узнаем что враг убит и возвращаем его место в списке
               {
                  _indexDead.Add(_enemyList.IndexOf(enemy));
               }
            }
         }
         if (_indexDead.Count > 0)
         {
            for (int i = 0; i < _indexDead.Count; i++)
            {
               if (_indexDead[i] != _listIndex[i])
               {
                  foreach (var letter in _letterList)
                  {
                     if (letter != null) Destroy(letter);
                     index = 0;
                     _enemyList.Clear();
                     _indexDead.Clear();
                     isTrue = true;
                     Win();
                     
                     
                  }
            
                  return;
               }
               else
               {
                  if (i == 4)
                  {
              
                     index = 0;
                     _enemyList.Clear();
                     _indexDead.Clear();
                     isTrue = true;
                   
                  }
               }
            }
         }
      }
      
      if(isTrue) StartBaf();
   
   }

   private List<GameObject> _letterList = new List<GameObject>();
 
   public void StartBaf()
   {
      Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));
      Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));   
      
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      
      foreach (var enemy in enemies)
      {
         if (enemy.name != "Eye(Clone)")
         {
            Vector2 pos = enemy.transform.position;
            if (pos.x < max.x && pos.x > min.x && pos.y > min.y && pos.y < max.y)
            {
               if (index < 5)
               {
                  if (!_enemyList.Contains(enemy))
                  {
                     _enemyList.Add(enemy);
                     index += 1;
                  }
               }
            }
         }
      }


      if (index != 5)
      {
         index = 0;
         _enemyList.Clear();
         isTrue = true;
      }

      if (index == 5)
      {
         for (int i = 0; i < _enemyList.Count; i++)
         {
            GameObject symbol = Instantiate(prefabDeath[i], _enemyList[i].transform);
            _letterList.Add(symbol);
            symbol.transform.position = new Vector3(_enemyList[i].transform.position.x, _enemyList[i].transform.position.y + 0.5f);
         }
         isTrue = false;
      }
   }
   void Win()
   {
      GameObject[] enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
      foreach (var enemy in enemiesList)
      {
         if (enemy != null)
         {
            if (Vector2.Distance(_player.transform.position, enemy.transform.position) < DamageDistance)
            {
               enemy.GetComponent<Enemy>().TakeDamage(this.Damage);
            }
         }
      }
         
   }
}
