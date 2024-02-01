using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : Enemy
{
    private Vector2 posDead;
    public GameObject deadBomb;

    public float Distance = 1;
    
    new void Update()
    {
        base.Update();
        Dead();
    }

     void Dead()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < Distance) Heal = 0;
        if (Heal <= 0)
        {
            speed = 0;
            posDead = transform.position;
            GameObject dead = Instantiate(deadBomb, posDead, Quaternion.identity);
            dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<DamageRadius>().Dead(Damage);
            Destroy(gameObject);
        }
    }
    new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.CompareTag("Player")) Heal = 0;
    }
}
