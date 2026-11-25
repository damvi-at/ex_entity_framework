using System.ComponentModel.DataAnnotations;

namespace App.Model;

public class Foguera {
    public int Id { get; set; }

    [Required] [MaxLength(60)] public required string Nom { get; set; }

    public int Descansos { get; set; }

    public virtual Zona Zona { get; set; }

    public override string ToString() {
        return $"{nameof(Id)}: {Id}, {nameof(Nom)}: {Nom}, {nameof(Descansos)}: {Descansos}";
    }
}