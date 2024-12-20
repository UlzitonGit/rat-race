using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManaUI : MonoBehaviour
{
    [Header("Code")]
    public float manaPoint;
    public float plusMaxMana;
    [SerializeField] private float multiplierPoint;
    [SerializeField] private float maxMana;
    public float mana;
    public float plusManaSec;
    private bool StartPlusMana;
    
    [Header("UI")]
    [SerializeField] private Image manaInMoment;

    // Start is called before the first frame update
    void Start()
    {
        maxMana = manaPoint * multiplierPoint + plusMaxMana;
        mana = maxMana;
        
    }

    // Update is called once per frame
    void Update()
    {
        Mana();
        
    }
    private void Mana()
    {
        maxMana = manaPoint * multiplierPoint + plusMaxMana;
        manaInMoment.fillAmount = mana / maxMana;
        
        if(mana < maxMana && StartPlusMana)
        {
            StartPlusMana = false;
            StartCoroutine(PlusManaInSecond());

        }
        if(mana >= maxMana)
        {
            StartPlusMana=true;
        }
        if(mana <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        

    }
    IEnumerator PlusManaInSecond()
    {
        while(mana < maxMana)
        {
            
            yield return new WaitForSecondsRealtime(0.1f);
            float limitManaPlus = mana + plusManaSec;
            if(limitManaPlus >= maxMana)
            {
                mana = maxMana;
                StartPlusMana = true;
                StopCoroutine(PlusManaInSecond());
            }
            else
            {
                mana += plusManaSec / 10;

            }
            
            
        }

    }
    }
