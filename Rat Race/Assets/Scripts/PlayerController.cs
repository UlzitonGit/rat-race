using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed = 5f;
    EnemyDetecter enemyDetecter;
    bool concentrate = false;
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
        float _Xspeed = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float _Yspeed = Input.GetAxisRaw("Vertical") * moveSpeed;
        moveDireciton = new Vector3(_Xspeed, 0, _Yspeed);
        moveDireciton.Normalize();
        rb.velocity = new Vector3(_Xspeed, rb.velocity.y, _Yspeed);
        if (rb.velocity.magnitude > 0.1f && concentrate == false)
        {

            Quaternion toRot = Quaternion.LookRotation(moveDireciton, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, 360 * Time.deltaTime);
        }
    }
    private void Animation()
    {
        if (rb.velocity.magnitude >= 0.1) animator.SetBool("walk", true);
        if (rb.velocity.magnitude <= 0.1) animator.SetBool("walk", false);
        animator.SetBool("Concentrate", concentrate);
        animator.SetFloat("Input", rb.velocity.magnitude);
        animator.SetFloat("Horizontal", moveDireciton.x * Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", moveDireciton.y * Input.GetAxis("Vertical"));
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
        if (Input.GetKeyUp(KeyCode.E) && enemyDetecter.enemies.Count != 0 && concentrate == false)
        {
            concentrate = true;
        }
        else if (Input.GetKeyUp(KeyCode.E) && concentrate == true)
        {
            enemyToConcentrate = 0;
            concentrate = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse2) && enemyDetecter.enemies.Count != 0)
        {

            if (enemyToConcentrate >= enemyDetecter.enemies.Count - 1) enemyToConcentrate = 0;
            else enemyToConcentrate++;
            print(enemyToConcentrate);

        }
    }
}
