using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector2 max;
    private Vector2 min;
    private Camera _camera;
    public Weapon _weapon;

    public int Damage;
    private SetFireTo _setFireTo;
    private Freezing _freezing;
    private AddBulletDamage _addBulletDamage;
    private FourBullet _fourBullet;
    private CriticalDamage _criticalDamage;
    private ReturnBullet _returnBullet;
    private bool isEnemy;
    public TrailRenderer _trailRenderer;
    
    
    public void Awake()
    {
        _setFireTo = FindObjectOfType<SetFireTo>();
        _freezing = FindObjectOfType<Freezing>();
        _addBulletDamage = FindObjectOfType<AddBulletDamage>();
        _fourBullet = FindObjectOfType<FourBullet>();
        _criticalDamage = FindObjectOfType<CriticalDamage>();
        _returnBullet = FindObjectOfType<ReturnBullet>();
        if (Camera.main != null) _camera = Camera.main;
        
        


    }
    

    private void Start()
    {

        _weapon = FindObjectOfType<Weapon>();
        Damage = _weapon.Damage;
        gameObject.transform.localScale += new Vector3(_weapon.SizeBullet, _weapon.SizeBullet, _weapon.SizeBullet);
        if (_trailRenderer != null)
        {
            _trailRenderer.startWidth = gameObject.transform.localScale.x - gameObject.transform.localScale.x / 4f;
            _trailRenderer.endWidth = 0;
        }

    }

    private void Update()
    {
        
         min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
         max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
         Vector2 pos = transform.position;
         if (pos.x > max.x + 1 || pos.y > max.y + 1 || pos.x < min.x -1|| pos.y < min.y-1)
         {
             Destroy(gameObject);
             if (!isEnemy) _addBulletDamage.countHits = 0;
             if(_addBulletDamage.isStart) _addBulletDamage.StartBaf();
         }
    }

    private bool isOne = true;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject != null)
            {
                isEnemy = true;
                _addBulletDamage.countHits++;
                if (_setFireTo.isFire) _setFireTo.SetFire(other.gameObject);
                if(_freezing.isFreeze) _freezing.Freeze(other.gameObject);
                if(_addBulletDamage.isStart) _addBulletDamage.StartBaf();
                if (_fourBullet.isStart)
                    _fourBullet.Baf(_weapon.bulletForce / 2f, other.gameObject.transform.position, Damage / 2,
                        other.gameObject);
                if(_criticalDamage.startBaf && _criticalDamage.RandomDamage())other.gameObject.GetComponent<Enemy>().CriticalTakeDamage(Damage * 2);
                if (_returnBullet.startBaf && isOne && _returnBullet.RandomReturnBullet())
                {
                    _weapon.magazine = _weapon.maxMagazine;
                    isOne = false;
                }
                other.gameObject.GetComponent<Enemy>().TakeDamage(Damage);
                
            }

        }
    }
    
}
