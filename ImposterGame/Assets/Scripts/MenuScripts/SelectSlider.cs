using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSlider : MonoBehaviour
{
    public Slider primarySlider;

    private void OnEnable()
    {
        primarySlider.Select();
    }
    // Start is called before the first frame update
    void Start()
    {
        primarySlider.Select();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
