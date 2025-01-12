using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTextChanger : MonoBehaviour
{
    public Button targetButton;
    public Text targetText;
    public GameObject objectA; 

    private int clickCount = 0;

    private string[] messages = {
        "일기를 작성할수록 책장에 딸린 부화장의 알이 성장하게 된다.",
        "할머니의 책장 뿐만 아니라 아니라 일기도 받게 되었는데,",
        "그곳에 \"내 소원은 성장 일지를 모두 채우는 것\" 이라는 글이 적혀 있었다.",
        "나는 할머니의 소원을 대신해서 들어드리기로 다짐했다.",
        "매일 꾸준히 일기를 작성해서 알의 성장을 끝까지 지켜봐주자!",
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
