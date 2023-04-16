using UnityEngine;

public class ShadowCreature : MonoBehaviour
{
    [SerializeField] float move_speed = 2;
    bool can_move = true;

    const string player_layer = "Player";

    const string torch_layer = "Torch";

    Torch torch = null;

    void Update()
    {
        transform.Translate(Vector2.right * move_speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(player_layer))
            PlayerControls.KillPlayer();

        if (other.CompareTag(torch_layer))
        {
            torch = other.GetComponent<Torch>();

            //keep moving if torch isn't glowing
            if (!torch.IsGlowing)
                return;

            torch.GlowStateUpdated += SetMoveState;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(torch_layer))
        {
            SetMoveState(true);

            torch.GlowStateUpdated -= SetMoveState;
        }
    }

    void SetMoveState(bool can_move)
    {
        this.can_move = can_move;
    }
}