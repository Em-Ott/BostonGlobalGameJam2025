using System;
using UnityEngine;

public class ExtraScript : MonoBehaviour
{
    public float movementSpeed;
    //this is distance from the center of the counter
    public float maxDistance; 
    public GameObject extraPrefab;
    public float timerDurationCustomer;
    public ClickScript clickScript;
    public float bobFrequency = 10;
    public float bobLength = 0.01f;
    public GameObject sadCustomer;
    private Vector3 endPos;
    private Vector3 newEnd;
    private Vector3 killPos;
    private float timer;
    private bool isReturningES;
    private bool madeNewClone = false;
    private SpriteRenderer srenderer;
    private bool happy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        killPos = new Vector3(9, -2.2f, 0);
        isReturningES = false;
        srenderer = sadCustomer.GetComponent<SpriteRenderer>();
        happy = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Handled timer manually since I think we could have a character effect who can maybe reduce time for
        //customers?
        timer += Time.deltaTime;

        // Makes the movement silly
        float dy = bobLength * (float) Math.Sin(bobFrequency * timer);

        transform.position = new Vector2(transform.position.x, transform.position.y + dy);

        //Handles timer adding a new customer every x seconds
        if (timer >= timerDurationCustomer && madeNewClone == false && 
        !ManagerScript.Instance.positioningScript.immediateExit) 
        {
            //add if statement to see if they're already maxed out on people maybe
            GameObject newExtra = Instantiate(extraPrefab, new Vector3(8.8f,-2.2f,0), Quaternion.identity);
            //Stops exponential growth
            madeNewClone = true; 
        } else if (ManagerScript.Instance.positioningScript.immediateExit)
        {
            //this stops lag from customer exiting the 2d trigger and instanatiating at the same time
            //this just delays it to next update so the customer's movement is smooth
            ManagerScript.Instance.positioningScript.immediateExit = false;
        }

        isReturningES = clickScript.isReturningCS;
        happy = isReturningES;

        //Without happy all customers after the first happy customer will have the happy sprite this
        //is because the renderer changes the renderer for the prefab as a whole, so we're just changing
        //it back if they're not happy (doesn't effect already created objects since they're already happy)
        if (!happy)
        {
            srenderer.sortingOrder = 5;
        } else
        {
            srenderer.sortingOrder = 1;
        }
        if(isReturningES) 
        {
            //Handles movement away from the counter
            transform.position = Vector3.MoveTowards(transform.position, 
            killPos,
            Time.deltaTime * movementSpeed);
            
            if(transform.position.x >= 8.5f)
            {
                Destroy(gameObject);
            }

        } else if(ManagerScript.Instance.positioningScript.miss)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
            new Vector3(-8, -2.2f, 0),
            Time.deltaTime * movementSpeed);
            if(transform.position.x <= -7.5f)
            {
                ManagerScript.Instance.positioningScript.miss = false;
                Destroy(gameObject);
            }
        }
        else 
        {
            endPos = ManagerScript.Instance.endPosManager;
            transform.position = Vector3.MoveTowards(transform.position, 
            endPos + (transform.position - endPos).normalized * maxDistance,
            Time.deltaTime * movementSpeed);
        }
    }
}