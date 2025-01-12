using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EggClick2 : MonoBehaviour
{
    public int eggID; 
    public GameObject speechBubble;  
    public Text speechText;          
    public Text[] choiceTexts;       
    public GameObject choice;        
    public GameObject choice2;       
    public Button choiceButton1;     
    public Button choiceButton2;     

    private bool isSpeechVisible = false; 
    private bool isWaitingForChoice = false; 

    private Dictionary<int, DialogueOption[]> eggDialogueSets = new Dictionary<int, DialogueOption[]>()
    {
        {
            1, new DialogueOption[] {
                new DialogueOption("�Ķ����� ���� �� ����."),
                new DialogueOption("�� ��� Ŭ��?"),
                new DialogueOption("���ϵ� �ð���?"),
                new DialogueOption("���� ������ �?", new string[] { "ȭâ��.", "���ξ�." }, new string[] { "����? ���� �� ���� �ͳ�.", "�̷�... �ƽ���." }),
                new DialogueOption("���� ���� � �����̾���?"),
                new DialogueOption("���� ��� ������ ���ھ�?", new string[] { "���� ũ��.", "�ǰ��ϱ⸸ �ϸ� ��." }, new string[] { "�װ� ��������...", "�θ�� ���� ���̳�! �����̾�." }),
                new DialogueOption("����� ���� �����."),
                new DialogueOption("��ȭ�ӳ�."),
                new DialogueOption("���� ����� �?", new string[] { "����.", "����." }, new string[] { "���� ���̱���! ���� ����!", "����! ������ ���� �ž�." }),
                new DialogueOption("�� �ڿ��� ����! Ǯ �Դ� �͵� ����!"),
            }
        },
        {
            2, new DialogueOption[] {
                new DialogueOption("���� �� Ŀ����!"),
                new DialogueOption("��ȫ���� ���� �� ����."),
                new DialogueOption("���� �帴���� �����ε�."),
                new DialogueOption("���� ���� �޸��� ���� ����̾�.", new string[] { "�޷�!", "�����ݾ�." }, new string[] { "�˿��ٰ� ������ �޾��ָ� ������ �ž�!", "�������̳�... ��ȭ�ϸ� �غ���." }),
                new DialogueOption("���� ���� �䳢���� �� ����. �Ƹ���."),
                new DialogueOption("������ ������ �޸��� �?", new string[] { "���� ������?", "����?" }, new string[] { "��ġ! ������ ������ ������ ���ڴ�.", "�� �� ������ ���� �� �Ƴ�?" }),
                new DialogueOption("���� �ʹ� �����ؼ� ������."),
                new DialogueOption("�ϱ�� �� ���� �־�?"),
                new DialogueOption("���� �������� ���ھ�?", new string[] { "��û ũ�� �������� ���ھ�.", "���� ����." }, new string[] { "��... ���߿� �Ǹ��ϸ� �����.", "����! �̷��� ���ȴ�." }),
            }
        },
        {
            3, new DialogueOption[] {
                new DialogueOption("�䳢�忡 ��Ҵ� ����� ��."),
                new DialogueOption("�䳢�忡 ������� ���� �Դٰ���."),
                new DialogueOption("ƴ�� ����! ���� ������ ���̱���."),
                new DialogueOption("���� �Ϸ�� ���?", new string[] { "���ο���.", "���Ҿ�." }, new string[] { "�ƽ���. ������ �ݵ�� �����ž�!", "��ڳ�! ���ϵ� ���⸦." }),
                new DialogueOption("�� ���� ����� �ӿ� �ʵ� �־�����?"),
                new DialogueOption("��� ����...", new string[] { "����?", "���Ƹ�����?" }, new string[] { "���� ���̾����� �߾�.", "�ƴ�... �䳢����." }),
                new DialogueOption("������ �ؿ��� ��� ������."),
                new DialogueOption("���ȴ�. ������ �˿��� �������?"),
                new DialogueOption("�䳢�忡 �־��� �� �����ٱ�?", new string[] { "�ñ���!", "�ƴ�." }, new string[] { "� ���̰� ��Ծ���.", "�˾Ҿ�. �̷��� �ִ� �͵� ����." }),
                new DialogueOption("�̾ �����ٱ�?", new string[] { "�ñ���!", "�ƴ�." }, new string[] { "���̰� ��û ���� �� �̾߱⸦ �����.", "������ �� �����ϴ� ����!" }),
                new DialogueOption("�������� �� ���� �ž�."),
                new DialogueOption("�� ���� ���� ��ȭ �� �����̷�."),
                new DialogueOption("�׷��� ��ȭ�� ����? �׸����� ����?"),
            }
        },
        {
            4, new DialogueOption[] {
                new DialogueOption("���� �ǰ� ����� �䳢ó�� ��Ҿ�."),
                new DialogueOption("�׷�, �� ���� ���� ���� �˾Ҿ�."),
                new DialogueOption("'�䰡����'��� ���þ�?"),
                new DialogueOption("���� ��������. �ܾ��ٷ�?", new string[] { "�׷�.", "�ƴ�." }, new string[] { "����! �� �� ģ���ϱ���.", "�ʵ� ���� �� ���?" }),
                new DialogueOption("���� �䰡������ �̾߱������ ���� ��ﳪ?", new string[] { "��, ��ﳪ.", "���� �� �ñ��ѵ�..." }, new string[] { "�� ���̰� ������ �̾߱⵵ �����.", "�� �ǰ� ��ȸ�� ����..." }),
                new DialogueOption("�� �����ܺ��� �䰡������ �� ���Ҿ�.", new string[] { "��?", "�䰡������ ���� ��?" }, new string[] { "���� ����... �� �̹� ū �Ͱ� �� ���� ���ݾ�!", "�׷� �ܼ��� ������ �ƴϾ�." }),
                new DialogueOption("�䰡������ ������ ���� ���ִ� ���̷�!"),
                new DialogueOption("�䰡������ �����߾�.", new string[] { "���ݵ�?", "��?" }, new string[] { "������! ������ �䳢�� ����.", "���� ������ �� ���� ���ݾ�!" }),
                new DialogueOption("������ ���� �Ķ� ���� ����?"),
                new DialogueOption("�� �Ķ� ����� �� ���̾�."),
                new DialogueOption("���� ������ ��� ���� �;�."),
                new DialogueOption("���� �� �ڶ�� ��� �ɱ�?"),
                new DialogueOption("���� �� �ڶ��ٰ� ������ �� �ƴ���?"),
                new DialogueOption("���� �� �ܰ� �� ���Ҿ�!"),
            }
        },
        {
            5, new DialogueOption[] {
                new DialogueOption("������� �Բ����༭ ����."),
                new DialogueOption("�� ���� ����. �ʹ� ������ �ʾ�?"),
                new DialogueOption("������ �� �ڶ󳵾�."),
                new DialogueOption("�� ��������?", new string[] { "��.", "�ƴ�." }, new string[] { "�? ��������? ������ �׷�.", "�̷� ���� ��ȸ�� �����ٴ�..." }),
                new DialogueOption("�� �ǰ� ����. ���� ��������̾�.", new string[] { "������ ������.", "������?" }, new string[] { "��§��! �̵��� ���� �����ٰ�.", "���� �׷��� ������!" }),
                new DialogueOption("�� ��� �־�?", new string[] { "�翬����.", "�� �𸣰ڳ�." }, new string[] { "������! �ڶ�������.", "���� ���ϱ�, �� �� ��� �־�." }),
                new DialogueOption("���� �� ���̾�."),
                new DialogueOption("���� ������ �� �� �־�."),
                new DialogueOption("�� ���� �� �� ����."),
                new DialogueOption("�� ���� �̷��."),
                new DialogueOption("���� ���� �ʾƵ� �� �� �־�."),
            }
        }
    };

    private List<DialogueOption> remainingDialogues = new List<DialogueOption>(); 

    void Start()
    {
        if (speechBubble != null)
            speechBubble.SetActive(false);
        if (choice != null)
            choice.SetActive(false);
        if (choice2 != null)
            choice2.SetActive(false);
        if (speechText != null)
            speechText.text = "";

        foreach (Text choiceText in choiceTexts)
        {
            choiceText.text = "";
            choiceText.gameObject.SetActive(false);
        }

        if (choiceButton1 != null)
        {
            choiceButton1.onClick.AddListener(() => OnChoiceSelected(0));
        }

        if (choiceButton2 != null)
        {
            choiceButton2.onClick.AddListener(() => OnChoiceSelected(1));
        }

        if (eggDialogueSets.ContainsKey(eggID))
        {
            remainingDialogues.AddRange(eggDialogueSets[eggID]);
        }
        else
        {
            Debug.LogError($"�� ��ȣ {eggID}�� �ش��ϴ� ��ȭ ��Ʈ�� �����ϴ�!");
        }
    }

    void Update()
    {
        // �����̽���-��ǳ�� ��Ȱ��ȭ
        if (isSpeechVisible && Input.GetKeyDown(KeyCode.Space))
        {
            HideSpeechBubble();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == choiceButton1.gameObject)
                    OnChoiceSelected(0);
                else if (hit.collider.gameObject == choiceButton2.gameObject)
                    OnChoiceSelected(1);
            }
        }
    }

    void OnMouseDown()
    {
        if (!isSpeechVisible)
        {
            ShowSpeechBubble();
        }
    }

    void ShowSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true); 
        }

        if (speechText != null)
        {
            string selectedDialogue = GetRandomDialogue(); 
            speechText.text = selectedDialogue; 
        }

        isSpeechVisible = true; 
    }

    void HideSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false);
        }

        if (speechText != null)
        {
            speechText.text = ""; 
        }

        foreach (Text choiceText in choiceTexts)
        {
            choiceText.text = ""; 
            choiceText.gameObject.SetActive(false); 
        }

        choice.SetActive(false);
        choice2.SetActive(false); 

        isSpeechVisible = false; 
    }

    private DialogueOption currentChoiceDialogue;

    string GetRandomDialogue()
    {
        if (remainingDialogues.Count == 0)
        {
            remainingDialogues.AddRange(eggDialogueSets[eggID]);
        }

        int randomIndex = Random.Range(0, remainingDialogues.Count);
        DialogueOption selectedDialogueOption = remainingDialogues[randomIndex];

        remainingDialogues.RemoveAt(randomIndex);

        if (selectedDialogueOption.HasChoices)
        {
            isWaitingForChoice = true;
            currentChoiceDialogue = selectedDialogueOption; 
            ShowChoiceImages(selectedDialogueOption); 
        }
        else
        {
            currentChoiceDialogue = null; 
        }

        return selectedDialogueOption.Dialogue;
    }

    void ShowChoiceImages(DialogueOption selectedDialogueOption)
    {
        if (choiceTexts.Length >= 2)
        {
            if (selectedDialogueOption.Choices.Length > 0)
            {
                choiceTexts[0].text = selectedDialogueOption.Choices[0]; 
                choiceTexts[0].gameObject.SetActive(true); 
                choice.SetActive(true); 
                choiceButton1.gameObject.SetActive(true); 
            }

            if (selectedDialogueOption.Choices.Length > 1)
            {
                choiceTexts[1].text = selectedDialogueOption.Choices[1]; 
                choiceTexts[1].gameObject.SetActive(true); 
                choice2.SetActive(true); 
                choiceButton2.gameObject.SetActive(true); 
            }
        }
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        if (isWaitingForChoice && currentChoiceDialogue != null)
        {
            if (currentChoiceDialogue.Reactions != null && choiceIndex < currentChoiceDialogue.Reactions.Length)
            {
                speechText.text = currentChoiceDialogue.Reactions[choiceIndex];
            }

            // ������ �����
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
            choice.SetActive(false);
            choice2.SetActive(false);

            // ���� ��� ���� ����
            isWaitingForChoice = false;
            currentChoiceDialogue = null; 
        }
    }

}

[System.Serializable]
public class DialogueOption
{
    public string Dialogue;  
    public string[] Choices; 
    public string[] Reactions; 

    public bool HasChoices => Choices != null && Choices.Length > 0;

    public DialogueOption(string dialogue)
    {
        Dialogue = dialogue;
        Choices = null;
        Reactions = null;
    }

    public DialogueOption(string dialogue, string[] choices, string[] reactions)
    {
        Dialogue = dialogue;
        Choices = choices;
        Reactions = reactions;
    }
}
