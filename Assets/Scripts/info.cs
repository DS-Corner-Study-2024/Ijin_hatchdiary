using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public GameObject obj;
    public Text levelText;
    public int eggLevel = 0;
    public Button checkButton;
    public Button nextButton;
    public Button levelUpButton; 

    private bool level25Active = false;
    private bool level50Active = false;
    private bool level75Active = false;
    private bool level100Active = false;

    void Start()
    {
        checkButton.onClick.AddListener(OnCheckButtonClick);
        nextButton.onClick.AddListener(OnNextButtonClick);
        levelUpButton.onClick.AddListener(OnLevelUpButtonClick);

        UpdateObjectVisibility();
        checkButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            eggLevel += 5;
            UpdateObjectVisibility();
        }
    }

    private void OnLevelUpButtonClick()
    {
        eggLevel += 1;
        UpdateObjectVisibility();
        levelText.text = $"레벨이 1 증가했습니다! 현재 레벨: {eggLevel}";
    }

    private void UpdateObjectVisibility()
    {
        if (eggLevel >= 100 && !level100Active)
        {
            obj.SetActive(true);
            levelText.text = "축하합니다!\n모든 성장을 완료하였습니다!.\n성장 일지가 업데이트 되었습니다.";
            nextButton.gameObject.SetActive(true);
            level100Active = true;
        }
        else if (eggLevel >= 75 && !level75Active)
        {
            obj.SetActive(true);
            levelText.text = "축하합니다!\n4단계 성장에 돌입하였습니다.\n성장 일지가 업데이트 되었습니다.";
            nextButton.gameObject.SetActive(true);
            level75Active = true;
        }
        else if (eggLevel >= 50 && !level50Active)
        {
            obj.SetActive(true);
            levelText.text = "축하합니다!\n3단계 성장에 돌입하였습니다.\n성장 일지가 업데이트 되었습니다.";
            nextButton.gameObject.SetActive(true);
            level50Active = true;
        }
        else if (eggLevel >= 25 && !level25Active)
        {
            obj.SetActive(true);
            levelText.text = "축하합니다!\n2단계 성장에 돌입하였습니다.\n성장 일지가 업데이트 되었습니다.";
            nextButton.gameObject.SetActive(true);
            level25Active = true;
        }
        else if (eggLevel < 25)
        {
            obj.SetActive(false);
            checkButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            ResetLevelStates();
        }
    }

    private void OnCheckButtonClick()
    {
        obj.SetActive(false);
        checkButton.gameObject.SetActive(false);
    }

    private void OnNextButtonClick()
    {
        if (level100Active)
        {
            levelText.text = "아이가 완전히 성장하였습니다!\n날개가 달린 아주 멋진 토끼네요!\n새로운 알을 받고 싶다면 상점에서 코인으로 구입해주세요!";
        }
        else if (level75Active)
        {
            levelText.text = "아이가 전생을 기억했나 봐요. 아주 멋진 말이라...\n그게 무엇일까요? 어쨌든 이제 다음이 마지막이네요.\n아이가 무사히 성장할 수 있도록 조금만 더 도와주세요!";
        }
        else if (level50Active)
        {
            levelText.text = "이 아이는 전생에 토끼였던 것으로 보입니다.\n분홍과 파랑을 좋아하고, 달리는 게 취미인 모양이에요.\n부디 원하는 방향으로 성장을 마칠 수 있으면 좋겠네요!";
        }
        else if (level25Active)
        {
            levelText.text = "아이는 별로 크지 않을 것으로 예상이 되지만...\n정성껏 돌보면 예상보다 더 크게 성장할 수 있을지도 몰라요!\n아이는 푸른 자연을 좋아하는 것으로 보이니 참고하세요~";
        }

        nextButton.gameObject.SetActive(false);
        checkButton.gameObject.SetActive(true);
    }

    private void ResetLevelStates()
    {
        level25Active = false;
        level50Active = false;
        level75Active = false;
        level100Active = false;
    }
}
