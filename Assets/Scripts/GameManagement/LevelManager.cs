using LevelGeneration;
using UnityEngine;

namespace GameManagement
{
    [RequireComponent(typeof(Minimap))]
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelData [] _levelsData;
        private MazeGenerator _mazeGenerator;
        private Minimap _minimap;
        private int _currentLevel;

        private  void Awake()
        {
            _currentLevel = 1;
            _mazeGenerator = GetComponent<MazeGenerator>();
            _minimap = FindObjectOfType<Minimap>();
            InitizalizeLevelData(_currentLevel);
        }
        public void NextLevel()
        {
            _currentLevel++;
            transform.GetChild(0).gameObject.SetActive(false);
            _minimap.ResetMinimap();
            InitizalizeLevelData(_currentLevel);
        }
        private void InitizalizeLevelData(int levelNumber)
        {   
            _mazeGenerator.LevelData = _levelsData[_currentLevel - 1];
            _mazeGenerator.ExecuteGeneration(this.gameObject.transform);
        }
    
    }
}

