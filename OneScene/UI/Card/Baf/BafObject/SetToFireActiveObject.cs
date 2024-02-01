using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetToFireActiveObject : MonoBehaviour
{
      public GameObject posEnd;
      public float Speed = 1;
      private SetToFireActive _setToFireActive;
      private GameObject _cursor;
      
      private void Awake()
      {
         _setToFireActive = FindObjectOfType<SetToFireActive>();
         _cursor = GameObject.FindWithTag("Cursor");
      }
      

      private bool isDamage = true;
      private IEnumerator Start()
      {
         if (_cursor.transform.position.x < transform.position.x)
         {
            var localScaleParent =transform.parent.gameObject.transform.localScale;
            transform.parent.gameObject.transform.localScale =
               new Vector3(localScaleParent.x, localScaleParent.y * -1, localScaleParent.z);
         }  
         yield return new WaitForSeconds(0.5f);
         isDamage = false;

      }
   
      private void Update()
      {
         transform.position = Vector2.MoveTowards(transform.position, posEnd.transform.position, Time.deltaTime * Speed);
         
      }
   
   
      private void OnTriggerEnter2D(Collider2D other)
      {
         if (other.CompareTag("Enemy") && isDamage)
         {
            
            other.gameObject.GetComponent<Enemy>().TakeDamage(_setToFireActive.Damage);
            StartCoroutine(SetToFire(other.gameObject.GetComponent<Enemy>()));

         }
      }

      IEnumerator SetToFire(Enemy enemy)
      {
        
         for (int i = 0; i < 5; i++)
         {
            if (enemy != null)
            {
               enemy.TakeDamage(_setToFireActive.DamageOnSecond);
               yield return new WaitForSeconds(0.3f);
            }
         
         }
      }
     
}
