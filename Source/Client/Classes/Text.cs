using Microsoft.Win32.SafeHandles;
using SFML.Graphics;
using SFML.System;
using System;
using System.Runtime.InteropServices;

namespace SabertoothClient
{
    public class RenderText : IDisposable
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

        public void DrawText(RenderWindow c_Window, string c_Text, Vector2f position, uint c_Size, Color c_Color)   //method for drawing text on the screen
        {
            defaultText.Font = defaultFont; //set the font
            defaultText.CharacterSize = c_Size;  //set it size
            defaultText.Position = position;    //set its location on the screen
            defaultText.DisplayedString = c_Text;    //what is actually being displayed (text)
            defaultText.Color = c_Color; //the color of the text we are drawing
            defaultText.Style = Text.Styles.Bold;

            c_Window.Draw(defaultText);  //window drawing function
        }
    }
}
