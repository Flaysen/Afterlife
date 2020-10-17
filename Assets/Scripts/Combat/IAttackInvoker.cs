using System;

namespace Combat
{
    public interface IAttackInvoker
    {
        event Action OnAttack;   
    }
}



