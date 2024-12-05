using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BossController : EnemyBehaviour
{
    private GameObject ManaUIScript;

    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask obstacles;
    [SerializeField] GameObject attackFirstSkill;
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
        yield return new WaitForSeconds(2);
        StartCoroutine(FirstAttack());
    }
    IEnumerator FirstAttack()
    {
        if (canAttack == true)
        {
            canAttack = false;
            isActive = false;
            agent.enabled = false;
            rb.isKinematic = false;
            attackFirstSkill.SetActive(true);
            rb.AddForce(transform.forward * 20, ForceMode.Impulse);
            yield return new WaitForSeconds(0.6f);
            isActive = true;
            agent.enabled = true;
            rb.isKinematic = true;
            
            
            attackFirstSkill.SetActive(false);
           



            yield return new WaitForSeconds(1);
            canAttack = true;
        }
      
    }
  
}
