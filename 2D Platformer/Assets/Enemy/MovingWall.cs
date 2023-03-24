using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;

    void Update()
    {
        this.transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}