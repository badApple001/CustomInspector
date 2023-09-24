using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [System.Serializable]
    public class AudioSetting
    {

        public string name;
        public string file;
    }


    [SerializeField] private List<AudioSetting> settings = new List<AudioSetting>();



    // Start is called before the first frame update
    void Start()
    {

        foreach ( var setting in settings )
        {
            Debug.Log( $"name: {setting.name}\nfile: {setting.file}" );
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
