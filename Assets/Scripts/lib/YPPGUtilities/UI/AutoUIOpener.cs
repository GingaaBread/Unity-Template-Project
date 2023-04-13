namespace YPPGUtilities
{
    namespace UI
    {
        using UnityEngine;

        /**
         *  <summary>
         *  Attached to a gameobject to automatically open a certain UIMenu when Start() is called.
         *  This can be used for menus that should be opened on a scene load, such as main menus, winning screens, etc.
         *  </summary>
         *   
         *  <see cref="UIMenu"/>
        */
        public class AutoUIOpener : MonoBehaviour
        {
            // Assign the UIMenu that should be opened in the inspector
            [SerializeField] private UIMenu menuToOpen;

            // Start is called before the first frame update
            private void Start()
            {
                menuToOpen.Open();
            }
        }
    }
}