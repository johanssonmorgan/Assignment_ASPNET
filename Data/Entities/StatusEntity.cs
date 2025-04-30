using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Index(nameof(StatusName), IsUnique = true)]
public class StatusEntity
{
    [Key]
    public int Id { get; set; }
    public string StatusName { get; set; } = null!;
    public ICollection<ProjectEntity> Projects { get; set; } = [];
}