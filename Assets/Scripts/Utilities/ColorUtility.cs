using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtility {

	public static Color AverageColorFromTexture(Texture2D tex)
	{

		Color[] texColors = tex.GetPixels();

		int total = texColors.Length;

		float r = 0;
		float g = 0;
		float b = 0;

        float w = 0;


		for (int i = 0; i < total; i++)
		{
            if (texColors[i].r > 0.95 && texColors[i].g > 0.95 && texColors[i].b > 0.95)
            {
                w++;
                continue;
            }


			r += texColors[i].r;

			g += texColors[i].g;

			b += texColors[i].b;

		}

		return new Color(r / (total-w), g / (total-w), b / (total-w), 1);

	}
}
