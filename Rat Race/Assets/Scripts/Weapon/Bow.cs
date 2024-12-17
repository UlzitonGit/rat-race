using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject arrowThirdSkill;
    [SerializeField] private GameObject arrowEffect;
    [SerializeField] private Transform[] spp;
    
    [SerializeField] private Animator anim;
    private bool canAttack = true;


    private bool firstSkill = true;
    private bool secondSkill = false; //true
    private bool thirdSkill = false; //true

    private bool isHolding = false;
    
    private int arrowsInAttack = 0;
    private EnemyDetecter enemyDetecter;
    List<GameObject> fakeArrows = new List<GameObject>();

    [Header("Mana")]
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    [SerializeField] private float HitMinusMana;
    private PlayerController playerController;

    [SerializeField] private float manaForFirstSkill;
    [SerializeField] private float manaForSecondSkill;
    [SerializeField] private float manaForThirdSkill;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        enemyDetecter = FindFirstObjectByType<EnemyDetecter>();
        manaUI = ManaUIScript.GetComponent<ManaUI>();
    }
    void Update()
    {
        Holding();
        SpeedWithBow();
        Skills();
        
    }
    private void SpeedWithBow()
    {
        if (isHolding && !playerController.HaveDebaf)
        {
            playerController.currentSpeed = playerController.walkWithBowSpeed;
        }

        if (!isHolding && !playerController.HaveDebaf) 
        {
            playerController.currentSpeed = playerController.moveSpeed;
        } 

    }
    private void Holding()
    {
        if(Input.GetKey(KeyCode.Mouse0) && !isHolding && canAttack )
        {
            isHolding = true;
            StartCoroutine(HoldingBow());
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && isHolding || arrowsInAttack == 4)
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
        
        anim.SetBool("AimBow", isHolding);

    }
    private void Skills()
    {
        if(firstSkill && Input.GetKey(KeyCode.Z))
        {
            manaUI.mana -= manaForFirstSkill;
            StartCoroutine(FirstSkill());
        }
        if (secondSkill && Input.GetKey(KeyCode.X))
        {
            manaUI.mana -= manaForSecondSkill;
            StartCoroutine(SecondSkill());
        }
        if (thirdSkill && Input.GetKey(KeyCode.C))
        {
            manaUI.mana -= manaForThirdSkill;
            StartCoroutine(ThirdSkill());
        }

    }
    IEnumerator FirstSkill()
    {
        firstSkill = false;
        canAttack = false;
        playerController.characterController.enabled = false;
        //playerController.rb.isKinematic = true;
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
        //playerController.rb.isKinematic = false;
        yield return new WaitForSeconds(7);
        firstSkill = true;
    }
    IEnumerator SecondSkill()
    {
        secondSkill = false;
        enemyDetecter.enemies[playerController.enemyToConcentrate].GetComponent<EnemyBehaviour>().BowSecond();
        yield return new WaitForSeconds(8);
        secondSkill = true; 
    }
    IEnumerator ThirdSkill()
    {
        thirdSkill = false;
        Instantiate(arrowThirdSkill, spp[0].position, spp[0].rotation);
        yield return new WaitForSeconds(6);
        thirdSkill = true;
    }
    IEnumerator HoldingBow()
    {
        yield return new WaitForSeconds(0.7f);
        manaUI.mana -= HitMinusMana;
        
        if(isHolding)
        {
            fakeArrows.Add(Instantiate(arrowEffect, spp[arrowsInAttack]));
            arrowsInAttack++;
        }
     
        if(isHolding && arrowsInAttack < 4) StartCoroutine(HoldingBow());
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
