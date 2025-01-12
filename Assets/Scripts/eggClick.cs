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
                new DialogueOption("파란색이 좋은 것 같아."),
                new DialogueOption("난 어떻게 클까?"),
                new DialogueOption("내일도 올거지?"),
                new DialogueOption("오늘 날씨는 어때?", new string[] { "화창해.", "별로야." }, new string[] { "정말? 나도 얼른 보고 싶네.", "이런... 아쉽네." }),
                new DialogueOption("나는 원래 어떤 생물이었지?"),
                new DialogueOption("내가 어떻게 컸으면 좋겠어?", new string[] { "아주 크게.", "건강하기만 하면 돼." }, new string[] { "그건 힘들지도...", "부모님 같은 말이네! 감동이야." }),
                new DialogueOption("여기는 아주 고요해."),
                new DialogueOption("평화롭네."),
                new DialogueOption("오늘 기분은 어때?", new string[] { "좋아.", "나빠." }, new string[] { "좋은 날이구나! 나도 좋아!", "힘내! 내일은 좋을 거야." }),
                new DialogueOption("난 자연이 좋아! 풀 먹는 것도 좋아!"),
            }
        },
        {
            2, new DialogueOption[] {
                new DialogueOption("조금 더 커졌어!"),
                new DialogueOption("분홍색도 좋은 것 같아."),
                new DialogueOption("뭔가 흐릿해진 느낌인데."),
                new DialogueOption("지금 당장 달리고 싶은 기분이야.", new string[] { "달려!", "못하잖아." }, new string[] { "알에다가 바퀴를 달아주면 가능할 거야!", "현실적이네... 부화하면 해볼게." }),
                new DialogueOption("나는 원래 토끼였던 것 같아. 아마도."),
                new DialogueOption("나한테 날개가 달리면 어때?", new string[] { "정말 멋진데?", "굳이?" }, new string[] { "그치! 나한테 날개가 있으면 좋겠다.", "날 수 있으면 좋은 거 아냐?" }),
                new DialogueOption("가끔 너무 조용해서 따분해."),
                new DialogueOption("일기는 잘 쓰고 있어?"),
                new DialogueOption("내가 뭐였으면 좋겠어?", new string[] { "엄청 크고 멋있으면 좋겠어.", "뭐든 좋아." }, new string[] { "헉... 나중에 실망하면 어떡하지.", "고마워! 미래가 기대된다." }),
            }
        },
        {
            3, new DialogueOption[] {
                new DialogueOption("토끼장에 살았던 기억이 나."),
                new DialogueOption("토끼장에 사람들이 많이 왔다갔어."),
                new DialogueOption("틈이 보여! 여긴 차분한 곳이구나."),
                new DialogueOption("오늘 하루는 어땠어?", new string[] { "별로였어.", "좋았어." }, new string[] { "아쉽다. 내일은 반드시 좋을거야!", "기쁘네! 내일도 좋기를." }),
                new DialogueOption("그 많은 사람들 속에 너도 있었을까?"),
                new DialogueOption("사실 나는...", new string[] { "나는?", "병아리였어?" }, new string[] { "내가 말이었으면 했어.", "아니... 토끼였어." }),
                new DialogueOption("뭔가가 밑에서 계속 빛나네."),
                new DialogueOption("기대된다. 다음은 알에서 벗어나겠지?"),
                new DialogueOption("토끼장에 있었던 일 말해줄까?", new string[] { "궁금해!", "아니." }, new string[] { "어떤 아이가 놀러왔었어.", "알았어. 이렇게 있는 것도 좋지." }),
                new DialogueOption("이어서 말해줄까?", new string[] { "궁금해!", "아니." }, new string[] { "아이가 엄청 멋진 말 이야기를 해줬어.", "조용한 걸 좋아하는 구나!" }),
                new DialogueOption("안정적인 건 좋은 거야."),
                new DialogueOption("그 멋진 말은 신화 속 동물이래."),
                new DialogueOption("그런데 신화가 뭐지? 그리스는 뭘까?"),
            }
        },
        {
            4, new DialogueOption[] {
                new DialogueOption("나는 되게 평범한 토끼처럼 살았어."),
                new DialogueOption("그래, 그 말이 뭔지 이제 알았어."),
                new DialogueOption("'페가수스'라고 들어봤어?"),
                new DialogueOption("등이 간지러워. 긁어줄래?", new string[] { "그래.", "아니." }, new string[] { "고마워! 넌 참 친절하구나.", "너도 손이 안 닿아?" }),
                new DialogueOption("내게 페가수스를 이야기해줬던 아이 기억나?", new string[] { "응, 기억나.", "별로 안 궁금한데..." }, new string[] { "그 아이가 유니콘 이야기도 해줬어.", "너 되게 사회성 없다..." }),
                new DialogueOption("난 유니콘보다 페가수스가 더 좋았어.", new string[] { "왜?", "페가수스를 먼저 들어서?" }, new string[] { "뿔은 별로... 난 이미 큰 귀가 두 개나 있잖아!", "그런 단순한 이유가 아니야." }),
                new DialogueOption("페가수스는 날개가 정말 멋있는 말이래!"),
                new DialogueOption("페가수스를 동경했어.", new string[] { "지금도?", "왜?" }, new string[] { "여전히! 하지만 토끼도 좋아.", "아주 빠른데 날 수도 있잖아!" }),
                new DialogueOption("껍질에 붙은 파란 세모 보여?"),
                new DialogueOption("이 파란 세모는 내 발이야."),
                new DialogueOption("빨리 껍질을 모두 벗고 싶어."),
                new DialogueOption("내가 다 자라면 어떻게 될까?"),
                new DialogueOption("설마 다 자랐다고 생각한 건 아니지?"),
                new DialogueOption("아직 한 단계 더 남았어!"),
            }
        },
        {
            5, new DialogueOption[] {
                new DialogueOption("여기까지 함께해줘서 고마워."),
                new DialogueOption("내 날개 봐봐. 너무 멋지지 않아?"),
                new DialogueOption("무사히 잘 자라났어."),
                new DialogueOption("귀 만져볼래?", new string[] { "응.", "아니." }, new string[] { "어때? 폭신하지? 날개도 그래.", "이런 좋은 기회를 날리다니..." }),
                new DialogueOption("나 되게 빨라. 거의 전투기급이야.", new string[] { "거짓말 하지마.", "멋진데?" }, new string[] { "진짠데! 이따가 내가 보여줄게.", "나도 그렇게 생각해!" }),
                new DialogueOption("잘 살고 있어?", new string[] { "당연하지.", "잘 모르겠네." }, new string[] { "멋지다! 자랑스러워.", "내가 보니까, 넌 잘 살고 있어." }),
                new DialogueOption("전부 네 덕이야."),
                new DialogueOption("이제 마음껏 날 수 있어."),
                new DialogueOption("난 나는 게 참 좋아."),
                new DialogueOption("내 꿈을 이뤘어."),
                new DialogueOption("말이 되지 않아도 날 수 있어."),
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
            Debug.LogError($"알 번호 {eggID}에 해당하는 대화 세트가 없습니다!");
        }
    }

    void Update()
    {
        // 스페이스바-말풍선 비활성화
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

            // 선택지 숨기기
            choiceButton1.gameObject.SetActive(false);
            choiceButton2.gameObject.SetActive(false);
            choice.SetActive(false);
            choice2.SetActive(false);

            // 선택 대기 상태 해제
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
