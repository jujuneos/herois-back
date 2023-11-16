using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHerois.Models;

[Table("Superpoderes")]
public class SuperPoder
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string? Superpoder { get; set; }
    [StringLength(250)]
    public string? Descricao { get; set; }

    public List<HeroisSuperPoderes>? Herois { get; set; }
}