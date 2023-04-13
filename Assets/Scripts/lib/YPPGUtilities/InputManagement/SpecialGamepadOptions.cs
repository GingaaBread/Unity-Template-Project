namespace YPPGUtilities
{
    namespace InputManagement
    {
        using UnityEngine;
        using UnityEngine.InputSystem;
        using UnityEngine.InputSystem.DualShock;
        using System.Collections;

        // Offers additional gamepad option management, such as Rumble or Dualshock4 bar-lighting
        public class SpecialGamepadOptions : MonoBehaviour
        {
            // Sets the dualshock bar-light to this colour by default
            [SerializeField] private Color defaultGamepadColour;

            // Lets the gamepad rumble for a set amount of time and intensity
            public void RumbleGamepad(float leftStrength, float rightStrength, float duration)
            {
                if (Gamepad.current != null && !InputChangeListener.usingClassicalInputLayout)
                {
                    StartCoroutine(Rumble(leftStrength, rightStrength, duration));
                }
            }

            private IEnumerator Rumble(float leftStrength, float rightStrength, float duration)
            {
                Gamepad.current?.SetMotorSpeeds(leftStrength, rightStrength);
                yield return new WaitForSecondsRealtime(duration);
                Gamepad.current?.SetMotorSpeeds(0f, 0f);
                BlinkControllerLight(defaultGamepadColour, 1, 0.5f);
            }

            // Lets the gamepad light blink
            public void BlinkControllerLight(Color colour, int repetitionAmount, float blinkingTime)
            {
                if (DualShockGamepad.current != null && !InputChangeListener.usingClassicalInputLayout)
                {
                    StartCoroutine(Blink(colour, repetitionAmount, blinkingTime));
                }
            }

            private IEnumerator Blink(Color colour, int repetitionAmount, float blinkingTime)
            {
                DualShockGamepad gamepad = (DualShockGamepad)Gamepad.current;

                for (int i = 0; i < repetitionAmount; i++)
                {
                    gamepad.SetLightBarColor(colour);
                    yield return new WaitForSecondsRealtime(blinkingTime);
                    gamepad.SetLightBarColor(Color.white);
                    yield return new WaitForSecondsRealtime(blinkingTime);
                }

                gamepad.SetLightBarColor(defaultGamepadColour);
            }
        }
    }
}