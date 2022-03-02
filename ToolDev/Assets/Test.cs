using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using AmazingTool;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using UnityEngine.Assertions;
using System.Xml.Serialization;
using UnityEngine.UIElements;

namespace AmazingTool
{
    [System.Serializable]
    public class TestData
    {
        public string name = "default";

        [OptionalField(VersionAdded = 2)]
        public string achterNaam = "defaultLastName";

        public float someFloat = 10f;
        public int someInt = 10;
        public List<int> someListOfData = new List<int>();

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            Debug.Log("wordt gesaved");
        }

        [OnSerialized]
        void OnSerialized(StreamingContext context)
        {
            Debug.Log("is gesaved");
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext context)
        {
            Debug.Log("wordt geladen");
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            Debug.Log("is geladen");

            // Binary Defaults...
            if (string.IsNullOrEmpty(achterNaam))
            {
                achterNaam = "defaultLastName";
            }
        }
    }
}

public class Test : MonoBehaviour
{
    public TextField fileNameInput;
    public Button createButton, saveButton, loadButton;
    public TestData myData;

    // TODO: Move this to a class of some sort, maybe with a generic / interface for applying/updating?
    public TextField nameField, intField, floatField;
    public VisualElement dataEditor;

    ISerializationStrategy<TestData> formatter;

    BinaryFormatter bFormatter;
    XmlSerializer xmlFormatter;

    // Start is called before the first frame update
    void Start()
    {
        #region UI Init
        var root = GetComponent<UIDocument>().rootVisualElement;

        // file input field
        fileNameInput = root.Q<TextField>("filename");

        // get top level buttons
        createButton = root.Q<Button>("create");
        saveButton = root.Q<Button>("save");
        loadButton = root.Q<Button>("load");

        // get data editor & child name field
        dataEditor = root.Q<IMGUIContainer>("data-editor");
        nameField = dataEditor.Q<TextField>("name");
        intField = dataEditor.Q<TextField>("int");
        floatField = dataEditor.Q<TextField>("float");

        // implement button reactions
        createButton.clicked += CreateButton_clicked;
        saveButton.clicked += SaveButton_clicked;
        loadButton.clicked += LoadButton_clicked;

        StartCoroutine(ChangeChecker());
        #endregion

        //bFormatter = new BinaryFormatter();
        //xmlFormatter = new XmlSerializer(typeof(TestData));

        //formatter = new XmlStrategy<TestData>();
        formatter = new BinaryStrategy<TestData>();
    }

    IEnumerator ChangeChecker()
    {
        while (Application.isPlaying)
        {
            ApplyChanges();
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("EXIT");
    }

    private void CreateButton_clicked()
    {
        Debug.Log("New Data Created!");

        // TODO: Als het niet null is... user waarschuwen?
        myData = new TestData();
    }

    public void LoadButton_clicked()
    {
        Debug.Log("Attempting Load");

        string url = Path.Combine(Application.persistentDataPath, fileNameInput.text);

        FileStream fstream = null;
        try
        {
            fstream = new FileStream(url, FileMode.Open);

            // do iets
            myData = (TestData)formatter.Deserialize(fstream);

            fstream.Close();

            UpdateEditorDisplay();

            Debug.Log("LOADED: " + url);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Serialization Error: " + e.Message);
            // Zinnig output
        }
    }

    public void SaveButton_clicked()
    {
        Debug.Log("Attempting Save");

        // not valid in this context
        // Assert.IsFalse(string.IsNullOrEmpty(fileNameInput.text));

        //Debug.Log(Application.persistentDataPath);

        string url = Path.Combine(Application.persistentDataPath, fileNameInput.text);

        FileStream fstream = null;
        try
        {
            fstream = new FileStream(url, FileMode.Create);

            // do iets
            formatter.Serialize(fstream, myData);

            fstream.Flush();
            fstream.Close();

            Debug.Log("SAVED: " + url);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Serialization Error: " + e.Message);
            // Zinnig output
        }
    }

    #region UI stuff

    private void ApplyChanges()
    {
        if (myData == null) myData = new TestData();

        myData.name = nameField.text;
        myData.someInt = SanitizeInt(intField);
        myData.someFloat = SanitizeFloat(floatField);
    }

    private int SanitizeInt(TextField field)
    {
        string sanitized;
        int retVal = 0;
        try
        {
            sanitized = Regex.Replace(field.text, "[^0-9]", "");
            retVal = int.Parse(sanitized);
            field.SetValueWithoutNotify(sanitized);
        }
        catch (System.FormatException e)
        {
            Debug.LogWarning("Format exception: " + e.Message);
            sanitized = "0";
        }
        return retVal;
    }

    private float SanitizeFloat(TextField field)
    {
        string sanitized;
        float retVal = 0;
        try
        {
            sanitized = Regex.Replace(field.text, "[^0-9.]", "");
            retVal = float.Parse(sanitized);
            field.SetValueWithoutNotify(sanitized);
        }
        catch (System.FormatException e)
        {
            Debug.LogWarning("Format exception: " + e.Message);
            sanitized = "0";
            field.SetValueWithoutNotify(sanitized);
        }
        return retVal;
    }

    private void UpdateEditorDisplay()
    {
        nameField.SetValueWithoutNotify(myData.name);
        intField.SetValueWithoutNotify(myData.someInt.ToString());
        floatField.SetValueWithoutNotify(myData.someFloat.ToString());
    }
    #endregion
}