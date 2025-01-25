using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public int money = 0;
    public int moneyIncrease;
    public float timerDurationDrink;
    private float timer = 0;
    private bool isBeingMade = false;
    private bool canMakeCS = false;
    private Vector3 endPosClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //canMakeCS = ManagerScript.Instance.extraScript.canMakeES;
        if (timer >= timerDurationDrink) 
        {
            money += moneyIncrease;
            isBeingMade = false;
            timer = 0;
            Debug.Log(money);

        } else if (isBeingMade) 
        {
            timer += Time.deltaTime;
        } else if (Input.GetMouseButtonDown(0) && canMakeCS) 
        {
            Debug.Log("start");
            isBeingMade = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        canMakeCS = true;
    } 

}

