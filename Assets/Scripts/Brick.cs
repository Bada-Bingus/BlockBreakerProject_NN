using System;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private GameManager gameManager;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] states;
    public int health { get; private set; }
    public int point = 100;
    public bool unbreakable;
    public bool isPowerBrick { get; private set; }
    public GameObject powerUpPrefab;
    public GameObject powerIndicator;
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        ResetBrick();
    }

    void Update()
    {
        
    }
    public void ResetBrick()
    {
        this.gameObject.SetActive(true);
        this.isPowerBrick = false;

        if (!this.unbreakable)
        {
            int maxVariants = 5;

            // Random.Range for ints: min inclusive, max exclusive
            int randomIndex = UnityEngine.Random.Range(0, maxVariants);
            this.health = randomIndex + 1;
            this.spriteRenderer.sprite = this.states[randomIndex];

            this.isPowerBrick = (UnityEngine.Random.Range(0, 30) < 5);

            if (this.powerIndicator != null)
                this.powerIndicator.SetActive(this.isPowerBrick);
        }
    }
    private void Hit()
    {
        if (this.unbreakable)
        {
            return;
        }
        this.health--;

        if (this.health <= 0)
        {
            if (this.isPowerBrick)
            {
                Debug.Log("PowerUp");
            }
            this.gameObject.SetActive(false);
        }
        else
        {

            this.spriteRenderer.sprite = this.states[this.health - 1];
        }

        UnityEngine.Object.FindFirstObjectByType<GameManager>().Hit(this);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }
}
