using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panel;

    public void TogglePanel() {
        Animator animator = panel.GetComponent<Animator>();
        animator.SetBool("Open", !animator.GetBool("Open"));
    }
}
