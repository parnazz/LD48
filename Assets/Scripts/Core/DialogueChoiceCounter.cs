using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceCounter : MonoBehaviour
{
    public List<int> choices;

    private void Start()
    {
        choices = new List<int> { 0, 0, 0 };
    }

    public void IncreaseBadChoices()
    {
        choices[0]++;
    }

    public void IncreaseNeutralChoices()
    {
        choices[1]++;
    }

    public void IncreaseGoodChoices()
    {
        choices[2]++;
    }
}
