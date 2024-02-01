using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DivisionBulletSword : MonoBehaviour
{
    public GameObject prefabBullet;
    public int Damage;
    public float bulletForce;
    public GameObject prefabAnimation;
    public AudioSource AudioSource;
    public void StartBar()
    {
        FindObjectOfType<SwordWeapon>().AddComponent<DivisionBulletSwordObject>();
    }

  
}
