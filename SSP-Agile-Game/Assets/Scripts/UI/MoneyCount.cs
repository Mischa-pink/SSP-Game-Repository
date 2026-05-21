using TMPro;
using UnityEngine;

public class MoneyCount : MonoBehaviour
{
    // Vars
    public GameObject moneyHoldObject;
    private int moneyAmount;
    private TextMeshProUGUI textObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get TMPro object
        textObject = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update money amount from the script that holds the amount
        moneyAmount = moneyHoldObject.GetComponent<MoneyHolder>().moneyAmount;

        // Put money amount in text object
        textObject.text = "$ " + moneyAmount.ToString();
    }
}
