using UnityEngine;

namespace DefaultNamespace
{
    public abstract class AbstractPlayer : MonoBehaviour
    {
        [SerializeField] protected GameObject _book;

        protected book bookScript;

        public abstract void HideBook();
        public abstract void ShowBook();
        public abstract void TurnForwardPage();
        public abstract void TurnBackPage();

        protected virtual void Awake()
        {
            Debug.Log("test");
            if (_book != null)
            {
                bookScript = _book.GetComponent<book>();
                
                if (bookScript == null)
                {
                    Debug.LogError("Book script not found on the assigned GameObject.");
                }
            }
            else
            {
                Debug.LogError("Book GameObject is not assigned in the Inspector.");
            }
        }
    }
}