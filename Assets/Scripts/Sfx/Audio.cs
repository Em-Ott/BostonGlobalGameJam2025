using UnityEngine;

public class Audio : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        FindObjectOfType<ManageAudio>().PlayLoop("bgMusic");
    }
}
