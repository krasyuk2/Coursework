using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : Enemy
{
   private LineRenderer _lineRenderer;
   private Vector2 dir;
   public GameObject prefabBullet;
   public float bulletForce;
   public float DistanceFire = 6;
   public Color startColor;
   public Color nextColor;
   public Color FireColor;
   public GameObject deadEye;

   new void Awake()
   {
      base.Awake();
      _lineRenderer = GetComponent<LineRenderer>();
   }
   new void Update()
   {
      base.Update();
      Rotation();
      Fire();
      Dead();
 

   }

   private void Start()
   {
      TempTime = TimeRateStart;
      TimeCurrent = TimeRateStart;
      _localScaleStart = transform.localScale;

   }

   void Rotation()
   {
      dir = (_player.transform.position - transform.position).normalized;
      float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, rotZ);
   }

   private bool isFire = true;
   public float TimeFireRate;
   private float TimeFireCurrent;
   void Fire()
   {
      float currentDistance = Vector2.Distance(transform.position, _player.transform.position);
      if (currentDistance < DistanceFire)
      {
         _lineRenderer.positionCount = 2;
         _lineRenderer.SetPosition(0, transform.position);
         Ray ray = new Ray(transform.position, dir);
         _lineRenderer.SetPosition(1, ray.GetPoint(currentDistance -1f));
         TrailLineColor();
      }
      else
      {
         _lineRenderer.positionCount = 0;
         TimeFireCurrent = TimeFireRate;
         TimeCurrent = TimeRateStart;
         TempTime = TimeRateStart;

      }
     
      if (startFire)
      {
         if (isFire)
         {
            _lineRenderer.startColor = FireColor;
            _lineRenderer.endColor = FireColor;
            GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletForce;
            bullet.GetComponent<BulletEnemy>().Damage = Damage;
            isFire = false;
         }
         if (TimeFireCurrent <= 0)
         {
            TempTime = TimeRateStart;
            startFire = false;
         }
         else
         {
            TimeFireCurrent -= Time.deltaTime;
         }
         
      }
    
   }

   public float TimeRateStart;
   private float TimeCurrent;
   private float TempTime;
   public float nextStepSetColor;
   private bool setColor;
   private bool startFire;
   void TrailLineColor()
   {
      if (TempTime > 0)
      {
;
         if (TimeCurrent <= 0)
         {
            StartCoroutine(SlowColor());
            if (setColor)
            {
               TempTime -= nextStepSetColor;
               TimeCurrent = TempTime;
            }
         }
         else
         {
            _lineRenderer.startColor = startColor;
            _lineRenderer.endColor = startColor;
            setColor = false;
            TimeCurrent -= Time.deltaTime;
         }
      }
      else
      {
         if(!startFire)  StartCoroutine(StartFire()); 
        
      }
   }

   IEnumerator SlowColor()
   {
      _lineRenderer.startColor = nextColor;
      _lineRenderer.endColor = nextColor;
      yield return new WaitForSeconds(0.05f);
      setColor = true;
   }

   IEnumerator StartFire()
   {
      isFire = true;
      yield return new WaitForSeconds(0.3f);
      TimeFireCurrent = TimeFireRate;
      startFire = true;
      
   }
    void Dead()
   {
      if (Heal <= 0)
      {
         speed = 0;
         Vector2 posDead = transform.position;
         GameObject dead = Instantiate(deadEye, posDead, Quaternion.identity);
         dead.transform.localScale = _localScaleStart;
         Destroy(gameObject);
      }
   } 
}
