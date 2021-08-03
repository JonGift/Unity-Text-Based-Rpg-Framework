using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject textObj;
    public Text textComp;
    public int charIndex;
    public GameObject tempGuy;
    Canvas canvas;
    public GameObject content;
    Scrollbar scrollbar;
    TextManager textManager;

    private void Start() {
        // textComp = textObj.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        canvas = GetComponent<Canvas>();
        scrollbar = textObj.transform.GetChild(1).GetComponent<Scrollbar>();
        textManager = textObj.GetComponent<TextManager>();
    }

    void PrintPos() {
        string text = textComp.text;

        if (charIndex >= text.Length + 1)
            return;

        TextGenerator textGen = new TextGenerator(text.Length);
        Vector2 extents = textComp.gameObject.GetComponent<RectTransform>().rect.size;
        textGen.Populate(text, textComp.GetGenerationSettings(extents));

        int newLine = text.Substring(0, charIndex).Split('\n').Length - 1;
        int whiteSpace = text.Substring(0, charIndex).Split(' ').Length - 1;
        int indexOfTextQuad = (charIndex * 4) + (newLine * 4) - 4;
        if (indexOfTextQuad < textGen.vertexCount) {
            Vector3 avgPos = (textGen.verts[indexOfTextQuad].position +
                textGen.verts[indexOfTextQuad + 1].position +
                textGen.verts[indexOfTextQuad + 2].position +
                textGen.verts[indexOfTextQuad + 3].position) / 4f;

            print(avgPos);
            Vector3 worldPos = textComp.transform.TransformPoint(avgPos) / canvas.scaleFactor;
            print(canvas.scaleFactor);
            Instantiate(tempGuy, worldPos, Quaternion.identity, textComp.transform);
            //PrintWorldPos(avgPos);
        } else {
            Debug.LogError("Out of text bound");
        }
    }

    public void TestText() {
        charIndex = textComp.text.IndexOf("~~");
        charIndex++;
        Debug.Log(charIndex);
        PrintPos();
        //charIndex++;
    }

    void PrintWorldPos(Vector3 testPoint) {
        Vector3 worldPos = textComp.transform.TransformPoint(testPoint);
        print(worldPos);
        new GameObject("point").transform.position = worldPos / canvas.scaleFactor;
        Debug.DrawRay(worldPos / canvas.scaleFactor, Vector3.up, Color.red, 50f);
    }

    public void OutText(string content) {
        textManager.AddContent(content);
    }
}
