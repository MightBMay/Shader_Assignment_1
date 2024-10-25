using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] options = new GameObject[4];
    public ShadowPuzzle currentPuzzle;
    
    public int FindObject(GameObject target)
    {
        for(int i = 0; i < options.Length; i++)
        {
            if (options[i] == target) return i;
        }
        return -1;
    }

    public void StartPuzzle()
    {
        currentPuzzle.Initialize();
    }

    public void EndPuzzle()
    {
        foreach (GameObject target in options) { Destroy(target); }
        currentPuzzle = null;
    }
}
