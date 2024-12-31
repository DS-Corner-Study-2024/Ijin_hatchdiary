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
        statusText.text = $"����: {level} \n ����: {coins}";
    }

    void Update()
    {
        // �׽�Ʈ�� Ű �Է�
        if (Input.GetKeyDown(KeyCode.M)) // L Ű�� ���� ���
        {
            AddLevel(1);
        }
        if (Input.GetKeyDown(KeyCode.N)) // C Ű�� ���� �߰�
        {
            AddCoins(10);
        }
    }
}
