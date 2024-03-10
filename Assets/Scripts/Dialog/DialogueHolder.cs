using UnityEngine;


[CreateAssetMenu(fileName = "New Dialogue Holder", menuName = "ScriptableObjects/Dialogue")]
public class DialogueHolder : ScriptableObject
{
    [SerializeField]
    [TextArea]
    public string[] dialogue;
}
