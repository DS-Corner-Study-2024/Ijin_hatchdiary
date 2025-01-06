using UnityEngine;

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
