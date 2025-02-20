using UnityEngine;

public class ReloadAnimator : MonoBehaviour
{
    public Animator animator;

    public void PlayReloadAnimation()
    {
        animator.SetTrigger("Reload");
    }
}
