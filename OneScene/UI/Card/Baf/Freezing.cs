using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezing : MonoBehaviour
{
    public bool isFreeze;
    public float timeFreezing = 3f;
    public Color colorFreeze;
    public void StartBaf()
    {
        isFreeze = true;
    }

    public void AddTimeFreezing()
    {
        timeFreezing += 2;
    }
    
    public void Freeze(GameObject enemy)
    {
        Enemy en = enemy.GetComponent<Enemy>();
        float startSpeed = en.speed;
        en.speed /= 2;
        enemy.GetComponent<SpriteRenderer>().material.color = colorFreeze;

        StartCoroutine(UpdateSpeed(en, startSpeed));
       
    }
    
    IEnumerator UpdateSpeed(Enemy enemy, float startSpeed)
    {
        yield return new WaitForSeconds(timeFreezing);
        if (enemy != null)
        {
            enemy.GetComponent<SpriteRenderer>().material.color = Color.white;
            enemy.speed = startSpeed;
        }
    }
}
