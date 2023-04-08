using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Barrel")]
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject barrel_pivot;
    [SerializeField] float barrel_rotation_speed = 90;
    [SerializeField] float smoothTime = 0.3f;
    Vector3 target_direction = new();
    float angle;
    float targetAngle;
    float currentVelocity;

    [Header("Projectile")]
    [SerializeField] GameObject projectile;
    [SerializeField] float fire_speed = 1;

    Vector3 target_position = new();

    const string player_tag = "Player";

    Coroutine fire_routine = null;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
        {
            target_position = other.transform.position;

            fire_routine = StartCoroutine(FireRoutine());
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
        {
            target_position = other.transform.position;

            //Determine which direction to rotate towards
            target_direction = target_position - barrel_pivot.transform.position;

            //Determine the angle in between the forward direction of the barrel pivot and the target
            targetAngle = Vector2.SignedAngle(Vector2.down, target_direction);

            //Get a smoothed out version of the angle
            angle = Mathf.SmoothDampAngle(angle, targetAngle, ref currentVelocity, smoothTime, barrel_rotation_speed);

            //rotate by angle
            barrel_pivot.transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
            StopCoroutine(fire_routine);
    }

    IEnumerator FireRoutine()
    {
        while (true)
        {
            GameObject new_projectile = Instantiate(projectile, barrel.transform.position, Quaternion.identity, this.transform);

            new_projectile.GetComponent<TurretProjectile>().SetTarget(target_position);

            yield return new WaitForSeconds(fire_speed);
        }
    }
}