using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreUnserializableLoggerState;

[Table("Children")]
public class MyChildEntity
{
    public MyChildEntity(
        long    id,
        string  name,
        long    parentId)
    {
        Id          = id;
        Name        = name;
        ParentId    = parentId;

        Parent = null!;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; init; }

    public string Name { get; set; }

    [ForeignKey(nameof(Parent))]
    public long ParentId { get; init; }

    public MyParentEntity Parent { get; init; }
}
