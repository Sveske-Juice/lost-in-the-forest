using UnityEngine;
using UnityEngine.Splines;

public class MinecartAnimation : MonoBehaviour
{
    [SerializeField] private SplineAnimate splineAnimator;
    [SerializeField] private Animator animator;

    public void StartMinecartTurn()
    {
        animator.SetBool("ShouldTurn", true);
    }

    private void Update()
    {
        // Get minecarts progress in ring
        float progress = splineAnimator.NormalizedTime;

        animator.SetFloat("RingProgress", progress);
    }
}
