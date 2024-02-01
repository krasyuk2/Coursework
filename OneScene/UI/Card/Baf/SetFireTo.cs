using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFireTo : MonoBehaviour
{
    public int Damage;
    public bool isFire;
    public int CountSetFire = 5;
    public void StartBaf()
    {
        isFire = true;

    }
    
   public void SetFire(GameObject enemy)
    {
        Enemy en = enemy.GetComponent<Enemy>();
        StartCoroutine(FireTakeDamage(en));
        

    }
    IEnumerator FireTakeDamage(Enemy enemy)
    {
       
        for (int i = 0; i < CountSetFire; i++)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    public void AddDamageSetFire()
    {
        Damage+=2;
    }

    public void AddCountSetFire()
    {
        CountSetFire += 3;
    }
}
