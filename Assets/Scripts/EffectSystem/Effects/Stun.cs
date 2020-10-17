using UnityEngine;

[CreateAssetMenu(fileName = "dot", menuName = "Effects/Stun", order = 55)]
public class Stun : Effect
{
    private IController _controller;

    public override void BeginEffect(Transform target)
    {
        _controller = target.GetComponent<IController>();

        if(_controller != null)
        {
            _controller.IsControlDisabled = true;
            Debug.Log("Stun");
            PlayParticles(target);
        }
    }

    public override void EndEffect(Transform target)
    {
        _controller.IsControlDisabled = false;
    }
}
