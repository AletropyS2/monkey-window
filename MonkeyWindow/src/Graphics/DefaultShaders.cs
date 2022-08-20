
namespace MonkeyWindow.Graphics;
public static class DefaultShaders
{

    static string SimpleVertexShader = @"

        #version 330 core

        layout(location = 0) in vec4 position;
        layout(location = 1) in vec3 color;

        out vec3 ourColor;

        void main()
        {
            gl_Position = position;
            ourColor = color;
        }
    ";

    static string SimpleFragmentShader = @"
    
        #version 330 core

        in vec3 ourColor;

        void main()
        {
            gl_FragColor = vec4(ourColor, 1.0f);
        }
    ";

    public static Shaders SimpleShader = new Shaders(SimpleVertexShader, SimpleFragmentShader);

}