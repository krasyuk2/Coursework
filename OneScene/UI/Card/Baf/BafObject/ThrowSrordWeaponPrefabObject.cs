using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSrordWeaponPrefabObject : MonoBehaviour
{
    private ThrowSwordWeapon _throwSwordWeapon;
    private Rigidbody2D _rigidbody;
    private Camera _camera;
    private Animator _animator;
    public AudioSource sound;

    

    public void Awake()
    {
        _throwSwordWeapon = FindObjectOfType<ThrowSwordWeapon>();
        _rigidbody =transform.parent.GetComponent<Rigidbody2D>();
        if (Camera.main != null) _camera = Camera.main;
        _animator = transform.parent.GetComponent<Animator>();

    }


    private bool isStop;
    private Vector2 posStop;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (_throwSwordWeapon.lvl > 0)
            {
                other.GetComponent<Enemy>().TakeDamage(_throwSwordWeapon.Damage);
            }
            
        }

        if (other.CompareTag("Wall"))
        {
            isStop = true;
            posStop = transform.parent.transform.position;
            _animator.SetBool(Animator.StringToHash("Throw"), false);
            sound.Play();
            isStopOne = true;
        }
        

    }

    private Vector2 posSword;
    private bool isStopOne;
    public void Update()
    {
        Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));
        Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        Vector2 pos = transform.parent.transform.position;
        if (!isStopOne)
        {
            if (pos.x > max.x - 2.5f || pos.y > max.y - 2.5f || pos.x < min.x + 2.5f || pos.y < min.y + 2.5f)
            {
                posStop = transform.parent.transform.position;
                _rigidbody.velocity = Vector2.zero;
                sound.Play();
                _animator.SetBool(Animator.StringToHash("Throw"), false);
                isStop = true;
                isStopOne = true;
            }
        }


        if (isStop) transform.parent.transform.position = posStop;
        if (isStop)
        {
            if (Input.GetButtonDown("Fire1")) isStop = false;
        }


        _throwSwordWeapon.isStop = isStop;

    }
}
