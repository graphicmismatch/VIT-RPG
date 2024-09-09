using UnityEngine;
using System.Collections.Generic;

public enum CompletionStatus { 
    LOCKED,UNLOCKED,STARTED,COMPLETED
}
[System.Serializable]
public struct TaskItem {
    public int[] prerequisites;
    public string Title;
    public string Description;
    public CompletionStatus state;
}
public class TaskSystem : MonoBehaviour
{
    public List<TaskItem> Tasks;
    public Animator objectiveToast;

    // Update is called once per frame
    void Update()
    {
        
    }
}
