using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MockPlayer : AbstractPlayer
    {
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
            Debug.Log(bookScript);
            bookScript.RotateForward();
        }

        public override void TurnBackPage()
        {
            bookScript.RotateBack();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H)) // Press 'H' to hide the book
            {
                HideBook();
            }
            if (Input.GetKeyDown(KeyCode.S)) // Press 'S' to show the book
            {
                ShowBook();
            }
            if (Input.GetKeyDown(KeyCode.D)) // Press 'D' to turn the page forward
            {
                TurnForwardPage();
            }
            if (Input.GetKeyDown(KeyCode.A)) // Press 'A' to turn the page back
            {
                TurnBackPage();
            }
        }
    }
}