using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 이벤트 시스템 추가
using System.Collections.Generic;

public class EggClick : MonoBehaviour
{
    public GameObject speechBubble;  // 말풍선 오브젝트
    public Text speechText;          // 말풍선 텍스트
    public Text[] choiceTexts;       // 선택지 텍스트들 (각각의 선택지 텍스트)
    public GameObject choice;        // 선택지 이미지 1
    public GameObject choice2;       // 선택지 이미지 2
    public Button choiceButton1;     // 선택지 버튼 1
    public Button choiceButton2;     // 선택지 버튼 2

    private bool isSpeechVisible = false; // 말풍선 상태 추적
    private bool isWaitingForChoice = false; // 선택지 대기 상태 추적

    // 대화 목록
    private DialogueOption[] dialogueOptions =
    {
        new DialogueOption("파란색이 좋은 것 같아"),
        new DialogueOption("난 어떻게 클까?"),
        new DialogueOption("내일도 올거지?"),
        new DialogueOption("오늘 날씨는 어때?", new string[] { "좋아", "별로야" }, new string[] { "날씨가 좋으면 기분이 좋아!", "날씨가 별로면 아쉬워!" })
    };

    private List<DialogueOption> remainingDialogues = new List<DialogueOption>(); // 남은 대화 기록

    void Start()
    {
        // 시작 시 말풍선과 텍스트 숨기기
        if (speechBubble != null)
            speechBubble.SetActive(false);
        if (choice != null)
            choice.SetActive(false);
        if (choice2 != null)
            choice2.SetActive(false);
        if (speechText != null)
            speechText.text = "";

        // 선택지 텍스트들 숨기기
        foreach (Text choiceText in choiceTexts)
        {
            choiceText.text = ""; // 선택지 텍스트 초기화
            choiceText.gameObject.SetActive(false); // 텍스트 숨기기
        }

        // 선택지 버튼에 이벤트 추가
        if (choiceButton1 != null)
            choiceButton1.onClick.AddListener(() => OnChoiceSelected(0)); // 첫 번째 선택지 클릭 시
        if (choiceButton2 != null)
            choiceButton2.onClick.AddListener(() => OnChoiceSelected(1)); // 두 번째 선택지 클릭 시

        // 대화 목록을 남은 대화 리스트에 복사
        remainingDialogues.AddRange(dialogueOptions);
    }

    void Update()
    {
        // 스페이스바를 눌렀을 때 말풍선 비활성화
        if (isSpeechVisible && Input.GetKeyDown(KeyCode.Space))
        {
            HideSpeechBubble();
        }
    }

    void OnMouseDown()
    {
        // Egg를 클릭했을 때 말풍선 표시
        if (!isSpeechVisible)
        {
            ShowSpeechBubble();
        }
    }

    void ShowSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(true); // 말풍선 보이기
        }

        if (speechText != null)
        {
            string selectedDialogue = GetRandomDialogue(); // 랜덤 대화 선택
            speechText.text = selectedDialogue; // 선택된 대화 텍스트 설정
        }

        isSpeechVisible = true; // 상태 업데이트
    }

    void HideSpeechBubble()
    {
        if (speechBubble != null)
        {
            speechBubble.SetActive(false); // 말풍선 숨기기
        }

        if (speechText != null)
        {
            speechText.text = ""; // 텍스트 숨기기
        }

        // 선택지 텍스트들 숨기기
        foreach (Text choiceText in choiceTexts)
        {
            choiceText.text = ""; // 텍스트 초기화
            choiceText.gameObject.SetActive(false); // 선택지 숨기기
        }

        choice.SetActive(false); // 선택지 이미지 1 숨기기
        choice2.SetActive(false); // 선택지 이미지 2 숨기기

        isSpeechVisible = false; // 상태 업데이트
    }

    // 남은 대화 중에서 하나를 랜덤으로 선택하고, 그 대화를 제거
    string GetRandomDialogue()
    {
        if (remainingDialogues.Count == 0)
        {
            // 모든 대화가 출력되었으면 리스트를 리셋하여 다시 시작
            remainingDialogues.AddRange(dialogueOptions);
        }

        // 랜덤으로 선택
        int randomIndex = Random.Range(0, remainingDialogues.Count);
        DialogueOption selectedDialogueOption = remainingDialogues[randomIndex];

        // 선택된 대화를 남은 대화 리스트에서 제거
        remainingDialogues.RemoveAt(randomIndex);

        // 선택지가 있으면 대화 텍스트에 그 선택지를 추가
        if (selectedDialogueOption.HasChoices)
        {
            isWaitingForChoice = true;
            ShowChoiceImages(selectedDialogueOption); // 선택지 텍스트 보이기
            return selectedDialogueOption.Dialogue; // 대화는 선택지 없이 바로 반환
        }
        else
        {
            return selectedDialogueOption.Dialogue;
        }
    }

    // 선택지 텍스트 보이기
    void ShowChoiceImages(DialogueOption selectedDialogueOption)
    {
        if (choiceTexts.Length >= 2)
        {
            // 첫 번째 선택지 처리
            if (selectedDialogueOption.Choices.Length > 0)
            {
                choiceTexts[0].text = selectedDialogueOption.Choices[0]; // 첫 번째 선택지 텍스트 설정
                choiceTexts[0].gameObject.SetActive(true); // 첫 번째 선택지 보이기
                choice.SetActive(true); // 첫 번째 선택지 이미지 보이기
            }

            // 두 번째 선택지 처리
            if (selectedDialogueOption.Choices.Length > 1)
            {
                choiceTexts[1].text = selectedDialogueOption.Choices[1]; // 두 번째 선택지 텍스트 설정
                choiceTexts[1].gameObject.SetActive(true); // 두 번째 선택지 보이기
                choice2.SetActive(true); // 두 번째 선택지 이미지 보이기
            }
        }
    }

    // 선택지에 대한 반응 처리
    public void OnChoiceSelected(int choiceIndex)
    {
        if (isWaitingForChoice)
        {
            DialogueOption selectedDialogueOption = remainingDialogues[remainingDialogues.Count - 1]; // 마지막 선택된 대화

            // 대화의 선택지가 있을 경우 반응을 표시
            if (selectedDialogueOption.Reactions != null && choiceIndex < selectedDialogueOption.Reactions.Length)
            {
                // 선택한 반응을 말풍선에 출력
                speechText.text = selectedDialogueOption.Reactions[choiceIndex];
            }

            // 선택지 텍스트들 숨기기
            foreach (Text choiceText in choiceTexts)
            {
                choiceText.text = ""; // 텍스트 초기화
                choiceText.gameObject.SetActive(false); // 선택지 숨기기
            }

            choice.SetActive(false); // 선택지 이미지 1 숨기기
            choice2.SetActive(false); // 선택지 이미지 2 숨기기

            // 대화 목록에서 선택된 대화 항목 제거
            remainingDialogues.RemoveAt(remainingDialogues.Count - 1);

            // 이제 선택 후 대기 상태를 해제
            isWaitingForChoice = false;
        }
    }
}

// 대화 옵션을 관리하는 클래스
[System.Serializable]
public class DialogueOption
{
    public string Dialogue;  // 대화 텍스트
    public string[] Choices; // 선택지
    public string[] Reactions; // 선택지에 대한 반응

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
