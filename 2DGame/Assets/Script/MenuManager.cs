using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void DelayGameStart()
    {
        Invoke("StartGame", 0.8f);
            }
    public void GameStart()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>

    public void DelayQuitGame()
    {
        Invoke("QuitGame", 0.8f);
    }
    public void QuitGame()
    {
   Application.Quit();
    }
}
