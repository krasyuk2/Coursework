using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrike : MonoBehaviour
{
     public GameObject prefab;
     public int damage;
     public float bulletForce;
     public float timeRate;
     private SwordWeapon swordWeapon;

     private GameObject _cursor;
     private float _time;
     public bool isStart;
     private void Awake()
     {
          _cursor = GameObject.FindWithTag("Cursor");
     }

     public void StartBaf()
     {
          swordWeapon = FindObjectOfType<SwordWeapon>();
          isStart = true;
     }

     public void LvlUp()
     {
          timeRate = swordWeapon.fireRate;
     }
     
     private void Update()
     {
          if (isStart)
          {
               if (_time <= 0)
               {
                    if (Input.GetButtonDown("Fire1"))
                    {
                         if (swordWeapon.FireTime <= 0)
                         {
                              var air = Instantiate(prefab, swordWeapon.gameObject.transform.position,
                                   swordWeapon.gameObject.transform.rotation);
                              air.GetComponent<Rigidbody2D>().velocity =
                                   (_cursor.transform.position - swordWeapon.transform.position).normalized *
                                   bulletForce;
                              _time = timeRate;

                         }
                         
                    }
               }
               else _time -= Time.deltaTime;
          }
        
     }
}
