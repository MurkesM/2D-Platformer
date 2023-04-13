using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] float move_speed = 1f;
    Vector3 target = new();

    [SerializeField] float time_to_destroy = 4;
    float time_alive = 0;

    const string player_tag = "Player";

    void OnEnable()
    {
        time_to_destroy += Time.time;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target * 1.75f, move_speed * Time.deltaTime);
        transform.rotation.SetLookRotation(target);

        time_alive = Time.time;

        if (time_alive >= time_to_destroy)
            DestroyProjectile();
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag(player_tag))
        {
            PlayerControls.KillPlayer();
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        //TODO: add sfx and/or vfx here

        Destroy(gameObject);
    }
}