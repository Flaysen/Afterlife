using Core;
using GameManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Maze
{
    [RequireComponent(typeof(TriggerOverlap))]
    public class MazeExit : MonoBehaviour
    {
        private TriggerOverlap _triggerOverlap;
        private LevelManager _levelManager;

        private void Awake()
        {
            _triggerOverlap = GetComponent<TriggerOverlap>();
            _triggerOverlap.OnTrigger += ExitLevel; 

            _levelManager = FindObjectOfType<LevelManager>(); 
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                _levelManager.NextLevel();
            }
        }
        private void ExitLevel(Collider collider)
        {
            if(collider.CompareTag("Player"))
            {
                //_levelManager.NextLevel();
                SceneManager.LoadScene(0);
            }
        }
    }
}


