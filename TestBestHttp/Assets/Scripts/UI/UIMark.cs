﻿using UnityEngine.UI;
using UnityEngine;

/// <inheritdoc />
/// <summary>
/// UI的标记
/// </summary>
public class UIMark : MonoBehaviour
{
    //public UIMarkType MarkType = UIMarkType.DefaultUnityElement;

    public Transform Transform
    {
        get { return transform; }
    }

    public string CustomComponentName = "";

    public virtual string ComponentName
    {
        get
        {
            if (!string.IsNullOrEmpty(CustomComponentName))
                return CustomComponentName;
            if (null != GetComponent("SkeletonAnimation"))
                return "SkeletonAnimation";
            if (null != GetComponent<ScrollRect>())
                return "ScrollRect";
            if (null != GetComponent<InputField>())
                return "InputField";
            if (null != GetComponent<Text>())
                return "Text";
            if (null != GetComponent("TMP.TextMeshProUGUI"))
                return "TextMeshProUGUI";
            if (null != GetComponent<Button>())
                return "Button";
            if (null != GetComponent<RawImage>())
                return "RawImage";
            if (null != GetComponent<Toggle>())
                return "Toggle";
            if (null != GetComponent<Slider>())
                return "Slider";
            if (null != GetComponent<Scrollbar>())
                return "Scrollbar";
            if (null != GetComponent<Image>())
                return "Image";
            if (null != GetComponent<ToggleGroup>())
                return "ToggleGroup";
            if (null != GetComponent<Animator>())
                return "Animator";
            if (null != GetComponent<Canvas>())
                return "Canvas";
            if (null != GetComponent("Empty4Raycast"))
                return "Empty4Raycast";
            if (null != GetComponent<RectTransform>())
                return "RectTransform";

            return "Transform";


        }
    }
}