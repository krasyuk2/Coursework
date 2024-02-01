using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSphereWeapon : MonoBehaviour
{
   
    private Vector2 max;
    private Vector2 min;
    private Camera _camera;


    private SetFireTo _setFireTo;
    private Freezing _freezing;
    private FourBullet _fourBullet;
    private SphereWeapon _sphereWeapon;
    
    
    
    public void Awake()
    {
        _setFireTo = FindObjectOfType<SetFireTo>();
        _freezing = FindObjectOfType<Freezing>();
        _fourBullet = FindObjectOfType<FourBullet>();
        _sphereWeapon = FindObjectOfType<SphereWeapon>();
        if (Camera.main != null) _camera = Camera.main;
 

    }
    
    
    private void Update()
    {
        
         min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
         max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
         Vector2 pos = transform.position;
         if (pos.x > max.x + 1 || pos.y > max.y + 1 || pos.x < min.x -1|| pos.y < min.y-1)
         {
             Destroy(gameObject);
        
         }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject != null)
            {
     
                if (_setFireTo.isFire) _setFireTo.SetFire(other.gameObject);
                if(_freezing.isFreeze) _freezing.Freeze(other.gameObject);
                if (_fourBullet.isStart)
                    _fourBullet.Baf(_sphereWeapon.bulletForce / 2f, other.gameObject.transform.position, _sphereWeapon.Damage / 2,
                        other.gameObject);
              
                other.gameObject.GetComponent<Enemy>().TakeDamage(_sphereWeapon.Damage);
                if(!_sphereWeapon.isPiercing) Destroy(gameObject);
            }

        }
    }
}
