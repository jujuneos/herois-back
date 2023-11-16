using System.ComponentModel.DataAnnotations;

namespace SuperHerois.Models;

public class HeroisSuperPoderes
{
    [Key]
    public int HeroiId { get; set; }
    public Heroi? Heroi { get; set; }

    [Key]
    public int SuperPoderId { get; set; }
    public SuperPoder? SuperPoder { get; set; }
}