using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    BoxCollider col;
    [SerializeField] ParticleSystem particle;
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
        if(other.TryGetComponent(out PuzzleOption option)) DialougeManager.instance.SendNextLine(PuzzleManager.instance.currentPuzzle.CheckAnswer(option.indexOfObject));
        PuzzleManager.instance.DespawnOptions();
        PlayerController.instance.DropObject();
        particle.Play();


    }
}
