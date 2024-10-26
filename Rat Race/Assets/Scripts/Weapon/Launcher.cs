using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [Header("Bullet")]
    public Transform spawnBullet;
    [SerializeField] private Transform spawnWall;
    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private GameObject firstSkillBullet;
    [SerializeField] private GameObject SecondSkillsWall;
    [SerializeField] private GameObject linkWall;
    private bool canAttack = true;

    [Header("Mana")]
    [SerializeField] private float HitMinusMana;
    [SerializeField] private float HitMinusManaFS;
    [SerializeField] private float HitMinusManaSS;
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    private bool canUseFirstSkill = true;
    private bool canUseSecondSkill = true;
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
            manaUI.mana -= HitMinusManaFS;
            Instantiate(firstSkillBullet, spawnBullet.position, spawnBullet.rotation);
            StartCoroutine(RealodFirstSkill());
        }
        if (Input.GetKey(KeyCode.X) && canUseSecondSkill)
        {
            manaUI.mana -= HitMinusManaSS;
            linkWall = Instantiate(SecondSkillsWall, spawnWall.position, spawnWall.rotation);
            
            StartCoroutine(RealodSecondSkill());
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
    IEnumerator RealodSecondSkill()
    {
        canUseSecondSkill = false;
        yield return new WaitForSecondsRealtime(3f);
        if (linkWall != null)
        {
            Destroy(linkWall);
        }

        yield return new WaitForSecondsRealtime(7f);
        canUseSecondSkill = true;
    }
}
