using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;

namespace Emojisskey
{
    public static class Converter
    {
        static readonly SKPngEncoderOptions _option = new SKPngEncoderOptions(SKPngEncoderFilterFlags.AllFilters, 9);

        public static IEnumerable<Stream> Tile(this SKBitmap source, int column, int row, int x = 0, int y = 0, int? width = default, int? height = default)
        {
            var rect = source.Info.Rect;
            var r = source.Width;
            var b = source.Height;
            var w = width ?? r / column;
            var h = height ?? b / row;

            var l = x;
            while (l < r)
            {
                var t = y;
                while (t < b)
                {
                    var result = new SKBitmap(w, h);
                    using (var canvas = new SKCanvas(source))
                        canvas.DrawBitmap(source, rect, new SKRectI(l, t, (l += w) > r ? r : l, (t += h) > b ? b : t));
                    yield return result.PeekPixels()?.Encode(_option).AsStream(true);
                }
            }
        }
    }
}
