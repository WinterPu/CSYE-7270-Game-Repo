using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RemoteSettingTester : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI txtPro;
    public Text txt;
    void Start()
    {
        if (txtPro)
            txtPro.text = RemoteSettings.GetString("PlayButton");
        else if (txt)
        {
            txt.text = RemoteSettings.GetString("Title");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
