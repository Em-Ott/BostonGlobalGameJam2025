using TMPro;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject counter;
    public Vector3 endPosManager;
    public static ManagerScript Instance;
    public PositioningScript positioningScript;
    public TextMeshProUGUI moneyCounter; 
    public int money;

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endPosManager = new Vector3(counter.transform.position.x, -2, 0);
        //endPos = (-4.31, -2.00, 0.00)
    }

    // Update is called once per frame
    void Update()
    {

    }
}
