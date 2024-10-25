using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public GameObject[] allOptions;
    GameObject[] options;
    public ShadowPuzzle currentPuzzle;
    GameObject currentShadowCaster;
    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(this); }
        StartPuzzle(new Shadow1());
    }

    public int FindObject(GameObject target)
    {
        for(int i = 0; i < options.Length; i++)
        {
            if (options[i] == target) return i;
        }
        return -1;
    }


    public void StartPuzzle(ShadowPuzzle puzzle)
    {
        currentPuzzle = puzzle;
        for(int i = 0; i < options.Length; i++)
        {
            Destroy(options[i]);
        }
        Destroy(currentShadowCaster);


        options = currentPuzzle.Initialize();
        currentShadowCaster = options[0];
        currentShadowCaster.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        for (int i =1; i<options.Length; i++)
        {
            options[i].transform.position = new Vector3(i, 0, 0);
        }
    }

    public void EndPuzzle()
    {
        foreach (GameObject target in options) { Destroy(target); }
        currentPuzzle = null;
    }
}
