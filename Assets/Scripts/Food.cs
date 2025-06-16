using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start() => RandomizePosition();

    private void OnTriggerEnter2D(Collider2D other) => RandomizePosition();

    public void RandomizePosition()
    {
        Bounds bounds = gridArea.bounds;

        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

        transform.position = new Vector2(x, y);
    }
}
