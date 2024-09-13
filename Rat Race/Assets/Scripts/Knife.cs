using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponParent
{
    
    public Transform spawnBullet;
    [SerializeField] private GameObject prefabBullet;
    public float speedBullet;
    public float timeLifeBullet;

    [Header("Mana")]
    [SerializeField] private GameObject ManaUIScript;
    private ManaUI manaUI;
    public float HitMinusMana;


    [SerializeField] GameObject trail;
    [SerializeField] Transform trailSpp;
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
            StartCoroutine(VFX());
            
            manaUI.mana -= HitMinusMana;
        }
    }
    IEnumerator VFX()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject trailSpawned = Instantiate(trail, trailSpp);
        Instantiate(prefabBullet, spawnBullet.position, spawnBullet.rotation);
        yield return new WaitForSeconds(0.2f);
        trailSpawned.transform.parent = null;
        yield return new WaitForSeconds(2.5f);
        Destroy(trailSpawned);

    }
}
