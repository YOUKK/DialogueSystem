using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private MyDialog myDialogDB; // DB����
    [SerializeField]
    private Distractor distractorDB; // DB����

    public Text nameText;
    public Text roleText;
    public Text dialogueText;
    public Text button1;
    public Text button2;
    public Text button3;

    public GameObject dialogBox;
    public GameObject dialogStartButton;
    public GameObject dialogNextButton;
    public GameObject distractor1;
    public GameObject distractorObject;

    private List<GameObject> distractorButtons;
    private List<Text> buttonTexts;

    private Queue<string> name;
    private Queue<string> role;
    private Queue<string> sentences;
    private List<string> distractorText;
    private List<int> jumpIndex;

    void Start()
    {
        name = new Queue<string>();
        role = new Queue<string>();
        sentences = new Queue<string>();
        distractorText = new List<string>();
        jumpIndex = new List<int>();

        distractorButtons = new List<GameObject>();
        distractorButtons.Add(distractorObject.transform.GetChild(0).gameObject);
        distractorButtons.Add(distractorObject.transform.GetChild(1).gameObject);
        distractorButtons.Add(distractorObject.transform.GetChild(2).gameObject);
        buttonTexts = new List<Text>();
        buttonTexts.Add(button1);
        buttonTexts.Add(button2);
        buttonTexts.Add(button3);
    }

    // �������� �ְų� ��ȭ ���� ������ ť�� name, role, sentence �ִ� �Լ�
    public void StartDialogue(int index)
    {
		dialogBox.SetActive(true);

        for(int i = index; i < myDialogDB.Sheet1.Count; i++)
		{
            // �������� ���� ���
            if (myDialogDB.Sheet1[i].distractor == 0)
            {
                name.Enqueue(myDialogDB.Sheet1[i].name);
                role.Enqueue(myDialogDB.Sheet1[i].role);
                sentences.Enqueue(myDialogDB.Sheet1[i].dialog1);

				if (myDialogDB.Sheet1[i].isEnd) // ��ȭ���� == true
				{
					break;
				}
			}
			else //�������� �ִ� ���
			{
                name.Enqueue(myDialogDB.Sheet1[i].name);
                role.Enqueue(myDialogDB.Sheet1[i].role);
                sentences.Enqueue(myDialogDB.Sheet1[i].dialog1);
                Debug.Log(distractorDB.Sheet1.Count);
                for (int j = 0; j < distractorDB.Sheet1.Count; j++)
				{
                    Debug.Log(myDialogDB.Sheet1[i].distractor);
                    Debug.Log(distractorDB.Sheet1[j].distractorID);
					if (myDialogDB.Sheet1[i].distractor == distractorDB.Sheet1[j].distractorID)
					{
                        distractorText.Add(distractorDB.Sheet1[j].dialog2);
                        jumpIndex.Add(distractorDB.Sheet1[j].jumpIndex);
					}
					else if (myDialogDB.Sheet1[i].distractor < distractorDB.Sheet1[j].distractorID)
					{
						break;
					}
				}
                break;
            }
		}

        DisplayNextSentence();
    }

    // ���� ��ư�� ������ ����Ǵ� �Լ�
    public void DisplayNextSentence()
	{
		// ���� ��ư ��Ȱ��ȭ
		dialogNextButton.SetActive(false);

        // ��ȭ ����
		if (sentences.Count == 0 && distractorText.Count == 0)
        {
            EndDialogue();
            return;
        }

        // ������ ����
        if (sentences.Count == 0 && distractorText.Count != 0)
        {
            DisplayDistractor();
            return;
        }

        nameText.text = name.Dequeue();
        roleText.text = role.Dequeue();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        // �� ���ھ� Ÿ����
        int i = 0;
        dialogueText.text = ""; // text �ʱ�ȭ
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.04f);

            // ���� ��ư Ȱ��ȭ
            i++;
            if(sentence.ToCharArray().Length - i < 4)
                dialogNextButton.SetActive(true);
        }
    }

    // �������� ���� �Լ�
    private void DisplayDistractor()
	{
        distractorObject.SetActive(true);
        dialogNextButton.SetActive(false);


        for (int i = 0; i < distractorText.Count; i++)
		{
            buttonTexts[i].text = distractorText[i];
            distractorButtons[i].SetActive(true);
		}
	}

    public void ClickButton1()
    {
        int jumpindex = jumpIndex[0];
        ButtonSet();
        StartDialogue(jumpindex);
    }

    public void ClickButton2()
	{
        int jumpindex = jumpIndex[1];
        ButtonSet();
        StartDialogue(jumpindex);
    }

    public void ClickButton3()
	{
        int jumpindex = jumpIndex[2];
        ButtonSet();
        StartDialogue(jumpindex);
    }

    private void ButtonSet()
	{
        distractorObject.SetActive(false);
        for(int i = 0; i < distractorButtons.Count; i++)
		{
            distractorButtons[i].SetActive(false);
        }
        distractorText.Clear();
        jumpIndex.Clear();
	}

    // �ڵ� ��� ���
    public void Automatic()
	{
        while(sentences.Count > 0)
		{
            //DisplayNextSentence();
            //StopAllCoroutines();
            StartCoroutine(Next());
        }
	}

    IEnumerator Next()
	{
        DisplayNextSentence();
        yield return new WaitForSeconds(2.0f);
    }

    void EndDialogue()
    {
        dialogBox.SetActive(false);
        dialogStartButton.SetActive(true);
        //�ڵ������ư false
    }
}
