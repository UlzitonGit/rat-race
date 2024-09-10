using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float _Xspeed = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float _Yspeed = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.velocity = new Vector3(_Xspeed, rb.velocity.y, _Yspeed);
        if(rb.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(_Xspeed,0 , _Yspeed));
        }
    }
}
