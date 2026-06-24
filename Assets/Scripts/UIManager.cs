using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public TextMeshProUGUI authorUI;
    public Canvas canvas;

    void Start()
    {
        canvas.gameObject.SetActive(false);
    }
    public void ShowText(string message, string author)
    {
        canvas.gameObject.SetActive(true);
        textUI.text = message;
        authorUI.text = author;

        CancelInvoke();
        Invoke("HideText", 5f);
    }

    void HideText()
    {
        canvas.gameObject.SetActive(false);
    }
}