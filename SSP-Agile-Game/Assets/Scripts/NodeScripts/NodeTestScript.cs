using UnityEngine;

public class NodeTestScript : MonoBehaviour
{
    void Start()
    {

        //FindObjectOfType<NodeGenerator>().DebugPrintTileNames();
        //FindObjectOfType<NodeGenerator>().GenerateNodes();
        //FindObjectOfType<NodeGenerator>().DebugPrintNodes();

        //FindObjectOfType<NodeGenerator>().ColorTilesByValue();
    }


    void SomeMethod()
    {
        FindObjectOfType<NodeGenerator>().GenerateNodes();
    }
}
