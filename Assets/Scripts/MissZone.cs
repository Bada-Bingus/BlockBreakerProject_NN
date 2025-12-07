using UnityEngine;

public class MissZone : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            Object.FindFirstObjectByType<GameManager>().Miss();
        }
    }
}
