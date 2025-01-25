using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    private Transform playerTransform;

    private void OnEnable()
    {
        playerInput = GetComponent<PlayerInput>();
        int index = playerInput.playerIndex;
        playerTransform = GameObject.Find($"Player {index + 1}").transform;
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int index = playerInput.playerIndex;
        playerTransform = GameObject.Find($"Player {index + 1}").transform;
    }

    public void OnMove(CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();


        // Temp code to test input
        if (playerTransform != null)
        {
            playerTransform.position += 0.2f * new Vector3(moveVector.x, moveVector.y, 0);
        }
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
