using UnityEngine;

public class WeaponTrigerEnemy : MonoBehaviour
{
    [SerializeField] public float damage;
    
    private GameObject ManaUIScript;
    private ManaUI manaUI;
    [SerializeField] private bool debaf;
    private PlayerController playerController;
    private GameObject Player;
    private void Start()
    {
        ManaUIScript = GameObject.FindWithTag("ManaUI");
        manaUI = ManaUIScript.GetComponent<ManaUI>();
        Player = GameObject.FindWithTag("Player");
        playerController = Player.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(debaf)
            {
                playerController.DebafApathy();
            }
            manaUI.mana -= damage;
            Destroy(gameObject);
        }


    }
    
}
