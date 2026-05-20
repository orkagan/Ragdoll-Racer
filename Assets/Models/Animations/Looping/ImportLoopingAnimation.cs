using UnityEngine;
using UnityEditor;

public class ImportLoopingAnimation : AssetPostprocessor
{
    void OnPreprocessAnimation()
    {
        if (assetPath.Contains("Animations"))
        {
            ModelImporter modelImporter = assetImporter as ModelImporter;
            var animations = modelImporter.defaultClipAnimations;

            for (int i=0; i <animations.Length; i++)
            {
                animations[i].loopTime = true;
                animations[i].loopPose = true;
            }

            modelImporter.clipAnimations = animations;

            Debug.Log("Imported animation with custom settings.");
        }
    }
}