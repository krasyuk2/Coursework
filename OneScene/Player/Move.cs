using System;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Move : MonoBehaviour
{
    public int MaxHeal = 100;
    public int Heal = 20;
    public int AddHealShield = 0;
    public float Speed = 1;
    private Rigidbody2D _rigidbody2D;
    private GameObject _cursor;
    private float invulnerabilityTime = 2f;
    private float invulnerabilityRate = 2f;
    public GameObject textDamage;
    private GameObject _canvas;
    private Vector3 localScale;
    public bool isStartUpAnimation;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _cursor = GameObject.FindWithTag("Cursor");
        _canvas =  GameObject.FindWithTag("CanvasTwo");
        _animator = GetComponent<Animator>();
        
        if(isStartUpAnimation) _animator.SetBool(Animator.StringToHash("StartUp"),true);
    }


    private void Update()
    {
        Invulnerability();
        LocalScalePlayer();
  

    }

    private void FixedUpdate()
    {
       MovePlayer();
      
    }

    void MovePlayer()
    {
        float vertical = Input.GetAxis("Debug Vertical");
        float horizontal = Input.GetAxis("Debug Horizontal");
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)) horizontal = 0;
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)) vertical = 0;
     
        _rigidbody2D.velocity = new Vector2(horizontal, vertical).normalized * Speed;
        
    }

    void LocalScalePlayer()
    {
       
        if (_cursor.transform.position.x > transform.position.x)
        {
            
            localScale = new Vector3(1, 1, 1);
        }
        else
        {
            localScale = new Vector3(-1, 1, 1);
        }
        transform.localScale = localScale;
    }

    public bool isDamage = true; // true - можем получить урон;
    public void TakeDamage(int damage)
    {
        if (isDamage)
        {
            Heal -= damage;
            if (AddHealShield - damage < 0)
            {
                AddHealShield = 0;
            }
            else
            {
                AddHealShield -= damage;
            }
            TextTakeDamage(damage,Color.white);
            invulnerabilityTime = invulnerabilityRate;
            isDamage = false;
        }
        
    }
    public void TakeHeal(int addHeal)
    {
        if (Heal < MaxHeal)
        {
            if (Heal + addHeal > MaxHeal)
            {
                int tempHeal = MaxHeal - Heal;
                Heal += tempHeal;
                TextTakeDamage(addHeal,Color.green,'+');
                
            }
            else
            {
                Heal += addHeal;
                TextTakeDamage(addHeal,Color.green,'+');
            }
        }
        
    }
    void Invulnerability()
    {
        if (invulnerabilityTime <= 0)
        {
            isDamage = true;
          
        }
        else invulnerabilityTime -= Time.deltaTime;
    }
    void TextTakeDamage(int damage, Color color,char addOrMin = ' ')
    {
        TMP_Text text = Instantiate(textDamage, _canvas.transform).GetComponent<TMP_Text>();
        text.gameObject.transform.position = gameObject.transform.position;
        text.color = color;
        text.text = addOrMin + "" + damage + " ";
    }
    
    
}
