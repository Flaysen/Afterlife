using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System;

namespace PlayerControl
{
    [RequireComponent(typeof(TriggerOverlap), typeof(TriggerExit))]
    public class InteractionBox : MonoBehaviour
    {
        private TriggerOverlap _triggerOverlap;

        private TriggerExit _triggerExit;

        private InputHandler _input;

        private Collider _collider;

        void Awake()
        {
            _triggerOverlap = GetComponent<TriggerOverlap>();

            _triggerExit = GetComponent<TriggerExit>();

            _input = GetComponentInParent<InputHandler>();

            _collider = GetComponent<Collider>();

            _triggerOverlap.OnTrigger += DisplayInteraction;

            _triggerExit.OnExit += HideInteraction;

            _input.OnInteract += ExecuteInteraction;
        }

        private void HideInteraction(Collider collider)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            
            if(interactable != null)
            {
                interactable.HandleInteractionInfoDisplay(false);
            }
        }

        private void DisplayInteraction(Collider collider)
        {
            IInteractable interactable = collider.GetComponent<IInteractable>();
            
            if(interactable != null)
            {
                interactable.HandleInteractionInfoDisplay(true);
            }
        }

        private void ExecuteInteraction()
        {
            Collider [] colliders = Physics.OverlapBox(_collider.bounds.center, _collider.bounds.extents / 2);

            foreach(Collider collider in colliders)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();

                if(interactable != null)
                {
                    interactable.Interact();
                }
            }    
        }

    }

}
