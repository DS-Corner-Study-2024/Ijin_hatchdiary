using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EggStatus : MonoBehaviour
{
    public Text eggStatusText;
    public InputField nameInputField;
    public Button saveNameButton;
    public Image eggLVImage;
    public Text growthDiaryText;

    public GameObject objectA;
    public GameObject objectB;
    public GameObject objectC;
    public GameObject objectD;
    public GameObject objectE;

    private string eggName = "알";
    public int eggLevel = 0;

    void Start()
    {
        saveNameButton.onClick.AddListener(SaveEggName);

        var eventTrigger = eggLVImage.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnEggLVImageDoubleClick(); });
        eventTrigger.triggers.Add(entry);

        UpdateEggStatusText();
        UpdateGrowthDiaryText();
        UpdateObjectVisibility();
    }

    // 레벨업
    public void AddEggLevel(int amount)
    {
        eggLevel += amount;
        UpdateEggStatusText();
        UpdateGrowthDiaryText();  // 여기에 추가
        UpdateObjectVisibility();
    }

    // 이름저장
    public void SaveEggName()
    {
        if (!string.IsNullOrEmpty(nameInputField.text))
        {
            eggName = nameInputField.text;
            UpdateEggStatusText();
            UpdateGrowthDiaryText();

            nameInputField.gameObject.SetActive(false);
            saveNameButton.gameObject.SetActive(false);
        }
    }

    // 레벨-이름 텍스트 업데이트
    private void UpdateEggStatusText()
    {
        eggStatusText.text = $"<{eggName}>\n레벨: {eggLevel}";
    }

    // 일지-이름 텍스트 업데이트
    private void UpdateGrowthDiaryText()
    {
        growthDiaryText.text = $"{eggName}의\n성장일지\n최대레벨:\n{eggLevel}";
    }

    protected virtual void UpdateObjectVisibility()
    {
        if (eggLevel >= 100)
        {
            objectD.SetActive(false);
            objectE.SetActive(true);
        }
        else if (eggLevel >= 75)
        {
            objectC.SetActive(false);
            objectD.SetActive(true);
        }
        else if (eggLevel >= 50)
        {
            objectB.SetActive(false);
            objectC.SetActive(true);
        }
        else if (eggLevel >= 25)
        {
            objectA.SetActive(false);
            objectB.SetActive(true);
        }
        else
        {
            objectA.SetActive(true);
            objectB.SetActive(false);
            objectC.SetActive(false);
            objectD.SetActive(false);
            objectE.SetActive(false);
        }
    }

    // 그림 더블 시 호출
    public void OnEggLVImageDoubleClick()
    {
        nameInputField.gameObject.SetActive(true);
        saveNameButton.gameObject.SetActive(true);

        nameInputField.text = eggName;
        nameInputField.ActivateInputField();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != nameInputField.gameObject)
        {
            // 테스트용 키
            if (Input.GetKeyDown(KeyCode.B))
            {
                AddEggLevel(5);
            }
        }
    }
}
