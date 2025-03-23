using UnityEngine;
using UnityEngine.SceneManagement;

public class UITools : MonoBehaviour
{

    public void ToggleGameObject(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
