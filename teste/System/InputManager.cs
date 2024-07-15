﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teste.Entities;

namespace teste.System
{
    public class InputManager
    {
        private static MouseState _lastMouseState;

        public static bool MouseLeftClicked { get; private set; }
        public static bool MouseRightClicked { get; private set; }

        public static void Update()
        {
            MouseLeftClicked = (Mouse.GetState().LeftButton == ButtonState.Pressed) && (_lastMouseState.LeftButton == ButtonState.Released);
            MouseRightClicked = (Mouse.GetState().RightButton == ButtonState.Pressed) && (_lastMouseState.RightButton == ButtonState.Released);
            _lastMouseState = Mouse.GetState();
        }

    }
}
