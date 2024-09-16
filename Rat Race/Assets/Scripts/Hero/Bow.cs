using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject arrowEffect;
    [SerializeField] private Transform[] spp;
    
    [SerializeField] private Animator anim;
    private bool canAttack = true;
    private bool isHolding = false;
    private int arrowsInAttack = 0;
    List<GameObject> fakeArrows = new List<GameObject>();
    // Start is called before the first frame update
   

    // Update is called once per frame
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
        anim.SetBool("AimBow", isHolding);
    }
    IEnumerator HoldingBow()
    {
        yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForSeconds(2f);
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
