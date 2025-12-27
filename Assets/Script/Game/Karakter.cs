using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    [Header("Refrensi")]

    public GamePlay gameplay;

    [Header("State")]
    [Header("Respawn State")]
    public bool isInvincible = false;

    public bool isDead = false;

    [Header("Komponen")]
    public Collider2D[] colliders;
    public CharacterRespawnEffect respawnEffect;


    [Header("Sistem Loncat dan Turun")]
    public float loncat;
    public float turun;
    private float dropDuration = 0.3f;
    public LayerMask platformLayer;
    public Rigidbody2D Rigidbody2D;
    private int MaxLoncat = 0;
    public bool isGrounded = true;
    bool wasGrounded = false;

    public ParticleSystem dustEffect;


    public Transform groundCheck;
    public float groundCheckRadius = 0.12f;

    public float jumpCooldown = 0.7f;  // delay setelah double jump
    bool jumpLocked = false;



    // Start is called before the first frame update
    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        respawnEffect = GetComponent<CharacterRespawnEffect>();
    }

    // Update is called once per frame
    void Update()
    {
            // Cek apakah groundCheck menyentuh object di platformLayer
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            platformLayer
        ) != null;


                // Setelah hitung isGrounded:

        if (!wasGrounded && isGrounded && !isDead)
        {
            dustEffect.Play();   // seketika mendarat
        }

        if (wasGrounded && !isGrounded)
        {
            dustEffect.Stop();   // seketika loncat
        }

        wasGrounded = isGrounded;


        // Android
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x > Screen.width / 2)
                {
                    Loncat();
                }
                else
                {
                    Turun();
                }
            }
        }

#if UNITY_EDITOR
        // Editor (Mouse)
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                Loncat();
            }
            else
            {
                Turun();
            }
        }
#endif
    }

            public void Loncat()
        {
                if (jumpLocked) return; // cegah spam

                if (isGrounded)
                {
                    MaxLoncat = 0;  // reset counter
                }

                if (MaxLoncat < 2 && !isDead)
                {
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, loncat);
                    AudioManager.instance.PlayJumpSFX();
                    MaxLoncat++;

                    // Kalau ini adalah loncatan kedua → aktifkan cooldown
                    if (MaxLoncat == 2)
                    {
                        StartCoroutine(JumpCooldown());
                    }
                }
         }


    public void Turun()
    {
        if (!isDead)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, -turun);
            StartCoroutine(DropThroughPlatform());
        }
       
    }

    

    IEnumerator DropThroughPlatform()
    {
        Vector2 boxSize = new Vector2(1f, 0.2f);
        Vector3 boxCenter = transform.position - new Vector3(0, 0.5f, 0);

        Collider2D platform = Physics2D.OverlapBox(boxCenter, boxSize, 0f, platformLayer);

        if (platform != null)
        {
            PlatformEffector2D effector = platform.GetComponent<PlatformEffector2D>();
            if (effector != null)
            {
                platform.enabled = false;
                yield return new WaitForSeconds(dropDuration);
                platform.enabled = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isInvincible) return;
    
        if (collision.gameObject.CompareTag("Monster") && !isDead)
        {
            isDead = true;

            gameplay.GameOver();
            respawnEffect.SetInvisible();
            isInvincible = true;
            AudioManager.instance.PlayGameoverSFX();
        }
    }

        void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible) return;

        if (collision.CompareTag("Huruf"))
        {
            AudioManager.instance.PlayCollectLetterSFX();
        }
    }

    public void Continue()
    {
        Vector3 pos = transform.position;
        pos.y = -1f;
        transform.position = pos;
        isDead = false;

        respawnEffect.PlayRespawnEffect(() =>
        {
            SetColliders(true);
            SetControl(true);
        });
    }

    IEnumerator JumpCooldown()
    {
        jumpLocked = true;
        yield return new WaitForSeconds(jumpCooldown);
        jumpLocked = false;
    }
    void SetControl(bool active)
    {
        enabled = active; // matikan Update()
    }

    void SetColliders(bool active)
    {
        foreach (var col in colliders)
        col.enabled = active;
    }

}
    
    

