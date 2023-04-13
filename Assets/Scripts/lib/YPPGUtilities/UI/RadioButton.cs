namespace YPPGUtilities
{
    namespace UI
    {
        using UnityEngine;

        // Used to create a RadioButton UI type that is added to a group, which manages the buttons
        public class RadioButton : ButtonCursorHover
        {
            [SerializeField] private RadioGroup belongsToGroup;
            [SerializeField] private GameObject selectionIndicationMarker;

            public void Deselect()
            {
                selectionIndicationMarker.SetActive(false);
            }

            public void Select()
            {
                belongsToGroup.DeselectAll();
                selectionIndicationMarker.SetActive(true);
            }
        }
    }
}