using System;

namespace Core
{
    public interface IInteractable
    {
        event Action<bool> OnInteractDisplay;
        void Interact();
        void HandleInteractionInfoDisplay(bool isVisible);
    }
}
