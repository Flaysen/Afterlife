using UnityEngine.UI;

public interface ISlot 
{
    int Id {get; set;}

    Image Image { get; }

    bool IsLocked { get; set; }
    
}

