using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public Image mainImage;
    public Sprite negative;
    public Sprite positive;
    public Sprite neutral;

    public TMP_Text mainText;
    private string ending;

    private void Awake()
    {
        ending = DialogCounterDataHolder.Type;

        if (ending == "negative")
        {
            mainImage.sprite = negative;
            mainText.text = @"The cursed forest sees everything and finds filth in the hearts of men to root in them. The youngest son has found the magic flower. But it will have no use for him.  The hero was consumed and became a part of the forest.";
        }
        else if (ending == "positive")
        {
            mainImage.sprite = positive;
            mainText.text = @"The youngest son succeeded, but his father and brothers perished in the woods. The queen’s order was fulfilled. And the hero awaited the reward he deserves.";
        }
        else
        {
            mainImage.sprite = neutral;
            mainText.text = @"The youngest son managed to get the flower, but did not fetch it to the queen. With the magic power of the flower, he decided to heal his father and brothers.";
        }
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
