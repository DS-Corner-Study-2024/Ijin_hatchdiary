using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BookManager : MonoBehaviour
{
    public GameObject pencil;  // 연필 모양 그림
    public GameObject bookPrefab;  // 책 모양 그림
    public Text yearText;  // 연도 텍스트 (2025 또는 2024)
    public GameObject arrow0;  // arrow_0 그림 (연도 +1)
    public GameObject arrow1;  // arrow_1 그림 (연도 -1)

    private List<GameObject> books2026 = new List<GameObject>();
    private List<GameObject> books2025 = new List<GameObject>();
    private List<GameObject> books2024 = new List<GameObject>();

    private List<Vector3> bookPositions = new List<Vector3>();  // 책 생성 위치 목록
    private int currentBookCount2025 = 0;  // 2025년 책 카운트
    private int currentBookCount2024 = 0;  // 2024년 책 카운트
    private int currentBookCount2026 = 0;  // 2026년 책 카운트
    private string currentYear = "2025";  // 기본 연도는 2025

    void Start()
    {
        // 책 생성 위치 미리 지정 (최대 36개의 위치)
        InitializeBookPositions();

        // 연필 클릭 이벤트 연결
        pencil.GetComponent<Collider2D>().isTrigger = true;
        pencil.AddComponent<PencilClickHandler>().SetManager(this);

        // 화살표 클릭 이벤트 연결
        arrow0.GetComponent<Collider2D>().isTrigger = true;
        arrow1.GetComponent<Collider2D>().isTrigger = true;

        arrow0.AddComponent<ArrowClickHandler>().SetManager(this, 1);  // 연도 증가
        arrow1.AddComponent<ArrowClickHandler>().SetManager(this, -1); // 연도 감소

        // 초기 연도 텍스트 설정
        UpdateYearText();
    }

    void InitializeBookPositions()
    {
        float xPos = 1;  // x축 초기 위치
        float yPos = 1.35f;  // y축 초기 위치
        int maxBooksInRow = 12;  // 한 줄에 최대 12권
        int bookCountInRow = 0;  // 한 줄에 생성된 책의 개수
        int totalBooks = 0;  // 총 생성된 책의 수

        for (int i = 0; i < 36; i++)
        {
            // 책 생성 위치 지정
            bookPositions.Add(new Vector3(xPos, yPos, 0));

            // x축으로 0.3씩 이동
            xPos += 0.3f;
            totalBooks++;  // 총 책 개수 증가

            // 3개의 책을 소환할 때마다 x축을 1칸 더 이동
            bookCountInRow++;
            if (bookCountInRow == 3)
            {
                xPos += 0.7f;  // x축을 1칸 추가
                bookCountInRow = 0;  // 3개의 책을 소환했으면 카운트를 리셋
            }

            
            // 한 줄에 최대 책 수를 생성했으면, x축 초기화하고 y축을 2만큼 증가
            if ((i + 1) % maxBooksInRow == 0)
            {
                xPos = 1;  // x축 초기화
                yPos -= 1.3f;
            }
        }
    }

    // 책 생성 함수
    void OnPencilClicked()
    {
        // 연도에 맞게 책을 생성
        if (currentYear == "2025" && currentBookCount2025 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2025], Quaternion.identity);
            newBook.SetActive(true);  // 생성된 책은 기본적으로 활성화
            books2025.Add(newBook);  // 2025에 책 추가
            currentBookCount2025++;
        }
        else if (currentYear == "2024" && currentBookCount2024 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2024], Quaternion.identity);
            newBook.SetActive(true);  // 생성된 책은 기본적으로 활성화
            books2024.Add(newBook);  // 2024에 책 추가
            currentBookCount2024++;
        }
        else if (currentYear == "2026" && currentBookCount2026 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2026], Quaternion.identity);
            newBook.SetActive(true);  // 생성된 책은 기본적으로 활성화
            books2026.Add(newBook);  // 2026에 책 추가
            currentBookCount2026++;
        }
    }

    // 연도 증가 함수
    public void IncreaseYear()
    {
        if (currentYear == "2025")
        {
            currentYear = "2026";  // 2025에서 +1
        }
        else if (currentYear == "2024")
        {
            currentYear = "2025";  // 2024에서 +1
        }

        UpdateYearText();
    }

    // 연도 감소 함수
    public void DecreaseYear()
    {
        if (currentYear == "2025")
        {
            currentYear = "2024";  // 2025에서 -1
        }
        else if (currentYear == "2026")
        {
            currentYear = "2025";  // 2026에서 -1
        }

        UpdateYearText();
    }

    void UpdateYearText()
    {
        // UI 텍스트 직접 업데이트
        yearText.text = currentYear;

        // 연도 변경에 따라 책 표시 및 숨김 처리
        if (currentYear == "2025")
        {
            // 2025 연도에 해당하는 책들은 화면에 보이도록
            foreach (var book in books2024)
            {
                book.SetActive(false);  // 2024에 해당하는 책은 숨김
            }
            foreach (var book in books2025)
            {
                book.SetActive(true);  // 2025에 해당하는 책은 화면에 표시
            }
            foreach (var book in books2026)
            {
                book.SetActive(false);  // 2026에 해당하는 책은 숨김
            }
        }
        else if (currentYear == "2024")
        {
            // 2024 연도에 해당하는 책들은 화면에 보이도록
            foreach (var book in books2025)
            {
                book.SetActive(false);  // 2025에 해당하는 책은 숨김
            }
            foreach (var book in books2024)
            {
                book.SetActive(true);  // 2024에 해당하는 책은 화면에 표시
            }
            foreach (var book in books2026)
            {
                book.SetActive(false);  // 2026에 해당하는 책은 숨김
            }
        }
        else if (currentYear == "2026")
        {
            // 2026 연도에 해당하는 책들은 화면에 보이도록
            foreach (var book in books2025)
            {
                book.SetActive(false);  // 2025에 해당하는 책은 숨김
            }
            foreach (var book in books2026)
            {
                book.SetActive(true);  // 2026에 해당하는 책은 화면에 표시
            }
            foreach (var book in books2024)
            {
                book.SetActive(false);  // 2024에 해당하는 책은 숨김
            }
        }
    }

    // Arrow 클릭 핸들러 클래스 (Collider 클릭 처리)
    public class ArrowClickHandler : MonoBehaviour
    {
        private BookManager bookManager;
        private int yearChange;

        public void SetManager(BookManager manager, int change)
        {
            bookManager = manager;
            yearChange = change;
        }

        void OnMouseDown()
        {
            if (yearChange == 1)
            {
                bookManager.IncreaseYear();
            }
            else if (yearChange == -1)
            {
                bookManager.DecreaseYear();
            }
        }
    }

    // 연필 클릭 핸들러
    public class PencilClickHandler : MonoBehaviour
    {
        private BookManager bookManager;

        public void SetManager(BookManager manager)
        {
            bookManager = manager;
        }

        void OnMouseDown()
        {
            bookManager.OnPencilClicked();
        }
    }
}
