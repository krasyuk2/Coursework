using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{

   private DialogManager _dialogManager;
   private GameObject _player;
   public float Distance;
   public Color colorTrigger;
   private void Awake()
   {
      _player = GameObject.FindGameObjectWithTag("Player");
      _dialogManager = FindObjectOfType<DialogManager>();
   }

   private int count;
   public List<GameObject> ReadyDialogNpc = new List<GameObject>();
   public GameObject currentNpcDialog;

   private void Update()
   {
      GameObject[] go = GameObject.FindGameObjectsWithTag("Npc");

      foreach (var npc in go) // Заполнение тех кто входит в радус диалога 
      {
         if (Vector2.Distance(_player.transform.position, npc.transform.position) < Distance)
         {
            if(!ReadyDialogNpc.Contains(npc))ReadyDialogNpc.Add(npc);
            
         }
         else
         {
           
               //npc.transform.GetChild(0).gameObject.SetActive(false);
               npc.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
               //npc.GetComponent<SpriteRenderer>().material.color = Color.white;
            

            if (ReadyDialogNpc.Contains(npc)) ReadyDialogNpc.Remove(npc);
      
         }
      }
      float distance = 200;
      
      foreach (var npc in ReadyDialogNpc) // Находим ближайшего из тех кто в радиусе
      {
         float dis = Vector2.Distance(npc.transform.position, _player.transform.position);
         if (dis < distance)
         {
            distance = dis;
            currentNpcDialog = npc;
         }
      }
      if (currentNpcDialog != null) // Если обьект есть то красив в белый другие и можем начать с ним диалог
      {
         foreach (var npc in ReadyDialogNpc)
         {
            if (npc != currentNpcDialog)
            {
               currentNpcDialog.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
               //currentNpcDialog.transform.GetChild(0).gameObject.SetActive(false);
               // npc.GetComponent<SpriteRenderer>().material.color = Color.white;
            }
         }

         currentNpcDialog.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
         //currentNpcDialog.transform.GetChild(0).gameObject.SetActive(true);
         //currentNpcDialog.GetComponent<SpriteRenderer>().material.color = colorTrigger;
         
         if (!_dialogManager.isStartDialog)
         {
            if (Input.GetKeyDown(KeyCode.E))
            {
               _dialogManager.StartDialog(currentNpcDialog.GetComponent<Dialog>());
            }
         }
         else
         {
            if (Input.GetKeyDown(KeyCode.E))
            {
               _dialogManager.DisplayNextSentence();
            }
         }
      }
      if (ReadyDialogNpc.Count == 0) // если в радиусе никого то теряем обьект
      {
         currentNpcDialog = null;
      }

   }





}

