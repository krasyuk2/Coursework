using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossLoot : MonoBehaviour
{
    private Enemy _enemy;
    public GameObject prefabBoxLoot;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (_enemy.Heal <= 0)
        {
            Instantiate(prefabBoxLoot, transform.position, Quaternion.identity);
        }
    }
}
