using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossController : EnemyBehaviour
{
    private GameObject ManaUIScript;

    [SerializeField] private Transform attackPosition;
    [SerializeField] private Transform[] shootPosition;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] GameObject attackFirstSkill;
    [SerializeField] ParticleSystem shootPart;
    [SerializeField] private float hitFromHand;
    private ManaUI manaUI;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
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
    IEnumerator ChooseAttack()
    {
        if(canAttack == true)
        {
            yield return new WaitForSeconds(2);

            StartCoroutine("Attack" + Random.Range(1, 3).ToString());
        }
       
    }
    IEnumerator Attack2()
    {
        if (canAttack == true)
        {
            canAttack = false;
            yield return new WaitForSeconds(1);
            isActive = false;

            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.5f);
                Shoot();
            }

            yield return new WaitForSeconds(1);
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
