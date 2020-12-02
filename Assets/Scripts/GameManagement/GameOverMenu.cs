
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class GameOverMenu : MonoBehaviour
    {
        public void ReloadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        } 
    }
}


