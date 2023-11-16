using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHerois.Models;

[Table("Herois")]
public class Heroi
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(120)]
    public string? Nome { get; set; }
    [Required]
    [StringLength(120)]
    public string? NomeHeroi { get; set; }
    public DateTime DataNascimento { get; set; }
    [Required]
    public float Altura { get; set; }
    [Required]
    public float Peso { get; set; }

    public List<HeroisSuperPoderes>? Superpoderes { get; set; }
}