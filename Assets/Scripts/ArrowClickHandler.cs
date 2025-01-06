using UnityEngine;

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
