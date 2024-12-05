using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.LogError("El nombre de la escena no ha sido asignado.");
        }
    }
}