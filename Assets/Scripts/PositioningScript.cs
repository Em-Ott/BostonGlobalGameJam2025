using UnityEngine;

public class PositioningScript : MonoBehaviour
{
    public bool ordering;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        ordering = true;
    } 

    private void OnTriggerExit2D(Collider2D collision)
    {
        ordering = false;
        if (collision.transform.position.x < 0) {
            // Game over? Something that happens when you miss? 
            // Scene change(?)
            Destroy(collision.gameObject);
        }
    }
}
