namespace YPPGUtilities
{
    namespace UI
    {
        using UnityEngine;

        // Used to manage an array radio buttons, which only lets one button be active at the same time
        public class RadioGroup : MonoBehaviour
        {
            // Determines which button index should be activated by default
            public int defaultSelectedRadioButton;
            
            [SerializeField] private RadioButton[] radioButtons;

            private void Awake()
            {
                radioButtons[defaultSelectedRadioButton].Select();
            }

            public void DeselectAll()
            {
                foreach (RadioButton radioButton in radioButtons)
                {
                    radioButton.Deselect();
                }
            }
        }
    }
}