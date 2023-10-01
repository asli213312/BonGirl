using System;
using System.Collections.Generic;
using UnityEngine;

namespace _BonGirl_.Editor.Scripts
{
    public class ConcreteDifferenceFinder : DifferenceFinder
    {
        public override List<Vector2> FindDifferences(Sprite originalImage, Sprite differentImage)
        {
            List<Vector2> differences = new List<Vector2>();
            
            Texture2D originalTexture = originalImage.texture;
            Texture2D differentTexture = differentImage.texture;

            int widthTexture = originalTexture.width;
            int heightTexture = originalTexture.height;

            for (int x = 0; x < widthTexture; x++)
            {
                for (int y = 0; y < heightTexture; y++)
                {
                    Color originalColor = originalTexture.GetPixel(x, y);
                    Color differentColor = differentTexture.GetPixel(x, y);
                    
                    if (FindColorDifference(originalColor, differentColor) > 0.1f)
                        differences.Add(new Vector2(x, y));
                }
            }

            return differences;
        }

        private float FindColorDifference(Color c1, Color c2)
        {
            float rDiff = Math.Abs(c1.r - c2.r);
            float gDiff = Math.Abs(c1.g - c2.g);
            float bDiff = Math.Abs(c1.b - c2.b);
            
            float totalDifference = rDiff + gDiff + bDiff;

            return totalDifference;
        }
    }
}