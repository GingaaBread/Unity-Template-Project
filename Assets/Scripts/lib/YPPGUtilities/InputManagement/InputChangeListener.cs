namespace YPPGUtilities
{
    namespace InputManagement
    {
        using Service;
        using UnityEngine;
        using UnityEngine.EventSystems;
        using UnityEngine.InputSystem;
        using UnityEngine.InputSystem.DualShock;
        using UnityEngine.InputSystem.Switch;
        using UnityEngine.InputSystem.XInput;
        using static UnityEngine.InputSystem.InputAction;

        // 
        /**
         *  <summary>
         *  This class is a vital part of the InputManagement that listens for input changes, swapping "classical" keyboard & mouse input and gamepad input 
         *  </summary>
         *  
         *  <see cref="InputManagement"/>
         */
        public class InputChangeListener : MonoBehaviour
        {
            // Determines if the classical input is used (Mouse & Keyboard) or not
            public static bool usingClassicalInputLayout = true;

            // Describes the current gamepad type to distinguish between them (for using icons for example)
            public static GamepadType currentGamepadType = GamepadType.NONE;

            /// <summary>
            /// This event needs to contain ALL bindings that should trigger the input change
            /// (This step is neccessary as there is no .anyKey for gamepads)
            /// </summary>
            private System.Action<CallbackContext> onAnyGamepadButtonPressedEvent;

            // This enum is expandable to add custom gamepads that are not explicitly specified
            public enum GamepadType
            {
                PLAY_STATION_4,
                X_BOX,
                SWITCH,
                OTHER,
                NONE
            }

            private void Start()
            {
                onAnyGamepadButtonPressedEvent = _ => OnAnyGamepadButtonPressed();
                InputManagement.instance.inputMaster.GameManagement.SwitchToGamepadControl.canceled += onAnyGamepadButtonPressedEvent;

                if (usingClassicalInputLayout)
                    OnInputSwitchedToClassical();
            }

            private void OnDisable()
            {
                InputService.Instance.inputMaster.GameManagement.SwitchToGamepadControl.canceled -= onAnyGamepadButtonPressedEvent;
            }

            // Update is called once per frame
            private void Update()
            {
                // Changes to the classical input when any keyboard or mouse button was clicked
                if (Keyboard.current != null && Mouse.current != null && !usingClassicalInputLayout && (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
                {
                    usingClassicalInputLayout = true;
                    OnInputSwitchedToClassical();
                }
            }

            // Changes the input layout to gamepad mode, selects the gamepad type and triggers the InputChangedToGamepad event of the InputManagement
            private void OnAnyGamepadButtonPressed()
            {
                if (usingClassicalInputLayout)
                {
                    usingClassicalInputLayout = false;
                    if (Gamepad.current != null)
                    {
                        if (Gamepad.current is DualShockGamepad)
                        {
                            currentGamepadType = GamepadType.PLAY_STATION_4;
                            print("Now using PLAYSTATION4");
                        }
                        else if (Gamepad.current is XInputController)
                        {
                            currentGamepadType = GamepadType.X_BOX;
                            print("Now using XBOX");
                        }
                        else if (Gamepad.current is SwitchProControllerHID)
                        {
                            currentGamepadType = GamepadType.SWITCH;
                            print("Now using SWITCH");
                        }
                        else
                        {
                            currentGamepadType = GamepadType.OTHER;
                            print("Now using an UNKNOWN GAMEPAD");
                        }
                    }

                    InputManagement.instance.inputChangedToGamepadEvent.Invoke();
                }
            }

            private void OnInputSwitchedToClassical()
            {
                print("Now using CLASSICAL");
                // Resets the gamepad type
                currentGamepadType = GamepadType.NONE;

                InputManagement.instance.inputChangedToClassicalEvent.Invoke();

                // Resets the selected object
                if (EventSystem.current != null)
                    EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}