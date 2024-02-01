using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
   public GameObject meteorPrefab;
   public float spawnRate;
   public int Damage;
   public int CountBullet;
   public int CountBulletOneSec;
   private float _time;
   private GameObject _player;
   public int indexLvl;
   public bool isStart;
   private void Awake()
   {
      _player = GameObject.FindWithTag("Player");
   }

   public void StartBaf()
   {
      isStart = true;
   }

   public void UpLvl()
   {
      indexLvl++;
   }
   
   private void Update()
   {
      if (isStart)
      {
         if (_time <= 0)
         {
            Instantiate(meteorPrefab, _player.transform.position, Quaternion.identity);
            _time = spawnRate;
         }
         else
         {
            _time -= Time.deltaTime;
         }
      }
   }
}
