using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;
    bool rotate = false;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject forwardButton;

    private void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].localRotation = Quaternion.identity;
        }
        if (pages.Count > 0)
        {
            pages[0].SetAsLastSibling();
        }
        backButton.SetActive(false);
        forwardButton.SetActive(pages.Count > 0);
    }

    public void RotateForward()
    {
        Debug.Log("BOOK: ROTATEFORWARD");
        if (gameObject.activeSelf == false) return;
        if (rotate) { return; }
        if (index >= pages.Count - 1) return;

        index++;
        float angle = 180f; 
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void RotateBack()
    {
        if (gameObject.activeSelf == false) return;
        if (rotate) { return; }
        if (index < 0) return;

        float angle = 0f; 
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, false));
    }

    IEnumerator Rotate(float targetAngle, bool forward)
    {
        rotate = true;
        float elapsed = 0f;
        Quaternion initialRotation = pages[index].localRotation;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * pageSpeed;
            pages[index].localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed);
            yield return null;
        }
        pages[index].localRotation = targetRotation;

        if (!forward)
        {
            index--;
        }
        backButton.SetActive(index >= 0);
        forwardButton.SetActive(index < pages.Count - 1);

        rotate = false;
    }
}
