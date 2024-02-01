using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnom : Enemy
{
    public GameObject deadGnome;

    new void Update()
    {
        base.Update();
        Dead();
    }
    private  void Dead()
    {
        if (Heal <= 0)
        {
          
            Vector2 posDead = transform.position;
            GameObject dead = Instantiate(deadGnome, posDead, Quaternion.identity);
            dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            Destroy(gameObject);
        }
    }
}
