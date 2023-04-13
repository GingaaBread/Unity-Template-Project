namespace YPPGUtilities
{
    namespace UI
    {
        using UnityEngine;
        using UnityEngine.EventSystems;

        public class UIMenu : MonoBehaviour
        {
            // This UI object will be selected / navigated as soon as the menu is opened
            [SerializeField] private GameObject selectedGameObject;

            private void Start()
            {
                SetSelectedGameObject();
            }

            public void Open()
            {
                gameObject.SetActive(true);

                if (EventSystem.current != null)
                    EventSystem.current.SetSelectedGameObject(selectedGameObject);
            }

            public void SetSelectedGameObject()
            {
                if (EventSystem.current != null)
                    EventSystem.current.SetSelectedGameObject(selectedGameObject);
            }
        }
    }
}