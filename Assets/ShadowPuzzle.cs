using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowPuzzle
{
    public byte correct; // used lower 4 bits to store which answers were used.
    public string[] prefabPaths = new string[4];

    public ShadowPuzzle(byte correct, string[] prefabPaths)
    {
        this.correct = correct;
        this.prefabPaths = prefabPaths;
        Initialize();
    }

    public void Initialize()
    {
        foreach (var path in prefabPaths) if( !string.IsNullOrEmpty(path) ) LoadObjectFromPath(path);
     
    }

    GameObject LoadObjectFromPath(string path)
    {
        return GameObject.Instantiate( (GameObject)Resources.Load(path) );
    }

    public bool CheckAnswer(byte answer)
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
    static byte answer = 0b00001111; // correct answer will be the 4th.
    static string[] paths = new[]
    {
        "Assets/Resources/Prefabs/ShadowPuzzles/pyramid.fbx",
        "Assets/Resources/Prefabs/ShadowPuzzles/pyramid.fbx",
        "Assets/Resources/Prefabs/ShadowPuzzles/pyramid.fbx",
        "Assets/Resources/Prefabs/ShadowPuzzles/RectTriCirc.fbx",
    };
    public Shadow1() : base(answer, paths)
    {

    
    }
}
