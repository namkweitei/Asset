using System.Collections;
using UnityEngine;
public enum EnemyState {Idle, Run, Attack, Death}
public enum AttackType {Shoot, Gore}
public enum EnemyType {Slime, Rino, Plant, Snail, Shell}
public class Enemy : Character
{
    [SerializeField] Collider2D col;
    [SerializeField]GameObject bulletPrefab;
    [SerializeField] public EnemyType enemyType;
    [SerializeField] AttackType attackType;
    [SerializeField] GameObject shell;

    private EnemyState state;
    private Player target;
    private float time;
    private float nextFireTime = 0;
    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        ChangeState(EnemyState.Idle);
        ChangeDicrect(true);
    }
    private void Update() {
        if(!isDeath)
        {
            UpdateState(state);

        }
    }
    public void ChangeState(EnemyState state){
        this.state = state;
        switch(state){
            case EnemyState.Idle:
                ChangeAnimation("idle");
                break;
            case EnemyState.Run:
                ChangeAnimation("run");
                break;
            case EnemyState.Attack:
                ChangeAnimation("run");
                break;
            case EnemyState.Death:
                ChangeAnimation("hit");
                break;
            default:
                break;
        }
    }
    public void UpdateState(EnemyState state){
        time -= Time.deltaTime;
        switch(state){
            case EnemyState.Idle:
                if(enemyType == EnemyType.Slime || enemyType == EnemyType.Snail || enemyType == EnemyType.Shell){
                    ChangeState(EnemyState.Run);
                }
                else if(enemyType == EnemyType.Rino){
                    if(time <= 0 ){
                        ChangeState(EnemyState.Run);
                    }
                }
                else if(enemyType == EnemyType.Plant){
                    ChangeState(EnemyState.Idle);
                }
                break;
            case EnemyState.Run:
                if(time <= 0 ){
                    rb.velocity = isRight ? Vector2.right * speed : Vector2.left * speed;
                }
                break;
            case EnemyState.Attack:
                Attack(attackType);
                break;
            case EnemyState.Death:
                break;
            default:
                break;
        }

    }
    public void Attack(AttackType attackType){
        switch(attackType){
            case AttackType.Shoot:
                if(target != null ){
                    ChangeDicrect(target.transform.position.x > transform.position.x);
                    nextFireTime -= Time.deltaTime;
                    if(nextFireTime<=0){
                        ChangeState(EnemyState.Attack);
                        nextFireTime = 2.4f;
                        GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.up * 0.06f, Quaternion.identity);
                        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                        bulletRb.velocity = isRight ? Vector2.right * speed : Vector2.left * speed; 
                        Destroy(bullet, 3f);
                    }else if(nextFireTime<=1.6f){
                        ChangeAnimation("idle");
                    }
                }
                else{
                    ChangeState(EnemyState.Idle);
                }
                break;
            case AttackType.Gore:
                if(target != null){
                    ChangeDicrect(target.transform.position.x > transform.position.x);
                    rb.velocity = isRight ? Vector2.right * speed : Vector2.left * speed; 
                }
                else{
                    ChangeState(EnemyState.Run);
                }
                break;
            default:
                break;
        }
    }
    public void SetTarget(Player player)
    {
        this.target = player;
        if(target != null){
            ChangeState(EnemyState.Attack);
        }else{
            ChangeState(EnemyState.Idle);
        }
    }
    public override void Hit()
    {
        if(enemyType == EnemyType.Snail){
            GameObject Shell = Instantiate(shell,transform.position,Quaternion.identity);
            
        }
        base.Hit();
        ChangeState(EnemyState.Death);
        // ChangeAnimation("hit");
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.isTrigger = true;
        Vector2 force = Random.insideUnitCircle;
        force.y = Mathf.Abs(force.y) * 1.2f;
        rb.AddForce(force * 150);
        
    }
}
