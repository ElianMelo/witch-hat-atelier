using UnityEngine;

public class SpellCastController : MonoBehaviour
{
    private Animator playerAnimator;

    private readonly static string AttackAnim = "Attack";

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            int randomAttackAnim = Random.Range(1, 5);
            playerAnimator.SetTrigger(AttackAnim + randomAttackAnim);
        }
    }
}
