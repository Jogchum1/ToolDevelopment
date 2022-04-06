using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PushSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderText;

    public BoidMaster boidMaster;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;

        slider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = v.ToString("0.00");
        });
    }

    // Update is called once per frame
    public void UpdateSlider()
    {
        boidMaster.pushValue = slider.value;
    }
}
