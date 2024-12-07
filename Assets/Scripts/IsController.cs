using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsController : MonoBehaviour
{
    [SerializeField] GameObject childToPlayer;
    public GameObject targetObject;
    public float delayTime = 1.0f;
    private bool isKeyPressed = false;
    private Coroutine activationCoroutine;

    void Start()
    {
        // Tìm đối tượng trong Scene theo tag
        targetObject = GameObject.FindWithTag("BoxChild");

        if (targetObject == null)
        {
            Debug.LogError("Không tìm thấy đối tượng với tag 'BoxChild' trong Scene!");
        }
    }
    void Update()
    {
        TestPressed();
    }

    private void TestPressed()
    {
        bool keyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)
                                  || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)
                                  || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow);

        if (keyPressed)
        {
            if (!isKeyPressed)
            {
                isKeyPressed = true;

                if (activationCoroutine != null)
                {
                    StopCoroutine(activationCoroutine);
                }
                targetObject.SetActive(false);
            }
        }
        else if (isKeyPressed)
        {
            isKeyPressed = false;
            activationCoroutine = StartCoroutine(ActivateAfterDelay());
        }
    }

    private IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        targetObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject == childToPlayer)
        {
            gameObject.GetComponent<Movement>().enabled = false;
            
        }
    
    }
}
