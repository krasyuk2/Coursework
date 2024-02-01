using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public GameObject prefabSphere;
    private GameObject _sphereShieldSpawn;
    public int damage;
    public bool isStart;
    private float spawnRate = 5f;
    private float _time;
    
    private void Awake()
    {
        _sphereShieldSpawn = GameObject.Find("_Diana_");
    }

    public void StartBaf()
    {
        isStart = true;
    }

    private void Update()
    {
        if (isStart)
        {
            if (_sphereShieldSpawn.transform.childCount == 0)
            {
                if (_time <= 0)
                {
                    StartBafSphereShield();
                    _time = spawnRate;
                }
                else
                {
                    _time -= Time.deltaTime;
                }
            }
        }
    }

    private List<float> _angleList = new List<float>() { 0, 180, 90,180};
    public void StartBafSphereShield()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject sphere = Instantiate(prefabSphere, _sphereShieldSpawn.transform);
            if (_sphereShieldSpawn.transform.childCount > 1)
            {
                sphere.GetComponent<DianaObject>().angle = _sphereShieldSpawn.transform
                                                                      .GetChild(
                                                                          _sphereShieldSpawn.transform.childCount - 2)
                                                                      .GetComponent<DianaObject>().angle +
                                                                  _angleList[
                                                                      _sphereShieldSpawn.transform.childCount - 1];
            }
        }
        
    }
}
