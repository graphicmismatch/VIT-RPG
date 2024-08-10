using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StringCallback : UnityEvent<string>
{
}
[RequireComponent(typeof(AudioSource))]
public class DialogHelper : MonoBehaviour
{

    public bool end;
    public Dialogue di;
    public int ind = 0;
    public static DialogHelper inst;
    public CharacterManager Left;
    public CharacterManager Right;
    public AudioSource asr;
    //triggers when a dialogue tree starts
    public StringCallback dialogueTreeStarted;
    //triggers when a new dialogue object is utilized
    public StringCallback dialogueObjectChanged;
    //triggers when a dialogue tree ends
    public StringCallback dialogueEnded;
    //triggers when a line is started
    public StringCallback lineStarted;
    //triggers when a line is finished printing
    public StringCallback linePrinted;
    //triggers when the next line/dialogue is called
    public StringCallback nextPressed;
    public StringCallback choosePress;
    private void Start()
    {
        inst = this;
        UIManager.toggleSpeakingPanel(false);
        if (di != null) {
            startd();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public static void startd() {
        
        inst.asr.Stop();
        inst.Left.changeCharacter(inst.di.Left.cha);
        inst.Left.changeDir(inst.di.Left.facingLeft);
        inst.Left.speakingMode(inst.di.isLeftSpeaking);
        inst.Right.changeCharacter(inst.di.Right.cha);
        inst.Right.changeDir(inst.di.Right.facingLeft);
        inst.Right.speakingMode(inst.di.isLeftSpeaking);
        if (inst.di.isLeftSpeaking)
        {
            UIManager.updateNameDisplay(inst.di.Left.charName, true);
        }
        else {
            UIManager.updateNameDisplay(inst.di.Right.charName, false);
        }
        inst.lineStarted.Invoke(inst.di.name);
        UIManager.toggleSpeakingPanel(true);
        ResponsePanelHelper.clearResponses();
        if (inst.di) {
            inst.ind = 0;
            if (inst.ind <= inst.di.clips.Length - 1) {
                if (inst.di.clips !=null && inst.di.clips[inst.ind] != null)
                {
                    inst.asr.PlayOneShot(inst.di.clips[inst.ind]);
                }
            }
            UIManager.updateTextDisplay(inst.di.lines[0],inst.di, inst.di.timeBetweenCharacters);
            
        }
    }

    public static void startd(Dialogue du)
    {
        inst.di = du;
        inst.dialogueObjectChanged.Invoke(inst.di.name);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        startd();
    }

    public static void next() {
        inst.ind++;
        inst.asr.Stop();
        ResponsePanelHelper.clearResponses();

        if (inst.ind <= inst.di.lines.Length - 1)
        {
            if (inst.ind <= inst.di.clips.Length - 1)
            {
                if (inst.di.clips != null && inst.di.clips[inst.ind] != null)
                {
                    inst.asr.PlayOneShot(inst.di.clips[inst.ind]);
                }
            }

            UIManager.updateTextDisplay(inst.ind,inst.di.lines[inst.ind], inst.di, inst.di.timeBetweenCharacters);

        }
        else {
            
            UIManager.toggleSpeakingPanel(false);
            inst.dialogueEnded.Invoke(inst.di.lines[0]);
            inst.di = null;
        }
    }
    public static void next(Dialogue d)
    {
        startd(d);
    }
}