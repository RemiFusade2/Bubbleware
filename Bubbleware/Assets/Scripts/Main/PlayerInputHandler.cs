using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    private Transform playerTransform;

    private Vector2 moveVector;

    private void FindPlayerTransform()
    {
        if (playerTransform == null)
        {
            playerInput = GetComponent<PlayerInput>();
            int index = playerInput.playerIndex;
            GameObject playerGameObject = GameObject.Find($"Player {index + 1}");
            if (playerGameObject != null)
            {
                playerTransform = playerGameObject.transform;
            }
        }
    }

    private void OnEnable()
    {
        FindPlayerTransform();
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindPlayerTransform();
    }

    private void FixedUpdate()
    {
        // Temp code to test input
        if (playerTransform != null && !moveVector.Equals(Vector2.zero))
        {
            playerTransform.position += 10 * Time.fixedDeltaTime * new Vector3(moveVector.x, moveVector.y, 0);
        }
    }

    public void OnMove(CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnConfirmButtonPressed(CallbackContext context)
    {
        bool confirmButtonPressed = context.ReadValueAsButton();


        // Temp code to test input
        if (playerTransform != null)
        {
            playerTransform.localScale = Vector3.one * (confirmButtonPressed ? 4 : 3);
        }
    }

    public void OnCancelButtonPressed(CallbackContext context)
    {
        bool cancelButtonPressed = context.ReadValueAsButton();


        // Temp code to test input
        if (playerTransform != null)
        {
            playerTransform.localScale = Vector3.one * (cancelButtonPressed ? 2 : 3);
        }
    }
}
