using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest currentQuest;

    private void Start()
    {
        currentQuest.Initialize();
    }
}
