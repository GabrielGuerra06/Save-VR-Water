using UnityEngine;
using UnityEngine.XR;

namespace DefaultNamespace
{
    public class VRPlayer : AbstractPlayer
    {
        private XRNode leftHandNode = XRNode.LeftHand;
        private float pinchThreshold = 0.8f;

        private void Update()
        {
            InputDevice leftHandDevice = InputDevices.GetDeviceAtXRNode(leftHandNode);

            // Detect Pinch Gesture for Show/Hide
            if (leftHandDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
            {
                if (gripValue > pinchThreshold)
                {
                    // Toggle book visibility
                    if (_book.activeSelf)
                        HideBook();
                    else
                        ShowBook();
                }
            }

            // Detect Trigger for Turning Forward
            if (leftHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
            {
                if (triggerValue > 0.5f)
                {
                    TurnForwardPage();
                }
            }

            // Detect Secondary Button for Turning Back
            if (leftHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonPressed) && secondaryButtonPressed)
            {
                TurnBackPage();
            }
        }

        // Implement the abstract methods as before
        public override void HideBook()
        {
            _book.SetActive(false);
        }

        public override void ShowBook()
        {
            _book.SetActive(true);
        }

        public override void TurnForwardPage()
        {
            if (bookScript != null)
            {
                bookScript.RotateForward();
            }
            else
            {
                Debug.LogError("Book script is not assigned.");
            }
        }

        public override void TurnBackPage()
        {
            if (bookScript != null)
            {
                bookScript.RotateBack();
            }
            else
            {
                Debug.LogError("Book script is not assigned.");
            }
        }
    }
}
