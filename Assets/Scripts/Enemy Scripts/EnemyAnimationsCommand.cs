using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAnimationsCommand
{
    public virtual void Execute(Animator anim) { }

    public virtual void Cancel(Animator anim) { }
}

public class DoIdle : EnemyAnimationsCommand 
{
    public override void Execute(Animator anim) 
    {
        anim.SetBool("isIdle", true);
    }

    public override void Cancel(Animator anim) 
    {
        anim.SetBool("isIdle", false);
    }
}

public class DoMove : EnemyAnimationsCommand 
{
    public override void Execute(Animator anim)
    {
        anim.SetBool("isWalking", true);
    }

    public override void Cancel(Animator anim)
    {
        anim.SetBool("isWalking", false);
    }
}

public class DoAttack: EnemyAnimationsCommand
{
    public override void Execute(Animator anim)
    {
        anim.SetTrigger("isAttacking");
    }
}

public class DoDeath: EnemyAnimationsCommand
{
    public override void Execute(Animator anim)
    {
        anim.SetTrigger("isDead");
    }
}
