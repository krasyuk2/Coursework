using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    private GameObject _player;
    private Animator _animator;
    public float distance;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) < distance)
        {
            EnableShadow(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                _animator.SetBool(Animator.StringToHash("Iron"),true);
                StartCoroutine(WaitDestroyAnimation());
            }
        }
        else
        {
            EnableShadow(false);
        }
    }

    void EnableShadow(bool temp) => transform.GetChild(0).gameObject.SetActive(temp);

    IEnumerator WaitDestroyAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool(Animator.StringToHash("Iron"),false);

    }
}
