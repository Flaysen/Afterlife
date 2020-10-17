using System.Collections.Generic;

namespace Stats
{
    public interface IStatModifier
    {
        List<StatModifier> StatModifiers { get; }
    }
}