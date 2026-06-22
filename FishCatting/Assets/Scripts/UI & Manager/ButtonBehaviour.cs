using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void LoadScene(string sceneToLoad)
    {
        PlayerDataManager.instance.SaveGame();
        SceneManager.LoadScene(sceneToLoad);
    }
    public void ReloadScene()
    {
        PlayerDataManager.instance.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PlaySound(AudioSource audio)
    {
        audio.Play();
    }
}
