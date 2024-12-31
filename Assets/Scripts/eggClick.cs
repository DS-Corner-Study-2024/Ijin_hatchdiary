using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // �̺�Ʈ �ý��� �߰�
using System.Collections.Generic;

public class EggClick : MonoBehaviour
{
    public GameObject speechBubble;  // ��ǳ�� ������Ʈ
    public Text speechText;          // ��ǳ�� �ؽ�Ʈ
    public Text[] choiceTexts;       // ������ �ؽ�Ʈ�� (������ ������ �ؽ�Ʈ)
    public GameObject choice;        // ������ �̹��� 1
    public GameObject choice2;       // ������ �̹��� 2
    public Button choiceButton1;     // ������ ��ư 1
    public Button choiceButton2;     // ������ ��ư 2

    private bool isSpeechVisible = false; // ��ǳ�� ���� ����
    private bool isWaitingForChoice = false; // ������ ��� ���� ����

    // ��ȭ ���
    private DialogueOption[] dialogueOptions =
    {
        new DialogueOption("�Ķ����� ���� �� ����"),
        new DialogueOption("�� ��� Ŭ��?"),
        new DialogueOption("���ϵ� �ð���?"),
        new DialogueOption("���� ������ �?", new string[] { "����", "���ξ�" }, new string[] { "������ ������ ����� ����!", "������ ���θ� �ƽ���!" })
    };

    private List<DialogueOption> remainingDialogues = new List<DialogueOption>(); // ���� ��ȭ ���

    void Start()
    {
        // ���� �� ��ǳ���� �ؽ�Ʈ �����
        if (speechBubble != null)
            speechBubble.SetActive(false);
        if (choice != null)
            choice.SetActive(false);
        if (choice2 != null)
            choice2.SetActive(false);
        if (speechText != null)
            speechText.text = "";

        // ������ �ؽ�Ʈ�� �����
        foreach (Text choiceText in choiceTexts)
        {
            choiceText.text = ""; // ������ �ؽ�Ʈ �ʱ�ȭ
            choiceText.gameObject.SetActive(false); // �ؽ�Ʈ �����
        }

        // ������ ��ư�� �̺�Ʈ �߰�
        if (choiceButton1 != null)
            choiceButton1.onClick.AddListener(() => OnChoiceSelected(0)); // ù ��° ������ Ŭ�� ��
        if (choiceButton2 != null)
            choiceButton2.onClick.AddListener(() => OnChoiceSelected(1)); // �� ��° ������ Ŭ�� ��

        // ��ȭ ����� ���� ��ȭ ����Ʈ�� ����
        remainingDialogues.AddRange(dialogueOptions);
    }

    void Update()
    {
        // �����̽��ٸ� ������ �� ��ǳ�� ��Ȱ��ȭ
        if (isSpeechVisible && Input.GetKeyDown(KeyCode.Space))
        {
            HideSpeechBubble();
        }
    }

    void OnMouseDown()
    {
        // Egg�� Ŭ������ �� ��ǳ�� ǥ��
        if (!isSpeechVisible)
        {
            ShowSpeechBubble();
        }
    }

    void ShowSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true); // ��ǳ�� ���̱�
        }

        if (speechText != null)
        {
            string selectedDialogue = GetRandomDialogue(); // ���� ��ȭ ����
            speechText.text = selectedDialogue; // ���õ� ��ȭ �ؽ�Ʈ ����
        }

        isSpeechVisible = true; // ���� ������Ʈ
    }

    void HideSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false); // ��ǳ�� �����
        }

        if (speechText != null)
        {
            speechText.text = ""; // �ؽ�Ʈ �����
        }

        // ������ �ؽ�Ʈ�� �����
        foreach (Text choiceText in choiceTexts)
        {
            choiceText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
            choiceText.gameObject.SetActive(false); // ������ �����
        }

        choice.SetActive(false); // ������ �̹��� 1 �����
        choice2.SetActive(false); // ������ �̹��� 2 �����

        isSpeechVisible = false; // ���� ������Ʈ
    }

    // ���� ��ȭ �߿��� �ϳ��� �������� �����ϰ�, �� ��ȭ�� ����
    string GetRandomDialogue()
    {
        if (remainingDialogues.Count == 0)
        {
            // ��� ��ȭ�� ��µǾ����� ����Ʈ�� �����Ͽ� �ٽ� ����
            remainingDialogues.AddRange(dialogueOptions);
        }

        // �������� ����
        int randomIndex = Random.Range(0, remainingDialogues.Count);
        DialogueOption selectedDialogueOption = remainingDialogues[randomIndex];

        // ���õ� ��ȭ�� ���� ��ȭ ����Ʈ���� ����
        remainingDialogues.RemoveAt(randomIndex);

        // �������� ������ ��ȭ �ؽ�Ʈ�� �� �������� �߰�
        if (selectedDialogueOption.HasChoices)
        {
            isWaitingForChoice = true;
            ShowChoiceImages(selectedDialogueOption); // ������ �ؽ�Ʈ ���̱�
            return selectedDialogueOption.Dialogue; // ��ȭ�� ������ ���� �ٷ� ��ȯ
        }
        else
        {
            return selectedDialogueOption.Dialogue;
        }
    }

    // ������ �ؽ�Ʈ ���̱�
    void ShowChoiceImages(DialogueOption selectedDialogueOption)
    {
        if (choiceTexts.Length >= 2)
        {
            // ù ��° ������ ó��
            if (selectedDialogueOption.Choices.Length > 0)
            {
                choiceTexts[0].text = selectedDialogueOption.Choices[0]; // ù ��° ������ �ؽ�Ʈ ����
                choiceTexts[0].gameObject.SetActive(true); // ù ��° ������ ���̱�
                choice.SetActive(true); // ù ��° ������ �̹��� ���̱�
            }

            // �� ��° ������ ó��
            if (selectedDialogueOption.Choices.Length > 1)
            {
                choiceTexts[1].text = selectedDialogueOption.Choices[1]; // �� ��° ������ �ؽ�Ʈ ����
                choiceTexts[1].gameObject.SetActive(true); // �� ��° ������ ���̱�
                choice2.SetActive(true); // �� ��° ������ �̹��� ���̱�
            }
        }
    }

    // �������� ���� ���� ó��
    public void OnChoiceSelected(int choiceIndex)
    {
        if (isWaitingForChoice)
        {
            DialogueOption selectedDialogueOption = remainingDialogues[remainingDialogues.Count - 1]; // ������ ���õ� ��ȭ

            // ��ȭ�� �������� ���� ��� ������ ǥ��
            if (selectedDialogueOption.Reactions != null && choiceIndex < selectedDialogueOption.Reactions.Length)
            {
                // ������ ������ ��ǳ���� ���
                speechText.text = selectedDialogueOption.Reactions[choiceIndex];
            }

            // ������ �ؽ�Ʈ�� �����
            foreach (Text choiceText in choiceTexts)
            {
                choiceText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
                choiceText.gameObject.SetActive(false); // ������ �����
            }

            choice.SetActive(false); // ������ �̹��� 1 �����
            choice2.SetActive(false); // ������ �̹��� 2 �����

            // ��ȭ ��Ͽ��� ���õ� ��ȭ �׸� ����
            remainingDialogues.RemoveAt(remainingDialogues.Count - 1);

            // ���� ���� �� ��� ���¸� ����
            isWaitingForChoice = false;
        }
    }
}

// ��ȭ �ɼ��� �����ϴ� Ŭ����
[System.Serializable]
public class DialogueOption
{
    public string Dialogue;  // ��ȭ �ؽ�Ʈ
    public string[] Choices; // ������
    public string[] Reactions; // �������� ���� ����

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
