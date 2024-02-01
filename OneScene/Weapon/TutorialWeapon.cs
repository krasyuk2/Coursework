using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWeapon : MonoBehaviour
{
  private Animator weaponAnimator;
    public GameObject prefabBullet;
    public GameObject spawn; 


    private GameObject _spawnBullet;
    private GameObject _player;
    public AudioSource FireSound;


    public float fireRate;
    public int Damage;
    public float bulletForce;
       
    public float FireTime;

    [Range(0f, 2f)] public float spawnDistance = 1f;
    private CameraGame _cameraGame;

    private bool isPlaySoundNoAmmo = true;


    private GameObject _cursor;
    //Fire 2



    delegate void CountBulletTrigger();
    private CountBulletTrigger _countBulletTrigger;
    private int calcFire = 0;
    private CountBulletTriggerWeapon _countBulletTriggerWeapon;
    
    private void Awake()
    {
        _spawnBullet =
            transform.GetChild(0)
                .gameObject; // Когда будешь цеплять скрипт к другим пушкам помни что точка спавна пуль первая в иерахии
        _cameraGame = FindObjectOfType<CameraGame>();
        _player = GameObject.FindWithTag("Player");


        weaponAnimator = GetComponent<Animator>();
   
        _cursor = GameObject.FindWithTag("Cursor");


    }
 
    
    private void Update()
    {
   
        
            if (FireTime <= 0)
            {
                Fire();
             
            }
            else
            {
                weaponAnimator.SetBool(Animator.StringToHash("Fire"), false);
                fire = false;
                FireTime -= Time.deltaTime;
            }
        
        
        if (FireTime < 0) FireTime = 0;




    }



    public bool fire = false;
    void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            SpawnBulletFire();
            weaponAnimator.SetBool(Animator.StringToHash("Fire"), true);
            FireSound.Play();
                
            
            FireTime = fireRate; // Дабаляем время для паузы между выстрелами 
                
              
        }
        
    }
    
    void SpawnBulletFire()
    {
        GameObject bullet = SpawnFire();
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = DirFireAndSpeed();
    }
    
    public GameObject SpawnFire()
    {
        return Instantiate(prefabBullet, _spawnBullet.transform.position,
            _spawnBullet.transform.rotation);
    }
    public Vector2 DirFireAndSpeed()
    {
        // (_cameraGame.ray.direction * bulletForce);
        Vector2 dir = _cursor.transform.position - _spawnBullet.transform.position;
        return  dir.normalized  * bulletForce;
    }
    
   
  
   




   
}
