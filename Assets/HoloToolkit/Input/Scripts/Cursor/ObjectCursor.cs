﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// The object cursor can switch between different game objects based on its state.
    /// It simply links the game object to set to active with its associated cursor state.
    /// </summary>
    public class ObjectCursor : Cursor
    {
        [Serializable]
        public struct ObjectCursorDatum
        {
            public string Name;
            public CursorStateEnum CursorState;
            public GameObject CursorObject;
        }

        [SerializeField]
        public ObjectCursorDatum[] CursorStateData;

        /// <summary>
        /// Sprite renderer to change.  If null find one in children
        /// </summary>
        public Transform ParentTransform;

        /// <summary>
        /// On enable look for a sprite renderer on children
        /// </summary>
        protected override void OnEnable()
        {
            if (ParentTransform == null)
            {
                ParentTransform = transform;
            }
            base.OnEnable();
        }

        /// <summary>
        /// Override OnCursorState change to set the correct animation
        /// state for the cursor
        /// </summary>
        /// <param name="state"></param>
        public override void OnCursorStateChange(CursorStateEnum state)
        {
            base.OnCursorStateChange(state);
            if (state != CursorStateEnum.Contextual)
            {
                ObjectCursorDatum newActive = CursorStateData.FirstOrDefault(p => p.CursorState == state);
                if (newActive.Name == null)
                {
                    return;
                }

                ObjectCursorDatum oldActive = CursorStateData.FirstOrDefault(p => p.CursorObject.activeSelf);
                if (oldActive.Name != null)
                {
                    oldActive.CursorObject.SetActive(false);
                }

                newActive.CursorObject.SetActive(true);
            }
        }
    }
}
