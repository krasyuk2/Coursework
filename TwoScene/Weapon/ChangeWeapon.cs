using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject currentWeapon;
    public int currentWeaponId;
    public Animator[] animatorsGetWeapon;

    public GameObject reloadPrefab;
    public void Awake()
    {
        
    }

    private void Start()
    {
        currentWeapon = transform.GetChild(0).gameObject; // Pistol
        currentWeaponId = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Change(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Change(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Change(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Change(4);
        }

        float mw = Input.GetAxis("Mouse ScrollWheel");
  
        if (mw == 0.1f)
        {
            Change(currentWeaponId + 1);
        }
        if (mw == -0.1f)
        {
            Change(currentWeaponId - 1);
        }
    }

   

    private void Change(int num)
    {
        GameObject weapon = null;
    
        if (transform.childCount >= num && num > 0)
        {
           
            weapon = transform.GetChild(num-1).gameObject;
        }
        if (transform.childCount < num)
        {
          
            num = 1;
            weapon = transform.GetChild(num-1).gameObject;
        }

        if (num < 1)
        {
          
            num = transform.childCount;
            weapon = transform.GetChild(num-1).gameObject;
        }
        if(weapon == null) return;
        if (currentWeapon != weapon)
        {
            Reload();
            currentWeaponId = num;
            currentWeapon.SetActive(false);
            weapon.SetActive(true);
            currentWeapon = weapon;
        }
    }

    void Reload()
    {
        if (reloadPrefab.activeSelf)
        {
            reloadPrefab.SetActive(false);
        }
    }
}
