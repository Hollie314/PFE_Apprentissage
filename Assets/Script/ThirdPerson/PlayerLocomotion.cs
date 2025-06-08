using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    MaybeInput inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    public float moveSpeed = 7f;
    public float rotationSpeed = 15f;

    private void Awake()
    {
        inputManager = GetComponent<MaybeInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleMovement()
    {
        Vector2 input = inputManager.movementInput;

        moveDirection = cameraObject.forward * input.y + cameraObject.right * input.x;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection *= moveSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = new Vector3(movementVelocity.x, playerRigidbody.velocity.y, movementVelocity.z);
    }

    public void HandleRotation()
    {
        Vector2 input = inputManager.movementInput;

        Vector3 targetDirection = cameraObject.forward * input.y + cameraObject.right * input.x;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = playerRotation;
        }
    }
}