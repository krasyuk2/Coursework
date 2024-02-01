using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDead : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        int random = Random.Range(1, 5);
        _animator.SetInteger(Animator.StringToHash("Dead"),random);
        StartCoroutine(DestroyObject());
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
