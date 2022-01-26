using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
   public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnGUI() {
        
    
	if (GUI.Button(new Rect(20, 100, 100, 20), "Перезапустить")) {
		SceneManager.LoadScene("SampleScene");
	}
}
    public void Insta( )
    {
        Application.OpenURL("https://www.instagram.com/nyphkastesnyphka/");
    }
}
