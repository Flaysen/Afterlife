using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellSystem
{
    public interface ISpellsProvider 
    {
        List<SpellData> Spells {get; }
    }
}

