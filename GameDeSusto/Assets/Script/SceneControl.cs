using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void ResetScene()
    {
        // Obter o nome da cena atual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Carregar novamente a cena atual
        SceneManager.LoadScene(currentSceneName);
    }
}
