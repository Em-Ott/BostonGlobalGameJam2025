using TMPro;
using UnityEngine;

public class ClickScript : MonoBehaviour {
    public int moneyIncrease;
    public float timerDurationDrink;
    public bool isReturningCS = false;
    private TextMeshProUGUI textUpdate;
    public int money;
    private float timer = 0;
    private bool isBeingMade = false;
    private bool canMake = false;
    private Vector3 endPosClick;
    public PositioningScript positioningScript;
    public Animator characterAnim;
    public Animator characterAnim2;
    public Animator characterAnim3;
    public Characters charactersScript;

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
            if (charactersScript.hasCow()) {
                moneyIncrease = 20;
            } else if (charactersScript.hasRat()) {
                moneyIncrease = 10;
            }

            money += moneyIncrease;
            isBeingMade = false;
            timer = 0;
            isReturningCS = true;
            transform.Rotate(0, 180, 0);
            textUpdate.text = money.ToString();
            ManagerScript.Instance.money = money;
            FindObjectOfType<ManageAudio>().Play("ding");
            characterAnim.SetBool("serve", false);
            characterAnim2.SetBool("serve", false);
            characterAnim3.SetBool("serve", false);

        } else if (isBeingMade) 
        {
            timer += Time.deltaTime;
        } else if (Input.GetMouseButtonDown(0) && canMake) 
        {
            FindObjectOfType<ManageAudio>().Play("slap");
            characterAnim.SetBool("serve", true);
            characterAnim2.SetBool("serve", true);
            characterAnim3.SetBool("serve", true);
            isBeingMade = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMake = true;
        }
        
    } 

    private void OnTriggerExit2D(Collider2D collision)
    {
        canMake = false;
    }

}

