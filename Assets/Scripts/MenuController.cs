using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    Vector2 startPos;
    private void Awake() {
        startPos = transform.position;
    }

    public void ToggleEnabled() {
        transform.position = startPos;
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
