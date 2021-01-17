﻿using System.Collections.Generic;
using Aarthificial.Reanimation.Nodes;
using UnityEditor;
using UnityEngine;

namespace Aarthificial.Reanimation.Editor
{
    [CustomEditor(typeof(MirroredAnimationNode))]
    public class MirroredAnimationNodeEditor : AnimationNodeEditor
    {
        protected readonly List<Sprite> SpritesLeft = new List<Sprite>();

        protected override void UpdateSpritesCache()
        {
            Sprites.Clear();
            SpritesLeft.Clear();
            for (var i = 0; i < Frames.arraySize; i++)
            {
                var frameProp = Frames.GetArrayElementAtIndex(i);
                var sprite = frameProp.FindPropertyRelative("sprite").objectReferenceValue as Sprite;
                if (sprite != null)
                    Sprites.Add(sprite);
                var spriteLeft = frameProp.FindPropertyRelative("spriteLeft").objectReferenceValue as Sprite;
                if (spriteLeft != null)
                    SpritesLeft.Add(spriteLeft);
            }
        }

        public override void OnPreviewGUI(Rect position, GUIStyle background)
        {
            DrawPlaybackControls();

            if (Sprites.Count == 0) return;
            int index = ShouldPlay
                ? (int) (EditorApplication.timeSinceStartup * FPS % Sprites.Count)
                : CurrentFrame;

            position.width /= 2;
            Helpers.DrawTexturePreview(position, Sprites[index]);
            position.x += position.width;
            if (index < SpritesLeft.Count)
                Helpers.DrawTexturePreview(position, SpritesLeft[index]);
        }
    }
}