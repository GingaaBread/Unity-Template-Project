using UnityEngine;
using YPPGUtilities.UI;

public class AdaptedInputSelector : InputAdaptor
{
    // The Holders
    [SerializeField] private GameObject keyboardMouseHolder;
    [SerializeField] private GameObject playstation4Holder;
    [SerializeField] private GameObject xBoxHolder;
    [SerializeField] private GameObject switchHolder;

    // Show the classical holder and hide the gamepad holders
    protected override void ClassicalInputLayout()
    {
        keyboardMouseHolder.SetActive(true);
        playstation4Holder.SetActive(false);
        xBoxHolder.SetActive(false);
        switchHolder.SetActive(false);
    }

    // Show the PS4 holder and hide the classical holder
    protected override void PlayStation4Layout()
    {
        playstation4Holder.SetActive(true);
        keyboardMouseHolder.SetActive(false);
    }

    // Show the XBox holder and hide the classical holder
    protected override void XBoxLayout()
    {
        xBoxHolder.SetActive(true);
        keyboardMouseHolder.SetActive(false);
    }

    // Show the Switch holder and hide the classical holder
    protected override void SwitchLayout()
    {
        switchHolder.SetActive(true);
        keyboardMouseHolder.SetActive(false);
    }

    // Show the Other holder and hide the classical holder
    protected override void OtherLayout()
    {
        xBoxHolder.SetActive(true);
        keyboardMouseHolder.SetActive(false);
    }
}
