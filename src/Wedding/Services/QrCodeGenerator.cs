
using System.Text;
using Net.Codecrete.QrCodeGenerator;
using Wedding.Extensions;

namespace Wedding.Services
{
    public static class QrCodeGenerator
    {
        public static string GenerateQRCode(string fileName, string saveDirectory, Uri url)
        {
            fileName = fileName + ".png";
            var filePath = Path.Combine(saveDirectory, fileName);

            var qr = QrCode.EncodeText(url.ToString(), QrCode.Ecc.Medium);
            qr.SaveAsPng(filePath, scale: 10, border: 4);

            return filePath;
        }
    }
}
