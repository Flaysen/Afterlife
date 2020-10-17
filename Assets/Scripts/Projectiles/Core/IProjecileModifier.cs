using System.Collections.Generic;

namespace Projectiles
{
    public interface IProjectileBehaviourModifier
    {
        List<SpellBehaviour> ProjectileBehaviourModifiers { get; }
    }
}

