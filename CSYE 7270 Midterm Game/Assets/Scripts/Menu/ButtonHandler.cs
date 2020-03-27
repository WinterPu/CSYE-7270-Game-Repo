using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnMouseOver()
    {
        MenuAudioManager.Instance.PlayButtonHoverSFX(0);
    }

    public void OnMyButtonClicked()
    {
        MenuAudioManager.Instance.PlayButtonClickSFX(0);
    }
}
