using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    private GameObject playerGameObject;

    private Vector2 moveVector;

    private void FindPlayerObject()
    {
        if (playerGameObject == null || !playerGameObject.activeInHierarchy)
        {
            playerInput = GetComponent<PlayerInput>();
            int index = playerInput.playerIndex;
            playerGameObject = GameObject.Find($"Player {index + 1}");
        }
    }

    private void OnEnable()
    {
        FindPlayerObject();
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindPlayerObject();
    }

    private void FixedUpdate()
    {
        FindPlayerObject();

        // Temp code to test input
        if (playerGameObject != null && moveVector.magnitude > 0.5f)
        {
            if (playerGameObject.GetComponent<IPlayerController>() != null)
            {
                playerGameObject.GetComponent<IPlayerController>().Move(moveVector);
            }

            //playerGameObject.transform.position += 10 * Time.fixedDeltaTime * new Vector3(moveVector.x, moveVector.y, 0);
        }
    }

    public void OnMove(CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    public void OnConfirmButtonPressed(CallbackContext context)
    {
        bool confirmButtonPressed = context.ReadValueAsButton();
        confirmButtonPressed = confirmButtonPressed && context.performed;

        // Temp code to test input
        if (playerGameObject != null && confirmButtonPressed)
        {
            if (playerGameObject.GetComponent<IPlayerController>() != null)
            {
                playerGameObject.GetComponent<IPlayerController>().OnConfirm();
            }
            //playerGameObject.transform.localScale = Vector3.one * (confirmButtonPressed ? 4 : 3);
        }
    }

    public void OnCancelButtonPressed(CallbackContext context)
    {
        bool cancelButtonPressed = context.ReadValueAsButton();
        cancelButtonPressed = cancelButtonPressed && context.performed;

        // Temp code to test input
        if (playerGameObject != null && cancelButtonPressed)
        {
            if (playerGameObject.GetComponent<IPlayerController>() != null)
            {
                playerGameObject.GetComponent<IPlayerController>().OnCancel();
            }
            //playerGameObject.transform.localScale = Vector3.one * (cancelButtonPressed ? 2 : 3);
        }
    }
}
