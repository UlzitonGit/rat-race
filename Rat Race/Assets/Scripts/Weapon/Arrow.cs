using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Vector3 dir;
    [SerializeField] private GameObject fakeArrow;
    [SerializeField] private float speed;
    [SerializeField] private bool isChain = false;
    [SerializeField] private bool isThirdSkill = false;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(!isChain)
            {
                GameObject arrow = Instantiate(fakeArrow, transform.position, transform.rotation);
                other.GetComponent<EnemyBehaviour>().arrows++;
                arrow.transform.SetParent(other.GetComponent<EnemyBehaviour>().arrowPos, true);
                arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, 0, arrow.transform.localPosition.z);
                if(!isThirdSkill)
                {
                    Destroy(gameObject);

                }
                
            }
            if (isChain)
            {
               
                other.GetComponent<EnemyBehaviour>().StunnByArrow();
                Destroy(gameObject);
             
            }
        }
    }
}
