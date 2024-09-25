using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
   
    [SerializeField] private GameObject arrowEffect;
    [SerializeField] private Transform[] spp;
    [SerializeField] private float manaForFirstSkill;
    
    [SerializeField] private Animator anim;
    private bool canAttack = true;
    private bool isHolding = false;
    private int arrowsInAttack = 0;
    List<GameObject> fakeArrows = new List<GameObject>();

    [Header("Mana")]
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    [SerializeField] private float HitMinusMana;
    private PlayerController playerController;
    bool firstSkill = true;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && isHolding == false && canAttack == true)
        {
            isHolding = true;
            StartCoroutine(HoldingBow());
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && isHolding == true || arrowsInAttack == 4)
        {
            isHolding = false;
            StopCoroutine(HoldingBow());
            for (int i = 0; i < arrowsInAttack; i++)
            {
                Destroy(fakeArrows[i]);
                
                Instantiate(arrow, spp[i].position, spp[i].rotation);
            }
            arrowsInAttack = 0;
            StartCoroutine(RealodShot());
            fakeArrows.Clear();
        }
        if(firstSkill == true && Input.GetKey(KeyCode.X))
        {
            manaUI.mana -= manaForFirstSkill;
            StartCoroutine(Stunning());
        } 
        anim.SetBool("AimBow", isHolding);
        if(isHolding == true && !playerController.HaveDebaf) playerController.currentSpeed = playerController.walkWithBowSpeed;
        if (isHolding == false && !playerController.HaveDebaf) playerController.currentSpeed = playerController.moveSpeed;
    }
    IEnumerator Stunning()
    {
        firstSkill = false;
        canAttack = false;
        playerController.characterController.enabled = false;
        playerController.rb.isKinematic = true;
        anim.SetTrigger("FirstSpellBow");
        anim.SetBool("Attacking", true);
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();
        for (int i = 0;i < enemies.Length;i++)
        {
            if (enemies[i].arrows > 0) enemies[i].StunnByArrow();
        }
        yield return new WaitForSeconds(1f);
        canAttack = true;
        anim.SetBool("Attacking", false);
        playerController.characterController.enabled = true;
        playerController.rb.isKinematic = false;
        yield return new WaitForSeconds(7);
        firstSkill = true;
    }
    IEnumerator HoldingBow()
    {
        yield return new WaitForSeconds(0.7f);
        manaUI.mana -= HitMinusMana;
        
        if(isHolding == true)
        {
            fakeArrows.Add((Instantiate(arrowEffect, spp[arrowsInAttack])));
            arrowsInAttack++;
        }
     
        if(isHolding == true && arrowsInAttack < 4) StartCoroutine(HoldingBow());
    }
    IEnumerator RealodShot()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    public void Switch()
    {
        isHolding = false;
        StopCoroutine(HoldingBow());
        for (int i = 0; i < arrowsInAttack; i++)
        {
            Destroy(fakeArrows[i]);

            Instantiate(arrow, spp[i].position, spp[i].rotation);
        }
        arrowsInAttack = 0;
        StartCoroutine(RealodShot());
        fakeArrows.Clear();
    }
}
