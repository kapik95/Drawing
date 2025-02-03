using System;
using Avalonia.OpenGL;
using OpenTK;
using OpenTK.Graphics;

namespace Drawing.Controls
{
    /// <summary>
    /// Этот код представляет собой связующее звено между Avalonia и OpenGL, 
    /// позволяя Avalonia приложениям использовать функции OpenGL для рендеринга графики. 
    /// Класс AvaloniaGlBindingsContext предоставляет метод для получения адресов функций OpenGL, 
    /// что необходимо для их вызова из кода C#.
    /// </summary>
    public class AvaloniaGlBindingsContext : IBindingsContext
    {
        private readonly GlInterface _gl;

        public AvaloniaGlBindingsContext(GlInterface gl)
        {
            _gl = gl;
        }

        public nint GetProcAddress(string procName)
        {
            return _gl.GetProcAddress(procName);
        }
    }
}
