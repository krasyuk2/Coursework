using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shup : MonoBehaviour
{
    private List<GameObject> _player = new List<GameObject>();
    private Cultist _cultist;

    private void Awake()
    {
        _cultist = FindObjectOfType<Cultist>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) _player.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player")) _player.Remove(other.gameObject);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.75f);
        if(_player.Count > 0) _player[0].GetComponent<Move>().TakeDamage(_cultist.Damage);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
