using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    public static IntroScene instance;
    [SerializeField] GameObject y, x, z;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    public void SetDark()
    {
        x.SetActive(false);
        y.SetActive(false);
        z.SetActive(false);
    }
    public  IEnumerator StartLights(float delay)
    {
        Debug.Log("s");
        yield return new WaitForSeconds(delay);
        x.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        y.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        z.SetActive(true);
        yield return new WaitForSeconds(1.5f);
    }
}
