using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    float contentTimer = 0f; // When this is 0, we can put another thing into our text box.
    public float delayDuration = .5f; // How long to wait between messages
    Queue<string> contentQueue;
    public GameObject textObj;
    Scrollbar scrollbar;

    // Start is called before the first frame update
    void Start()
    {
        contentQueue = new Queue<string>();
        scrollbar = transform.GetChild(1).GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        contentTimer -= Time.deltaTime;
        if (contentQueue.Count > 0 && contentTimer <= 0f)
            InsertContent();
            
    }

    string uppercaseFirst(string s) {
        if (string.IsNullOrEmpty(s)) {
            return string.Empty;
        }
        char[] a = s.ToCharArray();
        a[0] = char.ToUpper(a[0]);
        return new string(a);
    }

    // Note: also allow other types of content, such as button inputs, which we'll handle later.
    public void AddContent(string input) {
        string final = uppercaseFirst(input);
        contentQueue.Enqueue(final);
    }

    void InsertContent() {
        StartCoroutine(_InsertContent());
    }

    private IEnumerator _InsertContent() {
        string input = contentQueue.Dequeue();
        contentTimer = delayDuration;
        float scrollbarValue = scrollbar.value; // Used to jump or not.

        float yPos = 0;
        foreach (RectTransform rect in transform.GetChild(0).GetChild(0).GetComponentInChildren<RectTransform>()) {
            yPos -= rect.rect.height;
        }

        GameObject temp = Instantiate(textObj, transform.GetChild(0).GetChild(0));
        temp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPos);
        temp.GetComponent<Text>().text = input + '\n';
        //textObj.transform.GetChild(0).GetChild(0).GetComponent<Text>().text += temp.GetComponent<Text>().text.repl + '\n';

        yield return new WaitForSeconds(.05f);

        LayoutRebuilder.ForceRebuildLayoutImmediate(transform.GetChild(0).GetChild(0).GetComponent<RectTransform>());

        yield return new WaitForSeconds(.02f);

        if(scrollbarValue < .005f)
            scrollbar.value = 0;
    }
}
