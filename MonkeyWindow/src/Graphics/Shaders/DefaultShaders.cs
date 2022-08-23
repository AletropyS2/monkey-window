
namespace MonkeyWindow.Graphics;
public static class DefaultShaders
{

    static string SimpleVertexShader = @"

        #version 330 core

        layout(location = 0) in vec4 position;

        void main()
        {
            gl_Position = position;
        }
    ";

    static string SimpleFragmentShader = @"
    
        #version 330 core

        layout(location = 0) out vec4 color;

        uniform vec4 u_Color;

        void main()
        {
            color = u_Color;
        }
    ";

    public static Shader SimpleShader = new Shader(SimpleVertexShader, SimpleFragmentShader);

}