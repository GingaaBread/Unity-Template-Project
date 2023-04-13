namespace YPPGUtilities
{
    namespace UI
    {
        using UnityEngine;

        public class UIManagement : MonoBehaviour
        {
            public static UIManagement instance;

            public UIMenu currentlyOpenMenu;

            private void Awake()
            {
                instance = this;
            }

            public bool IsInMenu()
            {
                return currentlyOpenMenu != null;
            }
        }
    }
}
