using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossController : EnemyBehaviour
{
    private GameObject ManaUIScript;
    

    [SerializeField] private Transform attackPosition;
    [SerializeField] private Transform[] shootPosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float attackRadius;
    [SerializeField] GameObject deathPart;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] GameObject attackFirstSkill;
    [SerializeField] ParticleSystem shootPart;
    [SerializeField] ParticleSystem teleport;
    [SerializeField] private float hitFromHand;
    [SerializeField] Animator anim;
    [SerializeField] Image hpBar;
    [SerializeField] GameObject tentacle;
    float maxHealth;
    private ManaUI manaUI;

    PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        _playerController = Player.GetComponent<PlayerController>();
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();

    }

    // Update is called once per frame
    void Update()
    {
        SearchForPlayer();
        if(canAttack  == true && isRaged == true)
        {
            
            StartCoroutine(ChooseAttack());
        }

    }
    public override void GetDamage(float damage)
    {
        base.GetDamage(damage);
        if(currentEnemyHealth <= 0) Instantiate(deathPart, transform.position, Quaternion.identity);
        hpBar.fillAmount = currentEnemyHealth / health;
        
    }
    IEnumerator ChooseAttack()
    {
        if(canAttack == true)
        {
            yield return new WaitForSeconds(2);

            StartCoroutine("Attack" + Random.Range(1, 5).ToString());
        }
       
    }
    IEnumerator Attack3()
    {
        if(canAttack == true)
        {
            anim.SetTrigger("thirdAttack");
            canAttack = false;
            isActive = false;
            teleport.Play();
            yield return new WaitForSeconds (1);
            transform.position = _playerController.thirdAttackPos.position;
            yield return new WaitForSeconds(0.3f);
            transform.LookAt(playerController.position);
            teleport.Play();
            yield return new WaitForSeconds(0.4f);
            attackFirstSkill.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            attackFirstSkill.SetActive(false);
            yield return new WaitForSeconds(1);
            isActive = true;
            canAttack = true;
        }
    }
    IEnumerator Attack4()
    {
        if (canAttack == true)
        {
            anim.SetBool("idle", true);
            canAttack = false;
            isActive = false;
            anim.SetTrigger("Cast");
            yield return new WaitForSeconds(0.5f);
            Instantiate(tentacle, _playerController.thirdAttackPos.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            anim.SetBool("idle", false);
            isActive = true;
            canAttack = true;
        }
    }
    IEnumerator Attack2()
    {
        if (canAttack == true)
        {
            anim.SetBool("idle", true);
            canAttack = false;
            yield return new WaitForSeconds(1);
            isActive = false;

            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Shoot();
            }

            yield return new WaitForSeconds(1);
            anim.SetBool("idle", false);
            isActive = true;
            canAttack = true;
        }
    }
    IEnumerator Attack1()
    {
        if (canAttack == true)
        {
            canAttack = false;
            isActive = false;
            agent.enabled = false;
            anim.SetTrigger("Attack");
            rb.isKinematic = false;
            attackFirstSkill.SetActive(true);
            rb.AddForce(transform.forward * 50, ForceMode.Impulse);
            yield return new WaitForSeconds(0.65f);
            isActive = true;
            agent.enabled = true;
            rb.isKinematic = true;
            
            
            attackFirstSkill.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            Shoot();
            yield return new WaitForSeconds(1);
            
            canAttack = true;
        }
      
    }
    public void Shoot()
    {
        shootPart.Play();
        for (int i = 0; i < shootPosition.Length; i++)
        {
            Instantiate(bullet, shootPosition[i].position, shootPosition[i].rotation);
        }
    }
  
}
