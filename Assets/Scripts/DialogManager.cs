using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Queue<string> sentences;
    public GameObject canvaDialogue;
    private bool activeDialog = false;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        sentences = new Queue<string>();
        activeDialog = true;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Debut dialogue avec " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            //FindObjectOfType<AudioManager>().Play("Dialogue");
            yield return new WaitForSeconds(0.05f);
        }
    }

    void EndDialogue()
    {
        Destroy(canvaDialogue);
        Debug.Log("End dialog");
        activeDialog = false;
    }
    public bool get_active()
    {
        return activeDialog;
    }
}