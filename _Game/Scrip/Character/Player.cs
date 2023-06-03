
using UnityEngine;
public class Player : Character
{
    [SerializeField] GameObject smoke;
    [SerializeField] GameObject[] players;
    [SerializeField] GameObject playerCurrent;
    [SerializeField] ParticleSystem jumpVFX;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float wallSlideForce = 1f;
    [SerializeField] private float forceJump;

    private bool IsGrounded;
    private bool doubleJump;
    private bool isTouchingWall;
    private bool isJumping;
    private bool isAttack = false;
    private Vector3 savePoint;
    float direct;
    bool isUseButton;
    int fruit = 0;
    int hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        int player = PlayerPrefs.GetInt("Player",0);
        GameObject newplayer = Instantiate(players[player],transform.position,Quaternion.identity);
        newplayer.transform.SetParent(playerCurrent.transform.parent);
        Destroy(playerCurrent);
        rb = GetComponent<Rigidbody2D>();
        anim = newplayer.GetComponent<Animator>();
        sprite = newplayer.GetComponent<SpriteRenderer>();
        fruit = 0;
        SaveCheckPoint(transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        if(isDeath || isAttack){
            return;
        }
        if(!isUseButton){
            direct = Input.GetAxis("Horizontal");
        }                                                                        
        IsGrounded = CheckGround();
        if(IsGrounded ){
            doubleJump = false;
            smoke.SetActive(true);
        }
        CheckWall();
        WallJump();
        Control(direct);
        Jump();
        HandleDirect(direct);
        HandleAnimation(direct);
    }
    public void SetDirectButton(float direct){
        this.direct = direct;
        isUseButton = direct != 0;
        if(direct != 0){
            isUseButton = true;
        }else{
            isUseButton = false;
        }
    }
    public void JumpButton() {
        if( Mathf.Abs(rb.velocity.y) <= 0.05f && IsGrounded == true){
            JumpForce(forceJump);
            Instantiate(jumpVFX, transform.position + Vector3.down * 0.05f, Quaternion.identity);
        }else if( Mathf.Abs(rb.velocity.y) <= 0.05f && isTouchingWall == true){
            JumpForce(forceJump);
            Instantiate(jumpVFX, transform.position + Vector3.down * 0.05f, Quaternion.identity);
        }else if(Mathf.Abs(rb.velocity.y) <= 1.5f && doubleJump == false){
            JumpForce(forceJump );
            doubleJump = true;
            Instantiate(jumpVFX, transform.position + Vector3.down * 0.05f, Quaternion.identity);
            
        }
    }
    private void Control(float direct){
        rb.velocity = Vector2.right * direct * speed + Vector2.up * rb.velocity.y;
    }
    private void HandleDirect(float direct){
        if(direct != 0){
            ChangeDicrect(direct < 0);
        }
    }
    private void HandleAnimation(float direct){
        if(Mathf.Abs(rb.velocity.y) > 0.05f && rb.velocity.y > 0){
            if(doubleJump == true){
                ChangeAnimation("doublejump");
            }else{
                ChangeAnimation("jump");
            }
            smoke.SetActive(false);
        }else if(Mathf.Abs(rb.velocity.y) > 0.05f && rb.velocity.y < -2f){
            ChangeAnimation("fall");
        }else{
            if(direct == 0 && IsGrounded == true){
                ChangeAnimation("idle");
                smoke.SetActive(true);
            }else if(direct != 0 && IsGrounded == true){
                ChangeAnimation("run");
            }
        }
    }

    private void WallJump(){
        if (isTouchingWall == true && IsGrounded == false && rb.velocity.y < 0 )
        {
            doubleJump = false;
            ChangeAnimation("walljump");
            // rb.velocity = new Vector2(rb.velocity.x, 0f);
            // rb.AddForce(new Vector2( wallSlideForce, 0f), ForceMode2D.Force);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideForce, float.MaxValue));
        }
    }
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= 0.05f && IsGrounded == true){
            JumpForce(forceJump);
            Instantiate(jumpVFX, transform.position + Vector3.down * 0.05f, Quaternion.identity);
        }else if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= 0.05f && isTouchingWall == true){
            JumpForce(forceJump);
            Instantiate(jumpVFX, transform.position + Vector3.down * 0.05f, Quaternion.identity);
        }else if(Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) <= 1.5f && doubleJump == false){
            JumpForce(forceJump );
            doubleJump = true;
            Instantiate(jumpVFX, transform.position + Vector3.down * 0.05f, Quaternion.identity);
        }
    }
    public void SaveCheckPoint(Vector3 point){
        savePoint = point;
    }
    public void LoadStatus(){
        if(isDeath){
        ChangeAnimation("idle");
        isDeath = false;
        transform.position = savePoint;
        hp = 3;
        GamePlay.Instance.SetHp(hp);
        }else{
            isAttack = false;
            ChangeAnimation("idle");
        }
    }
    public override void Hit(){
        isAttack = true;
        JumpForce(forceJump);
        ChangeAnimation("hit");
        hp -= 1;
        GamePlay.Instance.SetHp(hp);
        Invoke(nameof(LoadStatus), 0.3f);
        if(hp < 1){
            isDeath = true;
            Invoke(nameof(LoadStatus), 0.5f);
        }
    }
    internal void JumpForce(float force)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * force);
    }

    public void AddFruit(int amount = 1){
        fruit += amount;
        GamePlay.Instance.SetFruit(fruit);
        GamePlay.Instance.SetRank(fruit);
    }
    private bool CheckGround()
    {   Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.18f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.18f, groundLayer);
        return hit.collider != null;
    }
    private  void CheckWall()
    {   Debug.DrawLine(transform.position, transform.position + Vector3.right * 0.085f, Color.red);
        Debug.DrawLine(transform.position, transform.position + Vector3.left * 0.085f, Color.red);
        RaycastHit2D right = Physics2D.Raycast(transform.position, Vector2.right, 0.085f, wallLayer);
        RaycastHit2D left = Physics2D.Raycast(transform.position, Vector2.left, 0.085f, wallLayer);
        
        if(right.collider != null) {
            isTouchingWall = true;
            ChangeDicrect(false);
        }else if(left.collider != null) {
            isTouchingWall = true;
            ChangeDicrect(true);
        }else{
            isTouchingWall = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy" && !isDeath && rb.velocity.y < 0){
            JumpForce(forceJump);
            other.GetComponent<Enemy>().Hit();
        }
    }
}
