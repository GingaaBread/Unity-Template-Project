namespace YPPGUtilities
{
    namespace UI
    {
        using UnityEngine;
        using YPPGUtilities.InputManagement;

        public abstract class InputAdaptor : MonoBehaviour
        {
            // Layout Methods
            protected abstract void ClassicalInputLayout();
            protected abstract void PlayStation4Layout();
            protected abstract void XBoxLayout();
            protected abstract void SwitchLayout();
            protected abstract void OtherLayout();

            // Update is called once per frame
            protected void Update()
            {
                if (InputChangeListener.usingClassicalInputLayout)
                    ClassicalInputLayout();
                else
                {
                    if (InputChangeListener.currentGamepadType == InputChangeListener.GamepadType.PLAY_STATION_4)
                        PlayStation4Layout();
                    else if (InputChangeListener.currentGamepadType == InputChangeListener.GamepadType.X_BOX)
                        XBoxLayout();
                    else if (InputChangeListener.currentGamepadType == InputChangeListener.GamepadType.SWITCH)
                        SwitchLayout();
                    else if (InputChangeListener.currentGamepadType == InputChangeListener.GamepadType.OTHER)
                        OtherLayout();
                }
            }
        }
    }
}
