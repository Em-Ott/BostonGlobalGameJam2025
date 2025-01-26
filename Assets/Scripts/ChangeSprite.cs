using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinWithGacha : MonoBehaviour
{
    // For sprite changes on the wheel
    public Sprite unclickedSprite;  // Sprite for unclicked state
      public Sprite clickedSprite;    // Sprite for clicked state
    private SpriteRenderer spriteRenderer; // Sprite renderer for button

    // For spinning the wheel
    public GameObject wheelPrefab;    // Prefab of the wheel (this is the sprite we will instantiate)
    public float initialSpinSpeed = 360f;  // Initial speed of rotation (degrees per second)
    public float spinDuration = 3f;        // How long the wheel spins at full speed
    public float decelerationTime = 2f;    // Time to decelerate the wheel after it reaches max speed
    private float currentSpinSpeed;        // Current spin speed
    private float timer;                   // Timer to track the spinning time
    private bool isSpinning = false;       // Whether the wheel is currently spinning
    private GameObject wheelInstance;      // The instantiated wheel sprite

    // Gacha variables
    public GameObject[] prizeOptions;  // Array of prize options (for the second wheel)
    private bool isFirstWheelSpinning = false;  // Whether the first wheel is spinning
    private bool firstWheelPassed = false;      // Whether the first wheel passed (success/fail)

    // Reference to the 2D Collider attached to the button
    private Collider2D buttonCollider;  // Collider2D of the button

    // Reference to the ClickScript to interact with the money variable
    public ClickScript clickScript;  // Drag and drop the ClickScript in the inspector

    // Start is called before the first frame update
    void Start()
    {
        // Ensure spriteRenderer is initialized
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the button!");
        }

        // Get the Collider2D component attached to the same GameObject (the button)
        buttonCollider = GetComponent<Collider2D>();

        if (buttonCollider == null)
        {
            Debug.LogError("Collider2D not found on the button!");
        }

        // Ensure the clickScript is assigned
        if (clickScript == null)
        {
            clickScript = FindObjectOfType<ClickScript>();  // Find the ClickScript in the scene if not assigned in the inspector
            if (clickScript == null)
            {
                Debug.LogError("ClickScript not found in the scene!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for mouse click and button interaction with Collider2D
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;  // Ensure we use the correct 2D plane (Z should be 0)

            // Check if the mouse is over the button and trigger action
            if (buttonCollider != null && buttonCollider.OverlapPoint(mousePos))
            {
                OnSpinButtonPressed();
            }
        }

        if (isSpinning && wheelInstance != null)
        {
            // Rotate the wheel sprite
            wheelInstance.transform.Rotate(0f, currentSpinSpeed * Time.deltaTime, 0f);

            // Update the timer
            timer += Time.deltaTime;

            // Decelerate the spin speed
            if (timer < spinDuration)
            {
                currentSpinSpeed = Mathf.Lerp(initialSpinSpeed, 0f, timer / spinDuration);
            }
            else
            {
                // Start decelerating after spinDuration has passed
                float decelerationProgress = (timer - spinDuration) / decelerationTime;
                currentSpinSpeed = Mathf.Lerp(0f, initialSpinSpeed, 1 - decelerationProgress);

                // If the wheel has stopped, handle the result
                if (currentSpinSpeed <= 0f)
                {
                    StopSpinning();

                    if (isFirstWheelSpinning)
                    {
                        // Check the result of the first wheel spin (pass/fail)
                        firstWheelPassed = CheckFirstWheelResult();
                        if (firstWheelPassed)
                        {
                            // Trigger second wheel to choose a reward
                            TriggerSecondWheel();
                        }
                        else
                        {
                            Debug.Log("First wheel failed. No rewards!");
                        }
                    }
                    else
                    {
                        TriggerGacha();
                    }
                }
            }
        }
    }

    // Called when the spin button is pressed
    void OnSpinButtonPressed()
    {
        if (!isSpinning)
        {
            // Change the sprite to the clicked sprite when the button is pressed
            spriteRenderer.sprite = clickedSprite;

            // Check if the player has more than 50 money to spin
            if (clickScript.money >= 50)  // Check if the player has more than 50 money
            {
                // Deduct the money cost to spin the wheel
                clickScript.money -= 50;

                // Create the wheel sprite (instantiate it) and start spinning
                CreateWheel();
            }
            else
            {
                Debug.Log("Not enough money to spin the wheel! You need at least 50.");
            }
        }
    }

    // Create the wheel sprite (instantiate it)
    void CreateWheel()
    {
        // Only create the wheel instance if it's not already created
        if (wheelInstance == null)
        {
            // Create the wheel instance from the prefab
            wheelInstance = Instantiate(wheelPrefab, transform.position, Quaternion.identity);

            // Set the initial sprite of the wheel
            spriteRenderer = wheelInstance.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = clickedSprite; // Optionally set a clicked state for the wheel sprite

            // Start the spinning of the wheel
            StartSpinning();

            // Determine whether we're spinning the first wheel or the second
            if (!isFirstWheelSpinning)
            {
                isFirstWheelSpinning = true;
            }
        }
    }

    // Start spinning the wheel
    void StartSpinning()
    {
        isSpinning = true;
        timer = 0f;  // Reset the timer
        currentSpinSpeed = initialSpinSpeed;  // Set the initial spin speed
    }

    // Stop spinning the wheel
    void StopSpinning()
    {
        isSpinning = false;
        timer = 0f;  // Reset the timer
        currentSpinSpeed = 0f;  // Ensure the wheel stops rotating
    }

    // Check the result of the first wheel (pass/fail)
    bool CheckFirstWheelResult()
    {
        // Let's assume a 50% chance for pass/fail
        float randomValue = Random.Range(0f, 1f);
        return randomValue > 0.5f;  // 50% chance for success
    }

    // Trigger the second wheel to choose a reward
    void TriggerSecondWheel()
    {
        // Hide the first wheel after it finishes spinning
        if (wheelInstance != null)
        {
            Destroy(wheelInstance);  // Destroy the first wheel
        }

        // Create and show the second wheel to choose a reward
        CreateWheel();  // Reuse the same CreateWheel method for the second wheel

        // Optionally, handle the second wheel's spin logic differently if needed
        isFirstWheelSpinning = false;  // Reset first wheel state
        Debug.Log("Second wheel triggered to choose reward!");
    }

    // Trigger the Gacha function for the second wheel's reward
    void TriggerGacha()
    {
        // Ensure there are prize options available
        if (prizeOptions != null && prizeOptions.Length > 0)
        {
            // Pick a random prize from the available options
            int randomIndex = Random.Range(0, prizeOptions.Length);
            GameObject selectedPrize = prizeOptions[randomIndex];

            // Log the result (or handle prize selection here)
            Debug.Log("Gacha Prize: " + selectedPrize.name);

            // Optionally, you can instantiate the prize or perform other actions with it
            // Instantiate(selectedPrize, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No prize options available for Gacha!");
        }
    }
}