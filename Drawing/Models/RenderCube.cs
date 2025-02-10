using Drawing.Helpers;
using OpenTK.GLControl;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Diagnostics;

namespace Drawing.Models
{
    public class RenderCube
    {
        private int _vao, _vbo, _ebo;
        private Shader _shader;

        private static float _scale = 1.0f;
        private static float _rotationX = 0.0f, _rotationY = 0.0f;


        private readonly float[] _vertices =
        {
            //  Координаты XYZ        Цвета RGB
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  0.0f, 1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 1.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  0.0f, 1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f, 1.0f
        };

        private readonly uint[] _indices =
        {
            0, 1, 2, 2, 3, 0,
            4, 5, 6, 6, 7, 4,
            0, 1, 5, 5, 4, 0,
            3, 2, 6, 6, 7, 3,
            1, 2, 6, 6, 5, 1,
            0, 3, 7, 7, 4, 0
        };

        public void Init()
        {
            _shader = new Shader("Shaders/vertexShader.glsl", "Shaders/fragmentShader.glsl");

            _vao = GL.GenVertexArray();
            _vbo = GL.GenBuffer();
            _ebo = GL.GenBuffer();

            GL.BindVertexArray(_vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
        }

        public void Draw(Matrix4 view, Matrix4 projection)
        {
            _shader.Use();

            Matrix4 model = GetModelMatrix();

            GL.UniformMatrix4(GL.GetUniformLocation(_shader.Handle, "model"), false, ref model);
            GL.UniformMatrix4(GL.GetUniformLocation(_shader.Handle, "view"), false, ref view);
            GL.UniformMatrix4(GL.GetUniformLocation(_shader.Handle, "projection"), false, ref projection);

            GL.BindVertexArray(_vao);
            GL.UseProgram(_shader.Handle);

            GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        private Matrix4 GetModelMatrix()
        {
            var model = Matrix4.Identity;
            model *= Matrix4.CreateScale(_scale);
            model *= Matrix4.CreateRotationX(MathHelper.DegreesToRadians(_rotationX));
            model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(_rotationY));
            return model;
        }

        public void Scale(float delta)
        {
            _scale = Math.Clamp(_scale + delta, 0.5f, 2.0f);

        }

        public void Rotate(float deltaX, float deltaY)
        {
            var speedRotation = 0.0025f;
            _rotationX += deltaX * speedRotation;
            _rotationY += deltaY * speedRotation;
        }
    }
}
