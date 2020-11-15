using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TetrisUI : MonoBehaviour
{
    private TextMeshProUGUI levelCountTM;
    private TextMeshProUGUI lineCountTM;

    // Start is called before the first frame update
    void Start()
    {
        levelCountTM = transform.Find("LevelCount").GetComponent<TextMeshProUGUI>();
        lineCountTM = transform.Find("LineCount").GetComponent<TextMeshProUGUI>();
    }

    public void ChangeLevelCount(int newLevelCount)
    {
        levelCountTM.text = newLevelCount.ToString();
    }

    public void ChangeLineCount(int newLineCount)
    {
        lineCountTM.text = newLineCount.ToString();
    }
}
