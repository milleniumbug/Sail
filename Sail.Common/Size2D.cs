namespace Sail;

public struct Size2D
{
    public int Width { get; }
    
    public int Height { get; }

    public Size2D(int width, int height)
    {
        Width = width;
        Height = height;
    }
    
    public static Size2D Square(int width) => new Size2D(width, width);
}