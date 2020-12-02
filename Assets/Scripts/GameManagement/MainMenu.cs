using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManagement
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }   
    }       
}


