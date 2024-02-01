using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
   public GameObject prefabEnemy;
   public AudioSource spawnSound;

   public void Start()
   {
      SpawnEnemy();
   }

   public void SpawnEnemy()
   {
      StartCoroutine(WaitSpawn());
   }

   IEnumerator WaitSpawn()
   {
      yield return new WaitForSeconds(0.15f);
      spawnSound.Play();
      Instantiate(prefabEnemy, transform.position, Quaternion.identity);
   }
}
