using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject barrel_pivot;
    [SerializeField] GameObject barrel;
    [SerializeField] GameObject projectile;
    [SerializeField] float rotate_speed = 1;

    const string player_tag = "Player";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
            StartCoroutine(FireRoutine(other.transform.position));
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
            barrel_pivot.transform.LookAt(other.transform.position * rotate_speed * Time.deltaTime);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(player_tag))
            StopCoroutine(FireRoutine(Vector3.zero));
    }

    IEnumerator FireRoutine(Vector3 player_position)
    {
        //TODO: add comments 

        while (true)
        {
            GameObject new_projectile = Instantiate(projectile, barrel.transform.position, Quaternion.identity);
            TurretProjectile turret_projectile = new_projectile.GetComponent<TurretProjectile>();
            turret_projectile.SetMoveDirection(player_position);
            turret_projectile.SetLookDirection(player_position);

            yield return new WaitForSeconds(.5F);
        }
    }
}