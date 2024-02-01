using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public GameObject deadSlime;

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
            GameObject dead = Instantiate(deadSlime, posDead, Quaternion.identity);
            dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            Destroy(gameObject);
        }
    }
}
