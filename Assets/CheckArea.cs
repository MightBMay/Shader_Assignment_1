using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PuzzleOption option))
        {
            if (PuzzleManager.instance.currentPuzzle.CheckAnswer(option.indexOfObject))
            {
                Debug.Log("correct");
            }
            else
            {
                Debug.Log("incorrect");
            }
        }
        
    }
}