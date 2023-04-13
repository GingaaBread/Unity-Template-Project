namespace YPPGUtilities
{
    namespace UI
    {
        using UnityEngine;
        using UnityEngine.UI;

        public class InputSpriteAdaptor : InputAdaptor
        {
            [Header("Sprite / Image Reference")]
            [SerializeField] private Sprite sprite;
            [SerializeField] private Image image;

            [Header("Holders")]
            [SerializeField] private Sprite keyboardMouseHolder;
            [SerializeField] private Sprite playstation4Holder;
            [SerializeField] private Sprite xBoxHolder;
            [SerializeField] private Sprite switchHolder;

            // Checks for illegal references
            private void Start()
            {
                if (sprite != null && image != null)
                    throw new System.Exception("InputSpriteAdaptor cannot reference both a Sprite and an Image");
                else if (sprite == null && image == null)
                    throw new System.NullReferenceException("InputSpriteAdaptor needs a reference to either a Sprite or an Image");
            }

            protected override void ClassicalInputLayout()
            {
                if (sprite != null) sprite = keyboardMouseHolder;
                else if (image != null) image.sprite = keyboardMouseHolder;
            }

            protected override void PlayStation4Layout()
            {
                if (sprite != null) sprite = playstation4Holder;
                else if (image != null) image.sprite = playstation4Holder;
            }

            protected override void XBoxLayout()
            {
                if (sprite != null) sprite = xBoxHolder;
                else if (image != null) image.sprite = xBoxHolder;
            }

            protected override void SwitchLayout()
            {
                if (sprite != null) sprite = switchHolder;
                else if (image != null) image.sprite = switchHolder;
            }

            protected override void OtherLayout()
            {
                if (sprite != null) sprite = xBoxHolder;
                else if (image != null) image.sprite = xBoxHolder;
            }
        }
    }
}
