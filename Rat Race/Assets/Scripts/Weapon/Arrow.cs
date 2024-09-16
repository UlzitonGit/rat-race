using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Vector3 dir;
   
    [SerializeField] private float speed;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
        
    }
    
}
