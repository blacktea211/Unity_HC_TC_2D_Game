using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// 按下開始遊戲後延遲一下(按鈕音效播完)在切換場景
    /// </summary>
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
