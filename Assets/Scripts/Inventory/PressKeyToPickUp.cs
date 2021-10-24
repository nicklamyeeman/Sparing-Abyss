using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PressKeyToPickUp : MonoBehaviour
{
    public Text pickUpText;

    bool pickUpAllowed = false;
    bool isConsumable = false;
    private GameObject pickObj = null;

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.F))
            PickUp();
        if (isConsumable && Input.GetKeyDown(KeyCode.F))
            Consume();
    }

    private void isPickUpAllowed(bool allow, GameObject pickUpObj, string action)
    {
        pickUpText.gameObject.GetComponent<Text>().text = "Press F to " + action;
        pickUpText.gameObject.SetActive(allow);
        pickObj = pickUpObj;
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Pickable")) {
            isPickUpAllowed(true, collider.gameObject, "pick up");
            pickUpAllowed = true;
            isConsumable = false;
        }

        if (collider.CompareTag("Consumable")) {
            isPickUpAllowed(true, collider.gameObject, "consume");
            pickUpAllowed = false;
            isConsumable = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Pickable")) {
            isPickUpAllowed(false, null, "pick up");
            pickUpAllowed = false;
        }

        if (collider.CompareTag("Consumable")) {
            isPickUpAllowed(false, null, "consume");
            isConsumable = false;
        }
    }

    void PickUp()
    {
        if (pickObj != null) {
            if (transform.parent.GetComponent<PlayerInventory>().checkHandsFull()) {
                pickUpText.gameObject.GetComponent<Text>().text = "Your hands are full";
                pickUpText.gameObject.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(3, pickUpText.gameObject));
            }
            else {
                GameObject freeHand = transform.parent.GetComponent<PlayerInventory>().freeHand();
                pickObj.transform.position = freeHand.transform.position;
                pickObj.transform.parent = freeHand.transform;

                pickObj.transform.GetComponent<BoxCollider>().enabled = !pickObj.transform.GetComponent<BoxCollider>().enabled;
                pickObj.transform.GetComponent<Rigidbody>().useGravity = false;
                isPickUpAllowed(false, null, "pick up");
                pickUpAllowed = false;
            }
        }
    }

    void Consume()
    {
        if (pickObj != null) {
            Destroy(pickObj);
            isPickUpAllowed(false, null, "consume");
            isConsumable = false;
        }
    }

}
