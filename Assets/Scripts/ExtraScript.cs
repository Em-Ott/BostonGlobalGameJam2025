using UnityEngine;

public class ExtraScript : MonoBehaviour
{
    public float movementSpeed;
    //this is distance from the center of the counter
    public float maxDistance; 
    public GameObject extraPrefab;
    public float timerDurationCustomer;
    public ClickScript clickScript;
    private Vector3 endPos;
    private Vector3 killPos;
    private float timer;
    private bool isReturningES;
    private bool madeNewClone = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        killPos = new Vector3(9, -2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Handled timer manually since I think we could have a character effect who can maybe reduce time for
        //customers?
        timer += Time.deltaTime;

        //Handles timer adding a new customer every x seconds
        if (timer >= timerDurationCustomer && madeNewClone == false) {
            //add if statement to see if they're already maxed out on people maybe
            GameObject newExtra = Instantiate(extraPrefab, new Vector3(8.8f,-2,0), Quaternion.identity);
            //Stops exponential growth
            madeNewClone = true; 
        }

        isReturningES = clickScript.isReturningCS;
        if(isReturningES)
        {
            //Handles movement away from the counter
            transform.position = Vector3.MoveTowards(transform.position, 
            killPos,
            Time.deltaTime * movementSpeed);
            if(transform.position.x >= 8.5f)
            {
                Debug.Log("should destroy");
                Destroy(gameObject);
            }

        } else
        {
            if(ManagerScript.Instance.positioningScript.ordering)
            {
                //Maybe have a second collider which stops movement now? Actually making our colliders stop 
                //prefabs maybe?? Having a second collider which is an on trigger and have it no longer be
                //on trigger when this (above) is true
                //That being said we still have an on trigger (prefab player) so that way players can go
                //through players
            } else 
            {
                //Handles movement to counter
                //MoveTowards moves object, toward endPos + unit Vector * max Distance we want from it
                //at a steady rate, adjust movementSpeed for faster/slower 
                //It'll stop when it hits the collider anyway 
                //(because of the if statement dictating other prefabs) but this is how it is rn???
                //I think I need to take a break and figure out position stuff
                endPos = ManagerScript.Instance.endPosManager;
                transform.position = Vector3.MoveTowards(transform.position, 
                endPos + (transform.position - endPos).normalized * maxDistance,
                Time.deltaTime * movementSpeed);
            }
        }


    } 
}
