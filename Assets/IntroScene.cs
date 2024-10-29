using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    public static IntroScene instance;
    [SerializeField] GameObject y, x, z;
    Renderer obfuscationSphere;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }
    private void Start()
    {
        obfuscationSphere = PuzzleManager.instance.GetComponent<Renderer>();
    }

    public void SetDark()
    {
        obfuscationSphere.enabled = false;
        x.SetActive(false);
        y.SetActive(false);
        z.SetActive(false);
    }
    public IEnumerator SetDarkSlow()
    {
        yield return new WaitForSeconds(5f);
        AudioManager.instance.PlayLightSound();
        yield return new WaitForSeconds(1.5f);
        y.SetActive(false);
        AudioManager.instance.PlayLightSound();
        yield return new WaitForSeconds(1.5f);
        z.SetActive(false);
        AudioManager.instance.PlayLightSound();
        yield return new WaitForSeconds(1.5f);
        obfuscationSphere.enabled = false;
        var temp =FindObjectsOfType<CheckArea>();
        foreach(var checkArea in temp) { Destroy(checkArea.gameObject); }
        x.SetActive(false);
        yield return new WaitForSeconds(15);
        Application.Quit();
    }
    public  IEnumerator StartLights(float delay)
    {
        yield return new WaitForSeconds(delay);
        obfuscationSphere.enabled = true;
        x.SetActive(true);
        AudioManager.instance.PlayLightSound();
        yield return new WaitForSeconds(1.5f);
        y.SetActive(true);
        AudioManager.instance.PlayLightSound();
        yield return new WaitForSeconds(1.5f);
        z.SetActive(true);
        AudioManager.instance.PlayLightSound();
        yield return new WaitForSeconds(1.5f);
    }
}
