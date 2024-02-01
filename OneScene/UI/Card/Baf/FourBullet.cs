using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourBullet : MonoBehaviour
{
    public bool isStart;
    public GameObject prefabBullet;
    private FourBulletObject _fourBulletObject;
    public void StartBaf()
    {
        isStart = true;
    }

    public void Baf(float bulletForce, Vector2 pos, int Damage, GameObject enemy)
    {
        GameObject one = Instantiate(prefabBullet, pos, Quaternion.identity);
        one.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletForce;
        one.GetComponent<FourBulletObject>().Damage = Damage;
        one.GetComponent<FourBulletObject>().start = enemy;
        GameObject two = Instantiate(prefabBullet, pos, Quaternion.identity);
        two.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletForce;
        two.GetComponent<FourBulletObject>().Damage = Damage;
        two.GetComponent<FourBulletObject>().start = enemy;
        GameObject tree = Instantiate(prefabBullet, pos, Quaternion.identity);
        tree.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletForce;
        tree.GetComponent<FourBulletObject>().Damage = Damage;
        tree.GetComponent<FourBulletObject>().start = enemy;
        GameObject four = Instantiate(prefabBullet, pos, Quaternion.identity);
        four.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletForce;
        four.GetComponent<FourBulletObject>().Damage = Damage;
        four.GetComponent<FourBulletObject>().start = enemy;
    }
}
