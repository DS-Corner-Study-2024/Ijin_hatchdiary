using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTextChanger : MonoBehaviour
{
    public Button targetButton;
    public Text targetText;
    public GameObject objectA; 

    private int clickCount = 0;

    private string[] messages = {
        "�ϱ⸦ �ۼ��Ҽ��� å�忡 ���� ��ȭ���� ���� �����ϰ� �ȴ�.",
        "�ҸӴ��� å�� �Ӹ� �ƴ϶� �ƴ϶� �ϱ⵵ �ް� �Ǿ��µ�,",
        "�װ��� \"�� �ҿ��� ���� ������ ��� ä��� ��\" �̶�� ���� ���� �־���.",
        "���� �ҸӴ��� �ҿ��� ����ؼ� ���帮��� �����ߴ�.",
        "���� ������ �ϱ⸦ �ۼ��ؼ� ���� ������ ������ ���Ѻ�����!",
        "e"
    };

    void Start()
    {
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(OnButtonClick);
        }

        if (objectA != null)
        {
            objectA.SetActive(false);
        }
    }

    void OnButtonClick()
    {
        if (clickCount < messages.Length)
        {
            targetText.text = messages[clickCount];
            clickCount++;

            if (clickCount == 2 && objectA != null)
            {
                objectA.SetActive(true);
            }

            if (clickCount == 4 && objectA != null)
            {
                objectA.SetActive(false);
            }
        }

        if (clickCount >= messages.Length)
        {
            targetButton.gameObject.SetActive(false);
            targetText.gameObject.SetActive(false);
        }
    }
}
