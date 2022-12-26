using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger_ : MonoBehaviour
{
    public Dialogue_ dialogue;

    public void TriggerDialog()
	{
		FindObjectOfType<DialogueManager_>().StartDialogue(dialogue);
	}
}
