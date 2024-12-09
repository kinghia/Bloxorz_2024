using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildMovement : MonoBehaviour
{
    [SerializeField] GameObject childMove1;
    [SerializeField] GameObject childMove2;
    [SerializeField] GameObject boxChild;
    public Movement movementScript;
    
    public MonoBehaviour  scriptA;
    public MonoBehaviour  scriptB;
    public Vector3 savedPositionA;
    public Vector3 savedPositionB;
    GameObject newChildMove1;
    GameObject newChildMove2;
    public GameObject newBoxChild;


    void Start()
    {
        //SaveBlockPositions();
        
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
        bool moveCurrent = newChildMove1.GetComponent<Movement>().enabled;
        bool controllerCurrent = newChildMove1.GetComponent<IsController>().enabled;

        newChildMove1.GetComponent<Movement>().enabled = !moveCurrent;
        newChildMove2.GetComponent<Movement>().enabled = moveCurrent;

        newChildMove1.GetComponent<IsController>().enabled = !controllerCurrent;
        newChildMove2.GetComponent<IsController>().enabled = controllerCurrent;

        Debug.Log("da chuyen doi code");
    }    

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "KickTrap")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in players)
            {
                player.SetActive(false);
            }


            newChildMove1 = Instantiate(childMove1, savedPositionA, Quaternion.identity);
            newChildMove2 = Instantiate(childMove2, savedPositionB, Quaternion.identity);

            newBoxChild = Instantiate(boxChild, transform.position, Quaternion.identity);
            newBoxChild.SetActive(true);

            newChildMove1.GetComponent<Movement>().enabled = true;
            newChildMove1.GetComponent<IsController>().enabled = true;

            newChildMove2.GetComponent<Movement>().enabled = false;
            newChildMove2.GetComponent<IsController>().enabled = false;


        }   
    }

    /*public void SaveBlockPositions()
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
    }*/
}
