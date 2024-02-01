using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public int Heal;
    public float speed = 1;
    public int Damage = 20;
    [HideInInspector]
    public GameObject _player;
    public TMP_Text textDamage;
    public GameObject criticalTextDamage;
    private Move _move;
    [HideInInspector]
    public Rigidbody2D _rigidbody2D;
    private GameObject _canvas;
    private Statistics _statistics;
    [HideInInspector]
    public Vector3 _localScaleStart;
    [HideInInspector]
    public Animator _animator;
    public Color colorDamage;
    private CountBulletTrigger _countBulletTrigger;
    public int countBulletTrigger = 2;
    public bool isLocalScale = true;
    public bool isReverseLocalScale;
    
    
    
    public GameObject prefabExp;

    public void Awake()
    {
        _countBulletTrigger = FindObjectOfType<CountBulletTrigger>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _player = GameObject.FindWithTag("Player");
        _move = FindObjectOfType<Move>();
        _canvas = GameObject.FindWithTag("CanvasTwo");
        _animator = GetComponent<Animator>();
        _localScaleStart = transform.localScale;
        _statistics = FindObjectOfType<Statistics>();
        if(_countBulletTrigger != null) _bulletTrigger += _countBulletTrigger.AddDelegateTrigger;
       

    }

    public void Update()
    {
        SpawnExp();
        CalcCountBulletTrigger();

    }

    public void FixedUpdate()
    {
        Move();
        if(isLocalScale)  LocalScale();
       
        
    }
    public void LocalScale()
    {
        int reverse = 1;
        if (isReverseLocalScale)
        {
            reverse = -1;
        }
        int localScale;
        if (_player.transform.position.x < transform.position.x) localScale = 1;
        else localScale = -1;
        transform.localScale = new Vector3(_localScaleStart.x * localScale * reverse, _localScaleStart.y, _localScaleStart.z);
    }
    void Move()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime); // сталкивается с калайдерами 
        Vector2 pos = (_player.transform.position - transform.position).normalized; // не сталкиваеться 
            _rigidbody2D.velocity = pos * speed;
    }
    public void TakeDamage(int damage)
    {
        if(_statistics != null)_statistics.Damage += damage;
        if(damage != 0) TextTakeDamage(damage);
        
        if (Heal - damage > 0 && damage != 0)
        {
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = colorDamage;
                StartCoroutine(WhileTime());
            }
        }
        
        Heal -= damage;
    }
    public void CriticalTakeDamage(int damage)
    {
        if(_statistics != null) _statistics.Damage += damage;
        CriticalTextTakeDamage(damage);
        if (Heal - damage > 0)
        {
            if (gameObject.GetComponent<SpriteRenderer>() != null)
            {
                gameObject.GetComponent<SpriteRenderer>().material.color = colorDamage;
                StartCoroutine(WhileTime());
            }
        }
        Heal -= damage;
    }

    IEnumerator WhileTime()
    {
        
        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.white;
        

    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _move.TakeDamage(Damage);
        }

        if (other.CompareTag("Bullet"))
        {
            countBulletTrigger++;
        }
    }

    private float _timeRate = 0.5f;
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_timeRate <= 0)
            {
                _move.TakeDamage(Damage);
                _timeRate = 0.5f;
            }
            else
            {
                _timeRate -= Time.deltaTime;
            }
        }
    }
    void TextTakeDamage(int damage)
    {
        TMP_Text text = Instantiate(textDamage, _canvas.transform).GetComponent<TMP_Text>();
        if (text.gameObject != null && gameObject  != null)
        {
            text.gameObject.transform.position = gameObject.transform.position;
            text.text = damage + " ";
        }
    }
    void CriticalTextTakeDamage(int damage)
    {
        GameObject criticalText = Instantiate(criticalTextDamage, _canvas.transform);
        TMP_Text text = criticalText.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
        criticalText.transform.position =
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y +2f);
        text.text = damage + " ";
    }


    public delegate void BulletTrigger(Enemy enemy);
    public BulletTrigger _bulletTrigger;
    
    
    void CalcCountBulletTrigger()
    {
        if (countBulletTrigger == 2)
        {
            _bulletTrigger?.Invoke(this);
            countBulletTrigger = 0;
        }
    }

    private bool isSpawnExp = true;
    
    void SpawnExp()
    {
        if (Heal <= 0)
        {
            if (isSpawnExp)
            {
                if(_statistics != null)_statistics.Kills++;
                if(prefabExp != null )Instantiate(prefabExp, transform.position,Quaternion.identity);
                isSpawnExp = false;
            }
        }
    }

    
 
    
}
