using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowSwordWeaponObject : MonoBehaviour
{
    private GameObject _prefabSword;
    private SwordWeapon _swordWeapon;
    private SpriteRenderer _spriteRendererSwordWeapon;
    private ThrowSwordWeapon _throwSwordWeapon;
    private GameObject _player;
    private bool _isWeaponInHand = true;
    private GameObject _cursor;
    private GameObject _lineRendererGameObject;
    private void Awake()
    {
        _swordWeapon = GetComponent<SwordWeapon>();
        _spriteRendererSwordWeapon = _swordWeapon.GetComponent<SpriteRenderer>();
        _throwSwordWeapon = FindObjectOfType<ThrowSwordWeapon>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _cursor = GameObject.FindGameObjectWithTag("Cursor");
    }

    void Start()
    {
        _prefabSword = _throwSwordWeapon.prefabSword;
        _lineRendererGameObject = _throwSwordWeapon.lineRenderObject;
        
    }

    private GameObject sword;
    private Rigidbody2D _rbSword;
    private bool _isReturnSword;
    void FireTwo()
    {
        if (_isWeaponInHand)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                DisabledWeapon();
                sword = Instantiate(_prefabSword, _player.transform.position,Quaternion.identity);
                _rbSword = sword.GetComponent<Rigidbody2D>();
                _rbSword.velocity = 
                (_cursor.transform.position - _swordWeapon.gameObject.transform.position).normalized *
                    _throwSwordWeapon.swordForce;
                _isWeaponInHand = false;
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                var lineRenderObject = Instantiate(_lineRendererGameObject);
                var line = lineRenderObject.GetComponent<LineRenderer>();
                line.SetPosition(0,_player.transform.position);
                _player.transform.position = sword.transform.position;
                line.SetPosition(1,_player.transform.position);
                if(_throwSwordWeapon.lvl > 1) AttackLvlTwo();
                Destroy(sword);
                EnabledWeapon();
                _isWeaponInHand = true;
                isTime = false;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                _isReturnSword = true;
            }
        }

        if (_throwSwordWeapon.isStop)
        {
            sword.transform.position = Vector3.MoveTowards(sword.transform.position, _player.transform.position, Time.deltaTime * _throwSwordWeapon.speedReturnSword);
            if (Vector2.Distance(sword.transform.position, _player.transform.position) < 1f)
            {
                EnabledWeapon();
                Destroy(sword);
                _isWeaponInHand = true;
                _isReturnSword = false;
                time = _throwSwordWeapon.TimeRate;
                _throwSwordWeapon.isStop = false;
                isTime = false;

            }
        }
    }

    
    public void ReturnSword()
    {
        sword.GetComponent<Animator>().SetBool(Animator.StringToHash("Throw"),true);
        _rbSword.velocity = Vector2.zero;
        sword.transform.position = Vector3.MoveTowards(sword.transform.position, _player.transform.position, Time.deltaTime * _throwSwordWeapon.speedReturnSword);
        if (Vector2.Distance(sword.transform.position, _player.transform.position) < 1f)
        {
            EnabledWeapon();
            Destroy(sword);
            _isWeaponInHand = true;
            _isReturnSword = false;
            isTime = false;

        }

    }

    private float time = 0;
    private bool isTime = true;
    void Update() 
    {
        if (time <= 0)
        {
            FireTwo();
          
            if (twoLvlPrefab != null)
            {
                twoLvlPrefab.transform.position = _player.transform.position;
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
        if (_isReturnSword) ReturnSword();
        
        if (_isWeaponInHand && !isTime)
        {
            time = _throwSwordWeapon.TimeRate;
            isTime = true;
        }
  

    }

    private void EnabledWeapon()
    {
        _swordWeapon.isFire1 = true;
        _spriteRendererSwordWeapon.enabled = true;
    }

    void DisabledWeapon()
    {
        _swordWeapon.isFire1 = false;
        _spriteRendererSwordWeapon.enabled = false;
    }

    private GameObject twoLvlPrefab;
    void AttackLvlTwo()
    {

        if (twoLvlPrefab != null)
        {
             Destroy(twoLvlPrefab);
        }
        else
        {
            twoLvlPrefab = Instantiate(_throwSwordWeapon.prefabLvlTwoAttack,_player.transform.position,Quaternion.identity);
        }
        
        StartCoroutine(DestroyLvlTwoPrefab());
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(_player.transform.position, enemy.transform.position) <
                _throwSwordWeapon.distanceAttack)
            {
               enemy.GetComponent<Enemy>().TakeDamage(_throwSwordWeapon.damageTwoAttack);
            }
        }
    }

    private IEnumerator DestroyLvlTwoPrefab()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(twoLvlPrefab);
    }
}
