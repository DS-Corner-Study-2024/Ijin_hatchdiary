using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BookManager : MonoBehaviour
{
    public GameObject pencil;
    public GameObject bookPrefab;
    public Text yearText;
    public GameObject arrow0;
    public GameObject arrow1;

    private List<GameObject> books2026 = new List<GameObject>();
    private List<GameObject> books2025 = new List<GameObject>();
    private List<GameObject> books2024 = new List<GameObject>();

    private List<Vector3> bookPositions = new List<Vector3>();
    private int currentBookCount2025 = 0;
    private int currentBookCount2024 = 0;
    private int currentBookCount2026 = 0;
    private string currentYear = "2025";

    void Start()
    {
        InitializeBookPositions();

        pencil.GetComponent<Collider2D>().isTrigger = true;
        pencil.AddComponent<PencilClickHandler>().SetManager(this);

        arrow0.GetComponent<Collider2D>().isTrigger = true;
        arrow1.GetComponent<Collider2D>().isTrigger = true;

        arrow0.AddComponent<ArrowClickHandler>().SetManager(this, 1);
        arrow1.AddComponent<ArrowClickHandler>().SetManager(this, -1);

        UpdateYearText();
    }

    void InitializeBookPositions()
    {
        float xPos = 1;
        float yPos = 1.35f;
        int maxBooksInRow = 12;
        int bookCountInRow = 0;
        int totalBooks = 0;

        for (int i = 0; i < 36; i++)
        {
            bookPositions.Add(new Vector3(xPos, yPos, 0));

            xPos += 0.3f;
            totalBooks++;

            bookCountInRow++;
            if (bookCountInRow == 3)
            {
                xPos += 0.7f;
                bookCountInRow = 0;
            }

            if ((i + 1) % maxBooksInRow == 0)
            {
                xPos = 1;
                yPos -= 1.3f;
            }
        }
    }

    public void OnPencilClicked()
    {
        if (currentYear == "2025" && currentBookCount2025 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2025], Quaternion.identity);
            newBook.SetActive(true);
            books2025.Add(newBook);
            currentBookCount2025++;
        }
        else if (currentYear == "2024" && currentBookCount2024 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2024], Quaternion.identity);
            newBook.SetActive(true);
            books2024.Add(newBook);
            currentBookCount2024++;
        }
        else if (currentYear == "2026" && currentBookCount2026 < 36)
        {
            GameObject newBook = Instantiate(bookPrefab, bookPositions[currentBookCount2026], Quaternion.identity);
            newBook.SetActive(true);
            books2026.Add(newBook);
            currentBookCount2026++;
        }
    }

    public void IncreaseYear()
    {
        if (currentYear == "2025")
        {
            currentYear = "2026";
        }
        else if (currentYear == "2024")
        {
            currentYear = "2025";
        }

        UpdateYearText();
    }

    public void DecreaseYear()
    {
        if (currentYear == "2025")
        {
            currentYear = "2024";
        }
        else if (currentYear == "2026")
        {
            currentYear = "2025";
        }

        UpdateYearText();
    }

    void UpdateYearText()
    {
        yearText.text = currentYear;

        if (currentYear == "2025")
        {
            foreach (var book in books2024) book.SetActive(false);
            foreach (var book in books2025) book.SetActive(true);
            foreach (var book in books2026) book.SetActive(false);
        }
        else if (currentYear == "2024")
        {
            foreach (var book in books2025) book.SetActive(false);
            foreach (var book in books2024) book.SetActive(true);
            foreach (var book in books2026) book.SetActive(false);
        }
        else if (currentYear == "2026")
        {
            foreach (var book in books2025) book.SetActive(false);
            foreach (var book in books2026) book.SetActive(true);
            foreach (var book in books2024) book.SetActive(false);
        }
    }
}
