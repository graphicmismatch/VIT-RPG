using UnityEngine;

public class DialogueDataReference : MonoBehaviour
{
    public Sprite[] CharacterPortraits;
    public static DialogueDataReference inst;
    public string defaultResponse;
    private void Awake()
    {
        inst = this;
    }
}
