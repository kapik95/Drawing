﻿#version 300 es
precision mediump float;

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec3 aColor;
out vec3 ourColor;
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform bool isEdge; 

void main()
{
    gl_Position = projection * view * model * vec4(aPos, 1.0f);
    
    if (isEdge) {
        ourColor = vec3(0.0, 0.0, 0.0); 
    } else {
        ourColor = aColor; 
    }
}
