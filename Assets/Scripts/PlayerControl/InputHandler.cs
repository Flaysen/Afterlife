using UnityEngine;
using System;
using Combat;
using UnityEngine.EventSystems;

namespace PlayerControl
{
    public class InputHandler : MonoBehaviour, IAttackInvoker
    {
        [SerializeField] private KeyCode _fireButton;

        [SerializeField] private KeyCode _dashButton;

        [SerializeField] private KeyCode _jumpButton;

        [SerializeField] private KeyCode _interactButton;

        [SerializeField] private KeyCode[] _spellCastButtons;

        public event Action OnAttack = delegate {};

        public event Action OnInteract = delegate {};

        public event Action<int> OnAnySpellCast = delegate {};

        public Vector3 MoveInput { get; private set; }
        public Vector3 MousePoisition { get; private set; }
        public bool IsJumpButtonPressed { get; private set; }
        public bool IsDashButtonPressed { get; private set; }

        private void LateUpdate()
        {
            GetMouseInput();

            GetKeyboardInput();
        }

        private void GetMouseInput()
        {
            MoveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            MousePoisition = Input.mousePosition;
        }

        private void GetKeyboardInput()
        {
            if (Input.GetKey(_fireButton) && !EventSystem.current.IsPointerOverGameObject()) OnAttack?.Invoke();

            if (Input.GetKey(_interactButton)) OnInteract?.Invoke();

            IsJumpButtonPressed = Input.GetKeyDown(_jumpButton) ? true : false;

            IsDashButtonPressed = (Input.GetKeyDown(_dashButton)) ? true : false;

            for (int i = 0; i < _spellCastButtons.Length; i++)
            {
                if (Input.GetKeyDown(_spellCastButtons[i]))
                {
                    OnAnySpellCast?.Invoke(i + 1);
                }
            }
        }
    }
}

