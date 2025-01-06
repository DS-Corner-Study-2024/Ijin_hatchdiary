using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public GameObject obj;
    public Text levelText; 
    public int eggLevel = 0; 
    public Button checkButton; 
    public Button nextButton; 

    private bool level25Active = false; 
    private bool level50Active = false; 
    private bool level75Active = false; 
    private bool level100Active = false; 

    void Start()
    {
        checkButton.onClick.AddListener(OnCheckButtonClick); 
        nextButton.onClick.AddListener(OnNextButtonClick); 
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

    private void UpdateObjectVisibility()
    {
        if (eggLevel >= 100 && !level100Active)
        {
            obj.SetActive(true);
            levelText.text = "�����մϴ�!\n��� ������ �Ϸ��Ͽ����ϴ�!.\n���� ������ ������Ʈ �Ǿ����ϴ�.";
            nextButton.gameObject.SetActive(true);
            level100Active = true; 
        }
        else if (eggLevel >= 75 && !level75Active)
        {
            obj.SetActive(true);
            levelText.text = "�����մϴ�!\n4�ܰ� ���忡 �����Ͽ����ϴ�.\n���� ������ ������Ʈ �Ǿ����ϴ�.";
            nextButton.gameObject.SetActive(true); 
            level75Active = true; 
        }
        else if (eggLevel >= 50 && !level50Active)
        {
            obj.SetActive(true);
            levelText.text = "�����մϴ�!\n3�ܰ� ���忡 �����Ͽ����ϴ�.\n���� ������ ������Ʈ �Ǿ����ϴ�.";
            nextButton.gameObject.SetActive(true); 
            level50Active = true; 
        }
        else if (eggLevel >= 25 && !level25Active)
        {
            obj.SetActive(true);
            levelText.text = "�����մϴ�!\n2�ܰ� ���忡 �����Ͽ����ϴ�.\n���� ������ ������Ʈ �Ǿ����ϴ�.";
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
            levelText.text = "���̰� ������ �����Ͽ����ϴ�!\n������ �޸� ���� ���� �䳢�׿�!\n���ο� ���� �ް� �ʹٸ� �������� �������� �������ּ���!";
        }
        else if (level75Active)
        {
            levelText.text = "���̰� ������ ����߳� ����. ���� ���� ���̶�...\n�װ� �����ϱ��? ��·�� ���� ������ �������̳׿�.\n���̰� ������ ������ �� �ֵ��� ���ݸ� �� �����ּ���!";
        }
        else if (level50Active)
        {
            levelText.text = "�� ���̴� ������ �䳢���� ������ ���Դϴ�.\n��ȫ�� �Ķ��� �����ϰ�, �޸��� �� ����� ����̿���.\n�ε� ���ϴ� �������� ������ ��ĥ �� ������ ���ڳ׿�!";
        }
        else if (level25Active)
        {
            levelText.text = "���̴� ���� ũ�� ���� ������ ������ ������...\n������ ������ ���󺸴� �� ũ�� ������ �� �������� �����!\n���̴� Ǫ�� �ڿ��� �����ϴ� ������ ���̴� �����ϼ���~";
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

