using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiaryEntryController : MonoBehaviour
{
    TextMeshProUGUI entryText;
    JournalController journal;

    public void CreateEntry(TextMeshProUGUI inputText, JournalController j) {
        journal = j;
        entryText = new TextMeshProUGUI();
        entryText.text = inputText.text;
    }

    public void DisplayEntry() {
        journal.GenerateJournalItem(entryText);
    }
}
