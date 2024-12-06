using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovement : MonoBehaviour
{
    [SerializeField] GameObject childMove1;
    [SerializeField] GameObject childMove2;
    public Movement movementScript;
    
    public MonoBehaviour  scriptA;
    public MonoBehaviour  scriptB;
    public Vector3 savedPositionA;
    public Vector3 savedPositionB;


    void Start()
    {
        SaveBlockPositions();
        childMove1.SetActive(false);
        childMove2.SetActive(false);
    }

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movementScript == null || !movementScript.isRotating)
            {
                if (movementScript == null || !movementScript.isRotating)
                {
                    ToggleScripts();
                }
            }
        }
    }
    public void ToggleScripts()
    {
        bool moveCurrent = childMove1.GetComponent<Movement>().enabled;
        bool controllerCurrent = childMove1.GetComponent<IsController>().enabled;

        childMove1.GetComponent<Movement>().enabled = !moveCurrent;
        childMove2.GetComponent<Movement>().enabled = moveCurrent;

        childMove1.GetComponent<IsController>().enabled = !controllerCurrent;
        childMove2.GetComponent<IsController>().enabled = controllerCurrent;

        Debug.Log("da chuyen doi code");
    }

    /*public void TogglerScripts()
    {
        bool moveCurrent = childMove1.GetComponent<Movement>().enabled;
        bool controllerCurrent = childMove1.GetComponent<IsController>().enabled;

        childMove1.GetComponent<Movement>().enabled = !moveCurrent;
        childMove2.GetComponent<Movement>().enabled = moveCurrent;

        childMove1.GetComponent<IsController>().enabled = !controllerCurrent;
        childMove2.GetComponent<IsController>().enabled = controllerCurrent;

        Debug.Log("da chuyen doi code");
    }*/
    

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                player.SetActive(false);
            }


            childMove1.SetActive(true);
            childMove2.SetActive(true);

            childMove1.GetComponent<Movement>().enabled = true;
            childMove1.GetComponent<IsController>().enabled = true;

            childMove2.GetComponent<Movement>().enabled = false;
            childMove2.GetComponent<IsController>().enabled = false;


        }   
    }

    public void SaveBlockPositions()
    {
        // Tìm tất cả các đối tượng có tag Cube1x1
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("CtoP");

        if (cubes.Length >= 1)
        {
            // Lưu vị trí của hai Cube1x1
            savedPositionA = cubes[0].transform.position;
            //savedPositionB = cubes[1].transform.position;

            Debug.Log("Positions saved: " + savedPositionA + " , " + savedPositionB);
        }
        else
        {
            Debug.Log("Không tìm thấy đủ các đối tượng Cube1x1 trong scene.");
        }
        // Tìm tất cả các đối tượng có tag Cube1x1
        GameObject[] cubes2 = GameObject.FindGameObjectsWithTag("CtoP2");

        if (cubes.Length >= 1)
        {
            // Lưu vị trí của hai Cube1x1
            
            savedPositionB = cubes2[0].transform.position;

            Debug.Log("Positions saved: " + savedPositionA + " , " + savedPositionB);
        }
        else
        {
            Debug.Log("Không tìm thấy đủ các đối tượng Cube1x1 trong scene.");
        }
    }
}
