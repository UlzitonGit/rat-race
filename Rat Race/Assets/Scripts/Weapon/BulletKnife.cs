using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnife : MonoBehaviour
{
    private GameObject knifeScript;
    Knife knife;
    private Rigidbody rb;
    [SerializeField] GameObject hitVfx;
    // Start is called before the first frame update
    void Start()
    {
        knifeScript = GameObject.FindWithTag("Player");
        knife = knifeScript.GetComponent<Knife>();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        FlyBullet();
    }

    private void FlyBullet()
    {
        rb.AddForce(transform.forward * knife.speedBullet);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(hitVfx, transform.position, transform.rotation);
        }
    }
}
