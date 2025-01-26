using UnityEngine;

public class PositioningScript : MonoBehaviour
{
    public bool immediateExit;
    public bool miss;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
    } 

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Customer")) {
            // Game over? Something that happens when you miss? 
            // Scene change(?)
            immediateExit = true;
            miss = true;
        }
    }
}
