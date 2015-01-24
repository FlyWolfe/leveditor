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

        /// <summary>
        ///  Exports to a data file that can be read in to modify the level.
        /// </summary>
        public async static void ExportCanvas()
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync("CanvasData.txt", CreationCollisionOption.ReplaceExisting);

            System.Diagnostics.Debug.WriteLine(file.Path);

            await FileIO.WriteTextAsync(file, Zone.ZoneWidth.ToString() + ";");
            await FileIO.AppendTextAsync(file, Zone.ZoneHeight.ToString() + ";");

            for (int i = 0; i < Zone.ZoneHeight; ++i)
            {
                for (int j = 0; j < Zone.ZoneWidth; ++j)
                {
                    foreach(int layer in Zone.Canvas[i, j])
                    {
                        await FileIO.AppendTextAsync(file, layer.ToString() + ","); // , separates layers at the same coordinate
                    }

                    await FileIO.AppendTextAsync(file, "|"); // Separates coordinates
                }
            }

            await FileIO.AppendTextAsync(file, ";");
        }

        /// <summary>
        ///  Exports to an image file (NOT modifiable) that can be used for backgrounds.
        /// </summary>
        public static void ExportCanvasImg(IMGFileExtension extension)
        {
        }

        /// <summary>
        /// TODO: Import a canvas from a file.
        /// </summary>
        public static void ImportCanvas()
        {
        }
    }
}
