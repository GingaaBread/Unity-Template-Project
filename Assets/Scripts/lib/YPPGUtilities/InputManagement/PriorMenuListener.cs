namespace YPPGUtilities
{
    namespace InputManagement
    {
        using System;
        using Service;
        using UnityEngine;
        using YPPGUtilities.UI;
        using static UnityEngine.InputSystem.InputAction;

        // Equips UI menus with the ability to return to a prior menu
        public class PriorMenuListener : MonoBehaviour
        {
            // Allows statically ignoring the listener (for example when in a certain menu that also uses the return button)
            public static bool preventListening;

            [SerializeField] private UIMenu priorMenu;

            private Action<CallbackContext> returnToPriorMenuEvent;

            private void Start()
            {
                returnToPriorMenuEvent = _ => ReturnToPriorMenu();
                InputService.Instance.inputMaster.UI.ReturnToLastMenu.performed += returnToPriorMenuEvent;
            }

            private void OnDestroy()
            {
                InputService.Instance.inputMaster.UI.ReturnToLastMenu.performed -= returnToPriorMenuEvent;
            }

            public void ReturnToPriorMenu()
            {
                if (!preventListening)
                {
                    priorMenu.Open();
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
