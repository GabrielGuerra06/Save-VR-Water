using UnityEngine;

namespace DefaultNamespace
{
    public class ErnestHandler : MonoBehaviour
    {
        public float targetYScale = 0f;
        private float duration = 1f;
        public UnityEngine.Events.UnityEvent onComplete;
        public GameObject target;
        public Animator animator;

        private Vector3 initialScale;
        private Vector3 initialPosition;
        private float elapsedTime = 0f;
        private bool isScaling = false;

        void Start()
        {
            if (target == null)
            {
                target = this.gameObject;
            }

            initialScale = target.transform.localScale;
            initialPosition = target.transform.localPosition;

            StartScaling();
        }
        
        public void StartScaling()
        {
            if (target == null)
            {
                Debug.LogWarning("No target GameObject assigned for scaling.");
                return;
            }

            isScaling = true;
            elapsedTime = 0f;
            initialScale = target.transform.localScale;
            initialPosition = target.transform.localPosition;
        }
        
        public void StartScaling(GameObject gameObjectToScale)
        {
            if (gameObjectToScale == null)
            {
                Debug.LogWarning("The provided GameObject is null.");
                return;
            }

            target = gameObjectToScale;
            isScaling = true;
            elapsedTime = 0f;
            initialScale = target.transform.localScale;
            initialPosition = target.transform.localPosition;
        }

        void Update()
        {
            if (isScaling && target != null)
            {
                if (elapsedTime < duration)
                {
                    float t = elapsedTime / duration;
                    t = Mathf.SmoothStep(0f, 1f, t);
                    float newYScale = Mathf.Lerp(initialScale.y, targetYScale, t);
                    float scaleChange = newYScale - initialScale.y;

                    Vector3 newScale = new Vector3(initialScale.x, newYScale, initialScale.z);
                    target.transform.localScale = newScale;
                    
                    float translationY = scaleChange * 0.5f;
                    Vector3 newPosition = initialPosition + new Vector3(0f, translationY, 0f);
                    target.transform.localPosition = newPosition;
                    if (GameManager.Instance.waterPercentage < 1.0f)
                    {
                        elapsedTime = GameManager.Instance.waterPercentage;
                    }
                    else
                    {
                        elapsedTime = 1.0f;
                    }
                }
                else
                {
                    Vector3 finalScale = new Vector3(initialScale.x, targetYScale, initialScale.z);
                    target.transform.localScale = finalScale;

                    float finalScaleChange = finalScale.y - initialScale.y;
                    float finalTranslationY = finalScaleChange * 0.5f;
                    target.transform.localPosition = initialPosition + new Vector3(0f, finalTranslationY, 0f);

                    isScaling = false;
                    onComplete?.Invoke();
                    if (animator != null)
                    {
                        animator.SetBool("IsDead", true);
                    }
                    else
                    {
                        Debug.LogWarning("Animator is not assigned in ErnestHandler.");
                    }
                }
            }
        }
    }
}
