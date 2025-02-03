using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Drawing.Helpers;
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
        private int _shaderProgram;

        protected override void OnOpenGlInit(GlInterface gl)
        {
            base.OnOpenGlInit(gl);

            // Загружаем OpenGL-контекст
            GL.LoadBindings(new AvaloniaGlBindingsContext(gl));

            Debug.WriteLine($"OpenGL версия: {GL.GetString(StringName.Version)}");
            Debug.WriteLine($"Графический драйвер: {GL.GetString(StringName.Renderer)}");

            // Очищаем буферы цвета и глубины
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(Color4.CornflowerBlue); // Установим фон для наглядности

            GL.Enable(EnableCap.DepthTest);

            GL.DepthFunc(DepthFunction.Less);

            // Теперь можно инициализировать рендеринг
            _cube = new RenderCube();
            _cube.Init();
        }

        protected override void OnOpenGlRender(GlInterface gl, int framebuffer)
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer); // Привязываем переданный Framebuffer
            GL.Viewport(0, 0, 800, 600);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.CornflowerBlue);

            _rotationAngle += 0.01f;
            Matrix4 model = Matrix4.CreateRotationY(_rotationAngle);
            Matrix4 view = Matrix4.CreateTranslation(0, 0, -3);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), 800f / 600f, 0.1f, 100f);

            _cube.Draw(model, view, projection);
        }




        // Метод для проверки ошибок OpenGL
        private void CheckGlError(string operation)
        {
            ErrorCode error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                Debug.WriteLine($"OpenGL Error after {operation}: {error}");
            }
        }
    }
}
