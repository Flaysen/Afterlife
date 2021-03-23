using UnityEngine;
using TMPro;

public class InstructionPanel : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();    
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.H))   
        {
            _text.enabled = !_text.enabled;
        }
    }
}
