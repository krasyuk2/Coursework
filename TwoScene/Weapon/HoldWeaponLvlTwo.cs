using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldWeaponLvlTwo : MonoBehaviour
{
   private CameraGame _cameraGame;
   private GameObject _player;
   private ChangeWeapon _changeWeapon;
   [Range(0f,2f)]
   public float[] distance;

   private void Awake()
   {
      _cameraGame = FindObjectOfType<CameraGame>();
      _player = GameObject.FindWithTag("Player");
      _changeWeapon = GetComponent<ChangeWeapon>();
   }
       
   private void Update()
   {
      Move();
      LocalScale();
      Hold();
   }
    
   void Move()
   {
      transform.position = _player.transform.position;
   }
   void Hold()
   {
      Vector3 dir = _cameraGame.ray.direction;
      float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
      transform.rotation = Quaternion.Euler(0, 0, rotZ);
   }
   void LocalScale()
   {
      for (int i = 0; i < transform.childCount; i++)
      {
         float localScaleStart = Mathf.Abs(transform.GetChild(i).transform.localScale.x);
         transform.GetChild(i).transform.localScale = new Vector3(localScaleStart,
            _player.transform.localScale.x * localScaleStart, localScaleStart);
         transform.GetChild(i).localPosition = new Vector3(distance[i], 0, 0);
      }
   }

   public void EnableWeapon(bool enable)
   {
      _changeWeapon.currentWeapon.SetActive(enable);
      _changeWeapon.enabled = enable;
   }
}
