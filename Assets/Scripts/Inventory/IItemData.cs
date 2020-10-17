using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemData  
{
    string Name { get; }
    string Description  { get; }
    Sprite ItemImage { get; }
}
