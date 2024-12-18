using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public CharacterController characterController;
    public float moveSpeed = 5f;
    [SerializeField] private float speedDebafApathy;
    [HideInInspector] public float currentSpeed = 5;
    private EnemyDetecter enemyDetecter;
    [HideInInspector] public bool concentrate = false;
    [SerializeField] private Animator animator;
    [SerializeField] public float walkWithBowSpeed = 1.5f;
    [SerializeField] private ParticleSystem dashPart;
    [HideInInspector] public int enemyToConcentrate = 0;
    private Vector3 moveDireciton;
    private bool canConcentrate = true;
    
    [HideInInspector] public bool canWalk = true;
    [HideInInspector] public bool canDash = true;

    [Header("Mana")]
    [SerializeField] float minusManaDech;
    [SerializeField] public Transform thirdAttackPos;
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    private float _falling;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMult = 3;
    [HideInInspector] public bool HaveDebaf;
    private Knife knife;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
        enemyDetecter = GetComponentInChildren<EnemyDetecter>();
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
        knife = GetComponent<Knife>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk();
        Concentration();
        ApplyGravity();
        Animation();
       
    } 
    private void Walk()
    {
       
        if (canWalk == false) return;
        float _Xspeed = Input.GetAxis("Horizontal") * currentSpeed;
        float _Yspeed = Input.GetAxis("Vertical") * currentSpeed;
        moveDireciton = new Vector3(_Xspeed, 0, _Yspeed);
        moveDireciton.Normalize();
       
       
        if (moveDireciton.magnitude > 0.1f && !concentrate)
        {
           
            Quaternion toRot = Quaternion.LookRotation(moveDireciton.ToIso(), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRot, 900 * Time.deltaTime);
        }
        if (concentrate)
        {
            characterController.Move(new Vector3(_Xspeed, _falling, _Yspeed)  * Time.deltaTime);
        }
        else
        {
            characterController.Move(new Vector3(transform.forward.x, _falling, transform.forward.z) * moveDireciton.magnitude * currentSpeed  * Time.deltaTime);

        }

        if (canDash == true && Input.GetKey(KeyCode.LeftShift) && concentrate == true)
        {
            StartCoroutine(Dashing(moveDireciton * 22));
        }
        if (canDash == true && Input.GetKey(KeyCode.LeftShift) && concentrate == false)
        {
            StartCoroutine(Dashing(transform.forward * 35));
        }
    }
    private void ApplyGravity()
    {
        if(moveDireciton.magnitude == 0) characterController.Move( new Vector3(0, _falling, 0)* currentSpeed * Time.deltaTime);
        _falling = gravity * gravityMult * Time.deltaTime;
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
    public void AttackDash()
    {
        StartCoroutine(AttackDashing());
    }
    public void Discard()
    {
        StartCoroutine(Discarding());
    }
    private void Concentration()
    {
        if (enemyDetecter.enemies.Count != 0 && concentrate == true && !knife.is3Ablty)
        {
            if (enemyToConcentrate > enemyDetecter.enemies.Count - 1) enemyToConcentrate = 0;
            Vector3 dir = enemyDetecter.enemies[enemyToConcentrate].transform.position;
            transform.LookAt(new Vector3(dir.x, transform.position.y, dir.z));
            
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
    public void DebafApathy()
    {
        StartCoroutine(DebafSlowdown());
    }
    IEnumerator DebafSlowdown()
    {
        HaveDebaf = true;
        currentSpeed = speedDebafApathy;
        yield return new WaitForSeconds(3);
        currentSpeed = moveSpeed;
        HaveDebaf = false;
    }
    IEnumerator Discarding()
    {      
        characterController.enabled = false;
        canWalk = false;      
        rb.AddForce(transform.forward * -35, ForceMode.Impulse);       
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
        characterController.enabled = true;
        canWalk = true;       
       
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
        characterController.enabled = false;
        canDash = false;
        rb.AddForce(dir, ForceMode.Impulse);
        animator.SetTrigger("Dash");
        dashPart.Play();
        yield return new WaitForSeconds(0.2f);
        characterController.enabled = true;
        rb.velocity = Vector3.zero;
        canWalk = true;
        yield return new WaitForSeconds(0.6f);
        canDash = true;
    }
     IEnumerator AttackDashing()
    {       
     
        currentSpeed = speedDebafApathy;
        //rb.AddForce(transform.forward / 3.3f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        currentSpeed = moveSpeed;
        //rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.6f);
        
        
    }

}
