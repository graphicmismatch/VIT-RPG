using UnityEngine;

[System.Serializable]
public struct Anim {
    public string Name;
    public Sprite[] frames;
}
public class NPCAnim : MonoBehaviour
{
    public Anim[] animations;
    public Animator an;
    public SpriteRenderer srt;

    public void UpdateAnimState(int AnimId, int frame) {
        srt.sprite = animations[AnimId].frames[frame];
    }
}
