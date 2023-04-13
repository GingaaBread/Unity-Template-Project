namespace YPPGUtilities.InputManagement
{
    using UnityEngine;
    using UnityEngine.UI;
    using YPPGUtilities.UI;

    public class ScrollViewItem : MonoBehaviour
    {
        public ScrollViewItemType scrollViewItemType;

        public Toggle toggle;
        public Slider slider;
        public SelectionBox selectionBox;

        public RectTransform RectTransform { get; private set; }
        private Animator highlighterAnimator;

        public enum ScrollViewItemType
        {
            // A toggle will be instantly toggled on selection
            TOGGLE,
            // A slider will wait for stick input on selection and increase/decrease it accordingly
            SLIDER,
            // A selection box will allow switching from the left to the right arrow
            SELECTION_BOX
        }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            highlighterAnimator = GetComponent<Animator>();

            CheckReferences();
        }

        // Checks if the reference setup is illegal
        private void CheckReferences()
        {
            if (scrollViewItemType == ScrollViewItemType.TOGGLE && toggle == null)
                throw new System.Exception("ScrollViewItemType set to toggle, but toggle reference not set");
            else if (scrollViewItemType == ScrollViewItemType.SLIDER && slider == null)
                throw new System.Exception("ScrollViewItemType set to slider, but slider reference not set");
            else if (scrollViewItemType == ScrollViewItemType.SELECTION_BOX && selectionBox == null)
                throw new System.Exception("ScrollViewItemType set to selection box, but selection box reference not set");

            if (toggle != null && slider != null || toggle != null && selectionBox != null || slider != null && selectionBox != null)
                throw new System.Exception("ScrollViewItemType reference should not be ambigious (multiple references set)");
        }

        public void OnHighlight() => highlighterAnimator.Play("ScrollViewItemHighlighted");
        public void OnUnhighlight() => highlighterAnimator.Play("ScrollViewItemUnhighlighted");
    }
}
