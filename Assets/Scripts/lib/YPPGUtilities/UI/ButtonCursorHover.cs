namespace YPPGUtilities
{
    namespace UI
    {
        using InputManagement;
        using UnityEngine;
        using UnityEngine.EventSystems;

        /**
         *  <summary>
         *  Equips buttons with the ability to change the cursor sprite if the player hovers over it
         *  </summary>
         *
         *  <see cref="InputManagement"/>
         */
        public class ButtonCursorHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
        {
            // Determines if the button is currently being hovered over by the mouse
            private bool hovering;

            // If the input layout changes to gamepads, the hovering should be aborted to prevent visual UI bugs
            private void Start()
            {
                InputManagement.instance.inputChangedToGamepadEvent.AddListener(() => OnPointerExit(null));
            }

            // Swaps the cursor sprite to its hovered version, but only if the cursor is shown and the hover-texture exists
            public void OnPointerEnter(PointerEventData eventData)
            {
                if (InputManagement.instance.CursorIsShown && InputManagement.instance.hoverCursor != null)
                {
                    Cursor.SetCursor(InputManagement.instance.hoverCursor, Vector2.zero, CursorMode.Auto);
                    hovering = true;
                }
            }

            // Swaps the cursor sprite to its default version if the button is stopped being hovered over
            public void OnPointerExit(PointerEventData eventData)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                hovering = false;
            }

            // Disposes of the button hover
            private void OnDisable()
            {
                if (hovering)
                {
                    OnPointerExit(null);
                }
            }
        }
    }
}