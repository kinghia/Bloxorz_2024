using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float loadSceneLevel = 1f;
    public float moveDistance = 1f;
    private bool hasTriggered = false;
    private bool hasTrapTriggered = false;


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Finish":
                ActiveRigid();
                DelayNextScene();
                transform.rotation = Quaternion.identity;
                break;

            case "FallPlatfom":
                ActiveRigid();
                FallPlat(other);
                break;

            case "HideTrap":
                HideTrap(other);
                break;

            case "Teleport":
                Teleport(other);
                break;

            case "OutSide":
                ActiveRigid();
                FallPlat(other);
                break;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Teleport":
                hasTriggered = false;
                GetComponent<Movement>().enabled = true;
                transform.rotation = Quaternion.identity;
                break;
            case "HideTrap":
                hasTrapTriggered = false;
                break;
        }
    }

    private void DelayNextScene()
    {
        Invoke("LoadNextScene", loadSceneLevel);
    }

    void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void Teleport(Collider other)
    {
        if (!hasTriggered)
        {
            if (other.transform.childCount > 0)
            {
                Vector3 targetPosition = other.transform.GetChild(0).position;
                transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
                GetComponent<Movement>().enabled = false;

                hasTriggered = true;
            }
        }
    }

    private void HideTrap(Collider other)
    {
        if (!hasTrapTriggered)
        {
            if (other.transform.childCount > 0)
            {
                Transform firstChild = other.transform.GetChild(0);
                bool currentState = firstChild.gameObject.activeSelf;
                firstChild.gameObject.SetActive(!currentState);
                Debug.Log("HideTrap triggered and state toggled.");
            }
            hasTrapTriggered = true;
        }
    }

    private static void FallPlat(Collider other)
    {
        Rigidbody fallPlatformRigidbody = other.GetComponent<Rigidbody>();
        if (fallPlatformRigidbody != null)
        {
            fallPlatformRigidbody.useGravity = true;
        }
    }

    private void ActiveRigid()
    {
        GetComponent<Movement>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
    }

    
}
