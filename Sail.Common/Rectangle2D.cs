namespace Sail;

public struct Rectangle2D
{
    public Point2D TopLeft { get; }
    
    public Point2D BottomRight => new Point2D(TopLeft.X + Size.Width, TopLeft.Y + Size.Height);
    
    public Point2D Center => new Point2D(TopLeft.X + Size.Width / 2, TopLeft.Y + Size.Height / 2);
    
    public Size2D Size { get; }

    public int Width => Size.Width;

    public int Height => Size.Height;

    public Rectangle2D(Point2D topLeft, Size2D size)
    {
        TopLeft = topLeft;
        Size = size;
    }
    
    public Rectangle2D(Point2D topLeft, Point2D bottomRight)
    {
        if (topLeft.X < bottomRight.X)
        {
            throw new ArgumentOutOfRangeException(nameof(bottomRight));
        }
        if (topLeft.Y < bottomRight.Y)
        {
            throw new ArgumentOutOfRangeException(nameof(bottomRight));
        }
        TopLeft = topLeft;
        Size = new Size2D(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
    }
}