using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager inst;
    public GameObject speakingPanel;
    public TMP_Text dlog;
    public TMP_Text nam;

    static bool updating;
    static bool skipD;
    // Start is called before the first frame update
    void Awake()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void toggleSpeakingPanel(bool s)
    {
        inst.speakingPanel.SetActive(s);
    }
    public static void updateTextDisplay(string s, Dialogue d, float t)
    {
        DialogHelper.inst.lineStarted.Invoke(d.name);
        inst.StartCoroutine(inst.slowType(s, d, t));
    }
    public IEnumerator slowType(string s, Dialogue di, float tBWc)
    {
        updating = true;
        dlog.text = "";
        foreach (char c in s)
        {
            dlog.text += c.ToString();
            yield return new WaitForSeconds(tBWc);
            if (skipD)
            {
                skipD = false;
                dlog.text = s;
                break;
            }
        }
        updating = false;
        DialogHelper.inst.linePrinted.Invoke(di.name);
        if (di.lines.Length > 1)
        {
            ResponsePanelHelper.addResponse(di.defResponses[0]);
        }
        else if (di.lines.Length == 1 && di.defResponses.Length == 1 && di.opt.Length == 0) {

            ResponsePanelHelper.addResponse(di.defResponses[0]);
        }
        else
        {
            foreach (Options op in di.opt)
            {
                ResponsePanelHelper.addResponse(op.optName, op.next);
            }
        }
    }

    public static void updateTextDisplay(int ind, string s, Dialogue d, float t)
    {
        DialogHelper.inst.lineStarted.Invoke(d.name);
        inst.StartCoroutine(inst.slowType(ind, s, d, t));
    }
    public IEnumerator slowType(int ind, string s, Dialogue di, float tBWc)
    {
        updating = true;
        dlog.text = "";
        foreach (char c in s)
        {
            
            dlog.text += c.ToString();
            yield return new WaitForSeconds(tBWc);
            if (skipD)
            {
                skipD = false;
                dlog.text = s;
                break;
            }
        }
        updating = false;
        DialogHelper.inst.linePrinted.Invoke(di.name);
        if (ind <= di.defResponses.Length - 1)
        {
            ResponsePanelHelper.addResponse(di.defResponses[ind]);
        }
        else
        {
            foreach (Options op in di.opt)
            {
                ResponsePanelHelper.addResponse(op.optName, op.next);
            }
        }
    }
    public void skip() {
        skipD = updating;
    }
    public static void updateNameDisplay(string s, bool isLeftTalking)
    {
        inst.nam.text = s;
        if (isLeftTalking)
        {
            inst.nam.horizontalAlignment = HorizontalAlignmentOptions.Left;
        }
        else
        {
            inst.nam.horizontalAlignment = HorizontalAlignmentOptions.Right;
        }
    }
}
