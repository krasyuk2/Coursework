using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurretBlock : MonoBehaviour
{
    public int Heal;
    public int AmmoMax;
    public float PriceOneAmmo;
    public int Ammo;
    public GameObject textAmmo;
    private TMP_Text _textAmmo;
    public GameObject prefabDisplayHeal;
    public GameObject bulletPrefab;
    public float forceBullet;
    public int damage;
    public float distance;
    public float fireRate;
    
    private float _time;
    private Slider _healUi;
    private void Start()
    {
        Ammo = AmmoMax;
        SettingDisplayHeal();
        SettingDisplayAmmo();
        transform.parent = GameObject.Find("Turret").transform;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    

    void Update()
    {
        if (_time <= 0)
        {
            if (Ammo > 0)
            {
                Fire();
            }

            _time = fireRate;
        }
        else
        {
            _time -= Time.deltaTime;
        }

        if (Heal <= 0)
        {
            Destroy(gameObject);
        }
        DisplayHeal();
        DisplayAmmo();
        
     
    }

    public virtual void Fire()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < distance)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<BulletLvlTwo>().SetDamageWeapon(damage);
                bullet.GetComponent<Rigidbody2D>().velocity =
                    ((enemy.transform.position - transform.position).normalized) * forceBullet;
                Ammo--;

            }
        }
    }
    public void TakeDamage(int damage)
    {
        Heal -= damage;
    }

    private void SettingDisplayHeal()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTwo");
        GameObject healUI = Instantiate(prefabDisplayHeal, canvas.transform);
        healUI.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        _healUi = healUI.GetComponent<Slider>();
        _healUi.maxValue = Heal;

    }

    void SettingDisplayAmmo()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("CanvasTwo");
        GameObject textPrefab = Instantiate(textAmmo, canvas.transform);
        textPrefab.transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, transform.position.z);
        _textAmmo = textPrefab.GetComponent<TMP_Text>();

    }

    private void DisplayAmmo()
    {
        _textAmmo.text = Ammo + "";
    }
    
    private void DisplayHeal()
    {
        _healUi.maxValue = 40;
        _healUi.value = Heal;
    }

    public void ResetAmmo(int countAmmoAdd)
    {
        Ammo += countAmmoAdd;
    }
}
