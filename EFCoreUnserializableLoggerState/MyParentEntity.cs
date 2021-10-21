using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreUnserializableLoggerState;
 
[Table("Parents")]
public class MyParentEntity
{
    public MyParentEntity(
        long    id,
        string  name)
    {
        Id      = id;
        Name    = name;

        Children = null!;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long Id { get; init; }

    public string Name { get; set; }

    public IReadOnlyCollection<MyChildEntity> Children { get; init; }
}
