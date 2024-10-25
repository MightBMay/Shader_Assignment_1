using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowPuzzle
{
    public int correct; // used lower 4 bits to store which answers were used.
    public int[] usedIndecies;
    public ShadowPuzzle(int correct, int[] indecies)
    {
        this.correct = correct;
        this.usedIndecies = indecies;
    }

    public GameObject[] Initialize()
    {
        GameObject[] loadedObjects = new GameObject[5];
        loadedObjects[0] = LoadObjectFromIndex(correct);
        for (int i = 1; i < 5; i++)
        {
            if (i > usedIndecies.Length){ Debug.Log("USED INDECIES TOO SMALL"); return Array.Empty<GameObject>(); }
            if (usedIndecies[i] > PuzzleManager.instance.allOptions.Length) { Debug.LogError("INDEX TOO BIG"); return Array.Empty<GameObject>(); }
            loadedObjects[i] = LoadObjectFromIndex(usedIndecies[i]);

        }

        return loadedObjects;
     
    }

    GameObject LoadObjectFromIndex(int index)
    {
        GameObject temp = GameObject.Instantiate(PuzzleManager.instance.allOptions[index]);
        temp.AddComponent<PuzzleOption>().indexOfObject = index;
        return temp;
    }

    public bool CheckAnswer(int answer)
    {
        return correct == answer;
    }
    /// <summary>
    /// Convert index from possible answers into Byte for answer comparison.
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public byte IndexToByteAnswer(int i)
    {
        return (byte)Mathf.Pow(2, i+1); // raise 2 to the power of the index+1, so element 0 = 2^1 = 1, element 1 = 2. this 
    }
}

public class Shadow1 : ShadowPuzzle
{
    static int answer = 1; // correct answer will be the 4th.
    static int[] paths = new[]
    {
      0,0,0,1
    };
    public Shadow1() : base(answer, paths)
    {

    
    }
}
