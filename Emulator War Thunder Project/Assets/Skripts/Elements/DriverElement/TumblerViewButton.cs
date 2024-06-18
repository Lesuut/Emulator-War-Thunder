using UnityEngine;

public class TumblerViewButton : MonoBehaviour
{
    [SerializeField] private GameObject[] obj;
    public void UpdateView()
    {
        foreach (var item in obj)
        {
            item.SetActive(!item.activeSelf);
        }
    }
}