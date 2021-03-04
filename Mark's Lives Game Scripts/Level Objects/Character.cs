using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Character : Unit
{
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float crawlingSpeed;
    [SerializeField]
    float jumpForce;

    float currentSpeed;
    float dirX;
    int healthPoints = 5;
    int bulletPoints = 3;
    public int Lives
    {
        get { return healthPoints; }
        set
        {
            if (value <= 5) healthPoints = value;
            livesBar.Refresh();
        }
    }

    public int Bullets
    {
        get { return bulletPoints; }
        set
        {
            if (value >= 0) bulletPoints = value;
            bulletsBar.Refresh();

        }
    }

    LivesBar livesBar;
    BulletsBar bulletsBar;

    bool isHurting, isDead;
    bool facingRight = true;
    Vector3 localScale;

    [SerializeField]
    bool doubleJumpAllowed;
    bool isGrounded;

    Rigidbody2D rb;

    int characterLayer, panelsLayer;

    [SerializeField]
    Sprite bulletImg;
    [SerializeField]
    float bulletSpeed;
    Bullet bullet;

    Renderer rend;

    bool isSitting;

    BoxCollider2D boxCollider;
    float InitialCrawlingBoxColliderSizeY, CrawlingBoxColliderSizeY;

    bool isInPipe, isJumping;

    protected override void Awake()
    {
        base.Awake();
        livesBar = FindObjectOfType<LivesBar>();
        bulletsBar = FindObjectOfType<BulletsBar>();

        rb = GetComponent<Rigidbody2D>();

        bullet = Resources.Load<Bullet>("Bullet");

        Physics2D.IgnoreLayerCollision(8, 10, false);
        Physics2D.IgnoreLayerCollision(8, 11, false);
    }

    private void Start()
    {
        localScale = transform.localScale;

        characterLayer = LayerMask.NameToLayer("Player");
        panelsLayer = LayerMask.NameToLayer("Panel");

        currentSpeed = moveSpeed;
        crawlingSpeed = moveSpeed * 0.5f;

        rend = GetComponent<Renderer>();

        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        InitialCrawlingBoxColliderSizeY = boxCollider.size.y;
        CrawlingBoxColliderSizeY = boxCollider.size.y / 2;
    }

    private void Update()
    {
        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            doubleJumpAllowed = true;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump") && isGrounded && !isDead)
        {
            Jump();
            isJumping = true;
        }
        else if (CrossPlatformInputManager.GetButtonDown("Jump") && doubleJumpAllowed && !isDead && rb.velocity.y >= 0)
        {
            Jump();
            doubleJumpAllowed = false;
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        if (CrossPlatformInputManager.GetAxis("Vertical") == 1 || isInPipe)
        {
            if (dirX == 0)
            {
                isSitting = true;
            }
            else
            {
                isSitting = false;
            }
            boxCollider.size = new Vector2(boxCollider.size.x, CrawlingBoxColliderSizeY);
        }
        else
        {
            isSitting = false;
            boxCollider.size = new Vector2(boxCollider.size.x, InitialCrawlingBoxColliderSizeY);
        }

        if (CrossPlatformInputManager.GetButtonDown("Throw"))
        {
            if (Bullets > 0)
            {
                if (rb.velocity.x == 0 && !isSitting)
                {
                    anim.SetTrigger("isThowing");
                }
                Shoot();
                FindObjectOfType<AudioManager>().PlaySound("PlayerThrow");
            }
            else
            {
                if (rb.velocity.x == 0 && !isSitting)
                {
                    anim.SetTrigger("NoBullets");
                }
            }
        }

        SetAnimationState();

        if (!isDead)
            dirX = CrossPlatformInputManager.GetAxis("Horizontal") * currentSpeed;

        if (rb.velocity.y > 0 || isJumping)
        {
            Physics2D.IgnoreLayerCollision(characterLayer, panelsLayer, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(characterLayer, panelsLayer, false);
        }

        if ((CrossPlatformInputManager.GetAxis("Vertical") == 1 && Mathf.Abs(dirX) > 0) || isInPipe)
        {
            currentSpeed = crawlingSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (!isHurting)
            rb.velocity = new Vector2(dirX, rb.velocity.y);
    }

    private void LateUpdate()
    {
        CheckWhereToFace();
    }

    private void SetAnimationState()
    {
        if (dirX == 0)
        {
            anim.SetBool("isRunning", false);
        }

        if (rb.velocity.y == 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }

        if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);

        if (CrossPlatformInputManager.GetAxis("Vertical") == 1 || isInPipe)
        {
            if (dirX == 0)
            {
                anim.SetBool("isCrawling", false);
                anim.SetBool("isSitting", true);
            }

            if (Mathf.Abs(dirX) > 0)
            {
                anim.SetBool("isCrawling", true);
            }
        }
        else
        {
            anim.SetBool("isSitting", false);
            anim.SetBool("isCrawling", false);
        }


        if (rb.velocity.y > 0)
            anim.SetBool("isJumping", true);

        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
    }

    private void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else if (dirX < 0)
            facingRight = false;

        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;

        transform.localScale = localScale;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy") || (col.gameObject.tag.Equals("FallingObstacle") && !isSitting))
        {
            ReceiveDamage();
        }

        if (col.gameObject.tag.Equals("DieArea"))
        {
            Die();
        }

        if (col.gameObject.tag.Equals("Enemy Damage Zone"))
        {
            Enemy enemy = col.gameObject.transform.parent.GetComponent<Enemy>();
            enemy.ReceiveDamage();

            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * 200.0f);
            FindObjectOfType<AudioManager>().PlaySound("PlayerJump");
        }

        if (col.gameObject.tag.Equals("Pipe"))
        {
            isInPipe = true;
        }

        if (col.gameObject.tag.Equals("Ending"))
        {
            Ending();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Pipe"))
        {
            isInPipe = false;
        }
    }

    public override void ReceiveDamage()
    {
        Lives--;

        if (healthPoints > 0)
        {
            anim.SetTrigger("isHurting");
            StartCoroutine("Hurt");
            FindObjectOfType<AudioManager>().PlaySound("PlayerDamage");
        }
        else
        {
            Die();
        }
    }
    public override void Die()
    {
        dirX = 0;
        isDead = true;
        anim.SetTrigger("isDead");

        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.Losing();
        FindObjectOfType<AudioManager>().PlaySound("PlayerLoose");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("MovingPanel"))
        {
            this.transform.parent = col.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("MovingPanel"))
        {
            this.transform.parent = null;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce);
        FindObjectOfType<AudioManager>().PlaySound("PlayerJump");
    }

    private IEnumerator Hurt()
    {
        isHurting = true;
        rb.velocity = Vector2.zero;

        if (facingRight)
            rb.AddForce(new Vector2(-200f, 200f));
        else
            rb.AddForce(new Vector2(200f, 200f));

        yield return new WaitForSeconds(0.5f);

        isHurting = false;
    }

    private void Shoot()
    {
        Bullets--;
        Vector3 position = transform.position; position.y += 0.2F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.spriteImg = bulletImg;
        newBullet.bulletSpeed = bulletSpeed;
        newBullet.Direction = newBullet.transform.right * (!facingRight ? -1.0F : 1.0F);
    }

    private void Ending()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.Ending();
        FindObjectOfType<AudioManager>().PlaySound("WinLevel");
    }
}
