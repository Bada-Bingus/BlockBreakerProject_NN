using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; } // public getter, private setter.
    public Vector2 direction { get; private set; }
    public float speed = 15f;
    public float maxBounceAngle = 75f;

    [Header("Horizontal bounds (world X)")]
    public bool useCameraBounds = true;
    public float minX = -8f;
    public float maxX = 8f;

    private Collider2D paddleCollider;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        // Ensure the paddle is kinematic so collisions don't transfer momentum to the ball
        this.rigidbody.bodyType = RigidbodyType2D.Kinematic;

        this.paddleCollider = GetComponent<Collider2D>();

        // Optional: compute left/right bounds from the main Camera (orthographic)
        if (useCameraBounds && Camera.main != null && this.paddleCollider != null)
        {
            var cam = Camera.main;
            float halfVert = cam.orthographicSize;
            float halfHor = halfVert * cam.aspect;
            float halfPaddle = this.paddleCollider.bounds.extents.x;

            Vector3 camCenter = cam.transform.position;
            minX = camCenter.x - halfHor + halfPaddle;
            maxX = camCenter.x + halfHor - halfPaddle;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        // Move the kinematic paddle directly instead of using AddForce
        if (this.direction != Vector2.zero)
        {
            Vector2 newPos = this.rigidbody.position + this.direction * this.speed * Time.fixedDeltaTime;

            // Clamp horizontally so paddle stays inside the game bounds
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);

            this.rigidbody.MovePosition(newPos);
        }
    }

    public void ResetPaddle()
    {
        var pos = this.transform.position;
        pos.x = Mathf.Clamp(0f, minX, maxX); // center but clamped
        this.transform.position = new Vector2(pos.x, this.transform.position.y);
        this.rigidbody.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.linearVelocity);
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);
            float speed = ball.rigidbody.linearVelocity.magnitude;
            Vector2 direction = Quaternion.AngleAxis(newAngle, Vector3.forward) * Vector2.up;
            ball.rigidbody.linearVelocity = direction * speed;
        }
    }
}
