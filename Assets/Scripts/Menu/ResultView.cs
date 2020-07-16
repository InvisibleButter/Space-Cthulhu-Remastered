using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultView : MonoBehaviour
{
    public Color FailColor, WinColor;
    public TextMeshProUGUI ResultTxt;
    public TextMeshProUGUI DescrTxt;
    public GameObject Result;

    public void ShowResult(bool win)
    {
        Result.SetActive(true);
        
        ResultTxt.text = win ? "Gratulation! You won!" : "You lost!";
        DescrTxt.text = win ? "You escaped the chtulhus and reached the board computer." : "You were killed by the cthulhus. Try again.";
        ResultTxt.color = win ? WinColor : FailColor;

        Cursor.lockState = CursorLockMode.None;
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
