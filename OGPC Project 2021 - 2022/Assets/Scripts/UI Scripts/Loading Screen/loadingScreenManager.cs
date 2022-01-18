using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingScreenManager : MonoBehaviour
{
    // loading screen variables \\
    private Animator animator;
    public static bool loadingDone = false;
    public GameObject loadingScreen;

    // caching and enabling the loading screen
    void Start()
    {
        animator = loadingScreen.GetComponent<Animator>();
        loadingScreen.SetActive(true);
        PlayerMovement.playerAbleMove = false;
    }

    // disable the loading screen when it is done
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.999f && animator.GetCurrentAnimatorStateInfo(0).IsName("loadingScreen_active")) {
            animator.SetBool("loadingDone", true);
            loadingDone = true;
            loadingScreen.SetActive(false);
            PlayerMovement.playerAbleMove = true;
        }
    }
}
