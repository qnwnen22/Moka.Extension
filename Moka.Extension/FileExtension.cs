using System.IO;
using System.Text;

namespace Moka.Extension
{
    public static class FileExtension
    {
        public static void WriteAllText(string path, string content, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            string directoryName = Path.GetDirectoryName(path);

            bool exists = Directory.Exists(directoryName);
            if (!exists)
            {
                Directory.CreateDirectory(directoryName);
            }
            File.WriteAllText(path, content, encoding);
        }
    }
}
