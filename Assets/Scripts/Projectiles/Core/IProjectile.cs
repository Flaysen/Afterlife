using UnityEngine;
using Resource;

public interface IProjectile 
{
    Rigidbody Rigidbody { get; }

    Transform Transform { get; }

    Transform Target { get; set; }
    
    IDamagable TargetToAvoid { get;}
}
