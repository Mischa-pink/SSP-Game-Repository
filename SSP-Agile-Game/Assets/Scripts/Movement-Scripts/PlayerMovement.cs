using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Vars
    private Rigidbody rb;

    private PlayerInput input;
    private InputAction moveAction;
    private Vector2 moveDir;
    public float playerSpeed;

    private void OnEnable()
    {
        // Get PlayerInput Component
        input = GetComponent<PlayerInput>();

        // Get inpput for every direction
        moveAction = input.actions["Move"];
        moveAction.Enable();
    }

    private void OnDisable()
    {
        // Disable if object is set false
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Get direction and move to direction
        moveDir = moveAction.ReadValue<Vector2>();
        transform.position += (Vector3)moveDir.normalized * playerSpeed * Time.deltaTime;


    }
}
