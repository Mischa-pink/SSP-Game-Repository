using Unity.VisualScripting;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public GameObject player;
    public LayerMask building;
    public GameObject dialogueObject;

    private void Awake() => GetComponent<BoxCollider2D>().isTrigger = true;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("object detects collision.");

        if (other.gameObject == player && dialogueObject.GetComponent<DialogueScript>().index == 3)
        {
            Debug.Log("object layer works fine");
            dialogueObject.GetComponent<DialogueScript>().NextLine();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Building") && dialogueObject.GetComponent<DialogueScript>().index == 5)
        {
            Debug.Log("object layer works fine");
            dialogueObject.GetComponent<DialogueScript>().NextLine();
            gameObject.SetActive(false);
        }

    }

}
