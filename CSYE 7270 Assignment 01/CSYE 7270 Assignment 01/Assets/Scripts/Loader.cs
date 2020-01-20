
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Loader : MonoBehaviour
{
    private string path;
    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.streamingAssetsPath + "/setting/setting.json";
        LoadSetting();
    }

    void LoadSetting()
    {
        string jsonstr = File.ReadAllText(path);
        Boundary boundary =  JsonUtility.FromJson<Boundary>(jsonstr);
        spawner.x_min = boundary.x_min;
        spawner.x_max = boundary.x_max;
        spawner.z_min = boundary.z_min;
        spawner.z_max = boundary.z_max;
    }

    [System.Serializable]
    public class Boundary
    {
        public float x_min;
        public float x_max;
        public float z_min;
        public float z_max;
    }
}
