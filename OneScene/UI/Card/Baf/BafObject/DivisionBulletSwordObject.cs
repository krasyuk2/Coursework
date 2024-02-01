using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class DivisionBulletSwordObject : MonoBehaviour
{
    private DivisionBulletSword _divisionBulletSword;
    private SwordWeapon _swordWeapon;
    
    public void Awake()
    {
        _divisionBulletSword = FindObjectOfType<DivisionBulletSword>();
        _swordWeapon = GetComponent<SwordWeapon>();
    }

    public List<GameObject> _bulletEnemyList = new List<GameObject>();
    private bool isFlag = false;

  


    private void OnTriggerStay2D(Collider2D other)
    {
        {
            if (other.CompareTag("EnemyFire"))
            {
                if (!_bulletEnemyList.Contains(other.gameObject))
                {
                    _bulletEnemyList.Add(other.gameObject);
                }
            }
        }
    }


    void Start()
    {
        
    }


    void Update()
    {
        if (_bulletEnemyList.Count > 0)
        {
            if (Input.GetButtonDown("Fire1") && _swordWeapon.FireTime <= 0 && _swordWeapon.isFire1) Division();
        }
    }

    private bool _isPlaySoundOne = true;
    void Division()
    {
        
        foreach (var bulletEnemy in _bulletEnemyList)
        {
            if (bulletEnemy != null)
            {
                if (_isPlaySoundOne)
                {
                    _divisionBulletSword.AudioSource.Play();
                    _isPlaySoundOne = false;
                }

                Instantiate(_divisionBulletSword.prefabAnimation, bulletEnemy.transform.position, Quaternion.identity);
                int angleRandom = Random.Range(-30, 30);
                for (int i = 0; i < 2; i++)
                {
                    if (i == 1) angleRandom *= -1;
                    Vector2 dir = Quaternion.AngleAxis(angleRandom, Vector3.forward) *
                                  -bulletEnemy.GetComponent<Rigidbody2D>().velocity;

                    GameObject bullet = Instantiate(_divisionBulletSword.prefabBullet, bulletEnemy.transform.position,
                        Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = dir * _divisionBulletSword.bulletForce;
                }
            }

            Destroy(bulletEnemy);
        }
        _bulletEnemyList.Clear();
        _isPlaySoundOne = true;



    }
}
