using TMPro;
using UnityEngine;

public class ErrorController : MonoBehaviour
{
    public TMP_Text descriptionText, specDescriptionText;

    public void Init(string description, string specDescription)
    {
        descriptionText.text = description; 
        specDescriptionText.text = specDescription;
    }
}
