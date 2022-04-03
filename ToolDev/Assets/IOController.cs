using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using UnityEngine.Assertions;
using UnityEngine.UI;

[System.Serializable]
public class IOController : MonoBehaviour
{
    public SaveData myData;
    public InputField fileNameInput;

    public Slider boidAmountSlider;
    public Slider speedSlider;
    public Slider separationSlider;
    public Slider cohesionSlider;
    public Slider pushSlider;

    ISerializationStrategy<SaveData> formatter;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeChecker());
        formatter = new BinaryStrategy<SaveData>();
    }

    IEnumerator ChangeChecker() {
        while( Application.isPlaying ) {
            ApplyChanges();
            yield return new WaitForSeconds(1f);
		}

        Debug.Log("EXIT");
	}

	private void CreateButton_clicked() {
        Debug.Log("New Data Created!");

        // TODO: Als het niet null is... user waarschuwen?
        myData = new SaveData();
    }

    public void LoadButton_clicked() {
        Debug.Log("Attempting Load");

        string url = Path.Combine(Application.persistentDataPath, fileNameInput.text);

        FileStream fstream = null;
        try {
            fstream = new FileStream(url, FileMode.Open);

            // do iets
            myData = (SaveData)formatter.Deserialize(fstream);

            fstream.Close();

            //UpdateEditorDisplay();

            Debug.Log("LOADED: " + url);
        }
        catch (System.Exception e) {
            Debug.LogError("Serialization Error: " + e.Message);
            // Zinnig output
        }
    }

    public void SaveButton_clicked() {
        Debug.Log("Attempting Save");

        // not valid in this context
        
        string url = Path.Combine(Application.persistentDataPath, fileNameInput.text);

        FileStream fstream = null;
        try {
            fstream = new FileStream(url, FileMode.Create);

            // do iets
            //formatter.Serialize(fstream, myData);
            string data = JsonUtility.ToJson(myData);
            System.IO.File.WriteAllText(url + ".json", data);
            fstream.Flush();
            fstream.Close();

            Debug.Log("SAVED: "+url);
        }
        catch ( System.Exception e ) {
            Debug.LogError("Serialization Error: " + e.Message);
            // Zinnig output
		}
    }


    private void ApplyChanges()
    {
        if (myData == null) myData = new SaveData();

        myData.name = fileNameInput.text;
        //myData.someInt = SanitizeInt(intField);
        myData.boidAmount = boidAmountSlider.value;
        myData.speed = speedSlider.value;
        myData.separation = separationSlider.value;
        myData.cohesion = cohesionSlider.value;
        myData.push = pushSlider.value;
        
    }

    public interface ISerializationStrategy<T>
    {
        T Deserialize(Stream stream);

        void Serialize(Stream stream, T data);
    }

    public class BinaryStrategy<T> : ISerializationStrategy<T>
    {
        private BinaryFormatter formatter;

        public BinaryStrategy()
        {
            formatter = new BinaryFormatter();
        }

        public T Deserialize(Stream stream)
        {
            T data = default(T);
            data = (T)formatter.Deserialize(stream);
            return data;
        }

        public void Serialize(Stream stream, T data)
        {
            formatter.Serialize(stream, data);
        }
    }
}