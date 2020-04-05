//@QnSBaseCode
//MdStart

namespace QuickNSmart.Contracts.Modules.Language
{
    public partial interface ITranslation : IIdentifiable, ICopyable<ITranslation>
    {
        LanguageCode KeyLanguage { get; set; }
        string Key { get; set; }
        LanguageCode ValueLanguage { get; set; }
        string Value { get; set; }
    }
}
//MdEnd