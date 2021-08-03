using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ContentController : MonoBehaviour, IPointerClickHandler
{
    TextMeshProUGUI text;
    bool done = false;
    float alpha = 0;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if (done)
            return;
        alpha += 3;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha / 255f);
        if (alpha > 255f)
            done = true;
    }

    public void OnPointerClick(PointerEventData eventData) {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, Camera.main);
        if (linkIndex != -1) {
            TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
            Debug.Log(linkInfo.GetLinkID());
        }
    }
}
