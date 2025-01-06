using UnityEngine;
using UnityEngine.UI;  // Button 컴포넌트를 사용하기 위해 필요

public class BookClickHandler : MonoBehaviour
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;

    // UI 요소들
    private GameObject diary;
    private GameObject arrow;
    private GameObject arrow2;
    private GameObject Allx;  
    private GameObject Canvass; 
    private Text YearText;  
    private GameObject arrow_00; 
    private GameObject pencil_0;

    void Awake()
    {
        // Books 오브젝트 찾기
        GameObject booksObject = GameObject.Find("Books");
        if (booksObject != null)
        {
            Transform booksTransform = booksObject.transform;

            diary = booksTransform.Find("diary_0")?.gameObject;
            arrow = booksTransform.Find("arrow_0")?.gameObject;
            arrow2 = booksTransform.Find("arrow_1")?.gameObject;
            Allx = booksTransform.Find("Allx_0")?.gameObject;
        }

        // Canvass 캔버스와 그 안의 Year 텍스트 찾기
        Canvass = GameObject.Find("Canvass");  
        if (Canvass != null)
        {
            YearText = Canvass.GetComponentInChildren<Text>(); 

            // bookshelf_0에서 arrow_00을 찾기
            GameObject bookshelf = Canvass.transform.Find("bookshelf_0")?.gameObject;  
            if (bookshelf != null)
            {
                arrow_00 = bookshelf.transform.Find("arrow_00")?.gameObject;
                pencil_0 = bookshelf.transform.Find("pencil_0")?.gameObject;
            }
        }

        // UI 요소 비활성화 (Year, arrow_00 제외)
        SetBookUIActive(false);
        if (YearText != null) YearText.gameObject.SetActive(true); 
        if (arrow_00 != null) arrow_00.SetActive(true);
        if (pencil_0 != null) pencil_0.SetActive(true);

        if (Allx != null)
        {
            Button allxButton = Allx.GetComponent<Button>(); 
            if (allxButton != null)
            {
                allxButton.onClick.AddListener(ActivateYear); 
            }
        }
    }

    void OnMouseDown()
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            OpenBook();
        }
        lastClickTime = Time.time;
    }

    void OpenBook()
    {
        SetBookUIActive(true);

        if (YearText != null)
        {
            YearText.gameObject.SetActive(false);
        }

        if (arrow_00 != null)
        {
            arrow_00.SetActive(false);  
        }
        if (pencil_0 != null)
        {
            pencil_0.SetActive(false);
        }
    }

    private void SetBookUIActive(bool isActive)
    {
        // 'book' 클론 자체는 비활성화하지 않음
        if (diary != null) diary.SetActive(isActive);
        if (arrow != null) arrow.SetActive(isActive);
        if (arrow2 != null) arrow2.SetActive(isActive);
        if (Allx != null) Allx.SetActive(isActive);
    }

    // "Allx" 버튼 클릭 시 Year UI 요소와 arrow_00 활성화, 다른 UI 요소들은 비활성화
    public void ActivateYear()
    {
        if (YearText != null)
        {
            YearText.gameObject.SetActive(true);
        }
        if (arrow_00 != null)
        {
            arrow_00.gameObject.SetActive(true); 
        }
        if (pencil_0 != null)
        {
            pencil_0.gameObject.SetActive(true);  
        }

        SetBookUIActive(false);
    }

    // "Allx"를 클릭했을 때 UI 요소들 비활성화
    public void CloseBook()
    {
        SetBookUIActive(false);
        if (YearText != null) YearText.gameObject.SetActive(false); 
        if (arrow_00 != null) arrow_00.SetActive(false);
        if (pencil_0 != null) pencil_0.SetActive(false);
    }
}
