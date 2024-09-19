using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace mf_apis_web_services_fuel_manager.Models
{
    public class LinkDto
    {
        [Key]
        public int Id { get; set; } // ID

        public string Href { get; set; } // Link

        public string Rel { get; set; } // Relacionamento

        public string Metodo { get; set; } // Metodo HTTP

        public LinkDto(int id, string href, string rel, string metodo) 
        {
            Id = id;
            Href = href;
            Rel = rel;
            Metodo = metodo;
        }

    }

    public class LinksHATEOS
    {
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();
    }
}
