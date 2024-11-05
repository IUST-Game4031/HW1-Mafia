using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int PlayerCount = PlayerPrefs.GetInt("PlayerCount");
        Debug.Log("Player Count: " + PlayerCount);

        // delete the key from the PlayerPrefs
        PlayerPrefs.DeleteKey("PlayerCount");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
