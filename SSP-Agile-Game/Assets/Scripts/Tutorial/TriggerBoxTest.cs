using Unity.VisualScripting;
using UnityEngine;

public class TriggerBoxTest : MonoBehaviour
{
    public GameObject player;
   
    public GameObject dialogueObject;

    private void Awake() => GetComponent<BoxCollider2D>().isTrigger = true;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("object detects collision.");
        if (other.gameObject == player && dialogueObject.GetComponent<DialogueScript>().index == 3)
        {
            Debug.Log("object detects collision");
            dialogueObject.GetComponent<DialogueScript>().NextLine();
            gameObject.SetActive(false);
        }

    }

}
