using System.Globalization;

namespace SchoolProject.Data.Commans
{
    public class GeneralLocalizableEntities
    {
        public bool IsDeleted { get; set; } = false;
        public string Localize(string textAr, string textEn)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textAr;
            return textEn;
        }
    }
}
