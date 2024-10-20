using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [Header("Bullet")]
    public Transform spawnBullet;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject firstSkillBullet;
    private bool canAttack = true;

    [Header("Mana")]
    [SerializeField] private float HitMinusMana;
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    private bool canUseFirstSkill = true;
    private void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
    }
    private void Update()
    {
        Attacka();
    }
    private void Attacka()
    {
        if(Input.GetKey(KeyCode.Mouse0) && canAttack)
        {
            manaUI.mana -= HitMinusMana;
            Instantiate(prefabBullet, spawnBullet.position, spawnBullet.rotation);
            StartCoroutine(RealodShot());
        }
        if (Input.GetKey(KeyCode.Z) && canUseFirstSkill)
        {
            manaUI.mana -= HitMinusMana;
            Instantiate(firstSkillBullet, spawnBullet.position, spawnBullet.rotation);
            StartCoroutine(RealodFirstSkill());
        }
    }
    IEnumerator RealodShot()
    {
        canAttack = false;
        yield return new WaitForSecondsRealtime(2f);
        canAttack = true;
    }
    IEnumerator RealodFirstSkill()
    {
        canUseFirstSkill = false;
        yield return new WaitForSecondsRealtime(2f);
        canUseFirstSkill = true;
    }
}
