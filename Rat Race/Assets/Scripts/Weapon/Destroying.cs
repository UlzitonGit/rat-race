using System.Collections;


using UnityEngine;

public class Destroying : MonoBehaviour
{
    [SerializeField] float duration;
    private void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSecondsRealtime(duration);
        Destroy(gameObject);
    }
}
