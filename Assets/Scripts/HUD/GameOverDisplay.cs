using System.Collections;
using System.Collections.Generic;
using Resource;
using UnityEngine;
using TMPro;

namespace HUD
{
    public class GameOverDisplay : MonoBehaviour
    {
        private void Start() 
        {
            PlayerHealthBehaviour.OnPlayerDeath += x;
        }

        private void x()
        {
            Invoke("DisplayGameOverScreen", 2.0f);
        }
        
        private void DisplayGameOverScreen()
        {
            foreach(RectTransform child in transform)
            {
                child.transform.gameObject.SetActive(true);
            }

        }

        private void OnDisable()
        {
            PlayerHealthBehaviour.OnPlayerDeath -=x;
        }
    }
}


