using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PARKIT_enterprise_final.Models
{
    
    public class Image
    {
        
        public Guid Id { get; set; }
        public byte[] ImageData { get; set; }

    }
}
