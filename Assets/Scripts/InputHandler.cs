using System.Drawing.Text;
using UnityEngine;

namespace Mechadroids {
    /// <summary>
    /// Handles input state from the Input System
    /// </summary>
    public class InputHandler {
        private InputActions inputActions;

        public Vector2 MovementInput { get; private set; }
        public Vector2 MouseDelta { get; private set; }
        public InputActions InputActions { get { inputActions ??= new InputActions(); return inputActions; } }

        public void Initialize() {
            
            InputActions.Enable();
            InputActions.Player.Move.performed += ctx => MovementInput = ctx.ReadValue<Vector2>();
            InputActions.Player.Move.canceled += ctx => MovementInput = Vector2.zero;

            InputActions.Player.Look.performed += ctx => MouseDelta = ctx.ReadValue<Vector2>();
            InputActions.Player.Look.canceled += ctx => MouseDelta = Vector2.zero;

            //InputActions.Player.Attack.performed += ctx =>
        }

        public void SetCursorState(bool visibility, CursorLockMode lockMode) {
            Cursor.visible = visibility;
            Cursor.lockState = lockMode;
        }

        public void Dispose() {
            SetCursorState(true, CursorLockMode.None);
            InputActions.Disable();
            InputActions.Player.Move.performed -= ctx => MovementInput = ctx.ReadValue<Vector2>();
            InputActions.Player.Move.canceled -= ctx => MovementInput = Vector2.zero;

            InputActions.Player.Look.performed -= ctx => MouseDelta = ctx.ReadValue<Vector2>();
            InputActions.Player.Look.canceled -= ctx => MouseDelta = Vector2.zero;
        }
    }
}
