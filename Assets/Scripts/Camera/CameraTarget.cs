using UnityEngine;
using Combat;
using Core;
using EZCameraShake;

public class CameraTarget : MonoBehaviour
{
    private CameraShaker _cameraShaker;

    private AttackHandler _attackHandler;

    void Awake()
    {
        Player player = FindObjectOfType<Player>();
        _attackHandler = player.GetComponent<AttackHandler>();

        _attackHandler.OnProjectileFired += ShakeCamera;
    }

    public void InitializeCameraShaker(CameraShaker cameraShaker)
    {
        _cameraShaker = cameraShaker;
    }

    private void ShakeCamera()
    {
        _cameraShaker.ShakeOnce(0.5f, 2f, .1f, .6f);
    }
}
