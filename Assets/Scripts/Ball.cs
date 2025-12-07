using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody {  get; private set; }
    public float speed = 8f;
    public bool enforceConstantSpeed = true;
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        ResetBall();
    }

    void Update()
    {
        
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }
    public void ResetBall()
    {
        this.transform.position = new Vector2(0f, -3.33f);
        this.rigidbody.linearVelocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void FixedUpdate()
    {
        if (this.enforceConstantSpeed)
        {
            var vel = this.rigidbody.linearVelocity;
            if (vel.sqrMagnitude > 0.0001f)
            {
                this.rigidbody.linearVelocity = vel.normalized * this.speed;
            }
        }
    }
}
