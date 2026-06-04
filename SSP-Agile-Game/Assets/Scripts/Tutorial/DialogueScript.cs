using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textObject;
    public string[] dialogueText;
    public float textSpeed;
    public int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textObject.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (textObject.text == dialogueText[index] && index != 3 && index != 4 && index != 6 && index != 7 )
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textObject.text = dialogueText[index];
            }
        }
        //if (Keyboard.current.fKey.wasPressedThisFrame)
        //{
        //    if(textObject.text == dialogueText[index])
        //    {
        //        NextLine();
        //    }
        //    else
        //    {
        //        StopAllCoroutines();
        //        textObject.text = dialogueText[index];
        //    }
        //}
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(DialogueWriter());
    }
    IEnumerator DialogueWriter()
    {
        foreach (char x in dialogueText[index])
        {
            textObject.text += x;
            yield return new WaitForSeconds(textSpeed);
        }

    }
   
    public void NextLine()
    {
        StopAllCoroutines();
        Debug.Log("next line is called");
        if (index < dialogueText.Length - 1)
        {
            index++;
            textObject .text = string.Empty;
            StartCoroutine (DialogueWriter());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
