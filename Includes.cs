using System.Security.Cryptography;
using System.Text;

public class Includes
{
    public static string filenameLimit(string uniqueFileName)
    {
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uniqueFileName);

        // Trim the file name to 250 characters or less
        if (fileNameWithoutExtension.Length > 250)
        {
            fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, 250);
        }

        // Get the file extension
        string fileExtension = Path.GetExtension(uniqueFileName);

        // Combine the trimmed filename and extension
        uniqueFileName = fileNameWithoutExtension + fileExtension;
       
        return uniqueFileName;
    }
}