using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    private readonly List<Transform> snakeSegments = new();
    public Transform snakeSegmentPrefab;
    public Vector2 direction = Vector2.right;
    private Vector2 input;
    public int initialSize = 4;

    private bool MoveUp() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    private bool MoveDown() => Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    private bool MoveRight() => Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    private bool MoveLeft() => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);

    void Start() => ResetState();

    void Update() => MoveSnake();

    private void FixedUpdate() => MoveOnPsychs();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
            GrowSnake();

        if (other.gameObject.CompareTag("Obstacle"))
            ResetState();
    }

    private void MoveSnake()
    {
        YMovement();
        XMovement();
    }

    private void XMovement() 
    {
        if (direction.y != 0f)
        {
            if (MoveRight())
                input = Vector2.right;

            if (MoveLeft())
                input = Vector2.left;
        }
    }
    private void YMovement()
    {
        if (direction.x != 0f)
        {
            if (MoveUp())
                input = Vector2.up;

            if (MoveDown())
                input = Vector2.down;
        }
    }

    private void TrackSnakeSegments()
    {
        for (int i = snakeSegments.Count - 1; i > 0; i--)
            snakeSegments[i].position = snakeSegments[i - 1].position;
    }

    private void MoveOnPsychs()
    {
        if (input != Vector2.zero)
            direction = input;

        TrackSnakeSegments();

        float x = Mathf.Round(transform.position.x) + direction.x;
        float y = Mathf.Round(transform.position.y) + direction.y;

        transform.position = new Vector2(x, y);
    }

    public void GrowSnake()
    {
        Transform segment = Instantiate(snakeSegmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }

    public void ResetState()
    {
        direction = Vector2.right;
        transform.position = Vector3.zero;

        for (int i = 1; i < snakeSegments.Count; i++)
            Destroy(snakeSegments[i].gameObject);

        snakeSegments.Clear();
        snakeSegments.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
            GrowSnake();
    }
}
