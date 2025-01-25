using TMPro;
using UnityEngine;

public class ClickScript : MonoBehaviour {
    public int moneyIncrease;
    public float timerDurationDrink;
    public bool isReturningCS = false;
    private TextMeshProUGUI textUpdate;
    private int money;
    private float timer = 0;
    private bool isBeingMade = false;
    private bool canMake = false;
    private Vector3 endPosClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isReturningCS = false;
        textUpdate = ManagerScript.Instance.moneyCounter;
    }

    // Update is called once per frame
    void Update()
    {
        //canMakeCS = ManagerScript.Instance.extraScript.canMakeES;
        if (timer >= timerDurationDrink) 
        {
            money = ManagerScript.Instance.money;
            money += moneyIncrease;
            isBeingMade = false;
            timer = 0;
            isReturningCS = true;
            textUpdate.text = money.ToString();
            ManagerScript.Instance.money = money;
            Debug.Log("Leaving!");
            FindObjectOfType<ManageAudio>().Play("ding");

        } else if (isBeingMade) 
        {
            timer += Time.deltaTime;
        } else if (Input.GetMouseButtonDown(0) && canMake) 
        {
            Debug.Log("start");
            FindObjectOfType<ManageAudio>().Play("slap");
            isBeingMade = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        canMake = true;
    } 

}

