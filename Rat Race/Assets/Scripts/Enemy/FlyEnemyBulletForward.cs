using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyBulletForward : MonoBehaviour
{
    [SerializeField] private Vector3 dir;
    
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
