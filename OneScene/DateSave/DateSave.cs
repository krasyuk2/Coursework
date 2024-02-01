using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateSave : MonoBehaviour
{
    private Move _player;
    private Ricochet _ricochet;
    private SphereShield _sphereShield;
    private SphereWeapon _sphereWeapon;
    private Sword _sword;
    private AddFireBullet _addFireBullet;
    private SetFireTo _setFireTo;
    private SwordLine _swordLine;
    private CircleBullet _circleBullet;
    private Pentagram _pentagram;
    private Book _book;

    private void Awake()
    {
        _player = FindObjectOfType<Move>();
        _ricochet = FindObjectOfType<Ricochet>();
        _sphereShield = FindObjectOfType<SphereShield>();
        _sphereWeapon = FindObjectOfType<SphereWeapon>();
        _sword = FindObjectOfType<Sword>();
        _addFireBullet = FindObjectOfType<AddFireBullet>();
        _setFireTo = FindObjectOfType<SetFireTo>();
        _swordLine = FindObjectOfType<SwordLine>();
        _circleBullet = FindObjectOfType<CircleBullet>();
        _pentagram = FindObjectOfType<Pentagram>();
        _book = FindObjectOfType<Book>();

    }

    void Start()
    {
        _player.Heal += (int)GetSave("PlayerHeal");
        _player.MaxHeal += (int)GetSave("PlayerHeal");
        _player.Speed += GetSave("PlayerSpeed");
        _ricochet.countRicochet += (int)GetSave("Ricochet");
        //_sphereShield.spawnCount += (int)GetSave("ShieldCount"); // наприсано в самом sphereShield
        _sphereShield.damage += (int)GetSave("ShieldDm");
        _sphereWeapon.fireRate -= GetSave("SphereWeaponKd");
        _sword.damage += (int)GetSave("SwordDm");
        _sword.spawnRate -= GetSave("SwordKd");
        _addFireBullet.CountBullet += (int)GetSave("BulletCount");
        _setFireTo.Damage += (int)GetSave("BulletFire");
        _swordLine.Damage += (int)GetSave("SwordLineDm");
        _swordLine.countSpawn += (int)GetSave("SwordLineCount");
        _circleBullet.countBullet += (int)GetSave("CircleCount");
        _pentagram.Damage += (int)GetSave("Pentagram");
        _book.Damage += (int)GetSave("BookDm");
        
        
       // SetSave("OneSceneLvl",20); 
    }
     
    public void SetSave(string nameId, float numSave)
    {
        PlayerPrefs.SetFloat(nameId, numSave);
    }

    public float GetSave(string nameId)
    {
        if (PlayerPrefs.HasKey(nameId)) return PlayerPrefs.GetFloat(nameId);
        else return 0;
    }
    
}
