﻿using System;
using D = System.Drawing;
using W = System.Windows;

namespace ScreenVersusWpf
{
    public struct ScreenRect : IEquatable<ScreenRect>
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ScreenRect(int x, int y, int width, int height)
        {
            Left = x;
            Top = y;
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"L={Left}, T={Top}, W={Width}, H={Height}";
        }

        #region Equality

        public static bool operator ==(ScreenRect rect1, ScreenRect rect2)
        {
            return rect1.Left == rect2.Left && rect1.Top == rect2.Top && rect1.Width == rect2.Width && rect1.Height == rect2.Height;
        }

        public static bool operator !=(ScreenRect rect1, ScreenRect rect2)
        {
            return !(rect1 == rect2);
        }

        public bool Equals(ScreenRect other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return obj == null ? false : !(obj is ScreenRect) ? false : (this == (ScreenRect) obj);
        }

        public override int GetHashCode()
        {
            return unchecked(Left + 997 * (Top + 997 * (Width + 997 * Height)));
        }

        #endregion

        #region Conversions

        public static implicit operator W.Int32Rect(ScreenRect rect)
        {
            return new W.Int32Rect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public WpfRect ToWpfRect()
        {
            return new WpfRect(
                Left / ScreenTools.DpiZoom,
                Top / ScreenTools.DpiZoom,
                Width / ScreenTools.DpiZoom,
                Height / ScreenTools.DpiZoom
            );
        }

        internal static ScreenRect FromSystem(D.Rectangle rect)
        {
            var screen = W.Forms.SystemInformation.VirtualScreen;
            return new ScreenRect(rect.X - screen.X, rect.Y - screen.Y, rect.Width, rect.Height);
        }

        internal D.Rectangle ToSystem()
        {
            var screen = W.Forms.SystemInformation.VirtualScreen;
            return new D.Rectangle(Left + screen.X, Top + screen.Y, Width, Height);
        }

        #endregion

        #region Utility

        public ScreenRect Grow(int amount)
        {
            return new ScreenRect(Left - amount, Top - amount, Width + 2 * amount, Height + 2 * amount);
        }

        #endregion
    }
}