using UnityEngine;

public class TrailReset : MonoBehaviour
{
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        _trailRenderer.enabled = true;    
    }

    private void OnDisable()
    {
        _trailRenderer.enabled = false;
    }
}
