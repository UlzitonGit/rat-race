using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] Vector3 dir;
    [SerializeField] private float damage;
    private GameObject ManaUIScript;
    ManaUI manaUI;
    // Start is called before the first frame update
    void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
        StartCoroutine(Destroying());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manaUI.mana -= damage;
            Destroy(gameObject);
        }
    }
    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
