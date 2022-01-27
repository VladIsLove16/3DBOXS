using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class CanvasButtons : MonoBehaviour{
public Sprite musicOn,musicOff;
private void Start(){
     if(PlayerPrefs.GetString("music")=="No" && gameObject.name=="Music")
     {
            
            GetComponent<Image>().sprite=musicOff;
        }
        

}

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
    public void MusicWork()
    {
        if(PlayerPrefs.GetString("music")=="No"){
            PlayerPrefs.SetString("music","Yes");
            GetComponent<Image>().sprite=musicOn;
        }
        else
        {
            PlayerPrefs.SetString("music","No");
            GetComponent<Image>().sprite=musicOff;
        } 
    }
}
