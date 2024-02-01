using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire2 : MonoBehaviour
{
    private WizardBulletFire2Add _wizardBulletFire2Add;
    public GameObject bulletPrefab;
    public float bulletForce;

    private void Awake()
    {
        _wizardBulletFire2Add = FindObjectOfType<WizardBulletFire2Add>();
        
    }

    void Start()
    {
        for (int i = 0; i < _wizardBulletFire2Add.countBullet; i++)
        {
            var x = Mathf.Cos((360f * Mathf.Deg2Rad / _wizardBulletFire2Add.countBullet) * i);
            var y = Mathf.Sin((360f * Mathf.Deg2Rad / _wizardBulletFire2Add.countBullet) * i);
            Vector2 temp = new Vector2(x, y) + (Vector2)transform.position;
            Vector2 dir = temp - (Vector2)transform.position;
            GameObject bullet = Instantiate(bulletPrefab, temp, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletForce;
            
        }
        Destroy(gameObject);
    }
    
}
