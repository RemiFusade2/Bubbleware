using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerHandler : MonoBehaviour
{
    public static PlayerInputManagerHandler Instance;

    private void OnEnable()
    {
        GetComponent<PlayerInputManager>().EnableJoining();
    }

    private void Awake()
    {
        // Does another instance already exist?
        if (Instance && Instance != this)
        {
            // Destroy myself
            Destroy(gameObject);
            return;
        }

        // Otherwise store my reference and make me DontDestroyOnLoad
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
