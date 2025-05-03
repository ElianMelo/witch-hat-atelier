using UnityEngine;

public class DrawCanvasController : MonoBehaviour
{
    public GameObject visuals;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            visuals.SetActive(!visuals.activeSelf);
        }        
    }
}
