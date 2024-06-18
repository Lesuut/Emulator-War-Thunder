using UnityEngine;
using UnityEngine.UI;

public class SelectNameSystem : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;

    private string saveKey = "_name";

    private void Awake()
    {
        string loadName = PlayerPrefs.GetString(saveKey);
        if (loadName == "")
        {
            string newName = $"Client {Random.Range(1000, 9999)}";
            PlayerPrefs.SetString(saveKey, newName);
            nameInputField.text = newName;
        }
        else
        {
            nameInputField.text = loadName;
        }
        nameInputField.onValueChanged.AddListener(ChangeName);
    }
    public void ChangeName(string newName)
    {
        nameInputField.text = newName;
        PlayerPrefs.SetString(saveKey, nameInputField.text);
    }
    public string GetName()
    {
        return nameInputField.text;
    }
}