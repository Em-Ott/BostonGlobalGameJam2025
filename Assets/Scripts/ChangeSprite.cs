using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpinWithGacha : MonoBehaviour
{
    // For sprite changes on the button
    public Sprite unclickedSprite;  // Sprite for unclicked state
    public Sprite clickedSprite;    // Sprite for clicked state
    private SpriteRenderer spriteRenderer; // Sprite renderer for button

    // Reference to the ManagerScript to interact with the money variable
    public ManagerScript managerScript;  // Drag and drop the ManagerScript in the inspector

    // Reference to the 2D Collider attached to the button
    private Collider2D buttonCollider;  // Collider2D of the button
    private int gachaChoice = 1;

    // Start is called before the first frame update
    void Start()
    {
        gachaChoice = 1;
        
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

        // Ensure the managerScript is assigned
        if (managerScript == null)
        {
            managerScript = ManagerScript.Instance;  // Use Singleton Instance of ManagerScript
            if (managerScript == null)
            {
                Debug.LogError("ManagerScript not found in the scene!");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;  // Ensure we use the correct 2D plane (Z should be 0)
        // Change the sprite to the clicked sprite when the button is pressed
        // Check for mouse click and button interaction with Collider2D
        if (Input.GetMouseButton(0) && buttonCollider.OverlapPoint(mousePos)) // Left mouse button
        {
            // Change the sprite to the clicked sprite when the button is pressed
            spriteRenderer.sprite = clickedSprite;

            // Check if the mouse is over the button and trigger action
            if (buttonCollider != null && buttonCollider.OverlapPoint(mousePos))
            {
                OnButtonPressed();
            }
        } 
    }

    // Called when the button is pressed
    void OnButtonPressed()
    {
        // Check if the player has more than 50 money to perform action
        if (managerScript.money >= 50)  // Check if the player has more than 50 money
        {
            // Deduct the money cost to perform action
            System.Random random = new System.Random();
            int randomNumber = random.Next(1, 4);
            gachaChoice = randomNumber;
            Debug.Log(randomNumber);
        }
    }

    public int gachaResult() {
        return gachaChoice;
    }
}