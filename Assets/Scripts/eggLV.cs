using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EggStatus : MonoBehaviour
{
    public Text eggStatusText;
    public InputField nameInputField;
    public Button saveNameButton;
    public Button levelUpButton; 
    public Button AButton; //성장일지 버튼

    public Button closeButton; 

    public Image eggLVImage;
    public Text growthDiaryText;

    public GameObject objectA;
    public GameObject objectB;
    public GameObject objectC;
    public GameObject objectD;
    public GameObject objectE;

    public GameObject Canvas1;
    public GameObject col_0;   
    public GameObject col_0_1c_0;
    public GameObject col_0_2_0;
    public GameObject col_0_3c_0;
    public GameObject col_0_4_0;
    public GameObject col_0_5_0;

    public GameObject bookshelf_0; 
    public GameObject Year;
    public GameObject arrow_00; 

    private string eggName = "알";
    public int eggLevel = 0;

    void Start()
    {
        saveNameButton.onClick.AddListener(SaveEggName);

        if (levelUpButton != null)
        {
            levelUpButton.onClick.AddListener(OnLevelUpButtonClicked);
        }

        if (AButton != null)
        {
            AButton.onClick.AddListener(OnAButtonClicked);
        }

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        var eventTrigger = eggLVImage.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => { OnEggLVImageDoubleClick(); });
        eventTrigger.triggers.Add(entry);

        UpdateEggStatusText();
        UpdateGrowthDiaryText();
        UpdateObjectVisibility();

        if (Canvas1 != null)
        {
            Canvas1.SetActive(false);
        }
    }

    private void OnAButtonClicked()
    {
        if (Canvas1 != null)
        {
            Canvas1.SetActive(true);
        }

        if (Year != null && arrow_00 != null)
        {
            Year.SetActive(false);
            arrow_00.SetActive(false);
        }
    }

    private void OnCloseButtonClicked()
    {
        if (Canvas1 != null)
        {
            Canvas1.SetActive(false);
        }

        if (Year != null && arrow_00 != null)
        {
            Year.SetActive(true);
            arrow_00.SetActive(true);
        }
    }

    private void OnLevelUpButtonClicked()
    {
        AddEggLevel(1);
    }

    // 레벨업
    public void AddEggLevel(int amount)
    {
        eggLevel += amount;
        UpdateEggStatusText();
        UpdateGrowthDiaryText();
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
            if (col_0 != null && col_0_1c_0 != null)
            {
                col_0_5_0.SetActive(true);
            }
        }
        else if (eggLevel >= 75)
        {
            objectC.SetActive(false);
            objectD.SetActive(true);
            if (col_0 != null && col_0_1c_0 != null)
            {
                col_0_4_0.SetActive(true);
            }
        }
        else if (eggLevel >= 50)
        {
            objectB.SetActive(false);
            objectC.SetActive(true);
            if (col_0 != null && col_0_1c_0 != null)
            {
                col_0_3c_0.SetActive(true);
            }
        }
        else if (eggLevel >= 25)
        {
            objectA.SetActive(false);
            objectB.SetActive(true);

            if (col_0 != null && col_0_1c_0 != null)
            {
                col_0_1c_0.SetActive(true);
                col_0_2_0.SetActive(true);
            }
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

    // 그림 더블 클릭 시 호출
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
            if (Input.GetKeyDown(KeyCode.B))
            {
                AddEggLevel(5);
            }
        }
    }
}
