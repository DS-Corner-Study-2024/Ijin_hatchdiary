using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BookManager : MonoBehaviour
{
    public GameObject pencil;  // ���� ��� �׸�
    public GameObject bookPrefab;  // å ��� �׸�
    public Text yearText;  // ���� �ؽ�Ʈ (2025 �Ǵ� 2024)
    public GameObject arrow0;  // arrow_0 �׸� (���� +1)
    public GameObject arrow1;  // arrow_1 �׸� (���� -1)

    private List<GameObject> books2026 = new List<GameObject>();
    private List<GameObject> books2025 = new List<GameObject>();
    private List<GameObject> books2024 = new List<GameObject>();

    private List<Vector3> bookPositions = new List<Vector3>();  // å ���� ��ġ ���
    private int currentBookCount2025 = 0;  // 2025�� å ī��Ʈ
    private int currentBookCount2024 = 0;  // 2024�� å ī��Ʈ
    private int currentBookCount2026 = 0;  // 2026�� å ī��Ʈ
    private string currentYear = "2025";  // �⺻ ������ 2025

    void Start()
    {
        // å ���� ��ġ �̸� ���� (�ִ� 36���� ��ġ)
        InitializeBookPositions();

        // ���� Ŭ�� �̺�Ʈ ����
        pencil.GetComponent<Collider2D>().isTrigger = true;
        pencil.AddComponent<PencilClickHandler>().SetManager(this);

        // ȭ��ǥ Ŭ�� �̺�Ʈ ����
        arrow0.GetComponent<Collider2D>().isTrigger = true;
        arrow1.GetComponent<Collider2D>().isTrigger = true;

        arrow0.AddComponent<ArrowClickHandler>().SetManager(this, 1);  // ���� ����
        arrow1.AddComponent<ArrowClickHandler>().SetManager(this, -1); // ���� ����

        // �ʱ� ���� �ؽ�Ʈ ����
        UpdateYearText();
    }

    void InitializeBookPositions()
    {
        float xPos = 1;  // x�� �ʱ� ��ġ
        float yPos = 1.35f;  // y�� �ʱ� ��ġ
        int maxBooksInRow = 12;  // �� �ٿ� �ִ� 12��
        int bookCountInRow = 0;  // �� �ٿ� ������ å�� ����
        int totalBooks = 0;  // �� ������ å�� ��

        for (int i = 0; i < 36; i++)
        {
            // å ���� ��ġ ����
            bookPositions.Add(new Vector3(xPos, yPos, 0));

            // x������ 0.3�� �̵�
            xPos += 0.3f;
            totalBooks++;  // �� å ���� ����

            // 3���� å�� ��ȯ�� ������ x���� 1ĭ �� �̵�
            bookCountInRow++;
            if (bookCountInRow == 3)
            {
                xPos += 0.7f;  // x���� 1ĭ �߰�
                bookCountInRow = 0;  // 3���� å�� ��ȯ������ ī��Ʈ�� ����
            }

            
            // �� �ٿ� �ִ� å ���� ����������, x�� �ʱ�ȭ�ϰ� y���� 2��ŭ ����
            if ((i + 1) % maxBooksInRow == 0)
            {
                xPos = 1;  // x�� �ʱ�ȭ
                yPos -= 1.3f;
            }
        }
    }

    // å ���� �Լ�
    void OnPencilClicked()
    {
        // ������ �°� å�� ����
        if (currentYear == "2025" && currentBookCount2025 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2025], Quaternion.identity);
            newBook.SetActive(true);  // ������ å�� �⺻������ Ȱ��ȭ
            books2025.Add(newBook);  // 2025�� å �߰�
            currentBookCount2025++;
        }
        else if (currentYear == "2024" && currentBookCount2024 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2024], Quaternion.identity);
            newBook.SetActive(true);  // ������ å�� �⺻������ Ȱ��ȭ
            books2024.Add(newBook);  // 2024�� å �߰�
            currentBookCount2024++;
        }
        else if (currentYear == "2026" && currentBookCount2026 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2026], Quaternion.identity);
            newBook.SetActive(true);  // ������ å�� �⺻������ Ȱ��ȭ
            books2026.Add(newBook);  // 2026�� å �߰�
            currentBookCount2026++;
        }
    }

    // ���� ���� �Լ�
    public void IncreaseYear()
    {
        if (currentYear == "2025")
        {
            currentYear = "2026";  // 2025���� +1
        }
        else if (currentYear == "2024")
        {
            currentYear = "2025";  // 2024���� +1
        }

        UpdateYearText();
    }

    // ���� ���� �Լ�
    public void DecreaseYear()
    {
        if (currentYear == "2025")
        {
            currentYear = "2024";  // 2025���� -1
        }
        else if (currentYear == "2026")
        {
            currentYear = "2025";  // 2026���� -1
        }

        UpdateYearText();
    }

    void UpdateYearText()
    {
        // UI �ؽ�Ʈ ���� ������Ʈ
        yearText.text = currentYear;

        // ���� ���濡 ���� å ǥ�� �� ���� ó��
        if (currentYear == "2025")
        {
            // 2025 ������ �ش��ϴ� å���� ȭ�鿡 ���̵���
            foreach (var book in books2024)
            {
                book.SetActive(false);  // 2024�� �ش��ϴ� å�� ����
            }
            foreach (var book in books2025)
            {
                book.SetActive(true);  // 2025�� �ش��ϴ� å�� ȭ�鿡 ǥ��
            }
            foreach (var book in books2026)
            {
                book.SetActive(false);  // 2026�� �ش��ϴ� å�� ����
            }
        }
        else if (currentYear == "2024")
        {
            // 2024 ������ �ش��ϴ� å���� ȭ�鿡 ���̵���
            foreach (var book in books2025)
            {
                book.SetActive(false);  // 2025�� �ش��ϴ� å�� ����
            }
            foreach (var book in books2024)
            {
                book.SetActive(true);  // 2024�� �ش��ϴ� å�� ȭ�鿡 ǥ��
            }
            foreach (var book in books2026)
            {
                book.SetActive(false);  // 2026�� �ش��ϴ� å�� ����
            }
        }
        else if (currentYear == "2026")
        {
            // 2026 ������ �ش��ϴ� å���� ȭ�鿡 ���̵���
            foreach (var book in books2025)
            {
                book.SetActive(false);  // 2025�� �ش��ϴ� å�� ����
            }
            foreach (var book in books2026)
            {
                book.SetActive(true);  // 2026�� �ش��ϴ� å�� ȭ�鿡 ǥ��
            }
            foreach (var book in books2024)
            {
                book.SetActive(false);  // 2024�� �ش��ϴ� å�� ����
            }
        }
    }

    // Arrow Ŭ�� �ڵ鷯 Ŭ���� (Collider Ŭ�� ó��)
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

    // ���� Ŭ�� �ڵ鷯
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
