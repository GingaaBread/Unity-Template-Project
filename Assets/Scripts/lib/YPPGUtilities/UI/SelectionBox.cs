namespace YPPGUtilities
{
    namespace UI
    {
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.Events;
        using UnityEngine.UI;

        // Adds a SelectionBox UI component that acts like a DropBox with chevrons to browse through options
        public class SelectionBox : MonoBehaviour
        {
            [SerializeField] private GameObject chevronLeft;
            [SerializeField] private GameObject chevronRight;
            [SerializeField] private Text content;

            private List<string> options = new List<string>();

            public UnityEvent OnSelect;
            public int SelectedIndex { get; set; }

            private void OnEnable()
            {
                if (options.Count != 0)
                {
                    content.text = options[SelectedIndex];
                }

                chevronLeft.SetActive(SelectedIndex != 0);
                chevronRight.SetActive(SelectedIndex != options.Count - 1);
            }

            public void SetOptions(List<string> options)
            {
                this.options = options;
            }

            public void ClearOptions()
            {
                options.Clear();
            }

            public void SelectNext()
            {
                SelectedIndex++;
                content.text = options[SelectedIndex];

                if (SelectedIndex == options.Count - 1)
                {
                    chevronRight.SetActive(false);
                }
                else
                {
                    chevronRight.SetActive(true);
                }
                chevronLeft.SetActive(true);

                OnSelect.Invoke();
            }

            public void SelectPrevious()
            {
                SelectedIndex--;
                content.text = options[SelectedIndex];

                if (SelectedIndex == 0)
                {
                    chevronLeft.SetActive(false);
                }
                else
                {
                    chevronLeft.SetActive(true);
                }
                chevronRight.SetActive(true);

                OnSelect.Invoke();
            }
        }
    }
}