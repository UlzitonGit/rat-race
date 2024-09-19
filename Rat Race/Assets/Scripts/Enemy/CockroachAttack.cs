using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockroachAttack : EnemyBehaviour
{
    [SerializeField] private Transform[] spp;
    [SerializeField] private GameObject bullet;
    [SerializeField] public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SearchForPlayer();      
        if(isRaged == true && canAttack == true && isActive == true)
        {
            StartCoroutine(Attacking());
        }

        anim.SetBool("walk", isRaged);
    }
    IEnumerator Attacking()
    {
        canAttack = false;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < spp.Length; i++)
        {
            Instantiate(bullet, spp[i].transform.position, spp[i].transform.rotation);
        }
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}
