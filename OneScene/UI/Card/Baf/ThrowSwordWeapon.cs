
using Unity.VisualScripting;
using UnityEngine;

public class ThrowSwordWeapon : MonoBehaviour
{
    public float swordForce = 10;
    public GameObject prefabSword;
    public GameObject lineRenderObject;
    public float speedReturnSword = 4f;
    public int Damage = 20;
    public int lvl = 0;
    public GameObject prefabLvlTwoAttack;
    public float distanceAttack;
    public int damageTwoAttack;
    public float TimeRate = 4f;
    public bool isStop;
    public void LvlUp()
    {
        lvl += 1;
        if (lvl == 3)
        {
            TimeRate = 1f;
        }
    }
    public void StartBaf()
    {
        var sword = FindObjectOfType<SwordWeapon>();

        FindObjectOfType<SwordWeapon>().AddComponent<ThrowSwordWeaponObject>();
        sword.isFire2 = false;
    }
    

}
