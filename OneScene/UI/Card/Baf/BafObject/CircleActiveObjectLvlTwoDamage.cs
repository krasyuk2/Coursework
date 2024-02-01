using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleActiveObjectLvlTwoDamage : MonoBehaviour
{
    private void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            if (Vector2.Distance(transform.position, enemy.gameObject.transform.position) < 3f)
            {
                enemy.GetComponent<Enemy>().TakeDamage(10);
            }
        }

        StartCoroutine(WaitDelete());
    }

    IEnumerator WaitDelete()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
