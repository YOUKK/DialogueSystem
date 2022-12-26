using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager_ : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue_ dialog)
	{
        animator.SetBool("isOpen", true);

        nameText.text = dialog.name;

        sentences.Clear();

        foreach(string sentence in dialog.sentences)
		{
            sentences.Enqueue(sentence);
		}

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
	{
        if(sentences.Count == 0)
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
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
		}
	}

    void EndDialogue()
	{
        animator.SetBool("isOpen", false);
    }
}
