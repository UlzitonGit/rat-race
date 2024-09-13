using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletKnife : MonoBehaviour
{
    private GameObject knifeScript;
    Knife knife;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        knifeScript = GameObject.Find("Hero");
        knife = knifeScript.GetComponent<Knife>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(TimeLife());
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
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            // тут типо - хп врагу, но пока врагов нету и ничего нету
        }
    }
    IEnumerator TimeLife()
    {
        yield return new WaitForSecondsRealtime(knife.timeLifeBullet);
        Destroy(gameObject);
    }
}
