using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void SaveGame()
    {
        //input saving here: should happen often enough
    }
    public void LoadGame()
    {
        //input loading here
    }
        public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }
}
