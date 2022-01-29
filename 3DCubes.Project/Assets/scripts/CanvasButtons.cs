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
        if(PlayerPrefs.GetString("music")=="Yes")
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Main");
    }
     public void LoadShop()
    {
        if(PlayerPrefs.GetString("music")=="Yes")
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Shop");
    }
    public void CloseShop()
    {
        if(PlayerPrefs.GetString("music")=="Yes")
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Main");
    }  
      //private void OnGUI() {
	//if ( && GUI.Button(new Rect(20, 100, 100, 20), "Перезапустить")) {
		//SceneManager.LoadScene("Main");
	//}}

    public void Insta( )
    {
        if(PlayerPrefs.GetString("music")=="Yes")
        GetComponent<AudioSource>().Play();
        Application.OpenURL("https://www.instagram.com/nyphkastesnyphka/");
    }
    public void MusicWork()
    {
        if(PlayerPrefs.GetString("music")=="No"){
            PlayerPrefs.SetString("music","Yes");
            GetComponent<Image>().sprite=musicOn;
 GetComponent<AudioSource>().Play();
        }
        else
        {
            PlayerPrefs.SetString("music","No");
            GetComponent<Image>().sprite=musicOff;
        } 
    }
}
