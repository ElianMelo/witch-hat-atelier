using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastController : MonoBehaviour
{
    public List<AnimationClip> attackAnimations = new();
    public GameObject spellPrefab;

    private Animator playerAnimator;
    private PlayerMovementController playerMovementController;
    private string currentAttackAnim;
    private IEnumerator waitAnimationCoroutine;

    private readonly static string AttackAnim = "Attack";

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerMovementController = GetComponent<PlayerMovementController>();
        currentAttackAnim = "Attack1";
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            int randomAttackAnim = Random.Range(1, 5);
            currentAttackAnim = AttackAnim + randomAttackAnim;
            float time = attackAnimations[randomAttackAnim - 1].length;
            playerAnimator.SetTrigger(currentAttackAnim);
            if (waitAnimationCoroutine != null) StopCoroutine(waitAnimationCoroutine);
            waitAnimationCoroutine = WaitAnimation((time/2));
            StartCoroutine(waitAnimationCoroutine);
            GameObject spellInstance = Instantiate(spellPrefab, transform.position, Quaternion.identity);
            spellInstance.transform.position = transform.position + transform.forward * 2;
        }
    }

    private IEnumerator WaitAnimation(float time)
    {
        playerMovementController.CanMove = false;
        yield return new WaitForSeconds(time);
        playerMovementController.CanMove = true;
    }
}
