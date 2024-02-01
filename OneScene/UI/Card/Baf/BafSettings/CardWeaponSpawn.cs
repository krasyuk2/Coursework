using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardWeaponSpawn : MonoBehaviour
{
 
     public GameObject[] prefabCard;
     private GameObject _cursor;
     private CameraGame _cameraGame;
     private GameObject _player;

     private void Awake()
     {
          _cursor = GameObject.FindWithTag("Cursor");
          _cameraGame = FindObjectOfType<CameraGame>();
          _player = GameObject.FindWithTag("Player");
     }

     public IEnumerator Start()
     {
          _cursor.transform.position = _player.transform.position;
          UnityEngine.Cursor.lockState = CursorLockMode.Locked;
          _cursor.SetActive(false);
          yield return new  WaitForSecondsRealtime(3f);
          UnityEngine.Cursor.lockState = CursorLockMode.None;
          foreach (var i in prefabCard)
          {
               i.SetActive(true);
          }
          _cursor.SetActive(true);
     }
}
