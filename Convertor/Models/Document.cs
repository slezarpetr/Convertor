using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Convertor.Models
{
    public abstract class Document
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public abstract string Type { get; }
        public byte[]  Data { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
