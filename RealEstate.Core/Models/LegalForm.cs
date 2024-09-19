using RealEstate.Core.Enums;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models;

public class LegalForm
{
    public LegalFormType FormType { get; set; }
    //  public string Description { get; set; }

    [JsonConstructor]
    public LegalForm(LegalFormType formType)
    {
        FormType = formType;
    }

    public override string ToString()
    {
        return $"Form Type: {FormType}";
    }
}
