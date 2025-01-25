using UnityEngine;

public class ExtraScript : MonoBehaviour
{
    public float movementSpeed;
    //this is distance from the center of the counter
    public float maxDistance; 
    public GameObject extraPrefab;
    public float timerDurationCustomer;
    public bool canMakeES = false;
    private Vector3 endPos;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endPos = ManagerScript.Instance.endPosManager;
    }

    // Update is called once per frame
    void Update()
    {
        //Handled timer manually since I think we could have a character effect who can maybe reduce time for
        //customers?
        timer += Time.deltaTime;

        //Handles timer adding a new customer every x seconds
        if (timer >= timerDurationCustomer) {
            //add if statement to see if they're already maxed out on people maybe
            GameObject newExtra = Instantiate(extraPrefab, new Vector3(8.8f,-2,0), Quaternion.identity); 
            timer = 0;
        }

        //Handles movement to counter
        //MoveTowards moves object, toward endPos + unit Vector * max Distance we want from it
        //at a steady rate, adjust movementSpeed for faster/slower 
        endPos = ManagerScript.Instance.endPosManager;
        transform.position = Vector3.MoveTowards(transform.position, 
        endPos + (transform.position - endPos).normalized * maxDistance,
        Time.deltaTime * movementSpeed);

    } 
}
