using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    [SerializeField] Material nonBobMaterial;
    public GameObject[] allOptions;
    GameObject[] options;
    public ShadowPuzzle currentPuzzle;
    [SerializeField] Transform OptionHolder;
    GameObject currentShadowCaster;
    byte puzzleIndex = 0;
    List<ShadowPuzzle> shadowPuzzles = new List<ShadowPuzzle>() {
        new Shadow1(),
        new Shadow2(),
        new Shadow3(),
        new Shadow4(),
        new Shadow5(),
        new Shadow6(),
        new Shadow7()
    };
    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this); }
       
    }
    private void Start()
    {
        StartCoroutine(StartPuzzle(shadowPuzzles[puzzleIndex]));
    }
    public int FindObject(GameObject target)
    {
        for(int i = 0; i < options.Length; i++)
        {
            if (options[i] == target) return i;
        }
        return -1;
    }


    public IEnumerator StartPuzzle(ShadowPuzzle puzzle)
    {


        currentPuzzle = puzzle;
        options = currentPuzzle.Initialize();
        currentShadowCaster = options[0];
        currentShadowCaster.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        currentShadowCaster.transform.position = new(0, 2.5f, 0);
        Destroy(currentShadowCaster.GetComponent<PuzzleOption>());
        yield return StartCoroutine(IntroScene.instance.StartLights(3));

        for (int i =1; i<options.Length; i++)
        {
            Transform shape = options[i].transform;
            shape.position += new Vector3(i * 1.5f, 0, 0);
            shape.SetParent(OptionHolder,false);
            shape.gameObject.SetActive(true);
            
            shape.GetComponent<Renderer>().material = nonBobMaterial;
        }
    }

    public void EndPuzzle()
    {
        foreach (GameObject target in options) { Destroy(target); }
        Destroy(currentShadowCaster);
        currentPuzzle = null;
        LoadNextPuzzle();
        IntroScene.instance.SetDark();
    }
    void LoadNextPuzzle()
    {
        puzzleIndex++;
        if(puzzleIndex>= shadowPuzzles.Count) { EndGame();}
        StartCoroutine(StartPuzzle(shadowPuzzles[puzzleIndex]));
    }
    void EndGame()
    {
        Debug.Log("end");
    }
}
