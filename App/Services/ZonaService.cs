using App.Data;
using App.Model;
using Microsoft.EntityFrameworkCore;

namespace App.Services;

public class ZonaService(CendresContext ctx) {
    public List<Zona> GetPerExplorar(int maxLevel) {
        return ctx.Zonas.Where(zona => zona.NivellRecomanat <= maxLevel && zona.Explorada == false).ToList();
    }

    public void MostraZona(int zoneId) {
        var zone = ctx.Zonas.Include(zona => zona.Subzones).FirstOrDefault(zona => zona.Id == zoneId);
        if (zone == null) {
            Console.WriteLine($"Could not find zone id {zoneId}");
            return;
        }

        Console.WriteLine($"{zone}\nSubzones: {string.Join(", ", zone.Subzones)}");
    }

    public bool AssignarRegio(int zoneId, int regionId) {
        if (zoneId == regionId) {
            Console.WriteLine("Cannot assign the zone to be its own region");
            return false;
        }

        var subzone = ctx.Zonas.Include(zona => zona.Regio).Include(zona => zona.Subzones)
            .FirstOrDefault(zona => zona.Id == zoneId);
        if (subzone == null) {
            Console.WriteLine($"Could not find zone id {zoneId}");
            return false;
        }

        var region = ctx.Zonas.FirstOrDefault(zona => zona.Id == regionId);
        if (region == null) {
            Console.WriteLine($"Could not find zone id {zoneId}");
            return false;
        }

        if (subzone.Regio == region) {
            Console.WriteLine("This region is already assigned to this zone");
            return false;
        }

        if (subzone.Subzones.Contains(region)) {
            Console.WriteLine("This region is already a subzone to this subzone");
            return false;
        }

        subzone.Regio = region;

        ctx.SaveChanges();

        return true;
    }

    public List<Zona> ZonesPendents() {
        return ctx.Zonas
            .Where(zona => zona.Explorada == false || zona.Subzones.Any(subZone => subZone.Explorada == false))
            .ToList();
    }

    public void ResumBiomes(int minSecrets) {
        var result = ctx.Zonas.GroupBy(zona => zona.Bioma)
            .Select(g => new {
                Name = g.Key,
                ZoneCount = g.Count(), // counts the number of zones in this biome
                Secrets = g.Sum(zona => zona.Secrets)
            })
            .Where(g => g.Secrets > minSecrets)
            .OrderByDescending(g => g.Secrets)
            .ToList();

        Console.WriteLine(string.Join(", ", result));
    }

    public IEnumerable<Zona> GetTotes() {
        return ctx.Zonas.ToList();
    }
}