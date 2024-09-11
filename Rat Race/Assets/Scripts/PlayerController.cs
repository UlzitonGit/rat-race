using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 5f;
    EnemyDetecter enemyDetecter;
    public bool concentrate = false;
    [SerializeField] Animator animator;
    int enemyToConcentrate = 0;
    Vector3 moveDireciton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyDetecter = GetComponentInChildren<EnemyDetecter>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk();
        Concentration();
        Animation();
    }
    private void Walk()
    {
        float _Xspeed = Input.GetAxis("Horizontal") * moveSpeed;
        float _Yspeed = Input.GetAxis("Vertical") * moveSpeed;
        moveDireciton = new Vector3(_Xspeed, 0, _Yspeed);
        moveDireciton.Normalize();
        
        
        if (moveDireciton.magnitude > 0.1f && concentrate == false)
        {
           
            Quaternion toRot = Quaternion.LookRotation(moveDireciton.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, 360 * Time.deltaTime);
        }
        if (concentrate)
        {
            rb.velocity = new Vector3(_Xspeed, rb.velocity.y, _Yspeed);

        }
        else
        {
            rb.MovePosition(transform.position + (transform.forward * moveDireciton.magnitude)* moveSpeed * Time.deltaTime);

        }
    }
    private void Animation()
    {
        if (moveDireciton.magnitude >= 0.1) animator.SetBool("walk", true);
        if (moveDireciton.magnitude <= 0.1) animator.SetBool("walk", false);
        Vector3 localMove = transform.InverseTransformDirection(moveDireciton);
        animator.SetBool("Concentrate", concentrate);
        //animator.SetFloat("Input", localMove.magnitude);
        animator.SetFloat("Horizontal", localMove.z);
        animator.SetFloat("Vertical", localMove.x);
    }
    private void Concentration()
    {
        if (enemyDetecter.enemies.Count != 0 && concentrate == true)
        {
            if (enemyToConcentrate > enemyDetecter.enemies.Count - 1) enemyToConcentrate = 0;
            transform.LookAt(enemyDetecter.enemies[enemyToConcentrate].transform);
        }
        if (enemyDetecter.enemies.Count == 0 && concentrate == true)
        {
            enemyToConcentrate = 0;
            concentrate = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && enemyDetecter.enemies.Count != 0 && concentrate == false)
        {
            concentrate = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && concentrate == true)
        {
            enemyToConcentrate = 0;
            concentrate = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse2) && enemyDetecter.enemies.Count != 0)
        {

            if (enemyToConcentrate >= enemyDetecter.enemies.Count - 1) enemyToConcentrate = 0;
            else enemyToConcentrate++;
            print(enemyToConcentrate);

        }
    }
}
