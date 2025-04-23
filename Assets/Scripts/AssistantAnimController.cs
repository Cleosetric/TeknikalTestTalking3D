using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantAnimController : MonoBehaviour
{
    public Animator animator;

    public void SetListeningAnimation(bool listening)
    {
        animator.SetBool("isListening", listening);
        if (listening)
            SetRandomExpression();
    }

    public void SetThinkingAnimation(bool thinking)
    {
        animator.SetBool("isThinking", thinking);
        if (thinking)
            SetRandomExpression();
    }

    public void SetTalkingAnimation(bool talking)
    {
        animator.SetBool("isTalking", talking);
        if (talking)
            SetRandomExpression();
    }

    private void SetRandomExpression()
    {
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                animator.Play("Layer_nodding_once", 1, 0.0f);
                break;
            case 1:
                animator.Play("Layer_swinging_body", 1, 0.0f);
                break;
            default:
                animator.Play("Layer_start", 1, 0.0f);
                break;
        }
    }
}
