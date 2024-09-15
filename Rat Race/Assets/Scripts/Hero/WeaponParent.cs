using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    [SerializeField] GameObject meleTrigger;
    [SerializeField] Transform spp;
    [SerializeField] public float damage;
    [SerializeField] Animator anim;

    public bool canAttack = true;
   
    public void AttackMele()
    {
        if (canAttack == false) return;
        StartCoroutine(Attacking());
    }
    IEnumerator Attacking()
    { 
        canAttack = false;
        GameObject mele = Instantiate(meleTrigger, spp.position, spp.rotation);
        mele.GetComponent<WeaponTriger>().damage = damage;
        anim.SetTrigger("Attack");
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(mele);
       // yield return new WaitForSeconds(0.5f);
        anim.SetBool("Attacking",false);   
        canAttack = true;
    }
}
