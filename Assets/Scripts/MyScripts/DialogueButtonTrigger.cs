using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �÷��̾ ������ ��ũ��Ʈ
public class DialogueButtonTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueStartButton;
	[SerializeField]
	private GameObject automaticButton;

	[SerializeField]
	private Text npcName;
	[SerializeField]
	private int startIndex;

	public void triggerDialog()
	{
		FindObjectOfType<DialogueManager>().StartDialogue(startIndex);
		dialogueStartButton.SetActive(false);
		//automaticButton.SetActive(true);
	}

	// ��ȭ ���� �Ŀ��� ��ȭ ���� ��ư �߰� �ϱ�
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("NPC"))
		{
			//Debug.Log(gameObject.transform.GetComponent<NPC>().NPCName);

			npcName.text = other.transform.GetComponent<NPC>().NPCName;
			startIndex = other.transform.GetComponent<NPC>().startIndex;
            dialogueStartButton.SetActive(true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("NPC"))
		{
			dialogueStartButton.SetActive(false);
		}
	}
}
