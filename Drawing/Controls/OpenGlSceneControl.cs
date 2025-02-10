using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Drawing.Models;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Diagnostics;


namespace Drawing.Controls
{
    public class OpenGlSceneControl : OpenGlControlBase
    {
        private RenderCube _cube;
        private float _rotationAngle = 0f;
        private float _width, _height;

        protected override void OnOpenGlInit(GlInterface gl)
        {
            base.OnOpenGlInit(gl);
            // Загружаем OpenGL-контекст
            GL.LoadBindings(new AvaloniaGlBindingsContext(gl));

            _width = (int)this.Bounds.Width;
            _height = (int)this.Bounds.Height;

            Debug.WriteLine($"OpenGL версия: {GL.GetString(StringName.Version)}");
            Debug.WriteLine($"Графический драйвер: {GL.GetString(StringName.Renderer)}");

            // Очищаем буферы цвета и глубины
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(Color4.CornflowerBlue); // Установим фон для наглядности

            GL.Enable(EnableCap.DepthTest);

            GL.DepthFunc(DepthFunction.Less);

            _cube = new RenderCube();
            _cube.Init();

            _timer.Tick += (s, e) => RequestNextFrameRendering();
            _timer.Start();
        }

        protected override void OnOpenGlRender(GlInterface gl, int framebuffer)
        {

            // Теперь можно инициализировать рендеринг

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer); // Привязываем переданный Framebuffer
            GL.Viewport(0, 0, (int)_width, (int)_height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);

            _rotationAngle += 0.01f;
            Matrix4 model = Matrix4.CreateRotationY(_rotationAngle);
            Matrix4 view = Matrix4.CreateTranslation(0, 0, -3);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), _width / _height, 0.1f, 100f);
            _cube.Draw( view, projection);
        }

        /// <summary>
        /// Таймер для перерисовки сцены 16 милисек = 60 fps
        /// </summary>
        private readonly DispatcherTimer _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(16)
        };
    }
}
