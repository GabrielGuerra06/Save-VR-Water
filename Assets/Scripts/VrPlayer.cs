using UnityEngine;
using Oculus;

namespace DefaultNamespace
{
    public class VRPlayer : AbstractPlayer
    {
        // Define the controller type
        private OVRInput.Controller leftController = OVRInput.Controller.LTouch;
        // Adjust the threshold as needed
        private float gripThreshold = 0.8f;

        private void Update()
        {
            // Detect Grip for Show/Hide
            float gripValue = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, leftController);
            Debug.Log($"Grip Value: {gripValue}");
            if (gripValue > gripThreshold)
            {
                Debug.Log("Grip value above threshold. Toggling book visibility.");
                if (_book.activeSelf)
                    HideBook();
                else
                    ShowBook();
            }

            // Detect Trigger for Turning Forward
            float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, leftController);
            Debug.Log($"Trigger Value: {triggerValue}");
            if (triggerValue > 0.5f)
            {
                Debug.Log("Trigger value above 0.5f. Turning forward page.");
                TurnForwardPage();
            }

            // Detect Secondary Button for Turning Back
            bool secondaryButtonPressed = OVRInput.GetDown(OVRInput.Button.One, leftController);
            Debug.Log($"Secondary Button Pressed: {secondaryButtonPressed}");
            if (secondaryButtonPressed)
            {
                Debug.Log("Secondary button pressed. Turning back page.");
                TurnBackPage();
            }
        }

        // Implement the abstract methods
        public override void HideBook()
        {
            _book.SetActive(false);
            Debug.Log("Book hidden.");
        }

        public override void ShowBook()
        {
            _book.SetActive(true);
            Debug.Log("Book shown.");
        }

        public override void TurnForwardPage()
        {
            if (bookScript != null)
            {
                Debug.Log("Turning forward page.");
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
                Debug.Log("Turning back page.");
                bookScript.RotateBack();
            }
            else
            {
                Debug.LogError("Book script is not assigned.");
            }
        }
    }
}
