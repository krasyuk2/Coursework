using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacteristicsWeapon : MonoBehaviour
{
    private Weapon _weapon;

    private SwordWeapon _swordWeapon;
    private Kunai _kunai;
    public int AddDamageValue;
    public float LowKdValue;
    public float CallDownValueFire2;
    public float LowReloadValue;
    public int AddBullet;
    public float AddSizeValue;
  
    private bool isWeapon;
    private void Update()
    {
       
        if (!isWeapon)
        {
            if (FindObjectOfType<Weapon>() != null)
            {
                _weapon = FindObjectOfType<Weapon>();
                isWeapon = true;
            }

            if (FindObjectOfType<SwordWeapon>())
            {
                _swordWeapon = FindObjectOfType<SwordWeapon>();
                isWeapon = true;
            }

            if (FindObjectOfType<Kunai>() != null)
            {
                _kunai = FindObjectOfType<Kunai>();
                isWeapon = true;
            }
        }
    }

   
   
    public void AddDamage()
    {
        if(_weapon != null) _weapon.Damage += AddDamageValue;
        if(_swordWeapon != null) _swordWeapon.Damage += AddDamageValue;
        if(_kunai != null) _kunai.Damage += AddDamageValue;
    }

    public void LowKd()
    {
        if(_weapon != null)_weapon.fireRate -= LowKdValue;
        if(_swordWeapon != null)_swordWeapon.fireRate -=LowKdValue;
        if(_kunai != null)_kunai.FireRate -= LowKdValue;;
    }

    public void LowCallDawnFire2()
    {
        if(_weapon != null)_weapon.CoolDawnFire2Rate -=CallDownValueFire2;
        if(_swordWeapon != null) _swordWeapon.CoolDawnFire2Rate -= CallDownValueFire2;
    }

    public void LowReload()
    {
        if(_weapon != null)_weapon.rechargeRate -= LowReloadValue;
    }

    public void AddMagazine()
    {
        if(_weapon != null) _weapon.maxMagazine += AddBullet;
    }

    public void AddSize()
    {
        if (_weapon != null) _weapon.SizeBullet += AddSizeValue;
    }


}
