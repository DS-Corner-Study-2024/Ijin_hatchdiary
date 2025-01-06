using UnityEngine;
using UnityEngine.UI; 

public class IVStatus : MonoBehaviour
{
    public Text statusText; 
    private int level = 1;
    private int coins = 0;

    void Start()
    {
        UpdateStatusText();
    }

    public void AddLevel(int amount)
    {
        level += amount;
        UpdateStatusText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        statusText.text = $"레벨: {level} \n 코인: {coins}";
    }

    void Update()
    {
        // 테스트용 키 
        if (Input.GetKeyDown(KeyCode.M)) 
        {
            AddLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.N)) 
        {
            AddCoins(10);
        }
    }
}

