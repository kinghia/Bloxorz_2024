using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildToPlayer : MonoBehaviour
{
    [SerializeField] GameObject childToPlayer;   // Prefab cần Instantiate
    [SerializeField] GameObject childMove1;
    [SerializeField] GameObject childMove2;
    [SerializeField] GameObject boxChild;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] Transform character;  // Đối tượng cần gán vào trong Scene (CtoP2)

    private GameObject instantiatedChild;  // Đối tượng vừa được instantiate

    void Start()
    {
        // Tìm đối tượng trong Scene theo tag
        childMove1 = GameObject.FindWithTag("CtoP2");
        childMove2 = GameObject.FindWithTag("CtoP");

        boxChild = GameObject.FindWithTag("BoxChild");

        // Tìm đối tượng trong Scene theo tag "CtoP2" và gán cho character
        GameObject characterObj = GameObject.FindWithTag("CtoP2");
        if (characterObj != null)
        {
            character = characterObj.transform;  // Gán Transform của đối tượng tìm thấy
        }
        else
        {
            Debug.LogWarning("character là null, không thể cập nhật vị trí.");
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CtoP"))
        {
            // Tính toán vị trí mới và instantiate prefab
            Vector3 newPosition = transform.position + positionOffset;
            instantiatedChild = Instantiate(childToPlayer, newPosition, childToPlayer.transform.rotation);

            // Destroy các đối tượng childMove1 và childMove2 nếu cần
            Destroy(childMove1);
            Destroy(childMove2);
            Destroy(boxChild);  // Destroy current object
        }
    }

    void LateUpdate()
    {
        // Kiểm tra xem gameObject đã bị Destroy chưa
        if (gameObject == null)
        {
            return;  // Nếu gameObject đã bị Destroy, không cập nhật vị trí nữa
        }

        if (character != null)
        {
            // Cập nhật vị trí của đối tượng hiện tại theo đối tượng character
            transform.position = new Vector3(character.position.x, 0.625f, character.position.z);
            transform.rotation = Quaternion.identity; // Giữ lại góc quay ban đầu
        }
        else
        {
            Debug.LogWarning("character là null, không thể cập nhật vị trí.");
        }
    }
}
