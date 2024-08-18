using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class DialogueUIManager : MonoBehaviour
{
    public GameObject[] characters;
    public TMP_Text charName;
    public TMP_Text dialogueLine;
    public Transform optionParent;
    public GameObject optionObject;
    public GameObject panel;
    public static UnityEvent destroySelf = new UnityEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void CloseDisplay()
    {
        panel.SetActive(false);
    }
    public void UpdateDisplay(DialogueData dd)
    {
        if (!panel.activeInHierarchy)
        {
            panel.SetActive(true);
        }
        int counter = 0;
        destroySelf.Invoke();
        foreach (int g in dd.charIDs)
        {
            if (g < 0 || g >= DialogueDataReference.inst.CharacterPortraits.Length || DialogueDataReference.inst.CharacterPortraits[g] == null)
            {
                Debug.LogWarning("Character Portrait for character id " + g + " is unassigned in DialogueDataReference. Character Portrait will not be displayed.");
                characters[counter].SetActive(false);
                counter++;
                continue;
            }
            characters[counter].SetActive(true);
            characters[counter].GetComponent<Image>().sprite = DialogueDataReference.inst.CharacterPortraits[g];
            counter++;
        }
        charName.text = DialogueTreeInterpreter.currentlyPlaying.chars[dd.charIDs[dd.charCurrentlySpeaking]].Name;
        switch (dd.charCurrentlySpeaking)
        {
            case 0:
                charName.horizontalAlignment = HorizontalAlignmentOptions.Left;
                break;
            case 1:
                charName.horizontalAlignment = HorizontalAlignmentOptions.Center;
                break;
            case 2:
                charName.horizontalAlignment = HorizontalAlignmentOptions.Right;
                break;
            default:
                Debug.LogWarning("This interpreter only supports a maximum of 3 characters(acceptable values are 0,1 and 2). Defaulted to displaying text for the left most character.");
                charName.horizontalAlignment = HorizontalAlignmentOptions.Left;
                break;
        }
        dialogueLine.text = dd.line;
        if (dd.options.Length == 0)
        {
            GameObject g = Instantiate(optionObject, optionParent);
            OptionData d = new OptionData();
            d.id = -1;
            d.title = DialogueDataReference.inst.defaultResponse;
            g.GetComponent<DialogueOptionManager>().init(d);
        }
        else
        {
            foreach (OptionData d in dd.options)
            {
                GameObject g = Instantiate(optionObject, optionParent);
                g.GetComponent<DialogueOptionManager>().init(d);
            }
        }
    }
}
