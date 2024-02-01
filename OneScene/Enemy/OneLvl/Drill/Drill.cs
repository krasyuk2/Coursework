using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Drill : Enemy
{
   private Camera _camera;
   public float DistanceSpawn;
   public float DistanceSpawnMax;
   public float fireRate = 4f;
   private float _time;
   public GameObject prefabFire;
   public GameObject deadDrill;
   public float BulletForce;


   
   new void Awake()
   {
      base.Awake(); 
      if (Camera.main != null) _camera = Camera.main;
   }

   private void Start()
   {
      _time = fireRate;
   }

   new void FixedUpdate()
   {
      if (isLocalScale) LocalScale();
      Move();
   }

   new void Update()
   {
      base.Update();
      if (Vector2.Distance(transform.position, _player.transform.position) < DistanceSpawnMax)
      {
         if (_time <= 0)
         {
            _time = fireRate;
            StartCoroutine(Fire());
         }
         else
         {
            _time -= Time.deltaTime;
         }
      }
      else
      {
         _time = fireRate;
      }

      Dead();
   }

   void Move()
   {
    
      Vector3 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
      Vector3 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
      Vector2 pos = transform.position;
      if (pos.x > max.x || pos.x < min.x || pos.y > max.y || pos.y < min.y)
      {
         Vector2 posSpawn = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
         if (Vector2.Distance(posSpawn, _player.transform.position) > DistanceSpawn &&
             Vector2.Distance(posSpawn, _player.transform.position) < DistanceSpawnMax) 
         {
            _animator.SetBool(Animator.StringToHash("Teleport"),true);
            transform.position = posSpawn;
            _time = fireRate;
            StartCoroutine(ExitTeleport());

         }
      }
   }
   IEnumerator ExitTeleport()
   {
      yield return new WaitForSeconds(1f);
      _animator.SetBool(Animator.StringToHash("Teleport"),false);
   }

   IEnumerator Fire()
   {
      _animator.SetBool(Animator.StringToHash("FIre"), true);
      StartCoroutine(ExitFire());
      yield return new WaitForSeconds(0.4f);
      Instantiate(prefabFire, transform.position, Quaternion.identity);


   }

   IEnumerator ExitFire()
   {
      yield return new WaitForSeconds(1f);
      _animator.SetBool(Animator.StringToHash("FIre"),false);
   }
   private  void Dead()
   {
      if (Heal <= 0)
      {
         
         Vector2 posDead = transform.position;
         GameObject dead = Instantiate(deadDrill, posDead, Quaternion.identity);
         dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
         Destroy(gameObject);
      }
   }

}
