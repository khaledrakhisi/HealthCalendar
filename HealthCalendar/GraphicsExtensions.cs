using System;
using System.Drawing;
using System.Windows.Forms;

namespace HealthCalendar
{
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Draws the image into the specified destination rectangle with the specified sizing
        /// padding for stretched drawing.
        /// </summary>
        /// <param name="gr">The extended Graphics object.</param>
        /// <param name="image">The image which will be drawn.</param>
        /// <param name="destination">The destination rectangle in which the image will be drawn.</param>
        /// <param name="padding">The padding specifying the image parts which won't be stretched.</param>
        public static void DrawImageWithPadding(this Graphics gr, Image image,
            Rectangle destination, Padding padding)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            DrawImageWithPadding(gr, image, destination, new Rectangle(Point.Empty, image.Size),
                padding);
        }

        /// <summary>
        /// Draws the part of the image defined by the source rectangle into the specified
        /// destination rectangle with the specified sizing padding for stretched drawing.
        /// </summary>
        /// <param name="gr">The extended Graphics object.</param>
        /// <param name="image">The image which will be drawn.</param>
        /// <param name="destination">The destination rectangle in which the image will be drawn.</param>
        /// <param name="source">The source rectangle in the image used for clipping parts of it.</param>
        /// <param name="padding">The padding specifying the image parts which won't be stretched.</param>
        public static void DrawImageWithPadding(this Graphics gr, Image image,
            Rectangle destination, Rectangle source, Padding padding)
        {
            if (gr == null)
            {
                throw new ArgumentNullException("gr");
            }
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            Rectangle[] destinations = GetSizingRectangles(destination, padding);
            Rectangle[] sources = GetSizingRectangles(source, padding);

            for (int i = 0; i < 9; i++)
            {
                gr.DrawImage(image, destinations[i], sources[i], GraphicsUnit.Pixel);
            }
        }

        private static Rectangle[] GetSizingRectangles(Rectangle rectangle, Padding padding)
        {
            int leftV = rectangle.X + padding.Left;
            int rightV = rectangle.X + rectangle.Width - padding.Right;
            int topH = rectangle.Y + padding.Top;
            int bottomH = rectangle.Y + rectangle.Height - padding.Bottom;
            int innerW = rectangle.Width - padding.Horizontal;
            int innerH = rectangle.Height - padding.Vertical;

            // Set parts in descending order to draw upper left tiles over bottom right ones
            Rectangle[] rectangles = new Rectangle[9];
            rectangles[8] = new Rectangle(rectangle.X, rectangle.Y, padding.Left, padding.Top);
            rectangles[7] = new Rectangle(leftV, rectangle.Y, innerW, padding.Top);
            rectangles[6] = new Rectangle(rightV, rectangle.Y, padding.Right, padding.Top);
            rectangles[5] = new Rectangle(rectangle.X, topH, padding.Left, innerH);
            rectangles[4] = new Rectangle(leftV, topH, innerW, innerH);
            rectangles[3] = new Rectangle(rightV, topH, padding.Right, innerH);
            rectangles[2] = new Rectangle(rectangle.X, bottomH, padding.Left, padding.Bottom);
            rectangles[1] = new Rectangle(leftV, bottomH, innerW, padding.Bottom);
            rectangles[0] = new Rectangle(rightV, bottomH, padding.Right, padding.Bottom);
            return rectangles;
        }
    }
}