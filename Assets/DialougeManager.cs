using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialougeManager : MonoBehaviour
{
    public static DialougeManager instance;
    [SerializeField] string[] correct;
    [SerializeField] string[] incorrect;
    [SerializeField] string neutralEnding;
    int indexC;
    int indexI;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject inputPrompt;
    public Action DialogueEnd = () => { };
    Coroutine currentLine;
    private void Awake()
    {
        if (instance != null) Destroy(instance);
        instance = this;

    }
    public string GetLine(bool isCorrect)
    {
        string answer;
        if (isCorrect)
        {
            answer = correct[indexC];
            indexC++;
        }
        else
        {
            answer = incorrect[indexI];
            indexI++;
        }

        return answer;
    }

    public void SendNextLine(bool correct) {
        if (currentLine != null) {StopCoroutine(currentLine); text.text = ""; }

        currentLine = StartCoroutine(
            LetterByLetter(
                GetLine(correct)
                )
            );
    }

    public IEnumerator FinalLine()
    {
        yield return new WaitForSeconds(1);
        string final = "";
        if (indexC == 6)
        {
            final = correct[7];
        }
        else if (indexI == 6)
        {
            final = incorrect[7];
        }
        else final = neutralEnding;

        

        yield return currentLine = StartCoroutine(
            LetterByLetter(final)
        );

        StartCoroutine(IntroScene.instance.SetDarkSlow());
    }
    IEnumerator LetterByLetter(string str, float speed = 0.05f)
    {
        text.text = "";
        for (int i = 0; i < str.Length; i++)
        {
            text.text += str[i];
            if (str[i] != ' ') { yield return new WaitForSeconds(speed); }
        }

        yield return new WaitForSeconds(1);
        inputPrompt.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        DialogueEnd();
        yield return null;
        yield return null; // had issues deleting first character of consecutive lines. this might be the worst way to fix it but wtv.
        text.text = "";
        inputPrompt.SetActive(false);
        currentLine = null;
        
    }
    public void ClearText()
    {
        text.text = "";
    }
}
