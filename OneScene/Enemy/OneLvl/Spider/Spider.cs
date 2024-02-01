using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spider : Enemy
{
    private Vector2 posDead;
    public GameObject DeadSpider;
    public float jumpForce = 10f;
    public float timeJumpRate = 0.3f;
    public float JumpDistance = 1f;
    [HideInInspector] public bool isJump;
    private float _timeJump;
    private float tempSpeed;
    
    
    new void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _timeJump = timeJumpRate;
        tempSpeed = speed;
    }


    new void Update()
    {
        base.Update();
        Dead();
        ChanceJump();
        Jump();

    }


    private void Dead()
    {
        if (Heal <= 0)
        {
          
            posDead = transform.position;
            GameObject dead = Instantiate(DeadSpider, posDead, Quaternion.identity);
            dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            Destroy(gameObject);
        }
    }

    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("Player"))
        {
            TakeDamage(20);
        }
        
    }

    private float TimeStart = 0.2f;
    private float TimeStop = 0.2f;
    void Jump()
    {
     
        if (isJump)
        {
            if (TimeStart <= 0) // задержка перед прыжком типо подготовка
            {
                speed = jumpForce;
                
                if (_timeJump <= 0)// время которые длиться прыжок 
                {
                    speed = 0;
                
                    if (TimeStop <= 0) // остановка после прыжка 
                    {
                        speed = tempSpeed;
                        isJump = false;
                    }
                    else
                    {
                        TimeStop -= Time.deltaTime;
                    }
                }
                else _timeJump -= Time.deltaTime;
            }
            else
            {
                TimeStart -= Time.deltaTime;
            }
        }
    }

    private bool chanceJump = true;
    void ChanceJump() 
    {
        if (Vector2.Distance(_player.transform.position, transform.position) < JumpDistance)
        {
          
            if (chanceJump) // сделано так чтобы проверка на прыжок была только раз не прыгнул ну и все и хватит 
            {
                int chance = Random.Range(1, 4);
                if (chance == 1)
                {
                    isJump = true;
                    TimeStart = 0.2f;
                    _timeJump = timeJumpRate;
                    TimeStop = 0.2f;
                    speed = 0;
                    
                }
                chanceJump = false;
            }
        }
    }
    


}
