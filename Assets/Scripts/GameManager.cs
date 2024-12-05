using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DefaultNamespace;
using Meta.XR.Editor.Tags;
using UnityEngine.SceneManagement; 

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameStatusText; 
        [SerializeField] private AudioSource audioSource; 
        [SerializeField] private AudioClip winClip; 
        [SerializeField] private AudioClip loseClip; 
        [SerializeField] private float animationSpeed = 2f; 
        
        [SerializeField] private AudioSource backgroundAudioSource; 
        [SerializeField] private AudioClip backgroundMusicClip; 
        [SerializeField] public AudioSource successAudioSource; 
        [SerializeField] public AudioClip successMusicClip; 
    
        [SerializeField] public AudioSource failAudioSource; 
        [SerializeField] public AudioClip failMusicClip; 
        

        private Vector3 initialPosition;
        private Vector3 targetPosition;

        public List<GameObject> correctlyWateredTrees = new List<GameObject>();
        public TextMeshProUGUI textMeshProComponent;
        public GameObject progressBar;
        public static GameManager Instance { get; private set; }
        public GameObject _book = null;

        public bool bookState = false;
        public OVRInput.Controller controller;

        public List<GameObject> trees = new List<GameObject>();
        public GameObject wateringCan;
        public float total_water = 10.0f;
        public float waterUsed = 0.0f;
        public float waterPercentage = 0.0f;

        private void Start()
        {
            GameObject[] taggedTrees = GameObject.FindGameObjectsWithTag("Tree");
            trees.AddRange(taggedTrees);
            Debug.Log("Number of trees added: " + trees.Count);

            initialPosition = gameStatusText.rectTransform.anchoredPosition;
            targetPosition = new Vector3(initialPosition.x, 0, initialPosition.z); 
            gameStatusText.gameObject.SetActive(false);
            
            if (backgroundAudioSource != null && backgroundMusicClip != null)
            {
                backgroundAudioSource.clip = backgroundMusicClip;
                backgroundAudioSource.loop = true; 
                backgroundAudioSource.Play();
            }
        }

        private bool hasGameEnded = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (!hasGameEnded)
            {
                if (GetTreeCountByState(Tree.TreeState.Watered) == GameObject.FindGameObjectsWithTag("Tree").Length)
                {
                    ShowGameStatus(true);
                    hasGameEnded = true; 
                    Time.timeScale = 0;
                    StartAfterDelay();
                }
                else if (waterUsed > total_water * 1.1f)
                {
                    ShowGameStatus(false);
                    hasGameEnded = true;
                    Time.timeScale = 0;  // Stop time (pause the game)
                    StartAfterDelay();
                }
            }

            waterPercentage = waterUsed / total_water;

            textMeshProComponent.text = "" + GetTreeCountByState(Tree.TreeState.Watered);

            if (OVRInput.GetDown(OVRInput.RawButton.X))
            {
                wateringCan.SetActive(!wateringCan.activeSelf);
            }
        }


        public void ShowGameStatus(bool isWin)
        {
            // Decide el texto según el resultado
            gameStatusText.text = isWin ? "¡Ganaste!" : "¡Perdiste!";
            gameStatusText.gameObject.SetActive(true);

            // Reproduce el sonido correspondiente
            AudioClip clip = isWin ? winClip : loseClip;
            audioSource.PlayOneShot(clip);

            // Comienza la animación
            StartCoroutine(AnimateText());
        }
        
        private System.Collections.IEnumerator AnimateText()
        {
            Vector3 currentPosition = initialPosition;

            while (Vector3.Distance(currentPosition, targetPosition) > 0.01f)
            {
                currentPosition = Vector3.Lerp(currentPosition, targetPosition, animationSpeed * Time.deltaTime);
                gameStatusText.rectTransform.anchoredPosition = currentPosition;
                yield return null;
            }
            gameStatusText.rectTransform.anchoredPosition = targetPosition;
        }
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            InitializeGame();
        }
        private void InitializeGame()
        {
            Debug.Log("Game Initialized.");
        }
        

        public void ToogleBook()
        {
            _book.SetActive(!_book.activeSelf);
        }

        public void forwardBook()
        {
            _book.GetComponent<book>().RotateForward();
        }

        public void backBook()
        {
            _book.GetComponent<book>().RotateBack();
        }
        
        public int GetTreeCountByState(Tree.TreeState state)
        {
            int count = 0;

            foreach (GameObject treeObj in trees)
            {
                if (treeObj != null)
                {
                    Tree treeComponent = treeObj.GetComponent<Tree>();
                    if (treeComponent != null && treeComponent.CurrentState == state)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
        
        // Coroutine to handle the delay and stop time
        public IEnumerator StartAfterDelay()
        {
            yield return new WaitForSeconds(1);  // Wait for 5 seconds
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}