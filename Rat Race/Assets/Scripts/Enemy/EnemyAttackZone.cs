using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackZone : MonoBehaviour
{
    ManaUI manaUI;
    private GameObject ManaUIScript;
    [SerializeField] private float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manaUI.mana -= damage;
            gameObject.SetActive(false);
        }
    }
}
