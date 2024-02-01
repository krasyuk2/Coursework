using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereShield : MonoBehaviour
{
    public GameObject prefabSphere;
    private GameObject _sphereShieldSpawn;
    public int damage;
    public int spawnCount;
    private DateSave _save;
    public float speedAngle = 50;
    public int indexLvl = 0;
    private void Awake()
    {
        _sphereShieldSpawn = GameObject.FindWithTag("SphereShieldSpawn");
        _save = FindObjectOfType<DateSave>();
    }

    public void LvlUp()
    {
        indexLvl++;
    }
    private void Start()
    {
        
        StartBafSphereShield();
    }

   

    public void AddSpeedAngle()
    {
        speedAngle += 25;
    }
    public void AddDamage()
    {
        damage += 10;
    }
    private List<float> _angleList = new List<float>() { 0, 180, 90, 180, 45, 90, 90, 90,22.5f,45,45,45,45,45,45,45,11.25f,22.5f,22.5f,22.5f,22.5f,22.5f,22.5f,22.5f,22.5f,22.5f,22.5f}; // ничего умнее не придумал
    public void StartBafSphereShield()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject sphere = Instantiate(prefabSphere, _sphereShieldSpawn.transform);
            if (_sphereShieldSpawn.transform.childCount > 1)
            {
                sphere.GetComponent<SphereShieldObject>().angle = _sphereShieldSpawn.transform
                                                                      .GetChild(
                                                                          _sphereShieldSpawn.transform.childCount - 2)
                                                                      .GetComponent<SphereShieldObject>().angle +
                                                                  _angleList[
                                                                      _sphereShieldSpawn.transform.childCount - 1];
            }
        }

        spawnCount = 1;
    }
    
}
