
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [Range(0f, 100f)][SerializeField] float backAngle = 65f;
    [Range(0f, 100f)][SerializeField] float sideAngle = 155f;
    [SerializeField] Transform mainTransform;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] PlayerMovement player;

    void LateUpdate()
    {
        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        Debug.DrawRay(Camera.main.transform.position, camForwardVector * 5f, Color.magenta);
        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);

        Vector2 animationDirection = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);
        spriteRenderer.flipX = true;
        

        if (angle < backAngle)
        {
            animationDirection = new Vector2(0f, -1f);
        }
        else if (angle < sideAngle)
        {
            animationDirection = new Vector2(1f, 0f);
            if (signedAngle < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;

            }
        }
        else
        {
            animationDirection = new Vector2 (0f, 1f);
        }

        if (player.isRunning)
            animator.SetBool("run", true);
        else
            animator.SetBool("run", false);

        animator.SetFloat("moveX", animationDirection.x);
        animator.SetFloat("moveY", animationDirection.y);
    }
}
