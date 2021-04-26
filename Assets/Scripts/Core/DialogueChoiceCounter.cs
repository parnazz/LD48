using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoiceCounter : MonoBehaviour
{
    public static DialogChoises choices;

    private void Start()
    {
        choices = new DialogChoises();
    }

    public void IncreaseNegativeChoices()
    {
        choices.negative++;
    }

    public void IncreaseNeutralChoices()
    {
        choices.neutral++;
    }

    public void IncreasePositiveChoices()
    {
        choices.positive++;
    }
}
