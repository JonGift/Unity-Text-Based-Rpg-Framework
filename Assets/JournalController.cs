using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JournalController : MonoBehaviour
{
    public GameObject buttonObj;
    public GameObject textObj;
    public GameObject diaryContent;

    public void CreateJournalEntry(TextMeshProUGUI inputText) {
        GameObject temp = Instantiate(buttonObj, diaryContent.transform);
        temp.GetComponent<DiaryEntryController>().CreateEntry(inputText, this);
    }

    public void GenerateJournalItem(TextMeshProUGUI inputText) {
        foreach (Transform child in transform.GetChild(2).GetChild(0).GetChild(0).transform)
            Destroy(child.gameObject);

        GameObject temp = Instantiate(textObj, transform.GetChild(2).GetChild(0).GetChild(0));
        temp.GetComponent<TextMeshProUGUI>().text = inputText.text;
    }
}
