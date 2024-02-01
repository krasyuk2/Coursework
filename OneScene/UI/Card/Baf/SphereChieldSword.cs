using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SphereChieldSword : MonoBehaviour
{
    public GameObject prefabSphereShield;
    public GameObject prefabAttack;
    public GameObject prefabAttackLvlUp;
    public int damage;
    public float distanceAttack;
    
    private GameObject _player;
    private Move _move;
    public float timeRate = 10;
    private float _time;
    private GameObject _attack;
    private float _timeLiveShield = 6f;
    private int _coefficientAddHeal = 5;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        
        _move = FindObjectOfType<Move>();
    }
    
    public void OneBaf() // Наносит только урон
    {
        damage += 20;
        prefabAttack = prefabAttackLvlUp;
        prefabSphereShield = null;
        
    }

    private bool _lvlUpShield;
    public void TwoBaf() // дает просто щит 
    {
        _timeLiveShield += 5f;
        _coefficientAddHeal += 5;
        prefabAttack = null;
        _lvlUpShield = true;


    }

    public void TreeBaf() // Делает все как прежде но чуть лучше
    {
        damage += 5;
        _timeLiveShield += 2f;
    }
    
    public (float,float) Baf()
    {
       
        if (_time <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
           
                
                if (prefabAttack != null)
                {
                    _attack = Instantiate(prefabAttack);
                    StartCoroutine(WaitDelete());

                    GiveShield(Attack());
                }
                else
                {
                    GiveShield(10);
                }

                _time = timeRate;

            }
        }
        else _time -= Time.deltaTime;

        if (_attack != null)
        {
            _attack.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -1);
        }

        if (prefabSphereShield != null)
        {
            if (prefabSphereShield.activeSelf && _move.AddHealShield <= 0)
            {
                StartCoroutine(WaitDeleteSphere());
            }
        }

        return (_time,timeRate);
    }

  
    int healAdd;
    int Attack()
    {
        int count = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, _player.transform.position) < distanceAttack)
            {
                enemy.GetComponent<Enemy>().TakeDamage(damage);
                count++;
            }
        }

        return count;
    }

    void GiveShield(int count)
    {
        if (count != 0)
        {
            
            prefabSphereShield.SetActive(true);
            if (_lvlUpShield)
            {
                prefabSphereShield.GetComponent<Animator>().SetBool("LvlUp", true);
            }

            StartCoroutine(WaitShield());
        }
        healAdd = count * _coefficientAddHeal;
        _move.Heal += healAdd;
        _move.AddHealShield += healAdd;
        
        
    }

    IEnumerator WaitShield()
    {
        yield return new WaitForSeconds(_timeLiveShield);
        if (_move.AddHealShield != 0) _move.Heal -= _move.AddHealShield;
            _move.AddHealShield = 0;
            StartCoroutine(WaitDeleteSphere());

    }

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(0.45f);
        Destroy(_attack);

    }

    IEnumerator WaitDeleteSphere()
    {
        prefabSphereShield.GetComponent<Animator>().SetBool("Delete", true);
        yield return new WaitForSeconds(0.2f);
        prefabSphereShield.SetActive(false);
        
    }
}
