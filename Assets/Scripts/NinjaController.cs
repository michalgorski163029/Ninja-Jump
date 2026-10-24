
using System.Collections;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float jumpForce = 5f;

    [Header("Ninja Animation")]
    public SpriteRenderer ninjaVisual;
    public Animator ninjaAnimator;
    public Sprite ninjaIdleSprite;

    private Rigidbody2D rb;
    private int direction = 1;
    private Coroutine jumpRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (ninjaVisual != null)
        {
            ninjaVisual.sprite = ninjaIdleSprite;
        }

        if (ninjaAnimator != null)
        {
            ninjaAnimator.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            if (jumpRoutine != null)
            {
                StopCoroutine(jumpRoutine);
            }

            jumpRoutine = StartCoroutine(PlayJumpAnimation());
        }
    }

    IEnumerator PlayJumpAnimation()
    {
        if (ninjaAnimator == null)
        {
            yield break;
        }

        ninjaAnimator.enabled = true;
        ninjaAnimator.Play("NinjaJump", 0, 0f);

        yield return null;

        AnimatorStateInfo state =
            ninjaAnimator.GetCurrentAnimatorStateInfo(0);

        float duration = state.length;

        if (duration <= 0f)
        {
            duration = 0.5f;
        }

        yield return new WaitForSeconds(duration);

        ninjaAnimator.enabled = false;

        if (ninjaVisual != null && ninjaIdleSprite != null)
        {
            ninjaVisual.sprite = ninjaIdleSprite;
        }

        jumpRoutine = null;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            direction * moveSpeed,
            rb.linearVelocity.y
        );

        if (ninjaVisual != null)
        {
            ninjaVisual.flipX = direction < 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "RightWall")
        {
            direction = -1;

            FindFirstObjectByType<ScoreManager>().AddPoint();
            FindFirstObjectByType<ShurikenSpawner>()
                .RandomizeShurikens();
        }
        else if (collision.gameObject.name == "LeftWall")
        {
            direction = 1;

            FindFirstObjectByType<ScoreManager>().AddPoint();
            FindFirstObjectByType<ShurikenSpawner>()
                .RandomizeShurikens();
        }
    }
}
