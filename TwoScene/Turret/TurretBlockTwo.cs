using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBlockTwo : TurretBlock
{
    public override void Fire()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject enemyMin = null;
        float tempDistance = 10000f;
        foreach (var enemy in enemies)
        {
            float temp = Vector2.Distance(enemy.transform.position, transform.position);
            if (temp < distance)
            {
                if (temp < tempDistance)
                {
                    tempDistance = temp;
                    enemyMin = enemy;
                }
            }
        }

        if (enemyMin != null)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletLvlTwo>().SetDamageWeapon(damage);
            bullet.GetComponent<Rigidbody2D>().velocity =
                (enemyMin.transform.position - transform.position).normalized * forceBullet;
            Ammo--;
        }
        
    }
}
