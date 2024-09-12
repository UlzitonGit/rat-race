using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponParent
{
    [SerializeField] private GameObject ManaUIScript;
    private ManaUI manaUI;
    public float HitMinusMana;
    
    // Start is called before the first frame update
    void Start()
    {
      manaUI = ManaUIScript.GetComponent<ManaUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && canAttack == true)
        {
            AttackMele();
            manaUI.mana -= HitMinusMana;
        }
    }
}
