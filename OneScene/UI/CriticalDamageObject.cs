using UnityEngine;

public class CriticalDamageObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount < 2) Destroy(gameObject);
    }
}
