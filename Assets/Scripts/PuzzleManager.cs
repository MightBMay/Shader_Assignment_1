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
        if (instance != null) Destroy(instance);
        instance = this;
       
    }
    private void Start()
    {
        StartCoroutine(StartPuzzle(shadowPuzzles[puzzleIndex], 5));
        DialougeManager.instance.DialogueEnd += ()=> EndPuzzle();
    }
    public int FindObject(GameObject target)
    {
        for(int i = 0; i < options.Length; i++)
        {
            if (options[i] == target) return i;
        }
        return -1;
    }


    public IEnumerator StartPuzzle(ShadowPuzzle puzzle, float delay = 3)
    {


        currentPuzzle = puzzle;
        options = currentPuzzle.Initialize();
        currentShadowCaster = options[0];
        currentShadowCaster.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        currentShadowCaster.transform.position = new(0, 2.5f, 0);
        currentShadowCaster.transform.SetParent(transform, true);
        Destroy(currentShadowCaster.GetComponent<PuzzleOption>());
        yield return StartCoroutine(IntroScene.instance.StartLights(delay));

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
        DespawnOptions();
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
   public void DespawnOptions()
    {
        foreach (GameObject target in options) { Destroy(target); }
    }
    void EndGame()
    {
        Debug.Log("end");
    }
}
