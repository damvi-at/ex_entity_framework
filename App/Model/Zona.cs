using System.ComponentModel.DataAnnotations;

namespace App.Model;

public class Zona {
    public Zona() {
        Subzones = [];
    }

    public int Id { get; set; }

    [Required] [MaxLength(60)] public required string Nom { get; set; }

    public Bioma Bioma { get; set; }

    [Range(1, 150)] public int NivellRecomanat { get; set; }

    public bool Explorada { get; set; }
    public int Secrets { get; set; }

    [Required] public virtual required Foguera Foguera { get; set; } = null!;

    public virtual Zona? Regio { get; set; }
    public virtual HashSet<Zona> Subzones { get; set; }

    public override string ToString() {
        return
            $"{nameof(Id)}: {Id}, {nameof(Nom)}: {Nom}, {nameof(Bioma)}: {Bioma}, {nameof(NivellRecomanat)}: {NivellRecomanat}, {nameof(Explorada)}: {Explorada}, {nameof(Secrets)}: {Secrets}, {nameof(Foguera)}: {Foguera}";
    }
}