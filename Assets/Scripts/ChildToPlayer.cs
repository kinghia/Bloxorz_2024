using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildToPlayer : MonoBehaviour
{
    [SerializeField] GameObject childToPlayer;
    [SerializeField] GameObject childMove1;
    [SerializeField] GameObject childMove2;
    [SerializeField] private Vector3 positionOffset;
    public Transform character;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Cube1x1")
        {
            Vector3 newPosition = transform.position + positionOffset;
            Instantiate(childToPlayer, newPosition, childToPlayer.transform.rotation);
            childMove1.SetActive(false);
            childMove2.SetActive(false);
        }   
    }

    void LateUpdate()
    {
        transform.position = new Vector3(character.position.x, 0.625f, character.position.z);

        transform.rotation = Quaternion.identity;
    }
}
