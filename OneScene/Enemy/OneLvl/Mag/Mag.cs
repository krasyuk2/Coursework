using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mag : Enemy
{
    private Camera _camera;
    public GameObject deadMag;
    public float DistanceSpawn;
    public float DistanceSpawnMax;
    public AudioSource SoundTeleport;
    public float FireRate = 3;
    private float _fireTime;
    public int countBullet;
    public float radius;
    public float bulletForce;
    public GameObject bulletPrefab;
    private Vector2 spawnPos;

    new void Awake()
    {
        base.Awake();
        if (Camera.main != null) _camera = Camera.main;
      
    }
    void Start()
    {
        _fireTime = FireRate;
      
    }

    new void Update()
    {
        spawnPos = transform.GetChild(0).transform.position;
        base.Update();
        Move();
        FireKd();
        Dead();
    }

    new void FixedUpdate()
    {
        LocalScale();
    }
    private  void Dead()
    {
        if (Heal <= 0)
        {
          
            Vector2 posDead = transform.position;
            GameObject dead = Instantiate(deadMag, posDead, Quaternion.identity);
            dead.transform.localScale = new Vector3(transform.localScale.x, 1, 1);
            Destroy(gameObject);
        }
    }
    void FireKd()
    {
        if (_fireTime <= 0)
        {
            
            StartCoroutine(Fire());
            _fireTime = FireRate;
        }
        else
        {
            
            _fireTime -= Time.deltaTime;
            
        }
    }
    
    IEnumerator Fire()
    {
        _animator.SetBool(Animator.StringToHash("Fire"),true);
        for (int i = 0; i < countBullet; i++)
        {
            var x = Mathf.Cos((360f * Mathf.Deg2Rad / countBullet) * i) * radius;
            var y = Mathf.Sin((360f * Mathf.Deg2Rad / countBullet) * i) * radius;
            Vector2 temp = new Vector2(x, y) + (Vector2)spawnPos;
            Vector2 dir = temp - (Vector2)spawnPos;
            GameObject bullet = Instantiate(bulletPrefab, temp, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bulletForce;
            bullet.GetComponent<BulletEnemy>().Damage = Damage;
            yield return new WaitForSeconds(0.04f);
        }
        _animator.SetBool(Animator.StringToHash("Fire"),false);
        

        
    }
    void Move()
    {

        Vector3 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
        Vector3 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        Vector2 pos = transform.position;
        if (pos.x > max.x || pos.x < min.x || pos.y > max.y || pos.y < min.y)
        {
            Vector2 posSpawn = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
            if (Vector2.Distance(posSpawn, _player.transform.position) > DistanceSpawn &&
                Vector2.Distance(posSpawn, _player.transform.position) < DistanceSpawnMax) 
            {
                _animator.SetBool(Animator.StringToHash("Teleport"),true);
                StartCoroutine(ExitTeleport());
                transform.position = posSpawn;
                _fireTime = FireRate;
                StopCoroutine(ExitTeleport());
            }
        }
    }

    IEnumerator ExitTeleport()
    {
        yield return new WaitForSeconds(0.15f);
        SoundTeleport.Play();
        yield return new WaitForSeconds(0.035f);
        _animator.SetBool(Animator.StringToHash("Teleport"),false);
    }
}
