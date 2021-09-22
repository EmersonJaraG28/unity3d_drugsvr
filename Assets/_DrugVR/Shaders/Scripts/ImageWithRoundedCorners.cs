using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageWithRoundedCorners : MonoBehaviour {
	private static readonly int Props = Shader.PropertyToID("_WidthHeightRadius");

	public float radius;

	void OnRectTransformDimensionsChange(){
		Refresh();
	}
	
	private void OnValidate(){
		Refresh();
	}

	private void Refresh(){

		if (GetComponent<Image>() != null)
		{
			var rect = ((RectTransform)transform).rect;
			GetComponent<Image>().material = new Material(Shader.Find("UI/RoundedCorners/RoundedCorners"));
			GetComponent<Image>().material.SetVector(Props, new Vector4(rect.width, rect.height, radius, 0));
		}

		if (GetComponent<RawImage>() != null)
		{
			var rect = ((RectTransform)transform).rect;
			GetComponent<RawImage>().material = new Material(Shader.Find("UI/RoundedCorners/RoundedCorners"));
			GetComponent<RawImage>().material.SetVector(Props, new Vector4(rect.width, rect.height, radius, 0));
		}
	}
}
