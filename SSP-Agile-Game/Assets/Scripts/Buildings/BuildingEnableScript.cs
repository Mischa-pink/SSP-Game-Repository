using UnityEngine;

public class BuildingEnableScript : MonoBehaviour
{
    [SerializeField] GameObject objectToEnable;

    public void EnableObject()
    {
        objectToEnable.SetActive(true);
    }
}
