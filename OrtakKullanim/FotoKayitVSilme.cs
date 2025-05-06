
namespace E_Ticaret.FotoKayit
{
    public class FotoKayitVSilme
    {
       public static async Task<string> FileLoaderAsync(IFormFile fromFile,string Fotoyolu = "/Fotolar/")
        {
            string DosyaAdi = string.Empty;
            if (fromFile != null && fromFile.Length > 0)
            {
                DosyaAdi = fromFile.FileName.ToLower();
                string directory = Directory.GetCurrentDirectory() + "/wwwroot" + Fotoyolu + DosyaAdi;
                using var stream = new FileStream(directory, FileMode.Create);
                await fromFile.CopyToAsync(stream);
            }

            return DosyaAdi;
        }

        public static bool FileDelete(string DosyaAdi, string Fotoyolu = "/Fotolar/")
        {
            string directory = Directory.GetCurrentDirectory() + "/wwwroot" + Fotoyolu + DosyaAdi;
            if (File.Exists(directory))
            {
                File.Delete(directory);
                return true;
            }
            return false;
        }
    }
}
