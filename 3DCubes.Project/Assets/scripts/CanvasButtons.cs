using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
   public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void Insta( )
    {
        Application.OpenURL("https://www.instagram.com/nyphkastesnyphka/");
    }
}
