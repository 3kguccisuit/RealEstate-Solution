using RealEstate.Core.Enums;
using System.Text.Json.Serialization;

namespace RealEstate.Core.Models;

public class LegalForm
{
    public LegalFormType FormType { get; set; }

    [JsonConstructor]
    public LegalForm(LegalFormType formType)
    {
        FormType = formType;
    }

    // Copy constructor for deep cloning
    public LegalForm(LegalForm other)
    {
        FormType = other.FormType;
    }

    public override string ToString()
    {
        return $"Form Type: {FormType}";
    }
}
