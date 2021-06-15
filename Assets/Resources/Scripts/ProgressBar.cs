using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar : MonoBehaviour
{
    public int minimum;
    public int maximum;
    public int current;
    public Image fill;
    
    // It's pitiful that Unity doesn't support auto-properties
    
    // public Color colorStart
    // {
    //     get => colorStartVector;
    //     set => colorStartVector = value;
    // }
    //
    // public Color colorEnd
    // {
    //     set => colorMagnitudeVector = value - colorStart;
    // }
    //
    // private Vector4 colorStartVector;
    // private Vector4 colorMagnitudeVector;

    public Color colorStart;
    public Color colorEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        var fillAmount = currentOffset / maximumOffset;
        fill.fillAmount = fillAmount;
        var newColor = colorStart + (colorEnd - colorStart) * (fillAmount / 1.00f);
        newColor.a = 1f;
        fill.color = newColor;
    }
}
