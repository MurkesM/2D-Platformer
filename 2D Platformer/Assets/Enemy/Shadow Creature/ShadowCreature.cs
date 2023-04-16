using UnityEngine;
using UnityEngine.EventSystems;

public class ShadowCreature : MonoBehaviour
{
    [SerializeField] float move_speed = 2;
    bool can_move = true;

    Vector2 move_direction = new();

    const string player_layer = "Player";
    const string torch_layer = "Torch";

    Torch torch = null;
    PlayerControls player_controls = null;

    void Awake()
    {
        player_controls = FindObjectOfType<PlayerControls>();

        //initialize move direciton
        move_direction = new(1, player_controls.transform.position.y);
    }

    void Update()
    {
        //update the height of the creature based on players height 
        move_direction.y = player_controls.transform.position.y;

        transform.Translate(move_direction * move_speed * Time.deltaTime);
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