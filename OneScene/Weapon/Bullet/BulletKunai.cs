using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKunai : MonoBehaviour
{
    private Kunai _kunai;
    private Camera _camera;
    private Piercing _piercing;
    private void Awake()
    {
        _kunai = FindObjectOfType<Kunai>();
        if (Camera.main != null) _camera = Camera.main;
        _piercing = FindObjectOfType<Piercing>();
    }

    private void Update()
    {
        Vector2 min = _camera.ViewportToWorldPoint(new Vector3(0, 0));   
        Vector2 max = _camera.ViewportToWorldPoint(new Vector3(1f, 1f));
        Vector2 pos = transform.position;
        if (pos.x > max.x || pos.y > max.y || pos.x < min.x || pos.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_kunai.Damage);
            if(!_piercing.isPiercing)  Destroy(gameObject);
          
        }
    }
}
