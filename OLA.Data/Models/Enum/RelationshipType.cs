using System.ComponentModel;
namespace OLA.Data.Models.Enum;

public enum RelationshipType
{
    [Description("Mother")]
    Mother = 1,
    [Description("Father")]
    Father = 2,
    [Description("Wife")]
    Wife = 3,
    [Description("Husband")]
    Husband = 4,
    [Description("Son")]
    Son = 5,
    [Description("Daughter")]
    Daughter = 6,
    [Description("Brother")]
    Brother = 7,
    [Description("Sister")]
    Sister = 8,
    [Description("Auntie")]
    Auntie = 9,
    [Description("Uncle")]
    Uncle = 10,
    [Description("Cousin")]
    Cousin = 11,
    [Description("Grand Father")]
    GrandFather = 12,
    [Description("Grand Mother")]
    GrandMother = 13,
    [Description("Others")]
    Others = 14
}
