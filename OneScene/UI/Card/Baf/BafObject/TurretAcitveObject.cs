using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretAcitveObject : MonoBehaviour
{
    private TurretActive _turretActive;
    private Vector2 _posSpawnBullet;
    private float _timeLive;
    private float _timeAttack;
    private GameObject _canvas;


    private void Awake()
    {
        _turretActive = FindObjectOfType<TurretActive>();
        _posSpawnBullet = transform.GetChild(0).transform.position;
        _canvas = GameObject.FindWithTag("CanvasTwo");
    }

    private Slider _slider;
    private bool _isDelete;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.65f);
        _isDelete = true;
        _timeLive = _turretActive.timeRateLive;
        GameObject timeBaf = Instantiate(_turretActive.timerBaf, _canvas.transform);
        _slider = timeBaf.GetComponent<Slider>();
        _slider.maxValue = _turretActive.timeRateLive;
        timeBaf.transform.position = new Vector2(_posSpawnBullet.x, _posSpawnBullet.y + 0.75f);
    }


    void Update()
    {
        if (_timeLive > 0)
        {
            _slider.value = _timeLive;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (!_turretActive.isLvlOne)
            {
                if (_timeAttack <= 0)
                {
                    Standart(enemies);
                }
                
               
            }
            else
            {
                if (_timeAttack <= 0)
                {
                    LvlOneAttack(enemies);
                   
                }
           
            }
            _timeAttack -= Time.deltaTime;
            _timeLive -= Time.deltaTime;
        }

        if (_isDelete)
        {
            if (_timeLive <= 0)
            {
                Destroy(_slider.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void Attack(GameObject en)
    {
        Vector2 dir = ((Vector2)en.transform.position - _posSpawnBullet).normalized;
        GameObject bullet = Instantiate(_turretActive.prefabBullet, _posSpawnBullet, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * _turretActive.bulletForce;
        
    }

    void Standart(GameObject[] enemies)
    {
        GameObject en = null;
        float temp = 100;
        foreach (var enemy in enemies)
        {
            float dis = Vector2.Distance(_posSpawnBullet, enemy.transform.position);
            if (dis < _turretActive.distance)
            {
                if (temp > dis)
                {
                    temp = dis;
                    en = enemy;
                }
            }
        }

        if (_timeAttack <= 0)
        {
            if (en != null)
            {
                Attack(en);
                _timeAttack = _turretActive.timeRateAttack;
            }
        }
       
    }

    void LvlOneAttack(GameObject[] enemies)
    {
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < _turretActive.distance)
            {
                if (_timeAttack <= 0)
                {
                    Attack(enemy);
                }
            }
        }
        _timeAttack = _turretActive.timeRateAttack;
    }
}
    