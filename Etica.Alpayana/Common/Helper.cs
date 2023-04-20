namespace Etica.Alpayana.Common
{
    public class Helper
    {
        public string guardarArchivos(IFormFileCollection files, string path, int folderName)
        {
            string fileName = "";
            string filePath = "";
            string Url = "";
            if (files != null)
            {
                string folderPath = System.IO.Path.Combine(path, folderName.ToString());
                if (!System.IO.Directory.Exists(folderPath))
                    System.IO.Directory.CreateDirectory(folderPath);

                foreach (var file in files)
                {
                    fileName = System.IO.Path.GetFileName(file.FileName);
                    filePath = System.IO.Path.Combine(folderPath, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(stream);
                }
                Url = folderPath;
            }
            return Url;
        }
    }
}
