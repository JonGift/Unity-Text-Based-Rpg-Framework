using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentController : MonoBehaviour
{
    Text text;
    bool done = false;
    float alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        /*text.text = "TEST!!!";
        for (int i = 0; i < Random.Range(2, 150); i++)
            text.text += i;
        text.text += "\n";*/
    }

    private void Update() {
        if (done)
            Destroy(this);
        alpha += 3;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha / 255f);
        if (alpha > 255f)
            done = true;
    }
}
