using UnityEngine;

public class Character : MonoBehaviour
{   
    protected Animator anim;
    [SerializeField] protected float speed = 1;
    protected SpriteRenderer sprite;
    [SerializeField] protected Rigidbody2D rb;
    protected bool isDeath = false;
    protected bool isRight;
    string animName = "idle";
    public void ChangeAnimation(string animName){
        if(this.animName != animName){
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(animName);
        }
    }
    public void ChangeDicrect(bool isRight){
        this.isRight = isRight;
        if(isRight){
            // transform.rotation = Quaternion.identity;
            sprite.flipX = true;
        }else{
            // transform.rotation = Quaternion.Euler(Vector2.up * 180);
            sprite.flipX = false;
        }
    }
    public virtual void Hit(){
            isDeath = true;
    }
}
