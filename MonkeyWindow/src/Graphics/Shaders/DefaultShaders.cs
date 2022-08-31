
namespace MonkeyWindow.Graphics;
public static class DefaultShaders
{

    static string SimpleVertexShader = @"

        #version 330 core

        layout(location = 0) in vec4 position;
        layout(location = 1) in vec2 texCoord;

        out vec2 v_TexCoord;

        void main()
        {
            gl_Position = position;
            v_TexCoord = texCoord;
        }
    ";

    static string SimpleFragmentShader = @"
    
        #version 330 core

        layout(location = 0) out vec4 color;

        in vec2 v_TexCoord;

        uniform vec4 u_Color;
        uniform sampler2D u_Texture;

        void main()
        {
            vec4 texColor = texture(u_Texture, v_TexCoord);
            color = texColor;
        }
    ";

    public static Shader SimpleShader = new Shader(SimpleVertexShader, SimpleFragmentShader);

}