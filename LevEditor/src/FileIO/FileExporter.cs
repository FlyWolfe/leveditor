using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace LevEditor
{
    public static class FileExporter
    {
        public enum IMGFileExtension
        {
            PNG,
            JPG,
        }

        // Exports to a data file that can be read in to modify the level.
        public static void ExportCanvas()
        {
        }

        // Exports to an image file (NOT modifiable) that can be used for backgrounds.
        public static void ExportCanvasImg(IMGFileExtension extension)
        {
        }

        public static void ImportCanvas()
        {
        }
    }
}
