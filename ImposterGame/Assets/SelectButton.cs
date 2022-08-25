using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI
;
public class SelectButton : MonoBehaviour
{
    public Button primaryButton;

    private void OnEnable()
    {
        primaryButton.Select();
    }
    // Start is called before the first frame update
    void Start()
    {
        primaryButton.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
