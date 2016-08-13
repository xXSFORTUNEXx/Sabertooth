using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using SFML.System;
using System;
using System.Runtime.InteropServices;

namespace Editor.Classes
{
    class RenderText : IDisposable
    {
        Font defaultFont = new Font("Resources/Fonts/Arial.ttf");   //Load the true type
        Text defaultText = new Text();  //create texture class
        bool disposed = false;  //variable for dipose
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);  //safehandle for dispose

        public void Dispose()   //for disposing of our true type
        {
            Dispose(true);  //dispose of the font
            GC.SuppressFinalize(this);  //garbage collector
        }

        public virtual void Dispose(bool disposing) //disposing
        {
            if (disposed)   //if its already disposed then theres nothing to dispose of now
                return; //exit method

            if (disposing)  //if its disposing now lets do it
            {
                handle.Dispose();   //dispose of thy handle
            }

            defaultFont.Dispose();  //dispose of the font
            defaultFont = null; //set it null
            defaultText.Dispose();  //dispose of the text class
            defaultText = null; //set it to null

            disposed = true;    //its disposed so let the client know
        }

        public void DrawText(RenderWindow dWindow, string dText, Vector2f position, uint dSize, Color cColor)   //method for drawing text on the screen
        {
            defaultText.Font = defaultFont; //set the font
            defaultText.CharacterSize = dSize;  //set it size
            defaultText.Position = position;    //set its location on the screen
            defaultText.DisplayedString = dText;    //what is actually being displayed (text)
            defaultText.Color = cColor; //the color of the text we are drawing

            dWindow.Draw(defaultText);  //window drawing function
        }
    }
}
