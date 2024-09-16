using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroying : MonoBehaviour
{
    [SerializeField] float duration;
   
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
