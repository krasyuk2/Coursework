using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBulletDamage : MonoBehaviour
{
    public int countHits;
    public GameObject[] numberPrefab; //1-5
    private GameObject _player;
    public int[] Damage;
    private Weapon _weapon;
    public bool isStart;
    private bool saveDamage;
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void BafStart()
    {
        isStart = true;
    }

    private GameObject currentGo;

    private void Update()
    {
        if(currentGo != null) currentGo.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + 1.5f);
    }

    private bool isEnd;
    private int startDamage;
    public void StartBaf()
    {
        _weapon = FindObjectOfType<Weapon>();
            switch (countHits)
            {
                case (>2) and (< 4):
                    if(currentGo != null) Destroy(currentGo);
                    Create(numberPrefab[0],Damage[0]);
                    break;
                case (>7) and (< 9):
                    if(currentGo != null) Destroy(currentGo);
                    Create(numberPrefab[1],Damage[1]);
                    break;
                case (>12) and (< 14):
                    if(currentGo != null) Destroy(currentGo);
                    Create(numberPrefab[2],Damage[2]);
                    break;
                case (>17) and (< 19):
                    if(currentGo != null) Destroy(currentGo);
                    Create(numberPrefab[3],Damage[3]);
                    break;
                case (>22):
                    if (!isEnd)
                    {
                        if(currentGo != null) Destroy(currentGo);
                        Create(numberPrefab[4], Damage[4]);
                        isEnd = true;
                    }
                    break;
                case 0:
                    if (currentGo != null)
                    {
                        currentGo.GetComponent<Animator>().SetBool(Animator.StringToHash("Def"),true);
                        _weapon.Damage = startDamage;
                    }
                    break;
            }

         
       
        if (!saveDamage)
        {
            startDamage = _weapon.Damage;
            saveDamage = true;
        }
        
        
       
    }
    public void Create(GameObject go, int damage)
    {
       
        currentGo = Instantiate(go,
            new Vector2(_player.transform.position.x, _player.transform.position.y + 1.5f), Quaternion.identity);
        _weapon.Damage += damage;

    }
    
}
