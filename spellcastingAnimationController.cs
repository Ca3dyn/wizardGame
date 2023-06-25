using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellcastingAnimationController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private bool isSpellcasting;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        //brain is kil
    }

    void Update()
    {

        bool qPressed = Input.GetKey(KeyCode.Q);
        bool isMagicCastAnimation = animator.GetCurrentAnimatorStateInfo(0).IsName("Magic Cast");

        if (qPressed && !isMagicCastAnimation)
        {
            animator.SetBool("isSpellcasting", true);
        }
        else if (!qPressed && isMagicCastAnimation)
        {
            animator.SetBool("isSpellcasting", false);
        }

        // Disable character controller while the animation is playing
        controller.enabled = !isMagicCastAnimation;

    }
}