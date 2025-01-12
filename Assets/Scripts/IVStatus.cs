using UnityEngine;
using UnityEngine.UI;

public class IVStatus : MonoBehaviour
{
    public Text statusText;
    public int level = 0;
    public int coins = 0;

    public Button levelUpButton;

    private int lastRewardLevel = 0;

    void Start()
    {
        UpdateStatusText();

        if (levelUpButton != null)
        {
            levelUpButton.onClick.AddListener(OnLevelUpButtonClicked);
        }
    }

    public void AddLevel(int amount)
    {
        level += amount;

        if (level / 25 > lastRewardLevel / 25)
        {
            AddCoins(10);
            lastRewardLevel = level;
        }

        UpdateStatusText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        statusText.text = $"레벨: {level} \n코인: {coins}";
    }

    void Update()
    {
        // 테스트용 키 
        if (Input.GetKeyDown(KeyCode.M))
        {
            AddLevel(5);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddCoins(10);
        }
    }

    private void OnLevelUpButtonClicked()
    {
        AddLevel(1);
    }
}
