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
    [SerializeField] ParticleSystem dashPart;
    int enemyToConcentrate = 0;
    Vector3 moveDireciton;
    bool canConcentrate = true;
    public bool canWalk = true;
    public bool canDash = true;

    [Header("Mana")]
    [SerializeField] float minusManaDech;
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyDetecter = GetComponentInChildren<EnemyDetecter>();
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
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
       
        if (canWalk == false) return;
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

        if (canDash == true && Input.GetKey(KeyCode.LeftShift) && concentrate == true)
        {
            StartCoroutine(Dashing(moveDireciton * 18));
        }
        if (canDash == true && Input.GetKey(KeyCode.LeftShift) && concentrate == false)
        {
            StartCoroutine(Dashing(transform.forward * 25));
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
        if (Input.GetKey(KeyCode.E) && enemyDetecter.enemies.Count != 0 && concentrate == false && canConcentrate == true)
        {
            concentrate = true;
            StartCoroutine(ReloadingConcentration());
        }
        else if (Input.GetKey(KeyCode.E) && concentrate == true && canConcentrate == true)
        {
            enemyToConcentrate = 0;
            concentrate = false;
            StartCoroutine(ReloadingConcentration());
        }
        if (Input.GetKey(KeyCode.Mouse2) && enemyDetecter.enemies.Count != 0 && canConcentrate == true)
        {

            if (enemyToConcentrate >= enemyDetecter.enemies.Count - 1) enemyToConcentrate = 0;
            else enemyToConcentrate++;
            print(enemyToConcentrate);
            StartCoroutine(ReloadingConcentration());

        }
    }
    IEnumerator ReloadingConcentration()
    {
        canConcentrate = false;
        yield return new WaitForSeconds(0.3f);
        canConcentrate = true;
    }

    IEnumerator Dashing(Vector3 dir)
    {

        manaUI.mana -= minusManaDech;
        canWalk = false;
        canDash = false;
        rb.AddForce(dir, ForceMode.Impulse);
        animator.SetTrigger("Dash");
        dashPart.Play();
        yield return new WaitForSeconds(0.4f);
        rb.velocity = Vector3.zero;
        canWalk = true;
        yield return new WaitForSeconds(0.6f);
        canDash = true;
    }
}
