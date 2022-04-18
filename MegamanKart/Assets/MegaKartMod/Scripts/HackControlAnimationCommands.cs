using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackControlAnimationCommands : MonoBehaviour
{
    public void OnAniEnd()
    {
        transform.parent.GetComponent<HackControl>().OnAnimationEnd();
    }
}
