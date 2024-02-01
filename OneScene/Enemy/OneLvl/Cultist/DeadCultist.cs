using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCultist : MonoBehaviour
{
    private Animator _animator;
    public AudioSource[] deadSound;
    bool isSound;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        if(isSound)   deadSound[Random.Range(0, deadSound.Length)].Play();
     
        
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
