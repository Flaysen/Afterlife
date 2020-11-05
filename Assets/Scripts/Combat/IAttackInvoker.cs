using System;

namespace Combat
{
    public interface IAttackHandler
    {
        event Action OnAttackTrigger;   
        event Action OnAttackCancel;
    }
}



