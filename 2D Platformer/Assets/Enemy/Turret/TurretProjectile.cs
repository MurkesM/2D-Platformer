using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] float move_speed = 1f;

    public void SetMoveDirection(Vector3 move_direction)
    {
        Vector3.MoveTowards(transform.position, move_direction, move_speed * Time.deltaTime);
    }

    public void SetLookDirection(Vector3 look_direction)
    {
        transform.LookAt(look_direction);
    } 

    //check if playyer, then kill player and destroy projectile

    //destory projectile after so many seconds
}
