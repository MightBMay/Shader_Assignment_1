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
        for (int i = 0; i < 4; i++)
        {
            if (i > usedIndecies.Length){ Debug.Log("USED INDECIES TOO SMALL"); return Array.Empty<GameObject>(); }
            if (usedIndecies[i] > PuzzleManager.instance.allOptions.Length) { Debug.LogError("INDEX TOO BIG"); return Array.Empty<GameObject>(); }
            loadedObjects[i+1] = LoadObjectFromIndex(usedIndecies[i]).DisableOnLoad();

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

}



/*
 * 
public class Shadow : ShadowPuzzle
{
    static int answer = ; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
      
    };
    public Shadow() : base(answer, indicies){}

}

 */


public class Shadow1 : ShadowPuzzle
{
    static int answer = 0; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
      1,3,2,0
    };
    public Shadow1() : base(answer, indicies){}
}

public class Shadow2: ShadowPuzzle
{
    static int answer = 1; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
        2,3,0,1
    };
    public Shadow2() : base(answer, indicies) { }
}
public class Shadow3 : ShadowPuzzle
{
    static int answer = 2; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
        2,1,3,4
    };
    public Shadow3() : base(answer, indicies) { }
}
public class Shadow4 : ShadowPuzzle
{
    static int answer = 3; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
        2,4,3,6
    };
    public Shadow4() : base(answer, indicies) { }
}
public class Shadow5 : ShadowPuzzle
{
    static int answer = 4; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
        2,5,4,0
    };
    public Shadow5() : base(answer, indicies) { }
}

public class Shadow6 : ShadowPuzzle
{
    static int answer = 5; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
        5,2,3,0
    };
    public Shadow6() : base(answer, indicies) { }
}

public class Shadow7 : ShadowPuzzle
{
    static int answer = 6; // correct answer will be the 4th.
    static int[] indicies = new[]
    {
        0,6,5,4
    };
    public Shadow7() : base(answer, indicies) { }
}


public static class GameObjExten
{
    public static GameObject DisableOnLoad(this GameObject g)
    {
        g.SetActive(false);
        return g;
    }
}