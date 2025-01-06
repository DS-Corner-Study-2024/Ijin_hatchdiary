using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DiaryHandler : MonoBehaviour
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;

    // 날짜 데이터
    private DateTime currentDate = DateTime.Today;
    private Dictionary<string, (string diary, string memo)> diaryData = new Dictionary<string, (string, string)>();

    [Header("날짜 UI")]
    public Text dateText;
    public Button arrowNextButton; // arrow_0
    public Button arrowPrevButton; // arrow_1

    // 내용 수정
    public InputField diaryInputField;
    public Button saveButton;
    public Text diaryText;
    public Image diaryImage;

    // 메모 수정
    public InputField diaryInputField2;
    public Button saveButton2;
    public Text diaryText2;
    public Image diaryImage2;

    void Start()
    {
        // UI 초기화
        InitializeUI();

        // 날짜 초기화
        UpdateDateDisplay();

        // 날짜 변경 버튼 리스너
        if (arrowNextButton != null) arrowNextButton.onClick.AddListener(NextDay);
        if (arrowPrevButton != null) arrowPrevButton.onClick.AddListener(PreviousDay);

        // 저장 버튼 리스너
        if (saveButton != null) saveButton.onClick.AddListener(SaveDiaryEntry);
        if (saveButton2 != null) saveButton2.onClick.AddListener(SaveDiaryEntry2);

        // 이미지 클릭 리스너
        AddImageClickListener(diaryImage, OnDiaryImageClick);
        AddImageClickListener(diaryImage2, OnDiaryImageClick2);
    }

    void InitializeUI()
    {
        if (diaryInputField != null) diaryInputField.gameObject.SetActive(false);
        if (saveButton != null) saveButton.gameObject.SetActive(false);
        if (diaryInputField2 != null) diaryInputField2.gameObject.SetActive(false);
        if (saveButton2 != null) saveButton2.gameObject.SetActive(false);
    }

    void AddImageClickListener(Image image, UnityEngine.Events.UnityAction action)
    {
        if (image != null)
        {
            var imageButton = image.GetComponent<Button>();
            if (imageButton == null)
            {
                imageButton = image.gameObject.AddComponent<Button>();
            }
            imageButton.onClick.AddListener(action);
        }
    }

    // 📅 날짜 변경
    void NextDay()
    {
        currentDate = currentDate.AddDays(1);
        UpdateDateDisplay();
    }

    void PreviousDay()
    {
        currentDate = currentDate.AddDays(-1);
        UpdateDateDisplay();
    }

    void UpdateDateDisplay()
    {
        if (dateText != null)
        {
            dateText.text = currentDate.ToString("yyyy-MM-dd");
        }

        // 해당 날짜의 데이터 불러오기
        string dateKey = currentDate.ToString("yyyy-MM-dd");

        if (diaryData.ContainsKey(dateKey))
        {
            diaryText.text = diaryData[dateKey].diary;
            diaryText2.text = diaryData[dateKey].memo;
        }
        else
        {
            diaryText.text = "";
            diaryText2.text = "";
        }
    }

    // 📝 저장 기능
    void SaveDiaryEntry()
    {
        string dateKey = currentDate.ToString("yyyy-MM-dd");
        string diaryContent = diaryInputField != null ? diaryInputField.text : "";

        if (diaryText != null)
        {
            diaryText.text = diaryContent.Replace("\\n", "\n");
        }

        if (!diaryData.ContainsKey(dateKey))
        {
            diaryData[dateKey] = ("", "");
        }

        diaryData[dateKey] = (diaryContent, diaryData[dateKey].memo);

        if (diaryInputField != null) diaryInputField.gameObject.SetActive(false);
        if (saveButton != null) saveButton.gameObject.SetActive(false);
    }

    void SaveDiaryEntry1()
    {
        // 날짜는 변경할 필요 없으므로 비워둠
    }

    void SaveDiaryEntry2()
    {
        string dateKey = currentDate.ToString("yyyy-MM-dd");
        string memoContent = diaryInputField2 != null ? diaryInputField2.text : "";

        if (diaryText2 != null)
        {
            diaryText2.text = memoContent.Replace("\\n", "\n");
        }

        if (!diaryData.ContainsKey(dateKey))
        {
            diaryData[dateKey] = ("", "");
        }

        diaryData[dateKey] = (diaryData[dateKey].diary, memoContent);

        if (diaryInputField2 != null) diaryInputField2.gameObject.SetActive(false);
        if (saveButton2 != null) saveButton2.gameObject.SetActive(false);
    }

    // 더블 클릭 처리
    void OnDiaryImageClick()
    {
        HandleDoubleClick(() => {
            if (diaryInputField != null) diaryInputField.gameObject.SetActive(true);
            if (saveButton != null) saveButton.gameObject.SetActive(true);
        });
    }

    void OnDiaryImageClick1() { }
    void OnDiaryImageClick2()
    {
        HandleDoubleClick(() => {
            if (diaryInputField2 != null) diaryInputField2.gameObject.SetActive(true);
            if (saveButton2 != null) saveButton2.gameObject.SetActive(true);
        });
    }

    void HandleDoubleClick(Action action)
    {
        if (Time.time - lastClickTime < doubleClickThreshold)
        {
            action?.Invoke();
        }
        lastClickTime = Time.time;
    }
}
