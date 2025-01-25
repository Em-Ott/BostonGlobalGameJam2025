using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject counter;
    public Vector3 endPosManager;
    public static ManagerScript Instance;

    void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endPosManager = new Vector3(counter.transform.position.x, -2, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
