using UnityEngine;

namespace DefaultNamespace
{
    public class ErnestHandler : MonoBehaviour
    {
        [Header("Scaling Settings")]
        [Tooltip("Target Y scale (e.g., 0 to scale down completely)")]
        public float targetYScale = 0f;
        private float duration = 1f;

        [Header("Optional: Callback on Completion")]
        public UnityEngine.Events.UnityEvent onComplete;

        [Header("Target Settings")]
        [Tooltip("The GameObject to scale. If not set, defaults to the current GameObject.")]
        public GameObject target;

        [Header("Animator Settings")]
        [Tooltip("Animator component to control animations.")]
        public Animator animator;

        // Internal variables
        private Vector3 initialScale;
        private Vector3 initialPosition;
        private float elapsedTime = 0f;
        private bool isScaling = false;

        void Start()
        {
            // If target is not set, default to the current GameObject
            if (target == null)
            {
                target = this.gameObject;
            }

            // Initialize the initial scale and position
            initialScale = target.transform.localScale;
            initialPosition = target.transform.localPosition;

            // Optionally, you can choose whether to start scaling automatically
            StartScaling();
        }

        /// <summary>
        /// Initiates the scaling process on the assigned target GameObject.
        /// </summary>
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

        /// <summary>
        /// Initiates the scaling process on a specified GameObject.
        /// </summary>
        /// <param name="gameObjectToScale">The GameObject to scale.</param>
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
                    // Calculate the interpolation factor
                    float t = elapsedTime / duration;

                    // Optionally, use easing for smoother transition
                    t = Mathf.SmoothStep(0f, 1f, t);

                    // Interpolate the Y scale
                    float newYScale = Mathf.Lerp(initialScale.y, targetYScale, t);

                    // Calculate the scale change
                    float scaleChange = newYScale - initialScale.y;

                    // Apply the new scale
                    Vector3 newScale = new Vector3(initialScale.x, newYScale, initialScale.z);
                    target.transform.localScale = newScale;

                    // Since pivot is at center, translate to keep base fixed
                    // Translation needed is half of the scale change
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
                    // Ensure the final scale is set
                    Vector3 finalScale = new Vector3(initialScale.x, targetYScale, initialScale.z);
                    target.transform.localScale = finalScale;

                    // Calculate final translation
                    float finalScaleChange = finalScale.y - initialScale.y;
                    float finalTranslationY = finalScaleChange * 0.5f;
                    target.transform.localPosition = initialPosition + new Vector3(0f, finalTranslationY, 0f);

                    // Stop scaling
                    isScaling = false;

                    // Invoke callback if set
                    onComplete?.Invoke();

                    // Print "done" to the console
                    Debug.Log("done");

                    // Trigger the death animation
                    if (animator != null)
                    {
                        animator.SetBool("IsDead", true);
                        Debug.Log("Death animation triggered.");
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
